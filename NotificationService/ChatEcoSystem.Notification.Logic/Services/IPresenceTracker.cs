using System.Threading.Tasks;

namespace ChatEcoSystem.Notification.Logic
{
    /// <summary>
    /// Интерфейс трекера онлайн-статусов
    /// </summary>
    public interface IPresenceTracker
    {
        // <summary>
        /// Проверить онлайн-статус пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        Task<bool> IsUserOnline(string userId);
    }
}
