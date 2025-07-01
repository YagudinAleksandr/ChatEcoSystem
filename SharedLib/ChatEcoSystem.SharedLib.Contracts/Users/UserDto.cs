using System;

namespace ChatEcoSystem.SharedLib.Contracts
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class UserDto
    {
        /// <inheritdoc"/>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// E-mail
        /// </summary>
        public string Email { get; set; }
    }
}
