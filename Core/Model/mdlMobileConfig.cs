using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Model
{
    public class mdlMobileConfig
    {
        [DataMember]
        public string BranchId { get; set; }

        [DataMember]
        public string ID { get; set; }

        [DataMember]
        public string Desc { get; set; }

        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public string TypeValue { get; set; }

        [DataMember]
        public Boolean Role { get; set; }
    }
}
