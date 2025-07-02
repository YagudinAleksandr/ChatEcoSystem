using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatEcoSystem.Chat.Logic.Data.Configurations
{ 
    /// <summary>
    /// Конфигурация сообщения
    /// </summary>
    internal class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages");
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Content).IsRequired().HasMaxLength(1024);
            builder.Property(x=>x.SenderId).IsRequired();
            builder.Property(x=>x.ChatRoomId).IsRequired();
        }
    }
}
