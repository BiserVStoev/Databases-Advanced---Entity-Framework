using _02.CreateUser.Models;

namespace _02.CreateUser
{
    using System.Data.Entity;

    public class UserContext : DbContext
    {
        public UserContext()
            : base("name=UserContext")
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<Album> Albums { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
               .HasMany(user => user.Friends)
               .WithMany()
               .Map(configuration =>
               {
                   configuration.MapLeftKey("UserId");
                   configuration.MapRightKey("FriendId");
                   configuration.ToTable("UserFriends");
               });

            base.OnModelCreating(modelBuilder);
        }
    }
}