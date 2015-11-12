using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
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
    public class ReviewsController : ControllerBase
    {

        //// GET: Reviews
        //public async Task<ActionResult> Index()
        //{
        //    var reviews = Context.Reviews.Include(r => r.MediaPost).Include(r => r.User);
        //    return View(await reviews.ToListAsync());
        //}

        //// GET: Reviews/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Review review = await Context.Reviews.FindAsync(id);
        //    if (review == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(review);
        //}

        // GET: Reviews/Create

        public ActionResult CreateReview(string id)
        {
            ViewBag.MediaPostId = id;
            return PartialView();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult> Create([Bind(Include = "MediaPostId,Text,Title,Ratings")] PostReviewViewModle review)
        {
            if (ModelState.IsValid)
            {

                var prod = await Context.MediaPosts.FindAsync(review.MediaPostId);
                if (User.IsInRole("User"))
                {
                    var userId = User.Identity.GetUserId();
                    if (await Context.MediaPosts.AnyAsync(mediaPost => mediaPost.Id == review.MediaPostId && mediaPost.Reviews.Any(rev => rev.UserId == userId)))
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Content("You can review a post only once!");
                    }
                }
                Context.Reviews.Add(new Review() { MediaPostId = review.MediaPostId.Value, Text = review.Text, Title = review.Title, Ratings = review.Ratings.Value, UserId = User.Identity.GetUserId() });
                await Context.SaveChangesAsync();
                prod.AverageScore = await Context.Reviews.Where(e => e.MediaPostId == review.MediaPostId).AverageAsync(e => e.Ratings);
                await Context.SaveChangesAsync();

                return RedirectToAction("Details", "MediaPosts", new { id = review.MediaPostId });
            }


            return View("CreateReview", review);
        }

        //// GET: Reviews/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Review review = await Context.Reviews.FindAsync(id);
        //    if (review == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.MediaPostId = new SelectList(Context.MediaPosts, "Id", "Title", review.MediaPostId);
        //    ViewBag.UserId = new SelectList(Context.Users, "Id", "Email", review.UserId);
        //    return View(review);
        //}

        //// POST: Reviews/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "Id,UserId,MediaPostId,Text,Title,Ratings")] Review review)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Context.Entry(review).State = EntityState.Modified;
        //        await Context.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.MediaPostId = new SelectList(Context.MediaPosts, "Id", "Title", review.MediaPostId);
        //    ViewBag.UserId = new SelectList(Context.Users, "Id", "Email", review.UserId);
        //    return View(review);
        //}

        //// GET: Reviews/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Review review = await Context.Reviews.FindAsync(id);
        //    if (review == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(review);
        //}

        //// POST: Reviews/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    Review review = await Context.Reviews.FindAsync(id);
        //    Context.Reviews.Remove(review);
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

    public class PostReviewViewModle
    {
        [Required]
        public int? MediaPostId { get; set; }

        [Required(ErrorMessage = "Review Text is required.")]
        [MinLength(10, ErrorMessage = "Review Text needs atleast 10 characters.")]
        [MaxLength(150, ErrorMessage = "Review Text can be 150 characters maximum.")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Review Title is required")]
        [MinLength(3, ErrorMessage = "Title needs atleast 3 characters.")]
        [MaxLength(30, ErrorMessage = "Title can be 30 characters maximum.")]
        public string Title { get; set; }
        [Range(1, 10)]
        [Required]
        public double? Ratings { get; set; }
    }
}
