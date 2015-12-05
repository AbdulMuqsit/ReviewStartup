using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ReviewStartup.Infrastructure.Entities;

namespace ReviewStartup.Controllers
{
    public class HomeController : ControllerBase
    {
        public async Task<ActionResult> Index()
        {
            var posts = await Repository.MediaPosts.AllAsync();

            var viewModel = new HomeIndexViewModel()
            {
                Movies = posts.Where(e => e.Type == MediaType.Movie).Reverse().Take(10),
                VideoGames = posts.Where(e => e.Type == MediaType.VideoGame).Reverse().Take(10),
                Music = posts.Where(e => e.Type == MediaType.Music).Reverse().Take(10),
                TvShow = posts.Where(e => e.Type == MediaType.TvShow).Reverse().Take(10),

            };
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var user = await
                    UserManager.Users.Include(e => e.Friends)
                        .Include(e => e.FriendRequests)
                        .SingleOrDefaultAsync(e => e.UserName == User.Identity.Name);
                viewModel.Friends = user?.Friends;
                viewModel.FriendRequests = user?.FriendRequests;
            }
            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }

    public class HomeIndexViewModel
    {
        public IEnumerable<MediaPost> Movies { get; set; }
        public IEnumerable<MediaPost> VideoGames { get; set; }
        public IEnumerable<MediaPost> Music { get; set; }
        public IEnumerable<MediaPost> TvShow { get; set; }
        public IEnumerable<User> Friends { get; set; }
        public IEnumerable<User> FriendRequests { get; set; }
    }
}