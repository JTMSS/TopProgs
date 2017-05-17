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
    /// Controller for channels
    /// </summary>
    public class ChannelController : Controller
    {
        // GET: Channel
        /// <summary>
        /// Channel view/selection
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<Channel> lstChannels = null;
            List<SelectListItem> selChans;
            SelectedIDsAndItems vmSelChans = null;
            APIHelper apiHlp;
            SavedDataHelper sdHlp;
            string[] arrStr = null;
            string strErr = "";

            try
            {
                selChans = new List<SelectListItem>();
                vmSelChans = new SelectedIDsAndItems();
                apiHlp = new APIHelper();
                sdHlp = new SavedDataHelper();

                if (apiHlp.GetChannels(User.Identity.Name, 440, ref lstChannels, ref strErr) &&
                    lstChannels != null &&
                    lstChannels.Count > 0)
                {
                    foreach (Channel c in lstChannels)
                    {
                        selChans.Add(new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
                    }

                    if (sdHlp.LoadItem(User.Identity.Name, null, null, "Channel", ref arrStr, ref strErr))
                    {
                        vmSelChans.SelectedItemIds = arrStr;
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Could not retrieve channels");
                }

                vmSelChans.Items = selChans;
            }
            catch (Exception exc)
            {
                ModelState.AddModelError("", "Unexpected error:" + exc.Message);
            }

            return View(vmSelChans);
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
                    if (sdHlp.SaveItem(User.Identity.Name, null, null, "Channel", SelectedItemIds, ref strErr))
                    {
                        return new RedirectResult("~/Date");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Please select at least one channel");
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
            return new RedirectResult("~/Region");
        }
    }
}