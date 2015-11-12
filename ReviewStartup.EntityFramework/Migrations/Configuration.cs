using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ReviewStartup.Infrastructure.Entities;

namespace ReviewStartup.EntityFramework.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ReviewsStartupDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

        }

        protected override void Seed(ReviewsStartupDbContext context)
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
                    Email = Ipsum.GetWord() + "@" + Ipsum.GetWord() + ".com",

                    UserName = Ipsum.GetWord(),
                };

                try
                {
                    result = userManager.Create(user, "adminadmin");



                    var posts = new List<MediaPost>();
                    for (var i = 0; i < 100; i++)
                    {
                        var reviews = new List<Review>();
                        for (int j = 0; j < rand.Next(1, 20); j++)
                        {
                            reviews.Add(new Review()
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
                            AverageScore = averageScore,


                        });
                    }
                    var postsSvaed = context.MediaPosts.AddRange(posts);


                    context.SaveChanges();

                }

                catch (Exception ex)
                {
                    Debug.WriteLine(result);
                    Debug.WriteLine(ex);

                }
            }
        }
    }
}