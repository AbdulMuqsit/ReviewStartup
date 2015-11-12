using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ReviewStartup.Controllers
{
    public class SearchController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IHttpActionResult> Get(string title = "", int take = 5)
        {
            if (take > 10) take = 10;
            var postsTask = Repository.MediaPosts.Where(post => post.Title.Replace(" ", "").Contains(title) || post.Title.Contains(title)).Take(take).Select(post => new { post.Title, post.Thumbnail, post.Id }).ToListAsync();
            var usersTask = UserManager.Users.Where(e => e.UserName.Replace(" ", "").Contains(title) || e.UserName.Replace(" ", "").Contains(title)).Take(take).Select(e => new { e.Picture, e.UserName, e.FirstName, e.LastName }).ToListAsync();
            await Task.WhenAll(postsTask, usersTask);
            return Ok(new { MediaPosts = postsTask.Result, Users = usersTask.Result });
        }
    }
}
