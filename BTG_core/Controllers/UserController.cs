using BTG_core.Models.Products;
using BTG_core.Models.Users;
using BTG_core.Services.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace BTG_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetById([FromRoute] string userId)
        {
            var user = await _userService.GetOneById(ObjectId.Parse(userId));
            return Ok(user);
        }
    }
}
