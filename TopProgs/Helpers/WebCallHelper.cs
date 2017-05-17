using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TopProgs.Helpers
{
    internal class WebCallHelper
    {
        /// <summary>
        /// Performs a POST operation (with access key if passed it as vstrToken)
        /// </summary>
        /// <param name="vstrEndpoint">URL</param>
        /// <param name="vstrPostdata">Post call data</param>
        /// <param name="rstrResult">Result if successful or error messaage if not</param>
        /// <param name="vstrToken">Bearer token if needed</param>
        /// <returns>True if successful, false if not</returns>
        public static bool PostCall(string vstrEndpoint,
                                    string vstrPostdata,
                                    ref string rstrResult,
                                    string vstrToken = "")
        {
            HttpWebRequest webReq;
            HttpWebResponse webResp;
            UTF8Encoding encThis;
            byte[] bytesPostData;
            bool blnOk = false;

            try
            {
                rstrResult = "";

                encThis = new UTF8Encoding();
                bytesPostData = encThis.GetBytes(vstrPostdata);

                webReq = (HttpWebRequest)HttpWebRequest.Create(vstrEndpoint);
                webReq.Method = "POST";
                webReq.ContentType = "application/json";
                webReq.ContentLength = bytesPostData.Length;

                if (!string.IsNullOrWhiteSpace(vstrToken))
                    webReq.Headers["Authorization"] = "Bearer " + vstrToken;

                using (Stream strm = webReq.GetRequestStream())
                {
                    strm.Write(bytesPostData, 0, bytesPostData.Length);
                }

                webResp = (HttpWebResponse)webReq.GetResponse();

                using (Stream stmThis = webResp.GetResponseStream())
                {
                    StreamReader rdrThis = new StreamReader(stmThis, Encoding.UTF8);
                    rstrResult = rdrThis.ReadToEnd();
                    rdrThis.Close();
                }

                webResp.Close();

                blnOk = true;
            }
            catch (Exception ex)
            {
                rstrResult = ex.Message;
            }

            return blnOk;
        }

        public static bool GetCall(string vstrEndpoint,
                                   ref string rstrResult,
                                   string vstrToken = "")
        {
            HttpWebRequest webReq;
            HttpWebResponse webResp;
            bool blnOk = false;

            try
            {
                rstrResult = "";

                webReq = (HttpWebRequest)HttpWebRequest.Create(vstrEndpoint);
                webReq.Method = "GET";

                if (!string.IsNullOrWhiteSpace(vstrToken))
                    webReq.Headers["Authorization"] = "Bearer " + vstrToken;

                webResp = (HttpWebResponse)webReq.GetResponse();

                using (Stream stmThis = webResp.GetResponseStream())
                {
                    StreamReader rdrThis = new StreamReader(stmThis, Encoding.UTF8);
                    rstrResult = rdrThis.ReadToEnd();
                    rdrThis.Close();
                }

                webResp.Close();

                blnOk = true;
            }
            catch (Exception ex)
            {
                rstrResult = ex.Message;
            }

            return blnOk;
        }
    }
}