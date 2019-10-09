using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Model
{
    public class mdlCostVisit
    {
        [DataMember]
        public string VisitID { get; set; }

        [DataMember]
        public string CostID { get; set; }

        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public string EmployeeID { get; set; }

        [DataMember]
        public string BranchID { get; set; }

        [DataMember]
        public string deviceID { get; set; }
    }
}
