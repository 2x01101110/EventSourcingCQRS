using EventSourcingCQRS.Domain.Models.Orders;
using EventSourcingCQRS.Infrastructure.EventSourcing;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventSourcingCQRS.Application.Commands.CreateOrderCommand
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
    {
        private readonly IEventSourcingRepository<Order, OrderId> _repository;

        public CreateOrderCommandHandler(IEventSourcingRepository<Order, OrderId> repository)
        {
            this._repository = repository;
        }

        public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            //var orderId = OrderId.NewOrderId(Guid.NewGuid());
            var orderId = OrderId.NewOrderId(Guid.Parse("c048f4bc-cf98-4e03-8345-8e76169cd92e"));

            var order = Order.CreateOrder(orderId);

            await this._repository.SaveAsync(order);

            return Unit.Value;
        }
    }
}
