using EventSourcingCQRS.Infrastructure.ReadModel.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingCQRS.Application.Queries.GetOrders
{
    public class GetOrdersQuery : IRequest<IEnumerable<OrderReadModel>>
    {
    }
}
