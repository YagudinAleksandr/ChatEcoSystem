using Microsoft.EntityFrameworkCore;

namespace ChatEcoSystem.Auth.Logic.Data
{
    /// <summary>
    /// Контекст БД сервиса авторизации
    /// </summary>
    public class UserAuthContext : DbContext
    {
        public UserAuthContext(DbContextOptions<UserAuthContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        }
    }
}
