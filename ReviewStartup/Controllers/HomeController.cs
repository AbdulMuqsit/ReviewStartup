using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ReviewStartup.Infrastructure.Entities;

namespace ReviewStartup.Controllers
{
    public class HomeController : ControllerBase
    {
        public async Task<ActionResult> Index()
        {
            var posts = await Repository.MediaPosts.AllAsync();

            var viewModel = new PostSummaryViewModel()
            {
                Movies = posts.Where(e => e.Type == MediaType.Movie).Reverse().Take(10),
                VideoGames = posts.Where(e => e.Type == MediaType.VideoGame).Reverse().Take(10),
                Music = posts.Where(e => e.Type == MediaType.Music).Reverse().Take(10),
                TvShow = posts.Where(e => e.Type == MediaType.TvShow).Reverse().Take(10)
            };
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

    public class PostSummaryViewModel
    {
        public IEnumerable<MediaPost> Movies { get; set; }
        public IEnumerable<MediaPost> VideoGames { get; set; }
        public IEnumerable<MediaPost> Music { get; set; }
        public IEnumerable<MediaPost> TvShow { get; set; }
    }
}