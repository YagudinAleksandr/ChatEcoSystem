#nullable disable
namespace ChatEcoSystem.Contracts
{
    /// <summary>
    /// Модель запроса авторизации
    /// </summary>
    public class AuthRequestDto
    {
        /// <summary>
        /// E-mail пользователя
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set; }
    }
}
