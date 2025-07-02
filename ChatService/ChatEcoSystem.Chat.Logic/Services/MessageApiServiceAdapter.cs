using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatEcoSystem.SharedLib.Contracts;

namespace ChatEcoSystem.Chat.Logic.Services
{
    /// <summary>
    /// Адаптер для IMessageService, возвращающий BaseApiResponse<T>
    /// </summary>
    public class MessageApiServiceAdapter : IMessageApiService
    {
        private readonly IMessageService _messageService;

        public MessageApiServiceAdapter(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task<BaseApiResponse<MessageDto>> SaveMessage(MessageDto messageDto)
        {
            var result = await _messageService.SaveMessage(messageDto);
            return new BaseApiResponse<MessageDto>
            {
                Body = result,
                StatusCode = result != null ? 201 : 400
            };
        }

        public async Task<BaseApiResponse<MessageDto>> UpdateMessage(Guid messageId, string newContent)
        {
            var result = await _messageService.UpdateMessage(messageId, newContent);
            return new BaseApiResponse<MessageDto>
            {
                Body = result,
                StatusCode = result != null ? 200 : 404
            };
        }

        public async Task<BaseApiResponse<bool>> DeleteMessage(Guid messageId)
        {
            await _messageService.DeleteMessage(messageId);
            return new BaseApiResponse<bool>
            {
                Body = true,
                StatusCode = 200
            };
        }

        public async Task<BaseApiResponse<MessageDto>> GetMessage(Guid messageId)
        {
            var result = await _messageService.GetMessage(messageId);
            return new BaseApiResponse<MessageDto>
            {
                Body = result,
                StatusCode = result != null ? 200 : 404
            };
        }

        public async Task<BaseApiResponse<List<MessageDto>>> GetChatHistory(Guid chatRoomId, int page = 1, int pageSize = 50)
        {
            var result = await _messageService.GetChatHistory(chatRoomId, page, pageSize);
            return new BaseApiResponse<List<MessageDto>>
            {
                Body = result,
                StatusCode = 200
            };
        }
    }
} 