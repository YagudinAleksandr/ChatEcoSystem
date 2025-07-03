using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ChatEcoSystem.Notification.Logic
{
    /// <inheritdoc cref="INotificationService"/>
    internal class NotificationService : INotificationService
    {
        /// <inheritdoc cref="IMessageRepository"/>
        private readonly IMessageRepository _messageRepository;

        /// <inheritdoc cref="IPresenceTracker"/>
        private readonly IPresenceTracker _presenceTracker;

        /// <inheritdoc cref="IEmailService"/>
        private readonly IEmailService _emailService;

        /// <inheritdoc cref="ILogger"/>
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(
            IMessageRepository messageRepository,
            IPresenceTracker presenceTracker,
            IEmailService emailService,
            ILogger<NotificationService> logger)
        {
            _messageRepository = messageRepository;
            _presenceTracker = presenceTracker;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task ProcessNotificationsAsync()
        {
            try
            {
                _logger.LogInformation("Starting notification processing...");

                // Получаем непрочитанные сообщения старше 5 минут
                var messages = await _messageRepository
                    .GetUnreadMessagesOlderThanAsync(TimeSpan.FromMinutes(5));

                _logger.LogInformation($"Found {messages.Count} unread messages");

                foreach (var message in messages)
                {
                    await ProcessMessageAsync(message);
                }

                _logger.LogInformation("Notification processing completed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in notification processing");
            }
        }

        /// <summary>
        /// Отправка сообщения
        /// </summary>
        /// <param name="message">Сообщение</param>
        private async Task ProcessMessageAsync(UnreadMessage message)
        {
            try
            {
                // Проверяем онлайн-статус получателя
                var isOnline = await _presenceTracker.IsUserOnline(message.ReceiverId);

                if (!isOnline)
                {
                    _logger.LogInformation($"Sending notification for message {message.MessageId} to {message.ReceiverEmail}");

                    // Формируем и отправляем email
                    var subject = "У вас непрочитанное сообщение";
                    var body = $"От: {message.SenderName}\n\n" +
                               $"Сообщение: {Truncate(message.Content, 100)}\n\n" +
                               $"Время: {message.SentTime:g}";

                    await _emailService.SendNotificationAsync(message.ReceiverEmail, subject, body);

                    // Помечаем сообщение как обработанное (опционально)
                    //await _messageRepository.MarkAsNotifiedAsync(message.MessageId);
                }
                else
                {
                    _logger.LogInformation($"User {message.ReceiverId} is online, skipping notification");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing message {message.MessageId}");
            }
        }

        /// <summary>
        /// Обрезаем длинный текст
        /// </summary>
        /// <param name="value">Значение</param>
        /// <param name="maxLength">Максимальное число знаков</param>
        /// <returns>Обрезанная строка</returns>
        private static string Truncate(string value, int maxLength)
        {
            return string.IsNullOrEmpty(value)
                ? value
                : value.Length <= maxLength
                    ? value
                    : value[..maxLength] + "...";
        }
    }
}
