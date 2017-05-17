using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TopProgs.Helpers;
using TopProgs.Models;

namespace TopProgs.Controllers
{
    public class ResultController : Controller
    {
        // GET: Result
        public ActionResult Index()
        {
            TopProgsHelper hlpTP;
            Result clsRes = null;
            string strErr = "";

            try
            {
                hlpTP = new TopProgsHelper();
                clsRes = new Result();

                if (!hlpTP.Calculate(User.Identity.Name, ref clsRes, ref strErr))
                {
                    ModelState.AddModelError("", strErr);
                }
            }
            catch (Exception exc)
            {
                ModelState.AddModelError("", "Unexpected error:" + exc.Message);
            }

            return View(clsRes);
        }

        public ActionResult Previous()
        {
            return new RedirectResult("~/Filter");
        }

        public ActionResult StartAgain()
        {
            return new RedirectResult("~/Region");
        }
    }
}