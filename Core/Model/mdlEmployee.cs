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
    public class mdlEmployeeParam
    {
        [DataMember]
        public string EmployeeID { get; set; }

        [DataMember]
        public string EmployeeName { get; set; }

        [DataMember]
        public string EmployeeTypeID { get; set; }

        [DataMember]
        public string BranchID { get; set; } 
    }

    public class mdlEmployee
    {
        [DataMember]
        public string EmployeeID { get; set; }

        [DataMember]
        public string EmployeeName { get; set; }

        [DataMember]
        public string EmployeeTypeID { get; set; }

        [DataMember]
        public string BranchID { get; set; } 
    }

    public class mdlEmployeeList
    {
        public List<mdlEmployee> EmployeeList { get; set; }
    }
}
