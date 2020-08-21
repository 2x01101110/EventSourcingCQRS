using EventSourcingCQRS.BuildingBlocks.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingCQRS.Domain.Models.Orders.DomainEvents
{
    public class OrderCreatedDomainEvent : DomainEventBase<OrderId>
    {
        OrderCreatedDomainEvent() { }
        public OrderCreatedDomainEvent(OrderId id) : base(id) 
        {
            var test = this.AggregateId;
        }
    }
}
