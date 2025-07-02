using System;
using ChatEcoSystem.SharedLib.Abstractions;
using ChatEcoSystem.SharedLib.Contracts.Chat;

namespace ChatEcoSystem.Chat.Logic.Data.Entities
{
    /// <summary>
    /// Кабинет чата
    /// </summary>
    public class ChatRoom : IEntity<Guid>
    {
        /// <inheritdoc/>
        public Guid Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип кабинета
        /// </summary>
        public ChatRoomTypeEnum Type { get; set; }

        /// <summary>
        /// Идентификаторы участников через запятую
        /// </summary>
        public string MemberIds { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
