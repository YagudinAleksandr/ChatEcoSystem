using System;
using System.Collections.Generic;
using ChatEcoSystem.Chat.Logic.Data;
using ChatEcoSystem.SharedLib.Contracts;
using System.Threading.Tasks;
using AutoMapper;
using ChatEcoSystem.SharedLib.Abstractions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ChatEcoSystem.Chat.Logic
{
    /// <inheritdoc cref="IMessageService"/>
    internal class MessageService : IMessageService
    {
        #region CTOR
        /// <inheritdoc cref="IRepository{TEntity, TKey}"/>
        private readonly IRepository<Message, Guid> _repository;

        /// <inheritdoc cref="IMapper"/>
        private readonly IMapper _mapper;

        public MessageService(IRepository<Message, Guid> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        public async Task<MessageDto> SaveMessage(MessageDto messageDto)
        {
            var message = _mapper.Map<Message>(messageDto);

            message = await _repository.Add(message);

            return _mapper.Map<MessageDto>(message);
        }

        public async Task<MessageDto> UpdateMessage(Guid messageId, string newContent)
        {
            var message = await _repository.GetById(messageId);
            if (message == null) 
            {
                return null;
            }

            message.Content = newContent;
            message.IsEdited = true;

            message = await _repository.Update(message);

            return _mapper.Map<MessageDto>(message);
        }

        public async Task DeleteMessage(Guid messageId)
        {
            var message = await _repository.GetById(messageId);
            if (message == null)
            {
                return;
            }

            await _repository.Delete(message);
        }

        public async Task<MessageDto> GetMessage(Guid messageId)
        {
            var message = await _repository.GetById(messageId);
            if(message == null)
            {
                return null;
            }

            return _mapper.Map<MessageDto>(message);
        }

        public async Task<List<MessageDto>> GetChatHistory(Guid chatRoomId, int page = 1, int pageSize = 50)
        {
            var messages = await (await _repository.GetAll()).Where(x => x.ChatRoomId == chatRoomId)
                .OrderByDescending(x => x.Timestamp)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return _mapper.Map<List<MessageDto>>(messages);
        }
    }
}
