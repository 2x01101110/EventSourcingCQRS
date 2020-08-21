using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventSourcingCQRS.Application.Commands.CancelOrderCommand;
using EventSourcingCQRS.Application.Commands.CreateOrderCommand;
using EventSourcingCQRS.Application.Queries.GetOrders;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcingCQRS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var query = new GetOrdersQuery();
            return Ok(await this._mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder()
        {
            var command = new CreateOrderCommand();
            await this._mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{orderId}")]
        public async Task<IActionResult> CancelOrder([FromRoute]Guid orderId)
        {
            var command = new CancelOrderCommand(orderId);
            await this._mediator.Send(command);

            return NoContent();
        }
    }
}
