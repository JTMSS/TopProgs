using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TopProgs.Helpers;
using TopProgs.Models;
using TopProgs.ViewModels;

namespace TopProgs.Controllers
{
    public class TargetController : Controller
    {
        // GET: Target
        public ActionResult Index()
        {
            List<Target> lstTargs = null;
            List<SelectListItem> selTargs;
            SelectedIDsAndItems vmSelTargs = null;
            APIHelper apiHlp;
            SavedDataHelper sdHlp;
            string[] arrStr = null;
            string strErr = "";

            try
            {
                selTargs = new List<SelectListItem>();
                vmSelTargs = new SelectedIDsAndItems();
                apiHlp = new APIHelper();
                sdHlp = new SavedDataHelper();

                if (apiHlp.GetTargets(User.Identity.Name, 440, ref lstTargs, ref strErr) &&
                    lstTargs != null &&
                    lstTargs.Count > 0)
                {
                    foreach (Target t in lstTargs)
                    {
                        selTargs.Add(new SelectListItem { Text = t.Name, Value = t.Id.ToString() });
                    }

                    if (sdHlp.LoadItem(User.Identity.Name, null, null, "Target", ref arrStr, ref strErr))
                    {
                        vmSelTargs.SelectedItemIds = arrStr;
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Could not retrieve targets");
                }

                vmSelTargs.Items = selTargs;
            }
            catch (Exception exc)
            {
                ModelState.AddModelError("", "Unexpected error:" + exc.Message);
            }

            return View(vmSelTargs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string[] SelectedItemIds)
        {
            SavedDataHelper sdHlp;
            string strErr = "";

            try
            {
                if (SelectedItemIds != null && SelectedItemIds.Count() > 0)
                {
                    sdHlp = new SavedDataHelper();
                    if (sdHlp.SaveItem(User.Identity.Name, null, null, "Target", SelectedItemIds, ref strErr))
                    {
                        return new RedirectResult("~/Filter");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Please select at least one target");
                }
            }
            catch (Exception exc)
            {
                ModelState.AddModelError("", "Unexpected error:" + exc.Message);
            }

            return Index();
        }

        public ActionResult Previous()
        {
            return new RedirectResult("~/Date");
        }
    }
}