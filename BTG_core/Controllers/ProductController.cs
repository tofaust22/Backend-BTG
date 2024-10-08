using BTG_core.Models.Products;
using BTG_core.Models.Users;
using BTG_core.Services.Core;
using BTG_core.Services.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace BTG_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase 
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            var products = await _productService.GetAll();
            return Ok(products);
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<User>> GetById([FromRoute] string productId)
        {
            var user = await _productService.GetOneById(ObjectId.Parse(productId));
            return Ok(user);
        }

    }
}
