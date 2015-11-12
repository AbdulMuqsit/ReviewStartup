using System;
using System.Collections.Generic;
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
            var user = await UserManager.FindByNameAsync(userName);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }
    }
}