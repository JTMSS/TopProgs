using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TopProgs.Models;

namespace TopProgs.Helpers
{
    internal class TopProgsHelper
    {
        public bool Calculate(string vstrUser,
                              ref Result rclsTPRes, 
                              ref string rstrErr)
        {
            SavedDataHelper sdHlp = null;
            string[] arrChanID = null;
            string[] arrRegID = null;
            string[] arrDate = null;
            string[] arrTargID = null;
            TPFilter clsFilt = null;
            BLTopProgsReport clsTPRep = null;
            bool blnOk = false;

            try
            {
                sdHlp = new SavedDataHelper();

                if (sdHlp.LoadItem(vstrUser, null, null, "Region", ref arrRegID, ref rstrErr))
                {
                    if (sdHlp.LoadItem(vstrUser, null, null, "Channel", ref arrChanID, ref rstrErr))
                    {
                        if (sdHlp.LoadItem(vstrUser, null, null, "Date", ref arrDate, ref rstrErr))
                        {
                            if (sdHlp.LoadItem(vstrUser, null, null, "Target", ref arrTargID, ref rstrErr))
                            {
                                if (sdHlp.LoadItem(vstrUser, null, null, "TPFilter", ref clsFilt, ref rstrErr))
                                {
                                    if (CalcTPRep(vstrUser, arrRegID, arrChanID, arrDate, arrTargID, clsFilt, ref clsTPRep, ref rstrErr))
                                    {
                                        blnOk = BuildReport(clsTPRep, ref rclsTPRes, ref rstrErr);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                rstrErr = "Unexpected error:" + exc.Message;
            }

            return blnOk;
        }

        private bool BuildReport(BLTopProgsReport vclsTPRep, 
                                 ref Result rclsTPResult, 
                                 ref string rstrErr)
        {
            ResultItem clsRow = null;
            List<double> lstFigs = null;
            bool blnOk = false;

            try
            {
                rclsTPResult = new Result();

                rclsTPResult.Headers.Add("Title");
                rclsTPResult.Headers.Add("Chan");
                rclsTPResult.Headers.Add("Date");
                rclsTPResult.Headers.Add("Time");

                foreach(string name in vclsTPRep.Targets)
                {
                    rclsTPResult.TargetNames.Add(name);
                }

                rclsTPResult.MetricNames.Add("GRP");
                rclsTPResult.MetricNames.Add("Thou");

                rclsTPResult.Region = String.Join(",", vclsTPRep.Regions);

                foreach (BLEventMetricResult er in vclsTPRep.EventResults)
                {
                    clsRow = new ResultItem();

                    clsRow.Title = er.Evnt.Name;
                    clsRow.Chan = er.Evnt.Channel.Name;
                    clsRow.TheDate = er.Evnt.Date_Start.Value.Date;
                    clsRow.Time = er.Evnt.Date_Start.Value.Hour * 100 + er.Evnt.Date_Start.Value.Minute;

                    // Loop targets
                    foreach (List<BLMetricResult> mr in er.Results)
                    {
                        lstFigs = new List<double>();

                        // Loop metrics
                        foreach (BLMetricResult x in mr)
                        {
                            lstFigs.Add(x.Result);
                        }
                        clsRow.Figures.Add(lstFigs);
                    }

                    rclsTPResult.Items.Add(clsRow);
                }

                blnOk = true;
            }
            catch (Exception exc)
            {
                rstrErr = "Unexpected error:" + exc.Message;
            }

            return blnOk;
        }

        private bool CalcTPRep(string vstrUser, 
                               string[] varrRegID, 
                               string[] varrChanID, 
                               string[] varrDate, 
                               string[] varrTarg, 
                               TPFilter vclsFilt, 
                               ref BLTopProgsReport rclsTPRep, 
                               ref string rstrErr)
        {
            TopProgsReportCall clsTPCall = null;
            APIHelper apiHlp = null;
            bool blnOk = false;

            try
            {
                clsTPCall = new TopProgsReportCall();

                foreach (string x in varrRegID)
                {
                    clsTPCall.RegionIDs.Add(Convert.ToInt32(x));
                }

                foreach (string x in varrChanID)
                {
                    clsTPCall.ChannelIDs.Add(Convert.ToInt32(x));
                }

                clsTPCall.EventTypeIDs.Add(2);

                clsTPCall.TimeFrom = vclsFilt.StartTime;
                clsTPCall.TimeTo = vclsFilt.EndTime;
                clsTPCall.InclusiveTimes = vclsFilt.TimeInclusive;

                clsTPCall.SearchTitle = vclsFilt.SearchTitle;

                foreach (string x in vclsFilt.DaysOfWk)
                {
                    clsTPCall.DaysOfWeek.Add(x);
                }

                clsTPCall.Dates.Add(DateTime.FromBinary(Convert.ToInt64(varrDate.First())));
                clsTPCall.Dates.Add(DateTime.FromBinary(Convert.ToInt64(varrDate.Last())));

                foreach (string x in varrTarg)
                {
                    clsTPCall.TargetIDs.Add(Convert.ToInt32(x));
                }

                clsTPCall.Metrics.Add(BLMETRICTYPE.GRP);
                clsTPCall.Metrics.Add(BLMETRICTYPE.Thousands);

                apiHlp = new APIHelper();
                if (apiHlp.PostTPReport(vstrUser, clsTPCall, ref rclsTPRep, ref rstrErr))
                {
                    if (rclsTPRep != null && rclsTPRep.EventResults != null && rclsTPRep.EventResults.Count > 0)
                    {
                        blnOk = true;
                    }
                    else
                    {
                        rstrErr = "No events found. Please change your selections.";
                    }
                }
            }
            catch (Exception exc)
            {
                rstrErr = "Unexpected error:" + exc.Message;
            }

            return blnOk;
        }
    }
}