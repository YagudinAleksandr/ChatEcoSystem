#nullable disable
namespace ChatEcoSystem.Contracts
{
    public class AuthResponseDto
    {
        /// <summary>
        /// Токен авторизации
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Информация о пользователе <see cref="UserDto"/>
        /// </summary>
        public UserDto User { get; set; }
    }
}
