using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.Reflection;

namespace Core.Services
{
    public struct JsonSerializerSettings
    {
        static JsonSerializerSettings()
        {

        }
        public object DateFormatHandling { get; set; }
    }
    public class RestPublisher
    {
        private readonly WebClient _client;

        public RestPublisher()
        {
            _client = new WebClient();
            _client.UploadStringCompleted += client_UploadStringCompleted;
            _client.Headers[HttpRequestHeader.ContentType] = "application/json";
        }

        //public void Send(string endpoint, object content, Action<UploadStringCompletedEventArgs> callback)
        //{
        //    _client.UploadStringAsync(
        //        new Uri(endpoint, UriKind.RelativeOrAbsolute),
        //        "POST", Serialize(content), callback);
        //}

        public void Send(string endpoint, object content)
        {
            _client.UploadStringCompleted -= client_UploadStringCompleted;
            var data = Serialize(content);
            _client.UploadString(new Uri(endpoint, UriKind.RelativeOrAbsolute), "POST", Serialize(content));
        }



        private void client_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            var callback = (Action<UploadStringCompletedEventArgs>)e.UserState;
            if (!IsCreated())
                callback(e);
        }




        public static string Serialize(Object obj)
        {
            var setting = new Newtonsoft.Json.JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            return JsonConvert.SerializeObject(obj, setting);
        }

        private int GetStatusCode(out string statusDescription)
        {
            FieldInfo responseField = _client.GetType().GetField("m_WebResponse", BindingFlags.Instance | BindingFlags.NonPublic);

            if (responseField != null)
            {
                var response = responseField.GetValue(_client) as HttpWebResponse;

                if (response != null)
                {
                    statusDescription = response.StatusDescription;
                    return (int)response.StatusCode;
                }
            }

            statusDescription = null;
            return 0;
        }

        private bool IsCreated()
        {
            string statusDescription;
            var statusCode = GetStatusCode(out statusDescription);
            return statusCode == 201;
        }

        public static T Deserialize<T>(string obj)
        {
            var setting = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            return JsonConvert.DeserializeObject<T>(obj);
        }
    }
}
