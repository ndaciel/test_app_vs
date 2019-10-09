using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Model
{

    public class mdlCompany
    {
        [DataMember]
        public string CompanyID { get; set; }

        [DataMember]
        public string CompanyName { get; set; }
    }

    public class mdlCompanyParam
    {
        [DataMember]
        public string CompanyID { get; set; }

        [DataMember]
        public string CompanyName { get; set; }
    }

    public class mdlCompanyList
    {
        public List<mdlCompany> CompanyList { get; set; }
    }

}
