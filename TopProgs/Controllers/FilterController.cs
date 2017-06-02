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
    public class FilterController : Controller
    {
        // GET: Filter
        public ActionResult Index()
        {
            FilterView clsFiltVw = null;
            DataSetInfo clsDSInf = null;
            TPFilter clsFilt = null;
            List<SelectListItem> selDays;
            SelectedIDsAndItems vmSelDays = null;
            APIHelper apiHlp;
            SavedDataHelper sdHlp;
            string strErr = "";

            try
            {
                clsFiltVw = new FilterView();
                selDays = new List<SelectListItem>();
                vmSelDays = new SelectedIDsAndItems();
                apiHlp = new APIHelper();
                sdHlp = new SavedDataHelper();

                if (apiHlp.GetDataSet(User.Identity.Name, 440, ref clsDSInf, ref strErr))
                {
                    if (clsDSInf.DayName != null && clsDSInf.DayNameAbbrev != null)
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            selDays.Add(new SelectListItem { Text = clsDSInf.DayName[i], Value = clsDSInf.DayName[i] });
                        }
                    }

                    if (sdHlp.LoadItem(User.Identity.Name, null, null, "TPFilter", ref clsFilt, ref strErr))
                    {
                        clsFiltVw.Filter = clsFilt;
                    }
                    else
                    {
                        clsFilt = new TPFilter(clsDSInf.DayNameAbbrev);
                    }

                    vmSelDays.Items = selDays;
                    vmSelDays.SelectedItemIds = clsFilt.DaysOfWk;
                    clsFiltVw.DaysOfWeek = vmSelDays;
                    clsFiltVw.Filter = clsFilt;
                }
                else
                {
                    ModelState.AddModelError("", "Could not retrieve days of the week");
                }

                vmSelDays.Items = selDays;
                clsFiltVw.DaysOfWeek = vmSelDays;
            }
            catch (Exception exc)
            {
                ModelState.AddModelError("", "Unexpected error:" + exc.Message);
            }

            return View(clsFiltVw);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FilterView model)
        {
            DataSetInfo clsDSInf = null;
            APIHelper apiHlp;
            SavedDataHelper sdHlp;
            string strErr = "";
            bool blnOk = false;

            try
            {
                if (model != null && model.DaysOfWeek != null && model.Filter != null)
                {
                    if (model.DaysOfWeek.SelectedItemIds == null ||
                        model.DaysOfWeek.SelectedItemIds.Count() == 0)
                    {
                        strErr = "Please select at least one day";
                    }
                    else
                    {
                        if (model.Filter == null)
                        {
                            apiHlp = new APIHelper();
                            if (apiHlp.GetDataSet(User.Identity.Name, 440, ref clsDSInf, ref strErr) &&
                                clsDSInf != null &&
                                clsDSInf.DayNameAbbrev != null)
                            {
                                model.Filter = new TPFilter(clsDSInf.DayNameAbbrev);
                                blnOk = true;
                            }
                            else
                            {
                                ModelState.AddModelError("", "Could not retrieve days of the week");
                            }
                        }
                        else
                        {
                            blnOk = model.Filter.Validate(ref strErr);
                        }

                        if (blnOk)
                        {
                            model.Filter.DaysOfWk = model.DaysOfWeek.SelectedItemIds;

                            sdHlp = new SavedDataHelper();
                            if (sdHlp.SaveItem(User.Identity.Name, null, null, "TPFilter", model.Filter, ref strErr))
                            {
                                return new RedirectResult("~/Result");
                            }
                        }
                    }
                }
                else
                {
                    strErr = "Please enter some filters";
                }
            }
            catch (Exception exc)
            {
                strErr = "Unexpected error:" + exc.Message;
            }

            ModelState.AddModelError("", strErr);

            return View(model);
        }

        public ActionResult Previous()
        {
            return new RedirectResult("~/Target");
        }
    }
}