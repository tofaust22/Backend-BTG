using BTG_core.Models.Dtos;
using BTG_core.Models.Products;
using BTG_core.Models.UserProducts;
using BTG_core.Models.Users;
using BTG_core.Services.Core;
using BTG_core.Services.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace BTG_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProductController : ControllerBase
    {
        private readonly IUserProductService _userProductService;

        public UserProductController(IUserProductService userProductService)
        {
            _userProductService = userProductService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserProduct>>> Get()
        {
            var userProducts = await _userProductService.GetAll();
            return Ok(userProducts);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserProduct>> GetById([FromRoute] string userId)
        {
            var user = await _userProductService.GetByUserId(userId);
            return Ok(user);
        }

        [HttpPut("add-product/{userId}/{productId}")]
        public async Task<ActionResult<UserProduct>> addProduct([FromRoute] string userId, [FromRoute] string productId, [FromBody] StateUserProductDto stateUserProductDto)
        {
            var userProduct = await _userProductService.AddProductById(userId, productId, stateUserProductDto);
            return Ok(userProduct);
        }

        [HttpPut("state/{userId}/{productId}")]
        public async Task<ActionResult<UserProduct>> updateStateByProductId([FromRoute] string userId, [FromRoute] string productId, [FromBody] StateUserProductDto stateUserProductDto)
        {
            var userProduct = await _userProductService.UpdateStateByProductId(userId, productId, stateUserProductDto);
            return Ok(userProduct);
        }
    }
}
