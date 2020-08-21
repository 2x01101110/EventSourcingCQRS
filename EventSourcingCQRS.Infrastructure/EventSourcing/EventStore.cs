using EventSourcingCQRS.BuildingBlocks.Domain;
using EventSourcingCQRS.BuildingBlocks.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Serialization;

using System.Threading.Tasks;

namespace EventSourcingCQRS.Infrastructure.EventSourcing
{
    public class EventStore : IEventStore
    {
        private readonly EventStoreContext _context;

        public EventStore(EventStoreContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Event<TAggregateId>>> ReadEventsAsync<TAggregateId>(TAggregateId id) where TAggregateId : IAggregateId
        {
            var queryId = id.Id;
            var events = await this._context.Events
                .Where(x => x.AggregateId == queryId)
                .OrderBy(x => x.Timestamp)
                .AsNoTracking()
                .ToListAsync();

            var domainEvents = new List<Event<TAggregateId>>();

            foreach (EventData eventData in events)
            {
                var domainEvent = (IDomainEvent<TAggregateId>)JsonConvert.DeserializeObject(
                    eventData.Data, 
                    Type.GetType(eventData.Type),
                    PrivateSetterContractResolver.Settings);

                domainEvents.Add(new Event<TAggregateId>(domainEvent));
            }

            return domainEvents;
        }

        public async Task SaveEvent<TAggregateId>(IDomainEvent<TAggregateId> @event) where TAggregateId : IAggregateId
        {
            var eventData = new EventData(
                @event.EventId,
                aggregateId: @event.AggregateId.GetId(),
                @event.GetType().AssemblyQualifiedName,
                @event);

            this._context.Add(eventData);

            await this._context.SaveChangesAsync();
        }

        public class PrivateSetterContractResolver : DefaultContractResolver
        {
            public static JsonSerializerSettings Settings = new JsonSerializerSettings()
            {
                ContractResolver = new PrivateSetterContractResolver()
            };

            protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                var prop = base.CreateProperty(member, memberSerialization);

                if (!prop.Writable)
                {
                    var property = member as PropertyInfo;
                    if (property != null)
                    {
                        var hasPrivateSetter = property.GetSetMethod(true) != null;
                        prop.Writable = hasPrivateSetter;
                    }
                }

                return prop;
            }
        }
    }
}
