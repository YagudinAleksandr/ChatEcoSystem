using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatEcoSystem.Notification.Logic
{
    /// <summary>
    /// Репозиторий для работы с непрочитанными сообщениями
    /// </summary>
    public interface IMessageRepository
    {
        // <summary>
        /// Получить непрочитанные сообщения старше указанного времени
        /// </summary>
        /// <param name="minAge">Минимальный возраст сообщений</param>
        Task<List<UnreadMessage>> GetUnreadMessagesOlderThanAsync(TimeSpan minAge);
    }
}
