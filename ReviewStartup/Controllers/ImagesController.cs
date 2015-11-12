using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ReviewStartup.Controllers
{
    public class ImagesController : ControllerBase
    {
        [HttpGet]
        // GET: Images
        [Route("Images/PostThumbnail/{id}")]
        public async Task<ActionResult> Thumbnail(int id)
        {

            var post = await Repository.MediaPosts.GetAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            return File(post.Thumbnail, "image/png");


        }
        [HttpGet]
        [Route("Images/UserThumbnail/{userName}")]
        public async Task<ActionResult> UserThumbnail(string userName)
        {

            var user = await UserManager.FindByNameAsync(userName);
            if (user == null)
            {
                return HttpNotFound();
            }

            return File(user.Picture, "image/png");


        }
    }
}