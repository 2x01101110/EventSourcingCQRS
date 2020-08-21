using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingCQRS.BuildingBlocks.Domain
{
    public abstract class DomainEventBase<TAggregateId> : IDomainEvent<TAggregateId>, IEquatable<DomainEventBase<TAggregateId>>
    {
        public Guid EventId { get; protected set; }
        public TAggregateId AggregateId { get; protected set; }

        protected DomainEventBase()
        {
            this.EventId = Guid.NewGuid();
        }

        protected DomainEventBase(TAggregateId aggregateId) : this()
        {
            this.AggregateId = aggregateId;
        }

        public override bool Equals(object obj) => base.Equals(obj as DomainEventBase<TAggregateId>);
        public bool Equals(DomainEventBase<TAggregateId> other) => other != null && this.EventId.Equals(other.EventId);
        public override int GetHashCode() => 123456789 + EqualityComparer<Guid>.Default.GetHashCode(this.EventId);
    }
}
