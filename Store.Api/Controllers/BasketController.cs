using Microsoft.AspNetCore.Mvc;
using Store.Service.Services.BasketService;
using Store.Service.Services.BasketService.Dtos;

namespace Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : Controller
    {
        
        private readonly IBasketService basketService;

        public BasketController(IBasketService basketService)
        {
            this.basketService = basketService;
        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasketDto>> GetBasketById(string id)
        {
            return Ok(await basketService.GetBasketAsync(id));
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasketAsync(CustomerBasketDto basket)
        {
            return Ok(await basketService.UpdateBasketAsync(basket));

        }

        [HttpDelete]

        public async Task<ActionResult> DeleteBasketAsync(string id)
        {
            return Ok(await basketService.DeleteBasketAsync(id));

        }
    }
}
