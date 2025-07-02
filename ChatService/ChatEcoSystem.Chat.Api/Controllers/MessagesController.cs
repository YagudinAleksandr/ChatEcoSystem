using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatEcoSystem.Chat.Logic;
using ChatEcoSystem.Chat.Logic.Services;
using ChatEcoSystem.SharedLib.Contracts;

namespace ChatEcoSystem.Chat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : BaseApiController
    {
        private readonly IMessageApiService _service;

        public MessagesController(IMessageApiService service)
        {
            _service = service;
        }

        /// <summary>
        /// Сохранение сообщения
        /// </summary>
        /// <param name="messageDto">Сообщение</param>
        /// <returns>Стандартный API-ответ с созданным сообщением</returns>
        [HttpPost(nameof(SaveMessage))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BaseApiResponse<MessageDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseApiResponse<MessageDto>))]
        public async Task<IActionResult> SaveMessage([FromBody] MessageDto messageDto)
        {
            return ActionRes(await _service.SaveMessage(messageDto));
        }

        /// <summary>
        /// Обновление сообщения
        /// </summary>
        /// <param name="messageId">ИД сообщения</param>
        /// <param name="newContent">Новые данные</param>
        /// <returns>Стандартный API-ответ с обновленным сообщением</returns>
        [HttpPut(nameof(UpdateMessage))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseApiResponse<MessageDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseApiResponse<MessageDto>))]
        public async Task<IActionResult> UpdateMessage(Guid messageId, [FromBody] string newContent)
        {
            return ActionRes(await _service.UpdateMessage(messageId, newContent));
        }

        /// <summary>
        /// Удаление сообщения
        /// </summary>
        /// <param name="messageId">ИД сообщения</param>
        /// <returns>Стандартный API-ответ с результатом удаления</returns>
        [HttpDelete(nameof(DeleteMessage))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseApiResponse<bool>))]
        public async Task<IActionResult> DeleteMessage(Guid messageId)
        {
            return ActionRes(await _service.DeleteMessage(messageId));
        }

        /// <summary>
        /// Получение сообщения по ИД
        /// </summary>
        /// <param name="messageId">ИД сообщения</param>
        /// <returns>Стандартный API-ответ с сообщением</returns>
        [HttpGet(nameof(GetMessage))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseApiResponse<MessageDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseApiResponse<MessageDto>))]
        public async Task<IActionResult> GetMessage(Guid messageId)
        {
            return ActionRes(await _service.GetMessage(messageId));
        }

        /// <summary>
        /// Получение истории чата
        /// </summary>
        /// <param name="chatRoomId">ИД чата</param>
        /// <param name="page">Страница</param>
        /// <param name="pageSize">Количество элементов на странице</param>
        /// <returns>Стандартный API-ответ со списком сообщений</returns>
        [HttpGet(nameof(GetChatHistory))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseApiResponse<List<MessageDto>>))]
        public async Task<IActionResult> GetChatHistory(Guid chatRoomId, int page = 1, int pageSize = 50)
        {
            return ActionRes(await _service.GetChatHistory(chatRoomId, page, pageSize));
        }
    }
}
