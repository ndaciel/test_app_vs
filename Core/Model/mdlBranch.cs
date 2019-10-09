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
    public class mdlBranch
    {
        [DataMember]
        public string BranchID { get; set; }

        [DataMember]
        public string BranchName { get; set; } 

        [DataMember]
        public string BranchDescription { get; set; }

        [DataMember]
        public string CompanyID { get; set; }

        [DataMember]
        public string Latitude { get; set; }

        [DataMember]
        public string Longitude { get; set; }

        [DataMember]
        public Boolean Role { get; set; }
    }

    public class mdlBranchParam
    {
        [DataMember]
        public string BranchID { get; set; }

        [DataMember]
        public string BranchDescription { get; set; }

        [DataMember]
        public string CompanyID { get; set; }
    }

    public class mdlBranchList
    {
        public List<mdlBranch> BranchList { get; set; }
    }
}
