using EventSourcingCQRS.BuildingBlocks.Domain;
using EventSourcingCQRS.Domain.Models.Orders.DomainEvents;

namespace EventSourcingCQRS.Domain.Models.Orders
{
    public class Order : AggregateBase<OrderId>
    {
        private Order(OrderId id)
        {
            this.AddDomainEvent(new OrderCreatedDomainEvent(id));
        }

        private Order() { }

        public static Order CreateOrder(OrderId id)
        {
            return new Order(id);
        }

        public void ReplayDomainEvent(IDomainEvent<OrderId> domainEvent)
        {
            switch (domainEvent)
            {
                case OrderCreatedDomainEvent @event:
                    this.Id = @event.AggregateId;
                    break;
                default:
                    break;
            }
        }
    }
}
