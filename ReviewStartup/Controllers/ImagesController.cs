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
        public async Task<ActionResult> Thumbnail(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var post = await Repository.MediaPosts.GetAsync(id.Value);


            return File(post.Thumbnail, "image/png");


        }
    }
}