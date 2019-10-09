using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Model
{
    [DataContract]
    public class mdlWarehouse
    {
        [DataMember]
        public string WarehouseID { get; set; }
        [DataMember]
        public string WarehouseName { get; set; }
        [DataMember]
        public string WarehouseAddress { get; set; }
        [DataMember]
        public string Latitude { get; set; }
        [DataMember]
        public string Longitude { get; set; }
        [DataMember]
        public string CustomerID { get; set; }
        [DataMember]
        public string Radius { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string Pic { get; set; }

    }
}
