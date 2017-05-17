using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;

namespace TopProgs.Helpers
{
    internal class TokenHelper
    {
        public static bool GetToken(string vstrUser, string vstrPWord, ref string rstrToken, string vstrEndPt = "")
        {
            string ENDPOINT_TOKEN = APIHelper.BASE_URL + "/Token";
            string strResult = "";
            string strPostdata;
            JObject jobjThis;
            JToken jtSuccess;
            bool blnOk = false;

            try
            {
                rstrToken = "";

                if (!string.IsNullOrWhiteSpace(vstrEndPt)) ENDPOINT_TOKEN = vstrEndPt + "Token";

                strPostdata = "userName=" + vstrUser + "&password=" + vstrPWord + "&grant_type=password";
                if (WebCallHelper.PostCall(ENDPOINT_TOKEN, strPostdata, ref strResult))
                {
                    jobjThis = JObject.Parse(strResult);
                    jtSuccess = jobjThis["access_token"];
                    rstrToken = jtSuccess.ToString();
                    blnOk = true;
                }
            }
            catch (Exception exc)
            {
                rstrToken = exc.Message;
            }

            return blnOk;
        }

        public static bool GetAndSaveToken(string vstrUser, string vstrPWord, string vstrEndPt = "")
        {
            string strToken = "";
            bool blnOk;

            // Get the token for the API.
            if (blnOk = GetToken(vstrUser, vstrPWord, ref strToken, vstrEndPt))
            {
                // Success, so save it to Redis Cache.
                blnOk = RedisHelper.SetIt("TP-" + vstrUser, strToken);
            }

            return blnOk;
        }
    }
}