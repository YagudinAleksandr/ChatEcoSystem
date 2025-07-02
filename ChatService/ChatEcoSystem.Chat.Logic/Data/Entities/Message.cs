using System;
using ChatEcoSystem.SharedLib.Abstractions;

namespace ChatEcoSystem.Chat.Logic.Data
{
    /// <summary>
    /// Сообщение
    /// </summary>
    internal class Message : IEntity<Guid>
    {
        /// <inheritdoc/>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор отправителя
        /// </summary>
        public Guid SenderId { get; set; }

        /// <summary>
        /// Содержимое сообщения
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Временная метка отправки
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Идентификаторы получателей (через запятую)
        /// </summary>
        public string ReceiverIds { get; set; }

        /// <summary>
        /// Флаг группового сообщения
        /// </summary>
        public bool IsGroupMessage { get; set; }

        /// <summary>
        /// Флаг редактирования сообщения
        /// </summary>
        public bool IsEdited { get; set; } = false;
    }
}
