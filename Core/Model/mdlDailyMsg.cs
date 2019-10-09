/* documentation
 *001 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Model
{

    [DataContract]
    public class mdlDailyMsg
    {
        [DataMember]
        public string MessageID { get; set; }

        [DataMember]
        public string MessageName { get; set; }

        [DataMember]
        public string MessageDesc { get; set; }

        [DataMember]
        public string MessageImg { get; set; }

        [DataMember]
        public string BranchID { get; set; }

        [DataMember]
        public string Date { get; set; }

        [DataMember]
        public string EndDate { get; set; }

        [DataMember]
        public string CreatedBy { get; set; }

        [DataMember]
        public Boolean Role { get; set; }
    }
    [DataContract]
    public class mdlDailyMsgList
    {
        [DataMember]
        public List<mdlDailyMsg> DailyMessageList { get; set; }
    }

    [DataContract]
    public class mdlDailyMsgRoleParam
    {
        [DataMember]
        public string MessageID { get; set; }

        [DataMember]
        public string EmployeeID { get; set; }

        [DataMember]
        public string BranchID { get; set; }
    }

    [DataContract]
    public class mdlDailyMsgRole
    {
        [DataMember]
        public string MessageID { get; set; }

        [DataMember]
        public string EmployeeID { get; set; }

        [DataMember]
        public string EmployeeName { get; set; }

        [DataMember]
        public string BranchID { get; set; }
    }


}
