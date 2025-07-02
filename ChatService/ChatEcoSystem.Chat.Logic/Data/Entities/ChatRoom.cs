using System;
using System.Collections.Generic;
using System.Linq;
using ChatEcoSystem.SharedLib.Abstractions;
using ChatEcoSystem.SharedLib.Contracts;

namespace ChatEcoSystem.Chat.Logic.Data.Entities
{
    /// <summary>
    /// Кабинет чата
    /// </summary>
    public class ChatRoom : IEntity<Guid>
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Название чат-комнаты
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип чата: 0 - личный, 1 - групповой
        /// </summary>
        public ChatRoomTypeEnum Type { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Идентификаторы участников (сериализованный список)
        /// </summary>
        public string MemberIdsSerialized { get; set; }

        /// <summary>
        /// Сообщения в чате
        /// </summary>
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

        /// <summary>
        /// Получить список идентификаторов участников
        /// </summary>
        public List<string> GetMemberIds()
        {
            return string.IsNullOrEmpty(MemberIdsSerialized)
                ? new List<string>()
                : MemberIdsSerialized.Split(',').ToList();
        }

        /// <summary>
        /// Установить список идентификаторов участников
        /// </summary>
        public void SetMemberIds(IEnumerable<string> memberIds)
        {
            MemberIdsSerialized = string.Join(",", memberIds.Distinct());
        }
    }
}
