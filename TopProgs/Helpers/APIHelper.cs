using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using TopProgs.Models;

namespace TopProgs.Helpers
{
    internal class APIHelper
    {
        public static string BASE_URL = "https://uk.metrics.global"; // "http://localhost:58968";
        static string ENDPOINT_VAXI_RF_CHANNELS = BASE_URL + "/api/Entity/Channels";
        static string ENDPOINT_VAXI_RF_REGIONS = BASE_URL + "/api/Entity/Regions";
        static string ENDPOINT_VAXI_RF_DATES = BASE_URL + "/api/Entity/AvailableDates";
        static string ENDPOINT_VAXI_RF_EVENTS = BASE_URL + "/api/Entity/EventInfos";
        static string ENDPOINT_VAXI_RF_TARGETS = BASE_URL + "/api/Entity/Targets";
        static string ENDPOINT_VAXI_RF_A_TARGET = BASE_URL + "/api/Entity/Targets/5";
        static string ENDPOINT_VAXI_RF_DATASET = BASE_URL + "/api/Entity/DataSet";
        static string ENDPOINT_VAXI_RF_INVENTORY = BASE_URL + "/api/Inventory";
        static string ENDPOINT_VAXI_RF_INV_EVENTMETRIC = BASE_URL + "/api/Inventory/EventMetrics";
        static string ENDPOINT_VAXI_RF_REP_TOPPROGS = BASE_URL + "/api/Report/TopProgs";

        public bool GetEntityList<T>(string vstrEndPoint,
                                     string vstrUser,
                                     int vintDS,
                                     ref List<T> rlstItems,
                                     ref string rstrErr,
                                     string vstrOtherParams = "")
        {
            JavaScriptSerializer jss;
            string strToken = "";
            string strResult = "";
            string strURL;
            bool blnOk = false;

            try
            {
                if (RedisHelper.GetIt("TP-" + vstrUser, ref strToken))
                {
                    strURL = vstrEndPoint + "?dsID=" + HttpUtility.UrlEncode(vintDS.ToString()) + vstrOtherParams;
                    strURL = HttpUtility.UrlPathEncode(strURL);

                    if (WebCallHelper.GetCall(strURL, ref strResult, strToken) &&
                        !string.IsNullOrWhiteSpace(strResult))
                    {
                        jss = new JavaScriptSerializer();
                        rlstItems = jss.Deserialize<List<T>>(strResult);
                        blnOk = true;
                    }
                    else
                    {
                        rstrErr = "Cannot retrieve items";
                    }
                }
                else
                {
                    rstrErr = "Missing second stage authentication";
                }
            }
            catch (Exception exc)
            {
                rstrErr = exc.Message;
                rlstItems = null;
            }

            return blnOk;
        }
        public bool GetEntity<T>(string vstrEndPoint,
                                 string vstrUser,
                                 int vintDS,
                                 ref T rclsItem,
                                 ref string rstrErr,
                                 string vstrOtherParams = "")
        {
            JavaScriptSerializer jss;
            string strToken = "";
            string strResult = "";
            string strURL;
            bool blnOk = false;

            try
            {
                if (RedisHelper.GetIt("TP-" + vstrUser, ref strToken))
                {
                    strURL = vstrEndPoint + "?dsID=" + HttpUtility.UrlEncode(vintDS.ToString()) + vstrOtherParams;
                    strURL = HttpUtility.UrlPathEncode(strURL);

                    if (WebCallHelper.GetCall(strURL, ref strResult, strToken) &&
                        !string.IsNullOrWhiteSpace(strResult))
                    {
                        jss = new JavaScriptSerializer();
                        rclsItem = jss.Deserialize<T>(strResult);
                        blnOk = true;
                    }
                    else
                    {
                        rstrErr = "Cannot retrieve item";
                    }
                }
                else
                {
                    rstrErr = "Missing second stage authentication";
                }
            }
            catch (Exception exc)
            {
                rstrErr = exc.Message;
                rclsItem = default(T);
            }

            return blnOk;
        }

        public bool GetChannels(string vstrUser, 
                                int vintDS, 
                                ref List<Channel> rlstChans,
                                ref string rstrErr)
        {
            return GetEntityList(ENDPOINT_VAXI_RF_CHANNELS, vstrUser, vintDS, ref rlstChans, ref rstrErr);
        }

        public bool GetRegions(string vstrUser,
                               int vintDS,
                               ref List<Region> rlstRegs,
                               ref string rstrErr)
        {
            return GetEntityList(ENDPOINT_VAXI_RF_REGIONS, vstrUser, vintDS, ref rlstRegs, ref rstrErr);
        }

        public bool GetDates(string vstrUser,
                             int vintDS,
                             ref List<AvailableDate> rlstDates,
                             ref string rstrErr)
        {
            return GetEntityList(ENDPOINT_VAXI_RF_DATES, vstrUser, vintDS, ref rlstDates, ref rstrErr);
        }

        public bool GetTargets(string vstrUser,
                               int vintDS,
                               ref List<Target> rlstTargs,
                               ref string rstrErr)
        {
            return GetEntityList(ENDPOINT_VAXI_RF_TARGETS, 
                                 vstrUser, 
                                 vintDS, 
                                 ref rlstTargs, 
                                 ref rstrErr, 
                                 "&mode=" + HttpUtility.UrlEncode("3"));
        }

        public bool GetTarget(string vstrUser,
                              int vintDS,
                              int vintTargID,
                              ref Target rclsTarg,
                              ref string rstrErr)
        {
            return GetEntity(ENDPOINT_VAXI_RF_A_TARGET,
                             vstrUser,
                             vintDS,
                             ref rclsTarg,
                             ref rstrErr,
                             "&id=" + HttpUtility.UrlEncode(vintTargID.ToString()));
        }

        public bool GetDataSet(string vstrUser,
                               int vintDS,
                               ref DataSetInfo rclsDSInf,
                               ref string rstrErr)
        {
            return GetEntity(ENDPOINT_VAXI_RF_DATASET, vstrUser, vintDS, ref rclsDSInf, ref rstrErr);
        }

        public bool PostInfo<T1,T2>(string vstrEndPoint,
                                    string vstrUser,
                                    T1 vclsPostBody,
                                    ref T2 rclsInfoOut,
                                    ref string rstrErr)
        {
            JavaScriptSerializer jss;
            string strToken = "";
            string strResult = "";
            string strPostData;
            string strURL;
            bool blnOk = false;

            try
            {
                if (RedisHelper.GetIt("TP-" + vstrUser, ref strToken))
                {
                    strURL = HttpUtility.UrlPathEncode(vstrEndPoint);

                    jss = new JavaScriptSerializer();
                    strPostData = jss.Serialize(vclsPostBody);

                    if (WebCallHelper.PostCall(strURL, strPostData, ref strResult, strToken) &&
                        !string.IsNullOrWhiteSpace(strResult))
                    {
                        rclsInfoOut = jss.Deserialize<T2>(strResult);
                        blnOk = true;
                    }
                    else
                    {
                        rstrErr = "Cannot retrieve events:" + strResult;
                    }
                }
                else
                {
                    rstrErr = "Missing second stage authentication";
                }
            }
            catch (Exception exc)
            {
                rstrErr = exc.Message;
                rclsInfoOut = default(T2);
            }

            return blnOk;
        }

        public bool PostEventInfo(string vstrUser,
                                  BLEventFilters vclsEvntFilt,
                                  ref List<EventInfo> rlstEvents,
                                  ref string rstrErr)
        {
            return PostInfo(ENDPOINT_VAXI_RF_EVENTS, vstrUser, vclsEvntFilt, ref rlstEvents, ref rstrErr);
        }

        public bool PostInventory(string vstrUser,
                                  InventoryCall vclsInvCall,
                                  ref string rstrInvKey,
                                  ref string rstrErr)
        {
            List<string> lstResult = null;
            bool blnOk = false;

            if (PostInfo(ENDPOINT_VAXI_RF_INVENTORY, vstrUser, vclsInvCall, ref lstResult, ref rstrErr))
            {
                if (lstResult != null && lstResult.Count > 0)
                {
                    rstrInvKey = lstResult[0];
                    blnOk = true;
                }
            }

            return blnOk;
        }

        public bool PostInvEventMetric(string vstrUser,
                                       InvEventMetricCall vclsEMCall,
                                       ref List<BLEventIDResult> rlstEvntMetric,
                                       ref string rstrErr)
        {
            return PostInfo(ENDPOINT_VAXI_RF_INV_EVENTMETRIC, vstrUser, vclsEMCall, ref rlstEvntMetric, ref rstrErr);
        }

        public bool PostTPReport(string vstrUser,
                                TopProgsReportCall vclsTPCall,
                                ref BLTopProgsReport rlstEventRes,
                                ref string rstrErr)
        {
            List<BLTopProgsReport> lstResult = null;
            bool blnOk = false;

            if (PostInfo(ENDPOINT_VAXI_RF_REP_TOPPROGS, vstrUser, vclsTPCall, ref lstResult, ref rstrErr))
            {
                if (lstResult != null && lstResult.Count > 0)
                {
                    rlstEventRes = lstResult[0];
                    blnOk = true;
                }
            }

            return blnOk;
        }
    }
}