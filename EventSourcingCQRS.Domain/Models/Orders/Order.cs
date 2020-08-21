using EventSourcingCQRS.BuildingBlocks.Domain;
using EventSourcingCQRS.Domain.Models.Orders.DomainEvents;

namespace EventSourcingCQRS.Domain.Models.Orders
{
    public class Order : AggregateBase<OrderId>
    {
        public OrderStatus Status { get; private set; }

        private Order(OrderId id)
        {
            this.Id = id;
            this.Status = OrderStatus.OrderCreated;
            this.AddDomainEvent(new OrderCreatedDomainEvent(id));
        }
        
        public static Order CreateOrder(OrderId id)
        {
            return new Order(id);
        }
        public void CancelOrder()
        {
            this.Status = OrderStatus.OrderCanceled;
            this.AddDomainEvent(new OrderCanceledDomainEvent(this.Id));
        }

        public void ReplayDomainEvent(IDomainEvent<OrderId> domainEvent)
        {
            switch (domainEvent)
            {
                case OrderCreatedDomainEvent @event:
                    this.Id = @event.AggregateId;
                    this.Status = @event.Status;
                    break;
                case OrderCanceledDomainEvent @event:
                    //this.Id = @event.AggregateId;
                    this.Status = @event.Status;
                    break;
                default:
                    break;
            }
        }
        private Order() { }
    }
}
