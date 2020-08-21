using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingCQRS.Domain.Models.Orders
{
    public class OrderStatus
    {
        public string Status { get; private set; }
        public DateTime StatusUpdated { get; private set; }

        private OrderStatus(string status)
        {
            this.Status = status;
            this.StatusUpdated = DateTime.UtcNow;
        }

        private OrderStatus() { }

        public static OrderStatus OrderCreated => new OrderStatus("OrderCreated");
        public static OrderStatus OrderCanceled => new OrderStatus("OrderCanceled");
    }
}
