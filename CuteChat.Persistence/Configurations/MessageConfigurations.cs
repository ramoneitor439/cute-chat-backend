using CuteChat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CuteChat.Persistence.Configurations;
public class MessageConfigurations : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(x => x.SenderId);
        builder.HasIndex(x => x.ReceiverId);

        builder.HasOne(x => x.Sender)
            .WithMany(x => x.SentMessages)
            .HasForeignKey(x => x.SenderId);

        builder.HasOne(x => x.Receiver)
            .WithMany(x => x.IncomingMessages)
            .HasForeignKey(x => x.ReceiverId);

        builder.Property(x => x.Content)
            .IsRequired()
            .HasMaxLength(2500);
    }
}
