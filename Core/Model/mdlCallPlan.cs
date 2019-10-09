/* documentation
 *001 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Model
{
    public class mdlCallPlanParam
    {
        [DataMember]
        public string CallPlanID { get; set; }

        [DataMember]
        public string EmployeeID { get; set; }

        [DataMember]
        public string Date { get; set; }

        [DataMember]
        public string VehicleID { get; set; }

        [DataMember]
        public string BranchID { get; set; }

        [DataMember]
        public string LastDate { get; set; }

        [DataMember]
        public string LastUpdateBy { get; set; }
    }

    public class mdlCallPlanDetailParam
    {
        [DataMember]
        public string CPDetailID { get; set; }

        [DataMember]
        public string CallPlanID { get; set; }

        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public string Sequence { get; set; }

        [DataMember]
        public string EmployeeID { get; set; }
    }

    public class mdlCallPlan
    {
        public mdlCallPlan()
        {
            CallPlanID = "";
            EmployeeID = "";
            Date = "";
            VehicleID = "";
            BranchID = "";
            Helper1 = "";
            Helper2 = "";
            Result = "";
            KMAkhir = "0";

        }

        [DataMember]
        public string CallPlanID { get; set; }

        [DataMember]
        public string EmployeeID { get; set; }

        [DataMember]
        public string Date { get; set; }

        [DataMember]
        public string VehicleID { get; set; }

        [DataMember]
        public string BranchID { get; set; }

        [DataMember]
        public string Helper1 { get; set; }

        [DataMember]
        public string Helper2 { get; set; }

        [DataMember]
        public string Result { get; set; }

        [DataMember]
        public string KMAkhir { get; set; }
    }

    public class mdlCallPlanDetail
    {
        public mdlCallPlanDetail()
        {
            CPDetailID = "";
            CallPlanID = "";
            CustomerID = "";
            Sequence = "0";
            WarehouseID = "";

        }

        [DataMember]
        public string CPDetailID { get; set; }

        [DataMember]
        public string CallPlanID { get; set; }

        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public string Time { get; set; }

        [DataMember]
        public string Sequence { get; set; }

        [DataMember]
        public string WarehouseID { get; set; }

        [DataMember]
        public bool BPStock { get; set; }


    }

    public class mdlCallPlanDetail2
    {
        [DataMember]
        public string CPDetailID { get; set; }

        [DataMember]
        public string CallPlanID { get; set; }

        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string Sequence { get; set; }

        [DataMember]
        public string WarehouseID { get; set; }

        [DataMember]
        public Boolean Role { get; set; }
    }

}
