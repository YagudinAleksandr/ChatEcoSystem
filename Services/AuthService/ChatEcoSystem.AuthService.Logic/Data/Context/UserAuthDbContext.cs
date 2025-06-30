using Microsoft.EntityFrameworkCore;

namespace ChatEcoSystem.AuthService.Logic
{
    /// <summary>
    /// Контекст базы данных для авторизации
    /// </summary>
    internal class UserAuthDbContext : DbContext
    {
        public UserAuthDbContext(DbContextOptions<UserAuthDbContext> options) : base(options) { }

        /// <summary>
        /// Пользователи
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Генерация миграции
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        }
    }
}
