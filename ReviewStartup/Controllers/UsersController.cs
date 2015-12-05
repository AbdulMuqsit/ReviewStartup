using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ReviewStartup.Controllers
{
    public class UsersController : ControllerBase
    {
        // GET: Users
        [Route("Users/Details/{userName}")]
        public async Task<ActionResult> Details(string userName)
        {
            var user = await UserManager.Users.Include(e => e.Friends).Include(e => e.FriendRequests).SingleOrDefaultAsync(e => e.UserName == userName);
            if (user == null)
            {
                return HttpNotFound();
            }
            if (Request.IsAuthenticated)
            {
                ViewBag.FriendRequestReceived =
                       (await
                           UserManager.Users.Include(e => e.FriendRequests)
                               .SingleOrDefaultAsync(e => e.UserName == User.Identity.Name)).FriendRequests.Any(e => e.UserName == userName);
            }

            return View(user);
        }
    }
}