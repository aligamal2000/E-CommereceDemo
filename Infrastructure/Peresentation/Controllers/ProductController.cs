using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstraction;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Dto_s;

namespace Peresentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IServicesManger servicesManager) : ControllerBase
    {
        // GET: api/Product/all
        [HttpGet("all")]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllProducts([FromQuery] ProductQueryParams productQuery)
        {
            var products = await servicesManager.productServices.GetAllProductAsync(productQuery);
            return Ok(products);
        }

        // GET: api/Product/brands
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            var brands = await servicesManager.productServices.GetAllBrandsAsync();
            return Ok(brands);
        }

        // GET: api/Product/types
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllTypes()
        {
            var types = await servicesManager.productServices.GetAllTypeAsync();
            return Ok(types);
        }

        // GET: api/Product/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
           
            var product = await servicesManager.productServices.GetProductAsync(id);
            return Ok(product);
        }
    }

}
