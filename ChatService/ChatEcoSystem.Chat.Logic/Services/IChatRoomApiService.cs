using System;
using System.Collections.Generic;
using ChatEcoSystem.SharedLib.Contracts;
using System.Threading.Tasks;

namespace ChatEcoSystem.Chat.Logic
{
    /// <summary>
    /// Сервис-обёртка для работы с чатами, возвращающий стандартные ответы API (BaseApiResponse)
    /// </summary>
    public interface IChatRoomApiService
    {
        /// <summary>
        /// Создание чата
        /// </summary>
        /// <param name="roomDto">Данные чата</param>
        /// <returns>Стандартный API-ответ с созданным чатом</returns>
        Task<BaseApiResponse<ChatRoomDto>> CreateChatRoom(ChatRoomDto roomDto);

        /// <summary>
        /// Получение чата по ИД
        /// </summary>
        /// <param name="roomId">ИД чата</param>
        /// <returns>Стандартный API-ответ с чатом</returns>
        Task<BaseApiResponse<ChatRoomDto>> GetChatRoom(Guid roomId);

        /// <summary>
        /// Получение чатов по пользователю
        /// </summary>
        /// <param name="userId">ИД пользователя</param>
        /// <returns>Стандартный API-ответ со списком чатов</returns>
        Task<BaseApiResponse<List<ChatRoomDto>>> GetUserChatRooms(string userId);

        /// <summary>
        /// Поиск персональных чатов
        /// </summary>
        /// <param name="user1Id">ИД пользователя 1</param>
        /// <param name="user2Id">ИД пользователя 2</param>
        /// <returns>Стандартный API-ответ с найденным чатом</returns>
        Task<BaseApiResponse<ChatRoomDto>> FindPersonalChat(string user1Id, string user2Id);

        /// <summary>
        /// Состоит ли пользователь в чате
        /// </summary>
        /// <param name="roomId">ИД чата</param>
        /// <param name="userId">ИД пользователя</param>
        /// <returns>Стандартный API-ответ с результатом проверки</returns>
        Task<BaseApiResponse<bool>> IsUserInChatRoom(Guid roomId, string userId);
    }
}
