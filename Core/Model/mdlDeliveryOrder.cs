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

    [DataContract]
    public class mdlDeliveryOrderParam
    {
        [DataMember]
        public string DONumber { get; set; }

        [DataMember]
        public string CallPlanID { get; set; }

        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public string WarehouseID { get; set; }

        [DataMember]
        public string EmployeeID { get; set; }

        [DataMember]
        public string DODate { get; set; }

        [DataMember]
        public string DOStatus { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Signature { get; set; }

        [DataMember]
        public string IsPrint { get; set; }

        [DataMember]
        public string BranchID { get; set; }

        [DataMember]
        public string Remark { get; set; }

        [DataMember]
        public string VehicleID { get; set; }

        [DataMember]
        public string VisitID { get; set; }

        [DataMember]
        public string deviceID { get; set; }

        [DataMember]
        public string SendDate { get; set; }
    }

    [DataContract]
    public class mdlDeliveryOrderDetailParam
    {
        [DataMember]
        public string DONumber { get; set; }

        [DataMember]
        public string ProductID { get; set; }

        [DataMember]
        public string UOM { get; set; }

        [DataMember]
        public string Quantity { get; set; }

        [DataMember]
        public string QuantityReal { get; set; }

        [DataMember]
        public string ProductGroup { get; set; }

        [DataMember]
        public string LotNumber { get; set; }

        [DataMember]
        public string EmployeeID { get; set; }

        [DataMember]
        public string ReasonID { get; set; }

        [DataMember]
        public string BoxID { get; set; }

        [DataMember]
        public string Line { get; set; }

        [DataMember]
        public string BranchID { get; set; }

        [DataMember]
        public string deviceID { get; set; }

    }
  

    [DataContract]
    public class mdlDeliveryOrder
    {
        [DataMember]
        public string DONumber { get; set; }

        [DataMember]
        public string CallPlanID { get; set; }

        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public string WarehouseID { get; set; }

        [DataMember]
        public string EmployeeID { get; set; }

        [DataMember]
        public string DODate { get; set; }

        [DataMember]
        public string DOStatus { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Signature { get; set; }

        [DataMember]
        public string IsPrint { get; set; }

        [DataMember]
        public string BranchID { get; set; }

        [DataMember]
        public string Remark { get; set; }

        [DataMember]
        public string VehicleID { get; set; }

        [DataMember]
        public string VisitID { get; set; }

    }

    [DataContract]
    public class mdlDeliveryOrderDetail
    {
        [DataMember]
        public string DONumber { get; set; }

        [DataMember]
        public string ProductID { get; set; }

        [DataMember]
        public string UOM { get; set; }

        [DataMember]
        public string Quantity { get; set; }

        [DataMember]
        public string QuantityReal { get; set; }

        [DataMember]
        public string ProductGroup { get; set; }

        [DataMember]
        public string LotNumber { get; set; }

        [DataMember]
        public string BoxID { get; set; }

        [DataMember]
        public string Line { get; set; }
    }

    //001
    [DataContract]
    public class mdlDOReport
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
        public string DONumber { get; set; }

        [DataMember]
        public string DODate { get; set; }

        [DataMember]
        public string DeliveryDate { get; set; }

        [DataMember]
        public string ProductID { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public string Quantity { get; set; }

        [DataMember]
        public string QuantityReal { get; set; }

        //[DataMember]
        //public string ImageBase64 { get; set; }

        //[DataMember]
        //public string ImagePath { get; set; }

        [DataMember]
        public string Remark { get; set; } 

        [DataMember]
        public string Reason { get; set; } 

        [DataMember]
        public string ArticleNumber { get; set; }

        [DataMember]
        public string LotNumber { get; set; }

        [DataMember]
        public string UOM { get; set; }

        [DataMember]
        public string VehicleID { get; set; } 
    }


}
