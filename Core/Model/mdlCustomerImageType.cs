using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Model
{
    public class mdlCustomerImageType
    {
        [DataMember]
        public string ImageID { get; set; }

        [DataMember]
        public string PhotoTypeID { get; set; }
    }
}
