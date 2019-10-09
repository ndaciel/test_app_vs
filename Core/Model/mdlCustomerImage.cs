/* documentation
 * 001 18 Okt'16 fernandes
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Model
{
    public class mdlCustomerImageParam
    {
        [DataMember]
        public string ImageID { get; set; }

        [DataMember]
        public string ImageDate { get; set; }

        [DataMember]
        public string ImageBase64 { get; set; }

        [DataMember]
        public string ImageType { get; set; }

        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public string WarehouseID { get; set; }

        [DataMember]
        public string EmployeeID { get; set; } 

        [DataMember]
        public string DocNumber { get; set; }

        [DataMember]
        public string VisitID { get; set; }

        [DataMember]
        public string BranchID { get; set; }

        [DataMember]
        public string deviceID { get; set; }

        [DataMember]
        public string Longitude { get; set; }

        [DataMember]
        public string Latitude { get; set; } 
    }

    //001
    public class mdlCustomerImageReport
    {
        [DataMember]
        public string BranchID { get; set; }

        [DataMember]
        public string BranchName { get; set; }

        [DataMember]
        public string EmployeeID { get; set; }

        [DataMember]
        public string EmployeeName { get; set; }

        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string WarehouseID { get; set; }

        [DataMember]
        public string WarehouseName { get; set; }

        [DataMember]
        public string VisitID { get; set; }

        [DataMember]
        public string DocNumber { get; set; } 

        [DataMember]
        public string ImageDate { get; set; }

        [DataMember]
        public string ImageBase64 { get; set; }

        [DataMember]
        public string ImagePath { get; set; }

        [DataMember]
        public string ImageType { get; set; }

        [DataMember]
        public string VehicleID { get; set; }

        [DataMember]
        public string CostName { get; set; }
    }

}
