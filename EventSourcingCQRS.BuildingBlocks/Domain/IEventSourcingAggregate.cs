using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingCQRS.BuildingBlocks.Domain
{
    public interface IEventSourcingAggregate<TAggregateId>
    {
        void ApplyDomainEvent(IDomainEvent<TAggregateId> @event);
        void AddDomainEvent(IDomainEvent<TAggregateId> @event);
        void ClearDomainEvents();

        IEnumerable<IDomainEvent<TAggregateId>> GetDomainEvents();
    }
}
