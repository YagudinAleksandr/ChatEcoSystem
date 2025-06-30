namespace ChatEcoSystem.AuthService.Abstractions
{
    /// <summary>
    /// Конфигурация настроек подключения к базе данных
    /// </summary>
    public class DatabaseConnectionConfiguration
    {
        /// <summary>
        /// Строка подключения
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
