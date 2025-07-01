using System.Threading.Tasks;
using ChatEcoSystem.SharedLib.Contracts;

namespace ChatEcoSystem.Auth.Logic
{
    /// <summary>
    /// Сервис авторизации
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Аутентифицировать пользователя и сгенерировать JWT токен
        /// </summary>
        /// <param name="userAuth">Данные для авторизации</param>
        /// <returns>Авторизированный пользователь <see cref="UserAuthResponseDto"/></returns>
        Task<BaseApiResponse<UserAuthResponseDto>> Authenticate(UserAuthRequestDto userAuth);
    }
}
