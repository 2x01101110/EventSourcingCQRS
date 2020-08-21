using EventSourcingCQRS.BuildingBlocks.Domain;
using EventSourcingCQRS.BuildingBlocks.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcingCQRS.Infrastructure.EventSourcing
{
    public interface IEventStore
    {
        Task SaveEvent<TAggregateId>(IDomainEvent<TAggregateId> @event) 
            where TAggregateId : IAggregateId;
        Task<IEnumerable<Event<TAggregateId>>> ReadEventsAsync<TAggregateId>(TAggregateId id) 
            where TAggregateId : IAggregateId;
    }
}
