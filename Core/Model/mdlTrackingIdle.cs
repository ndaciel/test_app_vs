using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Model
{
    public class mdlTrackingIdle
    {
        public string vehicleID { get; set; }
        public DateTime time { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        
    }

    public class newMdlTrackingIdle
    {
        public string vehicleID { get; set; }
        public string time { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string isIdle { get; set; }
    }

    public class mdlTrackingIdleList
    {
        public List<newMdlTrackingIdle> trackingIdleList { get; set; }
    }

    public class mdlTrackingIdleListFinal
    {
        public List<mdlTrackingIdleList> finalTrackingIdleList { get; set; }
    }
}
