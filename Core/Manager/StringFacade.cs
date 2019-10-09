using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Core.Manager
{
    public class StringFacade
    {
        public static string NormalizedBranch(string branches)
        {

            if (branches.Contains('|'))
            {


                var sb = new StringBuilder();
                string[] arrBranch = branches.Split('|');
                if (arrBranch.Count() == 1)
                {
                    return arrBranch.FirstOrDefault().Trim();
                }
                else if (arrBranch.Count() > 1)
                {
                    string last = arrBranch[arrBranch.Count() - 1];
                    foreach (string str in arrBranch)
                    {
                        if (str == last)
                        {
                            sb.Append("'" + str + "'");
                        }
                        else
                        {
                            sb.Append("'" + str + "',");
                        }
                    }

                    return sb.ToString();
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "'" + branches + "'";
            }

        }

        public static String urlencodeString(Dictionary<string, string> param)
        {
            // Urutkan berdasarkan nama
            var list = param.Keys.ToList();
            list.Sort();

            // Deretkan menjadi satu baris
            String result = "";
            foreach (var key in list)
            {
                String value = param[key];
                if (value == "")
                {
                    continue;
                }
                if (result != "")
                {
                    result += "&";
                }

                result += Uri.EscapeDataString(key) + "=" + Uri.EscapeDataString(value);
            }

            return result;
        }

        public static String PostAPISMSGateway(String url, String jsonContent)
        {
            String Result = "";
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(jsonContent);

            request.ContentLength = byteArray.Length;
            request.ContentType = @"application/x-www-form-urlencoded";

            try
            {
                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }
                long length = 0;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    length = response.ContentLength;
                    Stream responseStream = response.GetResponseStream();
                    var streamReader = new StreamReader(responseStream);
                    Result = streamReader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                Result = ex.ToString();

                using (WebResponse response = ex.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        // text is the response body
                        string text = reader.ReadToEnd();
                    }
                }
            }

            return Result;
        }
    }
}
