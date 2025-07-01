namespace ChatEcoSystem.SharedLib.Contracts
{
    /// <summary>
    /// Данные авторизации
    /// </summary>
    public class UserAuthResponseDto
    {
        /// <summary>
        /// Токен авторизации
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public UserDto User { get; set; }
    }
}
