using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Model
{
    [DataContract]
    public class mdlCustomerType
    {
        [DataMember]
        public string CustomerTypeID { get; set; }

        [DataMember]
        public string CustomerTypeName {get;set;}

        [DataMember]
        public string Description { get; set; }
    }

    [DataContract]
    public class mdlCustomerTypeParam
    {
        [DataMember]
        public string CustomerTypeID { get; set; }

        [DataMember]
        public string CustomerTypeName { get; set; }

        [DataMember]
        public string Description { get; set; }
    }

}
