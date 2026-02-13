using basic_ecommerce.Dto;
using basic_ecommerce.Interfaces;
using basic_ecommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace basic_ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {
        [HttpGet("{userId}")]
        public async Task<ActionResult<Product>> GetAllProducts(Guid userId)
        {
            var result = await productService.GetAllProduct(userId);

            if (result == null)
            {
                return NotFound("No product yet");
            }
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult<Product>> CreateNewProduct([FromBody] Products req)
        {
            var result = await productService.CreateProduct(req);

            if (result is null) return BadRequest(result);

            return Ok(result);
        }

        [HttpPatch("{productId}")]
        public async Task<ActionResult<Product>> UpdateProduct([FromBody] UpdateProduct req, Guid productId)
        {
            var result = await productService.UpdateProduct(req, productId);

            return Ok(result);
        }

        [HttpDelete("{productId}")]
        public async Task<ActionResult<Product>> DeleteProduct(Guid productId)
        {
            var result = await productService.DeleteProduct(productId);

            if (result is null) return BadRequest(result);

            return result;
        }
    }
}
