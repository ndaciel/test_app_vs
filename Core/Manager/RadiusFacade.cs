using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Device.Location;


namespace Core.Manager
{
    public class RadiusFacade
    {
        public static double distance_calculate(double lat1, double lon1, double lat2, double lon2, char unit)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            if (unit == 'K')
            {
                dist = dist * 1.609344;
            }
            else if (unit == 'N')
            {
                dist = dist * 0.8684;
            }
            return (dist);
        }
        private static double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }
        private static double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
        public static double GetDistanceInMeters(double Latitude1, double Longitude1, double Latitude2, double Longitude2)
        {
            double _status = 0.00;
            Console.WriteLine("Kilometer      : " + distance_calculate(Latitude1, Longitude1, Latitude2, Longitude2, 'K'));

            return _status;
        }
        public static double GetDistanceInnauticalmiles(double Latitude1, double Longitude1, double Latitude2, double Longitude2)
        {
            double _status = 0.00;

            Console.WriteLine("nautical miles : " + distance_calculate(Latitude1, Longitude1, Latitude2, Longitude2, 'N'));
            return _status;
        }

        public static double getDistance(GeoCoordinate CStart, GeoCoordinate CFinish)
        {
            return CStart.GetDistanceTo(CFinish);
        }
    }
}
