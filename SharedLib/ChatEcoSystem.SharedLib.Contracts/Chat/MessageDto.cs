using System;
using System.Collections.Generic;

namespace ChatEcoSystem.SharedLib.Contracts
{
    /// <summary>
    /// DTO для передачи данных о сообщении
    /// </summary>
    public class MessageDto
    {
        public Guid Id { get; set; }

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
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Идентификатор чат-комнаты
        /// </summary>
        public Guid ChatRoomId { get; set; }

        /// <summary>
        /// Флаг редактирования
        /// </summary>
        public bool IsEdited { get; set; }

        /// <summary>
        /// Идентификаторы получателей (для новых сообщений)
        /// </summary>
        public List<string> ReceiverIds { get; set; } = new List<string>();

        /// <summary>
        /// Дополнительные данные для клиента
        /// </summary>
        public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
    }
}
