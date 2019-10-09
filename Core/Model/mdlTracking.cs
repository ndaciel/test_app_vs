/* documentation
 * 001 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Model
{

    public class mdlTrackingParam
    {
        [DataMember]
        public string TrackingID { get; set; }

        [DataMember]
        public string TrackingDate { get; set; }

        [DataMember]
        public string VehicleID { get; set; }

        [DataMember]
        public string Longitude { get; set; }

        [DataMember]
        public string EmployeeID { get; set; }

        [DataMember]
        public string Latitude { get; set; }

        [DataMember]
        public string BranchID { get; set; }

        [DataMember]
        public string FlagCheckIn { get; set; }
    }
    
    public class mdlJourneyTracking
    {
        [DataMember]
        public string TrackingDate { get; set; } 

        [DataMember]
        public string Longitude { get; set; }

        [DataMember]
        public string Latitude { get; set; }

        [DataMember]
        public string StreetName { get; set; }
    }
    
    public class mdlLiveTracking
    {
        [DataMember]
        public string TrackingDate { get; set; }

        [DataMember]
        public string Longitude { get; set; }

        [DataMember]
        public string Latitude { get; set; }

        [DataMember]
        public string EmployeeID { get; set; }

        [DataMember]
        public string VehicleID { get; set; }

        [DataMember]
        public string BranchID { get; set; }

        [DataMember]
        public string StreetName { get; set; }
    }
 

}
