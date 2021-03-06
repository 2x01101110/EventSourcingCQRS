﻿using EventSourcingCQRS.BuildingBlocks.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingCQRS.Domain.Models.Orders.DomainEvents
{
    public class OrderCreatedDomainEvent : DomainEventBase<OrderId>
    {
        public OrderStatus Status { get; private set; }

        OrderCreatedDomainEvent() { }
        
        public OrderCreatedDomainEvent(OrderId id) : base(id) 
        {
            this.Status = OrderStatus.OrderCreated;
        }
    }
}
