using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using ReviewStartup.Infrastructure.Entities;

namespace ReviewStartup.Controllers
{
    public class SearchController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IHttpActionResult> Get(string title = "", int take = 5)
        {
            if (take > 10) take = 10;
            var postsTask = Repository.MediaPosts.Where(post => post.Title.Replace(" ", "").Contains(title) || post.Title.Contains(title)).Take(take).Select(post => new
            {
                post.Title,
                post.Thumbnail,
                post.Id,
                Type = post.Type
            }).ToListAsync();
            var usersTask = UserManager.Users.Where(e => e.UserName.Replace(" ", "").Contains(title) || e.UserName.Replace(" ", "").Contains(title)).Take(take).Select(e => new { e.Picture, e.UserName, e.FirstName, e.LastName }).ToListAsync();
            await Task.WhenAll(postsTask, usersTask);
            return Ok(new
            {
                MediaPosts = postsTask.Result.Select(post => new
                {
                    post.Title,
                    post.Thumbnail,
                    post.Id,
                    Type = Name(post.Type)
                }).ToList(),
                Users = usersTask.Result
            });
        }

        private static string Name(MediaType? postType)
        {
            if (postType == null)
                return String.Empty;

            return postType.GetType()
                .GetMember(postType.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>().Name;
        }
    }
}
