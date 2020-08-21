using EventSourcingCQRS.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingCQRS.Domain.Models.Orders
{
    public class OrderId : IdBase
    {
        private OrderId() : base() { }
        private OrderId(Guid id) : base(id) { }

        public static OrderId NewOrderId(Guid id) => new OrderId(id);
    }
}
