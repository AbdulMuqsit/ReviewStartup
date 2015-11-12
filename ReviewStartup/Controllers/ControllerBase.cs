using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ReviewStartup.EntityFramework;
using ReviewStartup.Models;


namespace ReviewStartup.Controllers
{
    public class ControllerBase : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;
        public IReviewStartupDataRepository Repository { get; set; }
        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? (_userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>()); }
            set { _userManager = value; }
        }

        public ApplicationSignInManager SignInManager
        {
            get { return _signInManager ?? (_signInManager = Request.GetOwinContext().Get<ApplicationSignInManager>()); }
            set { _signInManager = value; }
        }

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
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Request != null && Request.IsAuthenticated)
            {
                var user = UserManager.FindByName(User.Identity.Name);
                if (User != null && user.Picture != null) ViewBag.Picture = Convert.ToBase64String(user.Picture);
            }

            base.OnActionExecuting(filterContext);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }
    }

    public class ApiControllerBase : ApiController
    {
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;
        public IReviewStartupDataRepository Repository { get; set; }
        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? (_userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>()); }
            set { _userManager = value; }
        }

        public ApplicationSignInManager SignInManager
        {
            get { return _signInManager ?? (_signInManager = Request.GetOwinContext().Get<ApplicationSignInManager>()); }
            set { _signInManager = value; }
        }

        public ApiControllerBase()
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
       
        public ReviewsStartupDbContext Context { get; private set; }
       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}