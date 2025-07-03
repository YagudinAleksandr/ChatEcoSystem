namespace ChatEcoSystem.Notification.Logic
{
    /// <summary>
    /// Конфигурация SMTP подключения
    /// </summary>
    public class SmtpConfiguration
    {
        /// <summary>
        /// Отображаемое имя в сообщении
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Отправитель
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Хост
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Порт
        /// </summary>
        public int Port { get; set; }
    }
}
