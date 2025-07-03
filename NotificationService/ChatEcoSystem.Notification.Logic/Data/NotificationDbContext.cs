using Microsoft.EntityFrameworkCore;

namespace ChatEcoSystem.Notification.Logic
{
    /// <summary>
    /// Контекст подключения к БД сервиса уведомлений
    /// </summary>
    public class NotificationDbContext : DbContext
    {
        public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UnreadMessageConfiguration).Assembly);
        }
    }
}
