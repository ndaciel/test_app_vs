using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Model
{
    //model for license limit request
    [DataContract]
    public class mdlLimit
    {
        [DataMember]
        public string Limit { get; set; }
    }

}
