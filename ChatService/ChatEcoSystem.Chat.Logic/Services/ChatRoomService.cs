using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using AutoMapper;
using ChatEcoSystem.Chat.Logic.Data.Entities;
using ChatEcoSystem.SharedLib.Abstractions;
using ChatEcoSystem.SharedLib.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ChatEcoSystem.Chat.Logic.Services
{
    internal class ChatRoomService : IChatRoomService
    {
        #region CTOR
        /// <inheritdoc cref="IRepository{TEntity, TKey}"/>
        private readonly IRepository<ChatRoom, Guid> _repository;

        /// <inheritdoc cref="IMapper"/>
        private readonly IMapper _mapper;

        public ChatRoomService(IRepository<ChatRoom, Guid> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<ChatRoomDto> CreateChatRoom(ChatRoomDto roomDto)
        {
            var chatRoom = _mapper.Map<ChatRoom>(roomDto);

            chatRoom = await _repository.Add(chatRoom);

            return _mapper.Map<ChatRoomDto>(chatRoom);
        }

        public async Task<ChatRoomDto> GetChatRoom(Guid roomId)
        {
            var chatRoom = await _repository.GetById(roomId);
            if (chatRoom == null)
            {
                return null;
            }

            return _mapper.Map<ChatRoomDto>(chatRoom);
        }

        public async Task<List<ChatRoomDto>> GetUserChatRooms(string userId)
        {
            var chatRooms = await (await _repository.GetAll())
                .Where(r => r.MemberIdsSerialized.Contains(userId))
                .ToListAsync();

            return _mapper.Map<List<ChatRoomDto>>(chatRooms);
        }

        public async Task<ChatRoomDto> FindPersonalChat(string user1Id, string user2Id)
        {
            var chatRooms = await (await _repository.GetAll())
                .Where(x => x.Type == ChatRoomTypeEnum.Personal)
                .ToListAsync();

            var personalRooms = chatRooms.FirstOrDefault(r => 
                r.GetMemberIds().Contains(user1Id) &&
                r.GetMemberIds().Contains(user2Id));

            return personalRooms != null ? _mapper.Map<ChatRoomDto>(personalRooms) : null;
        }

        public async Task<bool> IsUserInChatRoom(Guid roomId, string userId)
        {
            var room = await _repository.GetById(roomId);
            return room?.GetMemberIds().Contains(userId) ?? false;
        }
    }
}
