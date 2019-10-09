using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Model
{
    public class mdlLogVisitParam
    {
        [DataMember]
        public string VisitID { get; set; }

        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public string Date { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string EmployeeID { get; set; }

        [DataMember]
        public string BranchID { get; set; }

        [DataMember]
        public string deviceID { get; set; }
    }
}
