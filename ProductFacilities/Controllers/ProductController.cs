using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProductFacilities.Application.Dto;
using ProductFacilities.Application.Interface;

namespace ProductFacilities.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository) 
        {
            _productRepository = productRepository;
        }

        [HttpGet("getallproducts/{search?}")]
        public async Task<IActionResult> GetAllProducts(string? search = null)
        {
            var products = await _productRepository.GetAllProducts(search);
            if (products.Count == 0)
                return NotFound();

            return Ok(products);
        }

        [HttpPost("createProduct")]
        public async Task<IActionResult> CreateProduct(ProductDto product)
        {
            return Ok(await _productRepository.CreateProduct(product));
        }

        [HttpDelete("deleteproduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productRepository.DeleteProduct(id);
            return NoContent();
        }
    }
}
