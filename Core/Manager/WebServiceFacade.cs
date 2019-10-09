using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Model;
using System.Net;
using System.IO;

namespace Core.Manager
{
    public class WebServiceFacade : Base.Manager
    {
        //get the limit of license request
        public static mdlLimit getLicenseLimit(string url, string jsonContent)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(jsonContent);

            request.ContentLength = byteArray.Length;
            request.ContentType = @"application/json";

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            long length = 0;
            var limitResult = new mdlLimit();

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    length = response.ContentLength;
                    Stream responseStream = response.GetResponseStream();
                    var streamReader = new StreamReader(responseStream);
                    
                    limitResult.Limit = streamReader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                limitResult.Limit = ex.ToString();
            }

            return limitResult;
        }

    }
}
