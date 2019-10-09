/* documentation
 * 001 18 Okt'16 Fernandes
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Model
{
    public class mdlDailyCostParam
    {

        [DataMember]
        public string DailyCostID { get; set; }

        [DataMember]
        public string CostID { get; set; }

        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public string WarehouseID { get; set; }

        [DataMember]
        public string EmployeeID { get; set; }

        [DataMember]
        public string VisitID { get; set; }

        [DataMember]
        public string Date { get; set; }

        [DataMember]
        public string CostValue { get; set; }

        [DataMember]
        public string BranchID { get; set; }

        [DataMember]
        public string deviceID { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string LastUpdatedBy { get; set; }

        public DateTime LastUpdatedDate { get; set; }
    }

    public class mdlDailyCost
    {
        [DataMember]
        public string DailyCostID { get; set; }

        [DataMember]
        public string CostID { get; set; }

        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public string WarehouseID { get; set; }

        [DataMember]
        public string EmployeeID { get; set; }

        [DataMember]
        public string VisitID { get; set; }

        [DataMember]
        public string Date { get; set; }

        [DataMember]
        public string CostValue { get; set; }
    }

    public class mdlCost
    {
        [DataMember]
        public string CostID { get; set; }

        [DataMember]
        public string CostName { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public Boolean Role { get; set; }
    }

    //001
    public class mdlCostReport
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
        public string CostName { get; set; }

        [DataMember]
        public int CostValue { get; set; } 

        [DataMember]
        public string CostImage { get; set; }

        [DataMember]
        public string CostImagePath { get; set; }

        [DataMember]
        public string VehicleID { get; set; }

        [DataMember]
        public string CostDate { get; set; }
    }

    public class mdlJourneyCostReport
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
        public string CostName { get; set; }

        [DataMember]
        public int CostValue { get; set; }

        [DataMember]
        public string CostImage { get; set; }

        [DataMember]
        public string CostDate { get; set; }

        [DataMember]
        public string VehicleID { get; set; }
    }

}
