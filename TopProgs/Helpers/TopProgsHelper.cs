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
            string[] arrTarg = null;
            TPFilter clsFilt = null;
            BLEventFilters clsEvntFilt = null;
            BLInventoryInfo clsInvInfo = null;
            List<int> lstEventIDs = null;
            List<EventInfo> lstEvents = null;
            List<BLEventIDResult> lstEventMetric = null;
            string strInvKey = "";
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
                            if (sdHlp.LoadItem(vstrUser, null, null, "Target", ref arrTarg, ref rstrErr))
                            {
                                if (sdHlp.LoadItem(vstrUser, null, null, "TPFilter", ref clsFilt, ref rstrErr))
                                {
                                    sdHlp.LoadItem(vstrUser, null, null, "TPInvKey", ref strInvKey, ref rstrErr);

                                    if (BuildEventFilter(arrRegID, arrChanID, arrDate, arrTarg, clsFilt, ref clsEvntFilt, ref rstrErr))
                                    {
                                        if (GetEvents(vstrUser, clsEvntFilt, ref lstEvents, ref lstEventIDs, ref rstrErr))
                                        {
                                            if (BuildInventoryInfo(vstrUser, arrRegID, arrDate, arrTarg, lstEventIDs, ref clsInvInfo, ref rstrErr))
                                            {
                                                if (BuildInventory(vstrUser, clsInvInfo, clsEvntFilt, ref strInvKey, ref rstrErr))
                                                {
                                                    sdHlp.SaveItem(vstrUser, "", "", "TPInvKey", strInvKey, ref rstrErr);

                                                    if (CalcMetrics(vstrUser, strInvKey, ref lstEventMetric, ref rstrErr))
                                                    {
                                                        blnOk = BuildReport(lstEvents, lstEventMetric, clsInvInfo, ref rclsTPRes, ref rstrErr);
                                                    }
                                                }
                                            }
                                        }
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

        private bool BuildReport(List<EventInfo> vlstEvents, 
                                 List<BLEventIDResult> vlstEventMetric, 
                                 BLInventoryInfo vclsInvInfo,
                                 ref Result rclsTPResult, 
                                 ref string rstrErr)
        {
            ResultItem clsRow = null;
            BLEventIDResult clsER = null;
            List<double> lstFigs = null;
            List<string> lstRegNames = null;
            EventInfo clsEI;
            bool blnOk = false;

            try
            {
                rclsTPResult = new Result();

                rclsTPResult.Headers.Add("Title");
                rclsTPResult.Headers.Add("Chan");
                rclsTPResult.Headers.Add("Date");
                rclsTPResult.Headers.Add("Time");

                rclsTPResult.TargetNames.Add(vclsInvInfo.TargetName);

                rclsTPResult.MetricNames.Add("GRP");
                rclsTPResult.MetricNames.Add("Thou");

                lstRegNames = new List<string>();
                foreach (int regID in vclsInvInfo.RegIDs)
                {
                    clsEI = vlstEvents.Find(x => x.Region.Id == regID);
                    if (clsEI != null)
                    {
                        lstRegNames.Add(clsEI.Region.Name);
                    }
                }
                rclsTPResult.Region = String.Join(",", lstRegNames);

                foreach (EventInfo ei in vlstEvents)
                {
                    clsRow = new ResultItem();

                    clsRow.Title = ei.Name;
                    clsRow.Chan = ei.Channel.Name;
                    clsRow.TheDate = ei.Date_Start.Value.Date;
                    clsRow.Time = ei.Date_Start.Value.Hour * 100 + ei.Date_Start.Value.Minute;

                    lstFigs = new List<double>();
                    clsER = vlstEventMetric.Find(x => x.EventID == ei.Id);
                    if (clsER != null)
                    {
                        foreach (var x in clsER.Results)
                        {
                            lstFigs.Add(x.Result);
                        }
                    }
                    else
                    {
                        foreach(var x in vlstEventMetric[0].Results)
                        {
                            lstFigs.Add(0);
                        }
                    }
                    clsRow.Figures.Add(lstFigs);

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

        private bool CalcMetrics(string vstrUser, 
                                 string vstrInvKey, 
                                 ref List<BLEventIDResult> rlstEventMetric, 
                                 ref string rstrErr)
        {
            InvEventMetricCall clsEMCall = null;
            APIHelper apiHlp = null;
            bool blnOk = false;

            try
            {
                clsEMCall = new InvEventMetricCall();
                clsEMCall.InvKey = vstrInvKey;
                clsEMCall.Metrics.Add(BLMETRICTYPE.GRP);
                clsEMCall.Metrics.Add(BLMETRICTYPE.Thousands);

                apiHlp = new APIHelper();
                blnOk = apiHlp.PostInvEventMetric(vstrUser, clsEMCall, ref rlstEventMetric, ref rstrErr);
            }
            catch (Exception exc)
            {
                rstrErr = "Unexpected error:" + exc.Message;
            }

            return blnOk;
        }

        private bool BuildInventory(string vstrUser, 
                                    BLInventoryInfo vclsInvInfo, 
                                    BLEventFilters vclsEvntFilt, 
                                    ref string rstrInvKey, 
                                    ref string rstrErr)
        {
            InventoryCall clsInvCall = null;
            APIHelper apiHlp = null;
            bool blnOk = false;

            try
            {
                clsInvCall = new InventoryCall();
                clsInvCall.InvInfo = vclsInvInfo;
                clsInvCall.Filter = vclsEvntFilt;

                apiHlp = new APIHelper();
                blnOk = apiHlp.PostInventory(vstrUser, clsInvCall, ref rstrInvKey, ref rstrErr);
            }
            catch (Exception exc)
            {
                rstrErr = "Unexpected error:" + exc.Message;
            }

            return blnOk;
        }

        private bool BuildInventoryInfo(string vstrUser, 
                                        string[] varrRegID, 
                                        string[] varrDate, 
                                        string[] varrTarg, 
                                        List<int> vlstEventIDs,
                                        ref BLInventoryInfo rclsInvInfo, 
                                        ref string rstrErr)
        {
            APIHelper apiHlp = null;
            List<Target> lstTarg = null;
            Target clsTarg = null;
            int intTargID;
            bool blnOk = false;

            try
            {
                intTargID = Convert.ToInt32(varrTarg.First());

                apiHlp = new APIHelper();
                if (apiHlp.GetTargets(vstrUser, 440, ref lstTarg, ref rstrErr))
                {
                    clsTarg = lstTarg.Find(x => x.Id == intTargID);
                    if (clsTarg != null)
                    {
                        rclsInvInfo = new BLInventoryInfo();
                        rclsInvInfo.Name = "TP Temp";

                        rclsInvInfo.EventIDs = vlstEventIDs;

                        foreach (string x in varrRegID)
                        {
                            rclsInvInfo.RegIDs.Add(Convert.ToInt32(x));
                        }

                        rclsInvInfo.DemoDate = DateTime.FromBinary(Convert.ToInt64(varrDate.First()));
                        rclsInvInfo.WgtDate = rclsInvInfo.DemoDate;

                        rclsInvInfo.IncludeIndsAndHomes = false;
                        rclsInvInfo.IsHome = clsTarg.Is_Home;
                        rclsInvInfo.TargetName = clsTarg.Name;

                        foreach (Target_DemoKey x in clsTarg.Target_DemoKey)
                        {
                            rclsInvInfo.DemoKeys.Add(x.DemoKey);
                        }

                        blnOk = true;
                    }
                    else
                    {
                        rstrErr = "Could not retrieve target";
                    }
                }
                else
                {
                    rstrErr = "Could not retrieve target list";
                }
            }
            catch (Exception exc)
            {
                rstrErr = "Unexpected error:" + exc.Message;
            }

            return blnOk;
        }

        private bool GetEvents(string vstrUser, 
                               BLEventFilters vclsEvntFilt, 
                               ref List<EventInfo> rlstEvents,
                               ref List<int> rlstEventIDs, 
                               ref string rstrErr)
        {
            APIHelper apiHlp = null;
            bool blnOk = false;

            try
            {
                rlstEventIDs = new List<int>();

                apiHlp = new APIHelper();
                if (apiHlp.PostEventInfo(vstrUser, vclsEvntFilt, ref rlstEvents, ref rstrErr) &&
                    rlstEvents != null &&
                    rlstEvents.Count > 0)
                {
                    foreach (EventInfo x in rlstEvents)
                    {
                        rlstEventIDs.Add(x.Id);
                    }

                    blnOk = true;
                }
                else
                {
                    rstrErr = "No events found. Please change your selections.";
                }
            }
            catch (Exception exc)
            {
                rstrErr = "Unexpected error:" + exc.Message;
            }

            return blnOk;
        }

        private bool BuildEventFilter(string[] varrRegID, 
                                      string[] varrChanID, 
                                      string[] varrDate, 
                                      string[] varrTarg, 
                                      TPFilter vclsFilt, 
                                      ref BLEventFilters rclsEvntFilt, 
                                      ref string rstrErr)
        {
            bool blnOk = false;
 
            try
            {
                rclsEvntFilt = new BLEventFilters();

                foreach (string x in varrRegID)
                {
                    rclsEvntFilt.RegionIDs.Add(Convert.ToInt32(x));
                }

                foreach (string x in varrChanID)
                {
                    rclsEvntFilt.ChannelIDs.Add(Convert.ToInt32(x));
                }

                rclsEvntFilt.EventTypeIDs.Add(2);

                rclsEvntFilt.TimeFrom = vclsFilt.StartTime;
                rclsEvntFilt.TimeTo = vclsFilt.EndTime;
                rclsEvntFilt.InclusiveTimes = vclsFilt.TimeInclusive;

                rclsEvntFilt.SearchTitle = vclsFilt.SearchTitle;

                //foreach (string x in vclsFilt.DaysOfWk)
                //{
                //    rclsEvntFilt.DaysOfWeek.Add(x);
                //}

                rclsEvntFilt.DateFrom = DateTime.FromBinary(Convert.ToInt64(varrDate.First()));
                rclsEvntFilt.DateTo = DateTime.FromBinary(Convert.ToInt64(varrDate.Last()));

                blnOk = true;
            }
            catch (Exception exc)
            {
                rstrErr = "Unexpected error:" + exc.Message;
            }

            return blnOk;
        }
    }
}