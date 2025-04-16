using BlazorWebRtc_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorWebRtc_Persistence.Configurations;

internal class UserFriendConfiguration : IEntityTypeConfiguration<UserFriend>
{
    public void Configure(EntityTypeBuilder<UserFriend> builder)
    {
        builder
            .HasOne(s => s.Requester)
            .WithMany()
            .HasForeignKey(s => s.RequesterId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(s => s.ReceiverUser)
            .WithMany()
            .HasForeignKey(s => s.ReceiverUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}