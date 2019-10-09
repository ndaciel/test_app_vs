using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace Core.Manager
{
    public class PushNotificationFacade
    {
        private static string SendGCMNotification(string apiKey, string postData, string postDataContentType = "application/json")
        {
            // from here:
            // http://stackoverflow.com/questions/11431261/unauthorized-when-calling-google-gcm
            //
            // original:
            // http://www.codeproject.com/Articles/339162/Android-push-notification-implementation-using-ASP

            ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateServerCertificate);

            //
            //  MESSAGE CONTENT
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            //
            //  CREATE REQUEST
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            Request.Method = "POST";
            Request.KeepAlive = false;
            Request.ContentType = postDataContentType;
            Request.Headers.Add(string.Format("Authorization: key={0}", apiKey));
            Request.ContentLength = byteArray.Length;

            Stream dataStream = Request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            //
            //  SEND MESSAGE
            try
            {
                WebResponse Response = Request.GetResponse();
                HttpStatusCode ResponseCode = ((HttpWebResponse)Response).StatusCode;
                if (ResponseCode.Equals(HttpStatusCode.Unauthorized) || ResponseCode.Equals(HttpStatusCode.Forbidden))
                {
                    var text = "Unauthorized - need new token";

                }
                else if (!ResponseCode.Equals(HttpStatusCode.OK))
                {
                    var text = "Response from web service isn't OK";
                }

                StreamReader Reader = new StreamReader(Response.GetResponseStream());
                string responseLine = Reader.ReadToEnd();
                Reader.Close();

                return responseLine;
            }
            catch (Exception e)
            {

            }
            return "error";
        }

        public static bool ValidateServerCertificate(
                                                  object sender,
                                                  X509Certificate certificate,
                                                  X509Chain chain,
                                                  SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }



        public static string SendNotif(string apiKey, string json)
        {
            string BrowserAPIKey = apiKey;


            string tickerText = "example test GCM";
            string contentTitle = "content title GCM";
            //string postData = "{ \"registration_ids\": [ \"" + token + "\" ], \"data\": {\"tickerText\":\"" + tickerText + "\", \"contentTitle\":\"" + contentTitle + "\", \"message\": \"" + message + "\"}}";
            //            string test = @"{ ""notification"":{""title"":""" + title + @""",""text"":""" + text + @"""},""data"": {
            //                    ""score"": ""5x1"",
            //                    ""time"": ""15:10""
            //                  },""to"":"" dfVQFLYFiaM:APA91bEz7Mpa-RgTO7oBmYCg0UQhwDZhxMuxbclMGBs40fGGCctckubZZ53iz1_8IpNYNhWPodmbhAxdgDn0m-SzkQGX-YF_4jUk6UChOaeXxfBtMI0oxFclpSV7Q-zhRg_CchQu5Po3""}";

            return SendGCMNotification(BrowserAPIKey, json);


        }


        public static List<string> GetAndroidKeyByBranchIDAndEmployeeID(string branchID, List<string> employeeID)
        {

            var listAndroidKey = new List<string>();

            string join = String.Join("','", employeeID.ToArray());
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                 new SqlParameter() {ParameterName = "@branchID", SqlDbType = SqlDbType.NVarChar, Value = branchID} 
            };


            DataTable dtAndroidKey = Manager.DataFacade.DTSQLCommand("SELECT AndroidKey FROM EmployeeAndroidKey WHERE EmployeeID IN('" + join + "') AND BranchID = @branchID ", sp);



            foreach (DataRow row in dtAndroidKey.Rows)
            {

                listAndroidKey.Add(row["AndroidKey"].ToString());
            }

            return listAndroidKey;
        }

        public static Model.mdlResult PushNotificationConfirmation(Core.Model.mdlPushNotificationConfirmationParam param)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                 new SqlParameter() {ParameterName = "@notificationID", SqlDbType = SqlDbType.NVarChar, Value = param.notificationID}, 
                 new SqlParameter() {ParameterName = "@action", SqlDbType = SqlDbType.NVarChar, Value = param.action},
            };

            var mdlRes = new Model.mdlResult();

            //mdlRes.Result = DataFacade.DTSQLVoidCommand("DELETE FROM Log_Notification WHERE NotificationID = @notificationID AND Action = @action", sp);

            mdlRes.Result = DataFacade.DTSQLVoidCommand("UPDATE Log_Notification SET Recieved = 1 WHERE NotificationID = @notificationID AND Action = @action", sp);

            return mdlRes;
        }

        public static string PushNotif(string notificationID,string title, string message, string branchID, List<String> blEmployeeStrList)
        {
            Core.Model.mdlPush mdlPush = new Core.Model.mdlPush();
            Core.Model.mdlNotification mdlNotif = new Core.Model.mdlNotification();
            mdlNotif.title = title;
            mdlNotif.body = message;

            Core.Model.mdlData mdlData = new Core.Model.mdlData();
            mdlData.title = title;
            mdlData.msg = message;
            mdlData.notificationid = notificationID;
            List<string> androidkeyList = new List<string>();
            androidkeyList = PushNotificationFacade.GetAndroidKeyByBranchIDAndEmployeeID(branchID, blEmployeeStrList);


            mdlPush.registration_ids = androidkeyList;

            mdlPush.priority = "high";
            mdlPush.delay_while_idle = false;
            mdlPush.content_available = true;

            //mdlPush.notification = mdlNotif;

            mdlPush.data = mdlData;

            string json = JsonConvert.SerializeObject(mdlPush);

            string apikey = "AIzaSyCMje7hG5mWLNIqxPLrFHTfuQ_pIJP-0wE";

            string res = Core.Manager.PushNotificationFacade.SendNotif(apikey, json);

            return res;
        }
    }
}
