using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository productRepository, ILogger<CatalogController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepository.GetProducts();

            return Ok(products);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null)
            {
                _logger.LogError($"Product {id} not found");
                return NotFound("Product not found");
            }
            return Ok(product);
        }
        [Route("[action]/{category}", Name = "GetProductByCategory")]
        [HttpGet]
        public async Task<ActionResult<Product>> GetProductByCategory(string category)
        {
            var product = await _productRepository.GetProductByCategory(category);
            if (product == null)
            {
                _logger.LogError($"Product {category} not found");
                return NotFound("Product not found");
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _productRepository.CreateProduct(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        public async Task<ActionResult<Product>> UpdateProduct([FromBody] Product product)
        {
            var result = await _productRepository.UpdateProduct(product);

            return Ok(result);
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        public async Task<ActionResult<Product>> DeleteProductById(string id)
        {
            var result = await _productRepository.DeleteProduct(id);
            return Ok(result);
        }
    }
}
