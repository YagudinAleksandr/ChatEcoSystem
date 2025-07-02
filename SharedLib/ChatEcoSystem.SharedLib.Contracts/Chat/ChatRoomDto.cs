using System;
using System.Collections.Generic;

namespace ChatEcoSystem.SharedLib.Contracts
{
    /// <summary>
    /// DTO для передачи данных о чат-комнате
    /// </summary>
    public class ChatRoomDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Название чат-комнаты
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип чата: 0 - личный, 1 - групповой
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Идентификаторы участников
        /// </summary>
        public List<string> MemberIds { get; set; } = new List<string>();

        /// <summary>
        /// Дополнительные данные для клиента
        /// </summary>
        public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
    }
}
