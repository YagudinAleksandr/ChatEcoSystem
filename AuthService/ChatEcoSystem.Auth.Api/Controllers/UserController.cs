using System.Threading.Tasks;
using ChatEcoSystem.Auth.Logic;
using ChatEcoSystem.SharedLib.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ChatEcoSystem.Auth.Api.Controllers
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

        [HttpPost(nameof(Create))]
        public async Task<BaseApiResponse<UserDto>> Create([FromBody]UserCreateDto user)
        {
            return await _userService.Create(user);
        }
    }
}
