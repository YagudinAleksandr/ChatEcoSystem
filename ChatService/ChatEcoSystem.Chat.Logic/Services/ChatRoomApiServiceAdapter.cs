using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatEcoSystem.SharedLib.Contracts;

namespace ChatEcoSystem.Chat.Logic.Services
{
    /// <summary>
    /// Адаптер для <see cref="IChatRoomService"/>, возвращающий <see cref="BaseApiResponse{TBody}"/>
    /// </summary>
    public class ChatRoomApiServiceAdapter : IChatRoomApiService
    {
        #region CTOR
        /// <inheritdoc cref="IChatRoomService"/>
        private readonly IChatRoomService _chatRoomService;

        public ChatRoomApiServiceAdapter(IChatRoomService chatRoomService)
        {
            _chatRoomService = chatRoomService;
        }
        #endregion

        public async Task<BaseApiResponse<ChatRoomDto>> CreateChatRoom(ChatRoomDto roomDto)
        {
            var result = await _chatRoomService.CreateChatRoom(roomDto);
            return new BaseApiResponse<ChatRoomDto>
            {
                Body = result,
                StatusCode = result != null ? 201 : 400
            };
        }

        public async Task<BaseApiResponse<ChatRoomDto>> GetChatRoom(Guid roomId)
        {
            var result = await _chatRoomService.GetChatRoom(roomId);
            return new BaseApiResponse<ChatRoomDto>
            {
                Body = result,
                StatusCode = result != null ? 200 : 404
            };
        }

        public async Task<BaseApiResponse<List<ChatRoomDto>>> GetUserChatRooms(string userId)
        {
            var result = await _chatRoomService.GetUserChatRooms(userId);
            return new BaseApiResponse<List<ChatRoomDto>>
            {
                Body = result,
                StatusCode = 200
            };
        }

        public async Task<BaseApiResponse<ChatRoomDto>> FindPersonalChat(string user1Id, string user2Id)
        {
            var result = await _chatRoomService.FindPersonalChat(user1Id, user2Id);
            return new BaseApiResponse<ChatRoomDto>
            {
                Body = result,
                StatusCode = result != null ? 200 : 404
            };
        }

        public async Task<BaseApiResponse<bool>> IsUserInChatRoom(Guid roomId, string userId)
        {
            var result = await _chatRoomService.IsUserInChatRoom(roomId, userId);
            return new BaseApiResponse<bool>
            {
                Body = result,
                StatusCode = 200
            };
        }
    }
} 