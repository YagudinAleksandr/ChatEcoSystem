using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatEcoSystem.SharedLib.Contracts;

namespace ChatEcoSystem.Chat.Logic
{
    /// <summary>
    /// Сервис-обёртка для работы с сообщениями, возвращающий стандартные ответы API (BaseApiResponse)
    /// </summary>
    public interface IMessageApiService
    {
        /// <summary>
        /// Сохранение сообщения
        /// </summary>
        /// <param name="messageDto">Сообщение</param>
        /// <returns>Стандартный API-ответ с созданным сообщением</returns>
        Task<BaseApiResponse<MessageDto>> SaveMessage(MessageDto messageDto);

        /// <summary>
        /// Обновление сообщения
        /// </summary>
        /// <param name="messageId">ИД сообщения</param>
        /// <param name="newContent">Новые данные</param>
        /// <returns>Стандартный API-ответ с обновленным сообщением</returns>
        Task<BaseApiResponse<MessageDto>> UpdateMessage(Guid messageId, string newContent);

        /// <summary>
        /// Удаление сообщения
        /// </summary>
        /// <param name="messageId">ИД сообщения</param>
        /// <returns>Стандартный API-ответ с результатом удаления</returns>
        Task<BaseApiResponse<bool>> DeleteMessage(Guid messageId);

        /// <summary>
        /// Получение сообщения по ИД
        /// </summary>
        /// <param name="messageId">ИД сообщения</param>
        /// <returns>Стандартный API-ответ с сообщением</returns>
        Task<BaseApiResponse<MessageDto>> GetMessage(Guid messageId);

        /// <summary>
        /// Получение истории чата
        /// </summary>
        /// <param name="chatRoomId">ИД чата</param>
        /// <param name="page">Страница</param>
        /// <param name="pageSize">Количество элементов на странице</param>
        /// <returns>Стандартный API-ответ со списком сообщений</returns>
        Task<BaseApiResponse<List<MessageDto>>> GetChatHistory(Guid chatRoomId, int page = 1, int pageSize = 50);
    }
} 