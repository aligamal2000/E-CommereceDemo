using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto_s.OrderDto;

namespace Peresentation.Controllers
{
    public class OrderController (IServicesManger serviceManager):ApiBaseController
    {
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderReturnDto>> CreateOrder( OrderDto orderDto)
        {

            var Email = User.FindFirstValue(ClaimTypes.Email);
            var Order = await serviceManager.orderServices.CreateOrder(orderDto, Email);
            return Ok(Order);

        }
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DelvieryMethodDto>>> GetDeliveryMethods()
        {
            var deliveryMethods = await serviceManager.orderServices.GetDeliveryMethodAsync();
            return Ok(deliveryMethods);
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderReturnDto>>> GetAllUserOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders = await serviceManager.orderServices.GetAllOrderAsync(email);
            return Ok(orders);

        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderReturnDto>> GetOrderById(Guid id)
        {
            var order = await serviceManager.orderServices.GetOrderByIdAsync(id);
            return Ok(order);
        }
    }
}
