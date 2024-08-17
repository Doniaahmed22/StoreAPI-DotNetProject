using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Data.Entites;
using Store.Service.HandelResponces;
using Store.Service.Services.OrderService;
using Store.Service.Services.OrderService.Dtos;
using System.Security.Claims;

namespace Store.Api.Controllers
{
    [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService) {
            this.orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<OrderResultDto>> CreateOrderAsync(OrderDto input)
        {
            var order = await orderService.CreateOrderasync(input);

            if (order is null)
            {
                return BadRequest(new Response(400, "error while creating your order"));
            }
            return Ok(order);
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderResultDto>>> GetAllOrdersForUserAsync()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders =  await orderService.GetAllOrdersForUserAsync(email);
            return Ok(orders);
        }


        [HttpGet("GetOrderByIdAsync")]
        public async Task<ActionResult<OrderResultDto>> GetOrderByIdAsync(Guid id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order = await orderService.GetOrderByIdasync(id, email);
            return Ok(order);
        }


        [HttpGet("GetAllDeleviryMethodsAsync")]
        public async Task<ActionResult<IReadOnlyList<DeleviryMethod>>> GetAllDeleviryMethodsAsync()
            => Ok(await orderService.GetAllDeleviryMethodAsync());


    }
}
