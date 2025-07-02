using System.Threading.Tasks;

namespace ChatEcoSystem.SharedLib.Abstractions
{
    /// <summary>
    /// Базовый интерфейс для миграций
    /// </summary>
    public interface IMigrator
    {
        /// <summary>
        /// Применение миграций
        /// </summary>
        Task Apply();
    }
}
