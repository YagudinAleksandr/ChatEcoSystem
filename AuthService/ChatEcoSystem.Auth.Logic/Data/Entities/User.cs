using System;
using ChatEcoSystem.SharedLib.Abstractions;

namespace ChatEcoSystem.Auth.Logic
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User : IEntity<Guid>
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

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
    }
}
