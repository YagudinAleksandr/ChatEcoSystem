using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatEcoSystem.Notification.Logic
{
    /// <summary>
    /// Конфигурация непрочитанных сообщений
    /// </summary>
    public class UnreadMessageConfiguration : IEntityTypeConfiguration<UnreadMessage>
    {
        public void Configure(EntityTypeBuilder<UnreadMessage> builder)
        {
            builder.ToTable("UnreadMessages");
            builder.HasKey(e => e.MessageId);
            builder.Property(e => e.ReceiverId).IsRequired();
            builder.Property(e => e.ReceiverEmail).IsRequired();
            builder.Property(e => e.SentTime).IsRequired();
            builder.HasIndex(e => e.ReceiverId);
            builder.HasIndex(e => e.SentTime);
        }
    }
}
