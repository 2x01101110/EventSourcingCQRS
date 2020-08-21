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
            var orderId = OrderId.NewOrderId(Guid.Parse("1510ffb9-23b5-4880-b512-7ab63f96c554"));

            //var order = Order.CreateOrder(orderId);

           // await this._repository.SaveAsync(order);

            await this._repository.GetByIdAsync(orderId);

            return Unit.Value;
        }
    }
}
