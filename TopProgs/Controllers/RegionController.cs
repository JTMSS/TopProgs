using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TopProgs.Models;
using TopProgs.ViewModels;
using TopProgs.Helpers;

namespace TopProgs.Controllers
{
    /// <summary>
    /// Controller for regions
    /// </summary>
    public class RegionController : Controller
    {
        // GET: Region
        /// <summary>
        /// Region view/selection
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<Region> lstRegions = null;
            List<SelectListItem> selRegs;
            SelectedIDsAndItems vmSelRegs = null;
            APIHelper apiHlp;
            SavedDataHelper sdHlp;
            string[] arrStr = null;
            string strErr = "";

            try
            {
                selRegs = new List<SelectListItem>();
                vmSelRegs = new SelectedIDsAndItems();
                apiHlp = new APIHelper();
                sdHlp = new SavedDataHelper();

                if (apiHlp.GetRegions(User.Identity.Name, 440, ref lstRegions, ref strErr) &&
                    lstRegions != null &&
                    lstRegions.Count > 0)
                {
                    foreach (Region r in lstRegions)
                    {
                        selRegs.Add(new SelectListItem { Text = r.Name, Value = r.Id.ToString() });
                    }

                    if (sdHlp.LoadItem(User.Identity.Name, null, null, "Region", ref arrStr, ref strErr))
                    {
                        vmSelRegs.SelectedItemIds = arrStr;
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Could not retrieve regions");
                }

                vmSelRegs.Items = selRegs;
            }
            catch (Exception exc)
            {
                ModelState.AddModelError("", "Unexpected error:" + exc.Message);
            }

            return View(vmSelRegs);
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
                    if (sdHlp.SaveItem(User.Identity.Name, null, null, "Region", SelectedItemIds, ref strErr))
                    {
                        return new RedirectResult("~/Channel");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Please select at least one region");
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
            return new RedirectResult("~/Home");
        }
    }
}