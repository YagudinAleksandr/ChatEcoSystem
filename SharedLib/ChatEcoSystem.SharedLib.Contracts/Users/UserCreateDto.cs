namespace ChatEcoSystem.SharedLib.Contracts
{
    /// <summary>
    /// Создание пользователя
    /// </summary>
    public class UserCreateDto
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

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
