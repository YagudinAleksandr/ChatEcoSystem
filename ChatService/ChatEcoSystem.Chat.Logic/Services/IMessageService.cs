using System;
using System.Collections.Generic;
using ChatEcoSystem.SharedLib.Contracts;
using System.Threading.Tasks;

namespace ChatEcoSystem.Chat.Logic
{
    /// <summary>
    /// Сервис работы с сообщениями
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// Сохранение сообщения
        /// </summary>
        /// <param name="messageDto">Сообщение</param>
        /// <returns>Созданное сообщение</returns>
        Task<MessageDto> SaveMessage(MessageDto messageDto);

        /// <summary>
        /// Обновление сообщения
        /// </summary>
        /// <param name="messageId">ИД сообщения</param>
        /// <param name="newContent">Новые данные</param>
        /// <returns>Обновленное сообщение</returns>
        Task<MessageDto> UpdateMessage(Guid messageId, string newContent);

        /// <summary>
        /// Удаление сообщения
        /// </summary>
        /// <param name="messageId">ИД сообщения</param>
        Task DeleteMessage(Guid messageId);

        /// <summary>
        /// Получение сообщения по ИД
        /// </summary>
        /// <param name="messageId">ИД сообщения</param>
        /// <returns>Сообщение</returns>
        Task<MessageDto> GetMessage(Guid messageId);

        /// <summary>
        /// Получение истории чата
        /// </summary>
        /// <param name="chatRoomId">ИД чата</param>
        /// <param name="page">Страница</param>
        /// <param name="pageSize">Количество элементов на странице</param>
        /// <returns>Список сообщений</returns>
        Task<List<MessageDto>> GetChatHistory(Guid chatRoomId, int page = 1, int pageSize = 50);
    }
}
