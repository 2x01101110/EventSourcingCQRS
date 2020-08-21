using EventSourcingCQRS.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingCQRS.Domain.Models.Orders.DomainEvents
{
    public class OrderCanceledDomainEvent : DomainEventBase<OrderId>
    {
        public OrderStatus Status { get; private set; }
        public OrderCanceledDomainEvent(OrderId id) : base(id)
        {
            this.Status = OrderStatus.OrderCanceled;
        }
    }
}
