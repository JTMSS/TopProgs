using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TopProgs.Models;

namespace TopProgs.Controllers
{
    public class ChannelController : Controller
    {
        // GET: Channel
        public ActionResult Index()
        {
            //if (!HttpContext.User.Identity.IsAuthenticated)
            //{
            //    return RedirectToAction("Login", "Account");
            //}
            //else
            //{
                var chans = new List<Channel> { new Channel { Name = "Channel 1", Abbrv = "Ch1", ID = 1 },
                                                new Channel { Name = "Channel 2", Abbrv = "Ch2", ID = 2 } };

                return View(chans);
            //}
        }
    }
}