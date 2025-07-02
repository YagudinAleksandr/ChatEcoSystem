using System;
using System.Collections.Generic;
using ChatEcoSystem.SharedLib.Contracts;
using System.Threading.Tasks;

namespace ChatEcoSystem.Chat.Logic.Services
{
    /// <summary>
    /// Сервис работы с чатами
    /// </summary>
    public interface IChatRoomService
    {
        /// <summary>
        /// Создание чата
        /// </summary>
        /// <param name="roomDto">Данные чата</param>
        /// <returns>Созданный чат</returns>
        Task<ChatRoomDto> CreateChatRoom(ChatRoomDto roomDto);

        /// <summary>
        /// Получение чата по ИД
        /// </summary>
        /// <param name="roomId">ИД</param>
        /// <returns>Чат</returns>
        Task<ChatRoomDto> GetChatRoom(Guid roomId);

        /// <summary>
        /// Получение чатов по пользователю
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<ChatRoomDto>> GetUserChatRooms(string userId);

        /// <summary>
        /// Поиск персональных чатов
        /// </summary>
        /// <param name="user1Id">ИД пользователя 1</param>
        /// <param name="user2Id">ИД пользователя 2</param>
        /// <returns></returns>
        Task<ChatRoomDto> FindPersonalChat(string user1Id, string user2Id);

        /// <summary>
        /// Состоит ли пользователь в чате
        /// </summary>
        /// <param name="roomId">ИД чата</param>
        /// <param name="userId">ИД пользователя</param>
        /// <returns>true - состоит, false не состоит</returns>
        Task<bool> IsUserInChatRoom(Guid roomId, string userId);
    }
}
