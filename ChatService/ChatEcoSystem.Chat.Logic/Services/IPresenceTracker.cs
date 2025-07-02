using System.Threading.Tasks;
using ChatEcoSystem.SharedLib.Contracts;

namespace ChatEcoSystem.Chat.Logic
{
    /// <summary>
    /// Сервис трекер онлайн статусов
    /// </summary>
    public interface IPresenceTracker
    {
        /// <summary>
        /// Пользователь подключился
        /// </summary>
        /// <param name="userId">ИД пользователя</param>
        /// <param name="connectionId">ИД соединения</param>
        Task UserConnected(string userId, string connectionId);

        /// <summary>
        /// Пользователь отключился
        /// </summary>
        /// <param name="userId">ИД пользователя</param>
        /// <param name="connectionId">ИД соединения</param>
        Task UserDisconnected(string userId, string connectionId);

        /// <summary>
        /// Пользователь онлайн
        /// </summary>
        /// <param name="userId">ИД пользователя</param>
        /// <returns>true - онлайн, false - не онлайн</returns>
        Task<BaseApiResponse<bool>> IsUserOnline(string userId);
    }
}
