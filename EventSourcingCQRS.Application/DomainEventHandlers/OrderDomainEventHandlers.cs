using EventSourcingCQRS.Domain.Models.Orders.DomainEvents;
using EventSourcingCQRS.Infrastructure.ReadModel;
using EventSourcingCQRS.Infrastructure.ReadModel.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EventSourcingCQRS.Application.DomainEventHandlers
{
    public class OrderDomainEventHandlers :
        INotificationHandler<OrderCreatedDomainEvent>,
        INotificationHandler<OrderCanceledDomainEvent>
    {
        private readonly IRepository<OrderReadModel> _repository;

        public OrderDomainEventHandlers(IRepository<OrderReadModel> repository)
        {
            this._repository = repository;
        }

        public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var orderReadModel = new OrderReadModel
            {
                Id = notification.AggregateId.Id,
                Status = notification.Status.Status,
                StatusUpdated = notification.Status.StatusUpdated
            };

            this._repository.InsertEntity(orderReadModel);

            await this._repository.SaveAsync();
        }

        public async Task Handle(OrderCanceledDomainEvent notification, CancellationToken cancellationToken)
        {
            var order = await this._repository.GetByIdAsync(notification.AggregateId.Id);

            order.Status = notification.Status.Status;
            order.StatusUpdated = notification.Status.StatusUpdated;

            await this._repository.SaveAsync();
        }
    }
}
