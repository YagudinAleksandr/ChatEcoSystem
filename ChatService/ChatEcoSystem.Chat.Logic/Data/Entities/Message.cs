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
        public Guid Id { get; set; } = new Guid();

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
        /// ИД кабинета
        /// </summary>
        public Guid ChatRoomId { get; set; }
    }
}
