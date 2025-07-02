using System.Threading.Tasks;
using ChatEcoSystem.Auth.Logic;
using ChatEcoSystem.SharedLib.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatEcoSystem.Auth.Api.Controllers
{
    /// <summary>
    /// Контроллер авторизации пользователя
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        #region CTOR
        /// <inheritdoc cref="IAuthService"/>
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        #endregion

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="request">Запрос авторизации <see cref="UserAuthRequestDto"/></param>
        [HttpPost(nameof(Login))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseApiResponse<UserAuthResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseApiResponse<UserAuthResponseDto>))]
        public async Task<IActionResult> Login([FromBody] UserAuthRequestDto request)
        {
            return ActionRes(await _authService.Authenticate(request));
        }
    }
}
