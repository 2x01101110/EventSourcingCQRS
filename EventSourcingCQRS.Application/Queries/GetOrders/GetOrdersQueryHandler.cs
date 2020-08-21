using EventSourcingCQRS.Infrastructure.ReadModel;
using EventSourcingCQRS.Infrastructure.ReadModel.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventSourcingCQRS.Application.Queries.GetOrders
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<OrderReadModel>>
    {
        private readonly IReadRepository<OrderReadModel> _repository;

        public GetOrdersQueryHandler(IReadRepository<OrderReadModel> repository)
        {
            this._repository = repository;
        }

        public async Task<IEnumerable<OrderReadModel>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            return await this._repository.FindAllAsync();
        }
    }
}
