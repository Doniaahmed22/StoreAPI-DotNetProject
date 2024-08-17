using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Api.Helper;
using Store.Reposatry.Specification.Product;
using Store.Service.HandelResponces;
using Store.Service.Helper;
using Store.Service.Services.ProductServices;
using Store.Service.Services.ProductServices.Dtos;

namespace Store.Api.Controllers
{
    [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }
        [HttpGet("GetAllBrands")]
        [Cache(100)]
        public async Task<ActionResult<IReadOnlyList<BrandTypeDetailsDto>>> GetAllBrands()
        {
            return Ok( await productService.GetAllBrandsAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<BrandTypeDetailsDto>>> GetAllTypes()
        {
            return Ok(await productService.GetAllTypesAsync());
        }

        [HttpGet("Products")]
        public async Task<ActionResult<PaginatedResultDto<ProductDetailsDto>>> GetAllProducts([FromQuery]ProductSpecification input)
        {
            return Ok(await productService.GetAllProductsAsync(input));
        }

        [HttpGet("Productsbyid")]
        public async Task<ActionResult<ProductDetailsDto>> GetProduct(int? id)
        {
            if (id == null)
            {
                return BadRequest(new CustomException(400,"Id Is Null"));
            }
            var product = await productService.GetProductByIdAsync(id);
            if (product is null)
            {
                return NotFound(new CustomException(404));
            }
            return Ok(product);
        }
    }
}
