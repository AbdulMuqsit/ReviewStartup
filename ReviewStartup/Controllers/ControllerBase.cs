using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ReviewStartup.EntityFramework;
using ReviewStartup.Infrastructure.Entities;
using ReviewStartup.Models;


namespace ReviewStartup.Controllers
{
    public class ControllerBase : Controller
    {
        public IReviewStartupDataRepository Repository { get; set; }
        public ApplicationUserManager UserManager => Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
        public ApplicationSignInManager SignInManager => Request.GetOwinContext().Get<ApplicationSignInManager>();

        public ControllerBase()
        {

            ReviewsStartupDbContext context = new ReviewsStartupDbContext();
            Context = context;
            Repository = new ReviewStartupDataRepository(context);
          
        }
        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        public ReviewsStartupDbContext Context { get; private set; }
    }

    public interface IReviewStartupDataRepository
    {
        IRepository<MediaPost> MediaPosts { get; set; }
    }

    class ReviewStartupDataRepository : IReviewStartupDataRepository
    {

        public ReviewStartupDataRepository(ReviewsStartupDbContext context)
        {

            MediaPosts = new Repository<MediaPost>(context);
        }

        public IRepository<MediaPost> MediaPosts { get; set; }

    }

}