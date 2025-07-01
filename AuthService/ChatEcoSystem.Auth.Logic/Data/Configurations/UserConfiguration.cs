using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatEcoSystem.Auth.Logic
{
    /// <summary>
    /// Конфигурация сущности <see cref="User"/>
    /// </summary>
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(60);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(256);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(60);
            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}
