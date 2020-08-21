using EventSourcingCQRS.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingCQRS.Domain.Models.Orders
{
    /*public class OrderId : IdBase
    {
        private OrderId(Guid id) :base(id)
        {
        }

        public static OrderId NewOrderId(Guid id) => new OrderId(id);
    }*/

    public class OrderId : IAggregateId
    {
        public Guid Id { get; private set; }

        private OrderId(Guid id)
        {
            this.Id = id;
        }

        public OrderId()
        {

        }

        public static OrderId NewOrderId(Guid id) => new OrderId(id);
        public Guid GetId() => this.Id;
    }
}
