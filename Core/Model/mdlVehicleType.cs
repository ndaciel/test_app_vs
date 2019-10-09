using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Model
{
    public class mdlVehicleType
    {
        [DataMember]
        public string VehicleTypeID { get; set; }

        [DataMember]
        public string VehicleTypeName { get; set; }
    }

    public class mdlVehicleTypeParam
    {
        [DataMember]
        public string VehicleTypeID { get; set; }

        [DataMember]
        public string VehicleTypeName { get; set; }
    }

    public class mdlVehicleTypeList
    {
        public List<mdlVehicleType> VehicleTypeList { get; set; }
    }
}
