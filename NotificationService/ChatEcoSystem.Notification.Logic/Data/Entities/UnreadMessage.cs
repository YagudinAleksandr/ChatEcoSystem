using System;

namespace ChatEcoSystem.Notification.Logic
{
    /// <summary>
    /// Не прочитанное сообщение
    /// </summary>
    public class UnreadMessage
    {
        /// <summary>
        /// Идентификатор сообщения
        /// </summary>
        public Guid MessageId { get; set; }

        /// <summary>
        /// Идентификатор получателя
        /// </summary>
        public string ReceiverId { get; set; }

        /// <summary>
        /// Email получателя
        /// </summary>
        public string ReceiverEmail { get; set; }

        /// <summary>
        /// Идентификатор отправителя
        /// </summary>
        public string SenderId { get; set; }

        /// <summary>
        /// Имя отправителя
        /// </summary>
        public string SenderName { get; set; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Время отправки сообщения
        /// </summary>
        public DateTime SentTime { get; set; }
    }
}
