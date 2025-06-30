using System;

#nullable disable
namespace ChatEcoSystem.Contracts
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// ИД
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// E-mail
        /// </summary>
        public string Email { get; set; }
    }
}
