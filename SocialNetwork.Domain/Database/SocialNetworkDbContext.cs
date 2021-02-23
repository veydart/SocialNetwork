using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Domain.Database
{
    public class SocialNetworkDbContext : DbContext
    {
        public SocialNetworkDbContext(DbContextOptions<SocialNetworkDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserSubscriber> UserSubscribers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserSubscriber>().HasKey(us => new { us.UserId, us.SubscriberId });
        }
    }
}