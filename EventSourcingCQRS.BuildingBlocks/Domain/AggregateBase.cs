using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventSourcingCQRS.BuildingBlocks.Domain
{
    public abstract class AggregateBase<TId> : IAggregate<TId>, IEventSourcingAggregate<TId>
    {
        private List<IDomainEvent<TId>> _domainEvents = new List<IDomainEvent<TId>>();

        public TId Id { get; set; }
        public IReadOnlyCollection<IDomainEvent<TId>> DomainEvents => _domainEvents.ToList();


        public void ApplyDomainEvent(IDomainEvent<TId> @event)
        {
            if (!this.DomainEvents.Any(e => Equals(e.EventId, @event.EventId)))
            {
                ((dynamic)this).ReplayDomainEvent((dynamic)@event);
            }
        }
        public void AddDomainEvent(IDomainEvent<TId> domainEvent)
        {
            this._domainEvents.Add(domainEvent);
        }
        public void ClearDomainEvents() => this._domainEvents.Clear();
        public IEnumerable<IDomainEvent<TId>> GetDomainEvents() => this._domainEvents;
    }
}
