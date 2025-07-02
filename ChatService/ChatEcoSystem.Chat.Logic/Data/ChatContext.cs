using ChatEcoSystem.Chat.Logic.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ChatEcoSystem.Chat.Logic.Data
{
    /// <summary>
    /// Контекст БД сервиса чатов
    /// </summary>
    public class ChatContext : DbContext
    {
        public ChatContext(DbContextOptions<ChatContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ChatRoomConfiguration).Assembly);
        }
    }
}
