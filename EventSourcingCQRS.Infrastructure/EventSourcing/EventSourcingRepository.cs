using EventSourcingCQRS.BuildingBlocks.Domain;
using EventSourcingCQRS.BuildingBlocks.Infrastructure;
using MediatR;
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
        private readonly IMediator _mediator;

        public EventSourcingRepository(IEventStore eventStore, IMediator mediator)
        {
            this._eventStore = eventStore;
            this._mediator = mediator;
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
                await this._mediator.Publish(@event);
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
