using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReviewStartup.Controllers
{
    public class ControlPanelController : ControllerBase
    {
        // GET: ControlPanel
        [Authorize(Roles = "Admin")]

        public ActionResult Index()
        {
            return View();
        }
    }
}