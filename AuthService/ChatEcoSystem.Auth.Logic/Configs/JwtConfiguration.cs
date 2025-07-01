namespace ChatEcoSystem.Auth.Logic
{
    /// <summary>
    /// Конфигурация JWT токена
    /// </summary>
    public class JwtConfiguration
    {
        /// <summary>
        /// Ключ для подписи токена
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Издатель токена
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Аудитория токена
        /// </summary>
        public string Audience { get; set; }
    }
}
