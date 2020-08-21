using EventSourcingCQRS.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingCQRS.BuildingBlocks.Infrastructure
{
    public class Event<TAggregateId>
    {
        public IDomainEvent<TAggregateId> DomainEvent { get; }

        public Event(IDomainEvent<TAggregateId> domainEvent)
        {
            this.DomainEvent = domainEvent;
        }
    }
}
