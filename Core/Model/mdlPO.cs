using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Model
{
    [DataContract]
    public class mdlPO
    {
        [DataMember]
        public string PONumber { get; set; }

        [DataMember]
        public string CustomerID { get; set; }
    }

    [DataContract]
    public class mdlPOParam
    {
        [DataMember]
        public string PONumber { get; set; }

        [DataMember]
        public string CustomerID { get; set; }
    }

}
