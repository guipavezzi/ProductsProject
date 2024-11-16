using Microsoft.AspNetCore.Mvc;
using ProductProject.Application.Interfaces;
using ProductProject.Domain.Entities;
using ProductProject.Shared.Requests;

namespace ProductProject.API.Controllers
{
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _productService.GetProductsServiceAsync());
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            return Ok(await _productService.GetProductServiceAsync(id));
        }
        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> PostProduct([FromBody] RequestProductRegister model)
        {
            return Created(string.Empty, await _productService.PostProductsServiceAsync(model));
        }
        [HttpPut]
        public async Task<IActionResult> PutProduct([FromBody] Product model)
        {
            return Ok(await _productService.PutProductsServiceAsync(model));
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return Ok(await _productService.DeleteProductsServiceAsync(id));
        }
        
    }
}