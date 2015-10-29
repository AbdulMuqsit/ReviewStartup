using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ReviewStartup.EntityFramework;
using ReviewStartup.Infrastructure.Entities;

namespace ReviewStartup.Controllers
{
    public class MediaPostsController : ControllerBase
    {

        // GET: MediaPosts
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult> Index()
        {
            var mediaPosts = Context.MediaPosts.Include(m => m.User);
            return View(await mediaPosts.ToListAsync());
        }

        // GET: MediaPosts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = await Context.MediaPosts.Include(e => e.Reviews.Select(k => k.User)).Include(e => e.User).Where(e => e.Id == id.Value).FirstOrDefaultAsync();
            if (post == null)
            {
                return HttpNotFound();
            }
            MediaPostDetailsViewModel model = new MediaPostDetailsViewModel()
            {
                Post = post,
                Reviews = post.Reviews.ToList()
            };

            return View(model);
        }

        // GET: MediaPosts/Create
        [Authorize(Roles = "Admin")]

        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(Context.Users, "Id", "Email");
            return View();
        }

        // POST: MediaPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create([Bind(Include = "Title,Type,Summary,Thumbnail")] MediaPostPostViewModel mediaPost)
        {
            if (ModelState.IsValid)
            {
                using (var stream = mediaPost.Thumbnail.InputStream)
                {
                    var memoryStream = stream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        await stream.CopyToAsync(memoryStream);

                    }

                    Context.MediaPosts.Add(new MediaPost()
                    {
                        UserId = User.Identity.GetUserId(),
                        Title = mediaPost.Title,
                        Type = mediaPost.Type,
                        Summary = mediaPost.Summary,
                        Thumbnail = memoryStream.ToArray()
                    });
                    await Context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            return View(mediaPost);
        }

        //// GET: MediaPosts/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MediaPost mediaPost = await Context.MediaPosts.FindAsync(id);
        //    if (mediaPost == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.UserId = new SelectList(Context.Users, "Id", "Email", mediaPost.UserId);
        //    return View(mediaPost);
        //}

        //// POST: MediaPosts/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Type,Summary,Ratings,Thumbnail,AverageScore,UserId,MarkText")] MediaPost mediaPost)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Context.Entry(mediaPost).State = EntityState.Modified;
        //        await Context.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.UserId = new SelectList(Context.Users, "Id", "Email", mediaPost.UserId);
        //    return View(mediaPost);
        //}

        //// GET: MediaPosts/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MediaPost mediaPost = await Context.MediaPosts.FindAsync(id);
        //    if (mediaPost == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(mediaPost);
        //}

        //// POST: MediaPosts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    MediaPost mediaPost = await Context.MediaPosts.FindAsync(id);
        //    Context.MediaPosts.Remove(mediaPost);
        //    await Context.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    public class MediaPostDetailsViewModel
    {
        public MediaPost Post { get; set; }
        public List<Review> Reviews { get; set; }
    }

    public class MediaPostPostViewModel
    {
        [Required(ErrorMessage = "Title is required")]
        [MinLength(1, ErrorMessage = "Atleast 1 characters is required for Title")]
        [MaxLength(40, ErrorMessage = "Tiltle can be 40 characters maximum")]
        public string Title { get; set; }


        [Required(ErrorMessage = "Specify your media type")]
        public MediaType? Type { get; set; }

        [Required(ErrorMessage = "Specify a summary for media")]
        [MinLength(20, ErrorMessage = "Summary needs atleast 20 characters")]
        [MaxLength(150, ErrorMessage = "Summary be 150 characters maximum")]
        public string Summary { get; set; }




        [Required(ErrorMessage = "Thumbnail image is required.")]
        public HttpPostedFileBase Thumbnail { get; set; }


    }
}
