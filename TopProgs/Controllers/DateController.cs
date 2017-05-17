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
    /// <summary>
    /// Controller for dates
    /// </summary>
    public class DateController : Controller
    {
        // GET: Date
        /// <summary>
        /// Dates view/selection
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<AvailableDate> lstDates = null;
            List<SelectListItem> selDates;
            SelectedIDsAndItems vmSelDates = null;
            APIHelper apiHlp;
            SavedDataHelper sdHlp;
            string[] arrStr = null;
            string strErr = "";

            try
            {
                selDates = new List<SelectListItem>();
                vmSelDates = new SelectedIDsAndItems();
                apiHlp = new APIHelper();
                sdHlp = new SavedDataHelper();

                if (apiHlp.GetDates(User.Identity.Name, 440, ref lstDates, ref strErr) &&
                    lstDates != null &&
                    lstDates.Count > 0)
                {
                    foreach (AvailableDate ad in lstDates)
                    {
                        for (DateTime d = ad.StartDate; d <= ad.EndDate; d = d.AddDays(1))
                        {
                            selDates.Add(new SelectListItem { Text = d.ToShortDateString(), Value = d.ToBinary().ToString() });
                        }
                    }

                    if (sdHlp.LoadItem(User.Identity.Name, null, null, "Date", ref arrStr, ref strErr))
                    {
                        vmSelDates.SelectedItemIds = arrStr;
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Could not retrieve dates");
                }

                vmSelDates.Items = selDates;
            }
            catch (Exception exc)
            {
                ModelState.AddModelError("", "Unexpected error:" + exc.Message);
            }

            return View(vmSelDates);
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
                    if (sdHlp.SaveItem(User.Identity.Name, null, null, "Date", SelectedItemIds, ref strErr))
                    {
                        return new RedirectResult("~/Target");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Please select at least one date");
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
            return new RedirectResult("~/Channel");
        }
    }
}