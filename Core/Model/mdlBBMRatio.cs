using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Model
{
    public class mdlBBMRatioParam
    {
        [DataMember]
        public string RatioID { get; set; }

        [DataMember]
        public string BranchID { get; set; }

        [DataMember]
        public string VehicleID { get; set; }

        [DataMember]
        public string EmployeeID { get; set; }

        [DataMember]
        public string Km { get; set; }

        [DataMember]
        public string Liter { get; set; }

        [DataMember]
        public string Harga { get; set; }

        [DataMember]
        public string Latitude { get; set; }

        [DataMember]
        public string Longitude { get; set; }

        [DataMember]
        public string Date { get; set; }

        [DataMember]
        public string DeviceID { get; set; }
    }

    public class mdlBBMRatioReport
    {
        [DataMember]
        public string BranchID { get; set; }

        [DataMember]
        public string BranchName { get; set; }

        [DataMember]
        public string VehicleID { get; set; }

        [DataMember]
        public string EmployeeID { get; set; }

        [DataMember]
        public string EmployeeName { get; set; }

        [DataMember]
        public int Km { get; set; }

        [DataMember]
        public double Liter { get; set; }

        [DataMember]
        public int Harga { get; set; }

        [DataMember]
        public string Latitude { get; set; }

        [DataMember]
        public string Longitude { get; set; }

        [DataMember]
        public string Date { get; set; }

        [DataMember]
        public string Time { get; set; }
    }
}
