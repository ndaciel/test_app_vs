using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Model
{
    [DataContract]
    public class mdlUser
    {
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string BranchID { get; set; }

        [DataMember]
        public string EmployeeID { get; set; }
        [DataMember]
        public string RoleID { get; set; }
        [DataMember]
        public Boolean Role { get; set; }
    }

    [DataContract]
    public class mdlUserCheckbox
    {
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public string UserName { get; set; }
    }
}

