using System;

namespace ChatEcoSystem.SharedLib.Contracts
{
    /// <summary>
    /// Обновление пользователя
    /// </summary>
    public class UserUpdateDto
    {
        /// <summary>
        /// ИД
        /// </summary>
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
