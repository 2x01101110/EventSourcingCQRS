using EventSourcingCQRS.Domain.Models.Orders;
using EventSourcingCQRS.Infrastructure.EventSourcing;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventSourcingCQRS.Application.Commands.CancelOrderCommand
{
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand>
    {
        private readonly IEventSourcingRepository<Order, OrderId> _repository;

        public CancelOrderCommandHandler(IEventSourcingRepository<Order, OrderId> repository)
        {
            this._repository = repository;
        }

        public async Task<Unit> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await this._repository.GetByIdAsync(request.Id);

            order.CancelOrder();

            await this._repository.SaveAsync(order);

            return Unit.Value;
        }
    }
}
