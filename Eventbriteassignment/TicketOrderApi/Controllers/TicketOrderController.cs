using Common.Messaging;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TicketOrderApi.Data;
using TicketOrderApi.Models;

namespace TicketOrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly TicketOrderContext _ordersContext;

        private readonly IConfiguration _config;

        private readonly ILogger<OrdersController> _logger;
        private IPublishEndpoint _bus;

        public OrdersController(TicketOrderContext ordersContext,
            ILogger<OrdersController> logger,
            IConfiguration config
            , IPublishEndpoint bus
            )
        {
            _config = config;
            _logger = logger;
            _ordersContext = ordersContext;
            _bus = bus;
        }

        [HttpGet("{id}", Name = "GetTicketOrder")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetTicketOrder(int id)
        {

            var item = await _ordersContext.TicketOrders
                .SingleOrDefaultAsync(ci => ci.OrderId == id);
            if (item != null)
            {
                return Ok(item);
            }

            return NotFound();

        }

        [Route("new")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateTicketOrder([FromBody] TicketOrder order)
        {
            order.OrderStatus = OrderStatus.Preparing;
            order.OrderDate = DateTime.UtcNow;

            _logger.LogInformation(" In Create Order");
            _logger.LogInformation(" Order" + order);


            _ordersContext.TicketOrders.Add(order);
            //_ordersContext.OrderItems.AddRange(order.OrderItems);
            _logger.LogInformation(" Order added to context");
            _logger.LogInformation(" Saving........");

            try
            {
                await _ordersContext.SaveChangesAsync();
                _bus.Publish(new OrderCompletedEvent(order.BuyerId)).Wait();
                return Ok(new { order.OrderId });
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("An error occored during Order saving .." + ex.Message);
                return BadRequest();
            }
        }
    }
}