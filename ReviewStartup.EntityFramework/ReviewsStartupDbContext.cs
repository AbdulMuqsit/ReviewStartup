using System.Data.Entity;
using System.Runtime.CompilerServices;
using Microsoft.AspNet.Identity.EntityFramework;
using ReviewStartup.Infrastructure.Entities;

namespace ReviewStartup.EntityFramework
{
    // You can add profile data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class ReviewsStartupDbContext : IdentityDbContext<User>
    {
        public ReviewsStartupDbContext()
            : base("DefaultConnection", false)
        {

            Configuration.LazyLoadingEnabled = true;
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasMany(e => e.MediaPosts).WithRequired(e => e.User).WillCascadeOnDelete(false);
            modelBuilder.Entity<MediaPost>().HasMany(e => e.Reviews).WithRequired(e => e.MediaPost).WillCascadeOnDelete(false);
            modelBuilder.Entity<User>().HasMany(e => e.Reviews).WithRequired(e => e.User).WillCascadeOnDelete(false);
        }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<MediaPost> MediaPosts { get; set; }

        public static ReviewsStartupDbContext Create()
        {
            return new ReviewsStartupDbContext();
        }
    }
}