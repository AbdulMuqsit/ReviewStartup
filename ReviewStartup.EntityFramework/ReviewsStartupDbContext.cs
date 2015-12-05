using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.AspNet.Identity;
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
            //Seed(this);
        }
            protected  void Seed(ReviewsStartupDbContext context)
            {
                if (!context.MediaPosts.Any())
                {
                    IdentityResult result = null;

                    if (!Debugger.IsAttached)
                        Debugger.Launch();
                    var userManager = new UserManager<User, string>(new UserStore<User>(context));
                    var roleManager = new RoleManager<IdentityRole, string>(new RoleStore<IdentityRole>(context));
                    roleManager.Create(new IdentityRole("Admin"));
                    var role = roleManager.FindByName("Admin");
                    var rand = new Random();
                    var user = new User
                    {
                        Email = "admin@reviewstartup.com",
                        UserName = "Admin"
                    };

                    try
                    {
                        result = userManager.Create(user, "admin@reviewstartup.com");

                        userManager.AddToRole(user.Id, "Admin");

                        var posts = new List<MediaPost>();
                        for (var i = 0; i < 100; i++)
                        {
                            var reviews = new List<Review>();
                            for (var j = 0; j < rand.Next(1, 20); j++)
                            {
                                reviews.Add(new Review
                                {
                                    Ratings = rand.Next(1, 10),
                                    UserId = user.Id,
                                    Text = Ipsum.GetPhrase(rand.Next(5, 20)),
                                    Title = Ipsum.GetPraise()
                                });
                            }
                            var averageScore = reviews.Average(e => e.Ratings);
                            posts.Add(new MediaPost
                            {
                                Title = Ipsum.GetPhrase(rand.Next(1, 6)),
                                Type = (MediaType?)rand.Next(0, 4),
                                Reviews = reviews,
                                Summary = Ipsum.GetPhrase(rand.Next(5, 20)),
                                UserId = user.Id,
                                AverageScore = averageScore
                            });
                        }
                        user.FriendRequests = new List<User>();
                        user.Friends = new List<User>();
                        var postsSvaed = context.MediaPosts.AddRange(posts);
                        for (var i = 0; i < rand.Next(3, 40); i++)
                        {
                            var localUser = new User
                            {
                                Email = Ipsum.GetWord() + "@" + Ipsum.GetWord() + rand.Next() + ".com",
                                UserName = Ipsum.GetWord() + rand.Next()
                            };

                            userManager.Create(localUser, "idkwmpsp!2");
                            user.FriendRequests.Add(localUser);

                            userManager.Update(user);
                        }
                        for (var i = 0; i < rand.Next(3, 40); i++)
                        {
                            var localUser = new User
                            {
                                Email = Ipsum.GetWord() + "@" + Ipsum.GetWord() + rand.Next() + ".com",
                                UserName = Ipsum.GetWord() + rand.Next()
                            };
                            userManager.Create(localUser, "idkwmpsp!2");
                            user.Friends.Add(localUser);
                            localUser.Friends = new List<User>() { user };
                            userManager.Update(user);
                            userManager.Update(localUser);
                        }
                        context.SaveChanges();
                    }

                    catch (Exception ex)
                    {
                        Debug.WriteLine(result);
                        Debug.WriteLine(ex);
                    }
                }
            }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasMany(e => e.MediaPosts).WithRequired(e => e.User).WillCascadeOnDelete(false);
            modelBuilder.Entity<User>().HasMany(e => e.Friends).WithMany().Map(config => config.ToTable("Friends").MapLeftKey("Initiatert").MapRightKey("Acceptor"));
            modelBuilder.Entity<User>().HasMany(e => e.FriendRequests).WithMany().Map(config => config.ToTable("FriendRequests").MapLeftKey("Sender").MapRightKey("Reciever"));
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