using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml.Linq;

namespace Core.Manager
{
    public class ReverseGeocodingFacade : Base.Manager
    {
        private static string newStreet = string.Empty;
        private static string baseUri = "http://maps.googleapis.com/maps/api/" +
                         "geocode/xml?latlng={0},{1}&sensor=false";

        public static string GetStreetName(string lat, string lng)
        {

            //RetrieveFormatedAddress(lat, lng);


            //return newStreet;
            if (lat != "" || lng != "")
            {

                var requestUri = string.Format(baseUri, lat, lng);
                var request = WebRequest.Create(requestUri);
                var response = request.GetResponse();
                var xdoc = XDocument.Load(response.GetResponseStream());


                var status = xdoc.Element("GeocodeResponse").Element("status").Value;

                if (status == "OK")
                {
                    var result = xdoc.Element("GeocodeResponse").Element("result");

                    var street = result.Element("formatted_address").Value;

                    string[] splitStreet = street.Split(',');
                    string finalStreet = splitStreet[0] + "," + splitStreet[1];

                    return finalStreet;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }



        }

        public static void RetrieveFormatedAddress(string lat, string lng)
        {
            string requestUri = string.Format(baseUri, lat, lng);

            using (WebClient wc = new WebClient())
            {
                wc.DownloadStringCompleted +=
                  new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);
                wc.DownloadStringAsync(new Uri(requestUri));
            }


        }

        static void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            var xmlElm = XElement.Parse(e.Result);

            var status = (from elm in xmlElm.Descendants()
                          where elm.Name == "status"
                          select elm).FirstOrDefault();
            if (status.Value.ToLower() == "ok")
            {
                var res = (from elm in xmlElm.Descendants()
                           where elm.Name == "formatted_address"
                           select elm).FirstOrDefault();
                //Console.WriteLine(res.Value);
                string[] splitValue = res.Value.Split(',');
                string street = splitValue[1];
                newStreet = street;



                //Console.WriteLine(street);

            }
            else
            {
                newStreet = "No Address Found";
                //Console.WriteLine("No Address Found");
            }
        }

        public static string GetReloadStreetName(string latitude, string longitude)
        {
            string streetName = "";

            if (latitude != "" || longitude != "")
            {

                string resultGetStreetName = "";
                int count = 1;
                while (count != 3 || resultGetStreetName == "")
                {
                    resultGetStreetName = ReverseGeocodingFacade.GetStreetName(latitude, longitude);
                    count++;
                }
                streetName = resultGetStreetName;
            }
            else
            {
                streetName = "";
            }

            return streetName;
        }
    }
}