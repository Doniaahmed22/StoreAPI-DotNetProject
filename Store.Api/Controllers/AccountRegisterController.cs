using Microsoft.AspNetCore.Mvc;
using Store.Service.HandelResponces;
using Store.Service.Services.UserService.Dtos;
using Store.Service.Services.UserService;

namespace Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountRegisterController : Controller
    {
        private readonly IUserService userService;

        public AccountRegisterController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpPost]
        public async Task<ActionResult<UserDto>> Register(RegisterDto input)
        {
            var user = await userService.Register(input);
            if (user == null)
            {
                return BadRequest(new CustomException(400,"email already exist"));
            }
            return Ok(user);

        }
    }
}
