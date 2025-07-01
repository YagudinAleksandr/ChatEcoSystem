using System.Threading.Tasks;
using ChatEcoSystem.Auth.Logic;
using ChatEcoSystem.SharedLib.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ChatEcoSystem.Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost(nameof(Login))]
        public async Task<BaseApiResponse<UserAuthResponseDto>> Login([FromBody] UserAuthRequestDto request)
        {
            return await _authService.Authenticate(request);
        }
    }
}
