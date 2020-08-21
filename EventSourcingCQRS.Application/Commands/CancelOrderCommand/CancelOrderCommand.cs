using EventSourcingCQRS.Domain.Models.Orders;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingCQRS.Application.Commands.CancelOrderCommand
{
    public class CancelOrderCommand : IRequest
    {
        public OrderId Id { get; }

        public CancelOrderCommand(Guid id)
        {
            this.Id = OrderId.NewOrderId(id);
        }
    }
}
