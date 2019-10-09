using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Model
{
    public class mdlTrackingJourney
    {
       
        public string vehicleID { get; set; }
        public string time { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string latitude2 { get; set; }
        public string longitude2 { get; set; }
    }

    public class mdlVisitJourney
    {
        public string visitID { get; set; }
        public string customerID { get; set; }
        public string customerName { get; set; }    
        public string startTime { get; set; }
        public string finishTime { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string isInRange { get; set; }
        public string reasonID { get; set; }
        public string reasonName { get; set; }

    }
}
