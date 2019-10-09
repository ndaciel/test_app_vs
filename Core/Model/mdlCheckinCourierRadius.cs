using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Model
{
    [DataContract]
    public class mdlCheckinCourierRadius
    {
        [DataMember]
        public string InRange { get; set; }

        [DataMember]
        public string Distance { get; set; }
    }
    [DataContract]
    public class mdlCheckinCourierRadiusParam
    {
        [DataMember]
        public string CustomerID { get; set; }
        [DataMember]
        public string WarehouseID { get; set; }
        [DataMember]
        public string Latitude { get; set; }
        [DataMember]
        public string Longitude { get; set; }
        [DataMember]
        public string EmployeeID { get; set; }
        [DataMember]
        public string BranchID { get; set; }
        [DataMember]
        public string VisitID { get; set; }
        [DataMember]
        public string Radius { get; set; }

    }
}
