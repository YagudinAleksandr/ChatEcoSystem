namespace ChatEcoSystem.SharedLib.Contracts
{
    /// <summary>
    /// Запрос на авторизацию пользователя
    /// </summary>
    public class UserAuthRequestDto
    {
        /// <summary>
        /// E-mail
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
    }
}
