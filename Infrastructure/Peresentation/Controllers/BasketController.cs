using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto_s.BasketDto;
using Abstraction;

namespace Peresentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController(IServicesManger servicesManager) : ControllerBase
    {
        // Get Basket
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(string key)
        {
            var basket = await servicesManager.basketServices.GetBasketAsync(key);
            return Ok(basket);
        }

        // Create or Update Basket
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket)
        {
            var updatedBasket = await servicesManager.basketServices.CreateOrUpdateBasketAsync(basket);
            return Ok(updatedBasket);
        }

        // Add Delete Basket logic here
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string key)
        {
            var result = await servicesManager.basketServices.DeleteBasketAsync(key);
            return Ok(result);
        }

    }

}
