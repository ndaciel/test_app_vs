using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Model
{
    public class mdlLoginParam
    {
        [DataMember]
        public string EmployeeID { get; set; }

        //[DataMember]
        //public string Password { get; set; }

        ///////////////////////


        [DataMember]
        public string Role { get; set; }

        [DataMember]
        public string BranchID { get; set; }

        

    }

    public class mdlLoginResult
    {
        [DataMember]
        public string EmployeeID { get; set; }

        [DataMember]
        public string Role { get; set; }

        [DataMember]
        public string Result { get; set; }

        [DataMember]
        public string Message { get; set; }

    }

   
}
