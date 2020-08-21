using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingCQRS.BuildingBlocks.Domain
{
    public interface IDomainEvent<TAggregateId> : INotification
    {
        Guid EventId { get; }
        TAggregateId AggregateId { get; }
    }
}
