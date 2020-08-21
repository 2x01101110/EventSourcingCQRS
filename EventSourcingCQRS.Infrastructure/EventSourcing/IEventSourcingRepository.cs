using EventSourcingCQRS.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcingCQRS.Infrastructure.EventSourcing
{
    public interface IEventSourcingRepository<TAggregate, TAggregateId> where TAggregate : IAggregate<TAggregateId>
    {
        Task<TAggregate> GetByIdAsync(TAggregateId id);
        Task SaveAsync(TAggregate aggregate);
    }
}
