using System.Threading.Tasks;
using ChatEcoSystem.Contracts;

namespace ChatEcoSystem.AuthService.Abstractions
{
    /// <summary>
    /// Сервис авторизации
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="request">Данные авторизации <see cref="AuthRequestDto"/></param>
        /// <returns>
        /// Данные авторизации <see cref="AuthResponseDto"/>
        /// </returns>
        Task<ApiResponse<AuthResponseDto>> Auth(AuthRequestDto request);
    }
}
