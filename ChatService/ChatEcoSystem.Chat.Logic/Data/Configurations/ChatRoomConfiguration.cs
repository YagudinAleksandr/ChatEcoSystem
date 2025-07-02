using ChatEcoSystem.Chat.Logic.Data.Entities;
using ChatEcoSystem.SharedLib.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatEcoSystem.Chat.Logic.Data.Configurations
{
    /// <summary>
    /// Конфигурация кабинетов чата
    /// </summary>
    internal class ChatRoomConfiguration : IEntityTypeConfiguration<ChatRoom>
    {
        public void Configure(EntityTypeBuilder<ChatRoom> builder)
        {
            builder.ToTable("ChatRooms");
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Name).IsRequired().HasMaxLength(512);
            builder.Property(x => x.Type).HasDefaultValue(ChatRoomTypeEnum.Personal);
            builder.Property(x=>x.MemberIdsSerialized).IsRequired();
            builder.HasIndex(r => r.MemberIdsSerialized);
        }
    }
}
