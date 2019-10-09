using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Model
{
    public class mdlGetWarehouseCoordinate
    {
        public string CustomerID { get; set; }
        public string WarehouseID { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Radius { get; set; }
    }
}
