using System;

namespace ChatEcoSystem.SharedLib.Contracts
{
    /// <summary>
    /// Обновление пароля пользователя
    /// </summary>
    public class UserUpdatePasswordDto
    {
        /// <summary>
        /// ИД
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Старый пароль
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// Новый пароль
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// Подтверждение нового пароля
        /// </summary>
        public string ConfirmPassword { get; set; }
    }
}
