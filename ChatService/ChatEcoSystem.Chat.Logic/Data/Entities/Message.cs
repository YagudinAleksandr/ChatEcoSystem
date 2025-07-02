using System;
using ChatEcoSystem.Chat.Logic.Data.Entities;
using System.ComponentModel.DataAnnotations;
using ChatEcoSystem.SharedLib.Abstractions;

namespace ChatEcoSystem.Chat.Logic.Data
{
    /// <summary>
    /// Сообщение
    /// </summary>
    public class Message : IEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Идентификатор отправителя
        /// </summary>
        public string SenderId { get; set; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Время отправки
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Идентификатор чат-комнаты
        /// </summary>
        public Guid ChatRoomId { get; set; }

        /// <summary>
        /// Флаг редактирования
        /// </summary>
        public bool IsEdited { get; set; }

        /// <summary>
        /// Идентификатор получателя (для личных сообщений)
        /// </summary>
        public string ReceiverId { get; set; }

        /// <summary>
        /// Навигационное свойство к чат-комнате
        /// </summary>
        public virtual ChatRoom ChatRoom { get; set; }
    }
}
