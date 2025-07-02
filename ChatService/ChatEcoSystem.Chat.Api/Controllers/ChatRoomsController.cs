using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatEcoSystem.Chat.Logic;
using ChatEcoSystem.Chat.Logic.Services;
using ChatEcoSystem.SharedLib.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatEcoSystem.Chat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatRoomsController : BaseApiController
    {
        #region CTOR
        /// <inheritdoc cref="IChatRoomService"/>
        private readonly IChatRoomApiService _service;

        public ChatRoomsController(IChatRoomApiService service)
        {
            _service = service;
        }
        #endregion

        /// <summary>
        /// Создание чата
        /// </summary>
        /// <param name="chat">Данные чата</param>
        /// <returns>Стандартный API-ответ с созданным чатом</returns>
        [HttpPost(nameof(Create))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BaseApiResponse<ChatRoomDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof (BaseApiResponse<ChatRoomDto>))]
        public async Task<IActionResult> Create([FromBody] ChatRoomDto chat)
        {
            return ActionRes(await _service.CreateChatRoom(chat));
        }

        /// <summary>
        /// Получение чата по ИД
        /// </summary>
        /// <param name="roomId">ИД чата</param>
        /// <returns>Стандартный API-ответ с чатом</returns>
        [HttpGet(nameof(GetChatRoom))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseApiResponse<ChatRoomDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseApiResponse<ChatRoomDto>))]
        public async Task<IActionResult> GetChatRoom(Guid roomId)
        {
            return ActionRes(await _service.GetChatRoom(roomId));
        }

        /// <summary>
        /// Получение чатов по пользователю
        /// </summary>
        /// <param name="userId">ИД пользователя</param>
        /// <returns>Стандартный API-ответ со списком чатов</returns>
        [HttpGet(nameof(GetUserChatRooms))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseApiResponse<List<ChatRoomDto>>))]
        public async Task<IActionResult> GetUserChatRooms(string userId)
        {
            return ActionRes(await _service.GetUserChatRooms(userId));
        }

        /// <summary>
        /// Поиск персональных чатов
        /// </summary>
        /// <param name="user1Id">ИД пользователя 1</param>
        /// <param name="user2Id">ИД пользователя 2</param>
        /// <returns>Стандартный API-ответ с найденным чатом</returns>
        [HttpGet(nameof(FindPersonalChat))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseApiResponse<ChatRoomDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseApiResponse<ChatRoomDto>))]
        public async Task<IActionResult> FindPersonalChat(string user1Id, string user2Id)
        {
            return ActionRes(await _service.FindPersonalChat(user1Id, user2Id));
        }

        /// <summary>
        /// Состоит ли пользователь в чате
        /// </summary>
        /// <param name="roomId">ИД чата</param>
        /// <param name="userId">ИД пользователя</param>
        /// <returns>Стандартный API-ответ с результатом проверки</returns>
        [HttpGet(nameof(IsUserInChatRoom))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseApiResponse<bool>))]
        public async Task<IActionResult> IsUserInChatRoom(Guid roomId, string userId)
        {
            return ActionRes(await _service.IsUserInChatRoom(roomId, userId));
        }
    }
}
