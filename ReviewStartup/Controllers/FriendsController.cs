using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ReviewStartup.Infrastructure.Entities;

namespace ReviewStartup.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class FriendsController : ControllerBase
    {
        [Route("Friends/AcceptRequest/{userName}")]
        public ActionResult AcceptRequest(string userName)
        {
            var user = UserManager.Users.Include(e => e.FriendRequests).Include(e => e.Friends).SingleOrDefault(e => e.UserName == User.Identity.Name);

            if (user?.FriendRequests != null && user.FriendRequests.Any(e => e.UserName == userName))
            {
                var friend = UserManager.Users.Include(e => e.Friends).SingleOrDefault(e => e.UserName == userName);
                if (friend == null)
                {
                    return new HttpNotFoundResult();
                }
                user.FriendRequests.Remove(friend);
                if (user.Friends == null)
                {
                    user.Friends = new List<User>();
                }
                if (friend.Friends == null)
                {
                    friend.Friends = new List<User>();
                }
                user.Friends.Add(friend);
                friend.Friends.Add(user);
                UserManager.Update(user);
                UserManager.Update(friend);

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        [Route("Friends/RejectRequest/{userName}")]

        public ActionResult RejectRequest(string userName)
        {
            var user = UserManager.Users.Include(e => e.FriendRequests).SingleOrDefault(e => e.UserName == User.Identity.Name);

            if (user?.FriendRequests != null && user.FriendRequests.Any(e => e.UserName == userName))
            {
                var friend = user.FriendRequests.SingleOrDefault(e => e.UserName == userName);
                user.FriendRequests.Remove(friend);
                UserManager.Update(user);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        [Authorize(Roles = "Admin, User")]
        [Route("Friends/SendRequest/{userName}")]

        public ActionResult SendRequest(string userName)
        {
            if (userName == User.Identity.Name)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = UserManager.Users.Include(e => e.FriendRequests).Include(e => e.Friends).SingleOrDefault(e => e.UserName == userName);

            if (user?.FriendRequests != null && user.FriendRequests.All(e => e.UserName != User.Identity.Name) && user.Friends != null && user.Friends.All(e => e.UserName != User.Identity.Name))
            {
                var friend = UserManager.FindByName(User.Identity.Name);
                user.FriendRequests.Add(friend);

                UserManager.Update(user);

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        [Authorize(Roles = "Admin, User")]
        [Route("Friends/RemoveFriend/{userName}")]

        public ActionResult RemoveFriend(string userName)
        {

            var user = UserManager.Users.Include(e => e.FriendRequests).Include(e => e.Friends).SingleOrDefault(e => e.UserName == User.Identity.Name);

            if (user?.Friends != null && user.Friends.Any(e => e.UserName == userName))
            {
                var friend = UserManager.Users.Include(e => e.Friends).SingleOrDefault(e => e.UserName == userName);
                if (friend == null)
                {
                    return new HttpNotFoundResult();
                }
                user.Friends.Remove(friend);
                friend.Friends.Remove(user);
                UserManager.Update(user);
                UserManager.Update(friend);

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        [Authorize(Roles = "Admin, User")]
        [Route("Friends/CancelRequest/{userName}")]
        public ActionResult CancelRequest(string userName)
        {
            var user = UserManager.Users.Include(e => e.FriendRequests).SingleOrDefault(e => e.UserName == userName);

            if (user?.FriendRequests != null && user.FriendRequests.Any(e => e.UserName == User.Identity.Name))
            {
                var friend = user.FriendRequests.SingleOrDefault(e => e.UserName == User.Identity.Name);
                user.FriendRequests.Remove(friend);
                UserManager.Update(user);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}