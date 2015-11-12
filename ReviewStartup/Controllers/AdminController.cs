using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using ReviewStartup.Infrastructure.Entities;
using ReviewStartup.Models;

namespace ReviewStartup.Controllers
{
    public class AdminController : ControllerBase
    {
        [AllowAnonymous]
        public ActionResult Register()
        {

            if (!Request.IsLocal)
            {
                return new HttpUnauthorizedResult("Not Authorized");
            }
            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {

            if (!Request.IsLocal)
            {
                return new HttpUnauthorizedResult("Not Authorized");
            }
            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.UserName, Email = model.Email };
                if (model.Picture != null)
                {
                    using (var stream = model.Picture.InputStream)
                    {
                        var memoryStream = stream as MemoryStream;

                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            await stream.CopyToAsync(memoryStream);
                        }
                        user.Picture = memoryStream.ToArray();
                    }
                }
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Role, "Admin"));
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);


                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            return View(model);
        }


    }
}