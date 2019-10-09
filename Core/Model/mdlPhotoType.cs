using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Model
{
    [DataContract]
    public class mdlPhotoType
    {
        [DataMember]
        public string PhotoTypeID { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public string Value { get; set; }
    }

    //public class mdlPhotoTypeList
    //{
    //    public List<mdlPhotoType> PhotoTypeList { get; set; }
    //}
}
