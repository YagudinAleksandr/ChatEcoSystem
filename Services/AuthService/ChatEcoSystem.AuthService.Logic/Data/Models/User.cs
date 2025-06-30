using System;
using ChatEcoSystem.Abstractions;

namespace ChatEcoSystem.AuthService.Logic.Models
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User : IEntity<Guid>
    {
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
