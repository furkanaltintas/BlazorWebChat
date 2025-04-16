using BlazorWebRtc_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorWebRtc_Persistence.Configurations;

internal class MessageRoomConfiguration : IEntityTypeConfiguration<MessageRoom>
{
    public void Configure(EntityTypeBuilder<MessageRoom> builder)
    {
        builder
            .HasOne(s => s.SenderUser)
            .WithMany()
            .HasForeignKey(s => s.SenderUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(s => s.ReceiverUser)
            .WithMany()
            .HasForeignKey(s => s.ReceiverUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}