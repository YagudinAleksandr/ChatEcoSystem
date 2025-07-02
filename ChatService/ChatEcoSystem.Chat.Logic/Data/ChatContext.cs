using Microsoft.EntityFrameworkCore;

namespace ChatEcoSystem.Chat.Logic.Data
{
    /// <summary>
    /// Контекст БД сервиса чатов
    /// </summary>
    public class ChatContext : DbContext
    {
        public ChatContext(DbContextOptions<ChatContext> options) : base(options) { }
    }
}
