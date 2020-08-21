using EventSourcingCQRS.BuildingBlocks.Domain;
using EventSourcingCQRS.BuildingBlocks.Infrastructure;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcingCQRS.Infrastructure.EventSourcing
{
    public class EventSourcingRepository<TAggregate, TAggregateId> : IEventSourcingRepository<TAggregate, TAggregateId>
        where TAggregate : AggregateBase<TAggregateId>, IAggregate<TAggregateId>
        where TAggregateId : IAggregateId
    {
        private readonly IEventStore _eventStore;

        public EventSourcingRepository(IEventStore eventStore)
        {
            this._eventStore = eventStore;
        }

        public async Task<TAggregate> GetByIdAsync(TAggregateId id)
        {
            var aggregate = CreateEmptyAggregate();
            IEventSourcingAggregate<TAggregateId> aggregatePersistence = aggregate;

            var events = await _eventStore.ReadEventsAsync(id);

            foreach (var @event in events)
            {
                IDomainEvent<TAggregateId> domainEvent = @event.DomainEvent;
                aggregatePersistence.ApplyDomainEvent(domainEvent);
            }

            return aggregate; 
        }

        public async Task SaveAsync(TAggregate aggregate)
        {
            IEventSourcingAggregate<TAggregateId> aggregatePersistance = aggregate;

            foreach (var @event in aggregatePersistance.GetDomainEvents())
            {
                await this._eventStore.SaveEvent(@event);
            }

            aggregatePersistance.ClearDomainEvents();
        }

        private TAggregate CreateEmptyAggregate()
        { 
            return (TAggregate)typeof(TAggregate)
                .GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null, new Type[0], new ParameterModifier[0])
                .Invoke(new object[0]);
        }
    }
}
