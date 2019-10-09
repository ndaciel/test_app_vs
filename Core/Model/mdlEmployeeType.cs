using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Model
{
    public class mdlEmployeeTypeParam
    {
        [DataMember]
        public string EmployeeTypeID { get; set; }

        [DataMember]
        public string EmployeeTypeName { get; set; }

        [DataMember]
        public string Description { get; set; }
    }

    public class mdlEmployeeType
    {
        [DataMember]
        public string EmployeeTypeID { get; set; }

        [DataMember]
        public string EmployeeTypeName { get; set; }

        [DataMember]
        public string Description { get; set; }
    }

    public class mdlEmployeeTypeList
    {
        public List<mdlEmployeeType> EmployeeTypeList { get; set; }
    }
}
