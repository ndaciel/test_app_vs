using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Model
{
    [DataContract]
    public class mdlUserConfig
    {
        [DataMember]
        public string DeviceID { get; set; }
        [DataMember]
        public string EmployeeID { get; set; }
        [DataMember]
        public string BranchID { get; set; }
        [DataMember]
        public string BranchName { get; set; }
        [DataMember]
        public string VehicleNumber { get; set; }
        [DataMember]
        public string IpLocal { get; set; }
        [DataMember]
        public string PortLocal { get; set; }
        [DataMember]
        public string IpPublic { get; set; }
        [DataMember]
        public string PortPublic { get; set; }
        [DataMember]
        public string IpAlternative { get; set; }
        [DataMember]
        public string PortAlternative { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public Boolean Role { get; set; }
    }
}
