using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using ChatEcoSystem.Chat.Logic.Services;
using ChatEcoSystem.SharedLib.Contracts;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace ChatEcoSystem.Chat.Logic
{
    public class ChatHub : Hub
    {
        #region CTOR
        /// <inheritdoc cref="IMessageService"/>
        private readonly IMessageService _messageService;

        /// <inheritdoc cref="IChatRoomService"/>
        private readonly IChatRoomService _chatRoomService;

        /// <inheritdoc cref="IPresenceTracker"/>
        private readonly IPresenceTracker _presenceTracker;

        /// <inheritdoc cref="ILogger"/>
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(
            IMessageService messageService,
            IChatRoomService chatRoomService,
            IPresenceTracker presenceTracker,
            ILogger<ChatHub> logger)
        {
            _messageService = messageService;
            _chatRoomService = chatRoomService;
            _presenceTracker = presenceTracker;
            _logger = logger;
        }
        #endregion

        public async Task SendMessage(MessageDto message)
        {
            try
            {
                var userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                    throw new HubException("Unauthorized");

                // Создание личного чата при необходимости
                if (message.ChatRoomId == Guid.Empty && message.ReceiverIds?.Count == 1)
                {
                    var receiverId = message.ReceiverIds[0];
                    var personalChat = await _chatRoomService.FindPersonalChat(userId, receiverId);

                    if (personalChat == null)
                    {
                        personalChat = await _chatRoomService.CreateChatRoom(new ChatRoomDto
                        {
                            Type = 0,
                            Name = $"Чат с {receiverId}",
                            MemberIds = new List<string> { userId, receiverId }
                        });
                    }

                    message.ChatRoomId = personalChat.Id;
                }

                // Проверка участия в чате
                if (!await _chatRoomService.IsUserInChatRoom(message.ChatRoomId, userId))
                    throw new HubException("You are not a member of this chat");

                // Сохранение сообщения
                message.SenderId = userId;
                message.Timestamp = DateTime.UtcNow;

                var savedMessage = await _messageService.SaveMessage(message);

                // Отправка сообщения участникам чата
                await Clients.Group(message.ChatRoomId.ToString())
                    .SendAsync("ReceiveMessage", savedMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending message");
                throw new HubException($"Failed to send message: {ex.Message}");
            }
        }

        public async Task EditMessage(Guid messageId, string newContent)
        {
            try
            {
                var userId = GetUserId();
                var message = await _messageService.GetMessage(messageId);

                if (message == null)
                    throw new HubException("Message not found");

                if (message.SenderId != userId)
                    throw new HubException("You can only edit your own messages");

                var updatedMessage = await _messageService.UpdateMessage(messageId, newContent);

                await Clients.Group(message.ChatRoomId.ToString())
                    .SendAsync("MessageEdited", messageId, newContent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error editing message {messageId}");
                throw new HubException($"Failed to edit message: {ex.Message}");
            }
        }

        public async Task DeleteMessage(Guid messageId)
        {
            try
            {
                var userId = GetUserId();
                var message = await _messageService.GetMessage(messageId);

                if (message == null)
                    throw new HubException("Message not found");

                if (message.SenderId != userId)
                    throw new HubException("You can only delete your own messages");

                await _messageService.DeleteMessage(messageId);

                await Clients.Group(message.ChatRoomId.ToString())
                    .SendAsync("MessageDeleted", messageId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting message {messageId}");
                throw new HubException($"Failed to delete message: {ex.Message}");
            }
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                var userId = GetUserId();
                await _presenceTracker.UserConnected(userId, Context.ConnectionId);

                // Подключение к чат-комнатам пользователя
                var userRooms = await _chatRoomService.GetUserChatRooms(userId);
                foreach (var room in userRooms)
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, room.Id.ToString());
                }

                await base.OnConnectedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OnConnectedAsync");
                throw;
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                var userId = GetUserId();
                await _presenceTracker.UserDisconnected(userId, Context.ConnectionId);

                // Отключение от всех чат-комнат
                var userRooms = await _chatRoomService.GetUserChatRooms(userId);
                foreach (var room in userRooms)
                {
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, room.Id.ToString());
                }

                await base.OnDisconnectedAsync(exception);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OnDisconnectedAsync");
                throw;
            }
        }

        private string GetUserId()
        {
            return Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
