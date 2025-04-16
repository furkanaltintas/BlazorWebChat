using BlazorWebRtc_Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebRtc_Persistence.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected AppDbContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Ioc).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<MessageRoom> MessageRooms { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserFriend> UserFriends { get; set; }
}