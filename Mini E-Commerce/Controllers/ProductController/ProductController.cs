using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mini_E_Commerce.DTOs.Request;
using Mini_E_Commerce.Models;
using Mini_E_Commerce.Repository;
using Mini_E_Commerce.Repository.IRepository;

namespace Mini_E_Commerce.Controllers.ProductController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
       private IRepository<Product> _productRepository;

        public ProductController(IRepository<Product> repositoryProduct)
        {
            _productRepository = repositoryProduct;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]int page = 1,[FromQuery] int pageSize = 10)
        {
            var allProducts = await _productRepository.GetAsync();
            int totalCount = allProducts.Count();
            int totalPages = (int)Math.Ceiling((decimal)totalCount /  pageSize);
            if (page > totalPages && totalPages > 0)
                return NotFound();  
                allProducts = allProducts.Skip((page -1) * pageSize).Take(pageSize).ToList();
            return Ok(allProducts);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ProductRequest productRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = productRequest.Adapt<Product>();

                await _productRepository.CreateAsync(product);
                await _productRepository.CommitAsync();
            
            return Created("", product);

        }

    }
}
