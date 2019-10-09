using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Model
{
    [DataContract]
    public class mdlSaveAndroidKeyParam
    {
        [DataMember]
        public string BranchID { get; set; }
        [DataMember]
        public string EmployeeID { get; set; }
        [DataMember]
        public string AndroidKey { get; set; }

    }

    public class mdlAndroidKey
    {

        public string BranchID { get; set; }

        public string EmployeeID { get; set; }
    }
}
