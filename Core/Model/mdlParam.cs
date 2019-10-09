using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Model
{
    public class mdlParam
    {
        [DataMember]
        public string EmployeeID { get; set; }

        [DataMember]
        public string BranchID { get; set; }

        [DataMember]
        public string Date { get; set; }

        [DataMember]
        public string DeviceID { get; set; }

        [DataMember]
        public string Role { get; set; }
    }
}
