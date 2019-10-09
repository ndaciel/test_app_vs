using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Core.Model
{
    public class mdlVehicle
    {
        [DataMember]
        public string VehicleID { get; set; }

        [DataMember]
        public string VehicleName { get; set; }

        [DataMember]
        public string VehicleNumber { get; set; }

        [DataMember]
        public string STNKDate { get; set; }

        [DataMember]
        public string YearManufacturing { get; set; }

        [DataMember]
        public string KIRNumber { get; set; }

        [DataMember]
        public string EngineNumber { get; set; }

        [DataMember]
        public string VehicleTypeID { get; set; }

        [DataMember]
        public string SubVehicleTypeID { get; set; }

        [DataMember]
        public string PlantID { get; set; }

        [DataMember]
        public string CapacityVolume { get; set; }

        [DataMember]
        public string CapacityWeight { get; set; }

        [DataMember]
        public string Brand { get; set; }
    }

    public class mdlVehicleBranchParam
    {
        [DataMember]
        public string BranchID { get; set; }
    }

    public class mdlVehicleParam
    {
        [DataMember]
        public string VehicleID { get; set; }

        [DataMember]
        public string VehicleName { get; set; }

        [DataMember]
        public string VehicleNumber { get; set; }

        [DataMember]
        public string STNKDate { get; set; }

        [DataMember]
        public string YearManufacturing { get; set; }

        [DataMember]
        public string KIRNumber { get; set; }

        [DataMember]
        public string EngineNumber { get; set; }

        [DataMember]
        public string VehicleTypeID { get; set; }

        [DataMember]
        public string SubVehicleTypeID { get; set; }

        [DataMember]
        public string PlantID { get; set; }

        [DataMember]
        public string CapacityVolume { get; set; }

        [DataMember]
        public string CapacityWeight { get; set; }

        [DataMember]
        public string Brand { get; set; }

        [DataMember]
        public string BranchID { get; set; }
    }

    public class mdlVehicleList
    {
        public List<mdlVehicle> VehicleList { get; set; }
    }
}
