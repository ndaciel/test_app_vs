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
    public class mdlReturOrderParam
    {
        [DataMember]
        public string ReturNumber { get; set; }

        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public string EmployeeID { get; set; }

        [DataMember]
        public string BranchID { get; set; }

        [DataMember]
        public string ReturDate { get; set; }

        [DataMember]
        public string ReturStatus { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Signature { get; set; }

        [DataMember]
        public string IsPrint { get; set; }

        [DataMember]
        public string Remark { get; set; }  

        [DataMember]
        public string ReceivedDate { get; set; }

        [DataMember]
        public string IsNewRetur { get; set; } 

        [DataMember]
        public string VisitID { get; set; }

      
    }

    [DataContract]
    public class mdlReturOrderDetailParam
    {
        [DataMember]
        public string ReturNumber { get; set; }

        [DataMember]
        public string ProductID { get; set; }

        [DataMember]
        public string UOM { get; set; }

        [DataMember]
        public string Quantity { get; set; }

        [DataMember]
        public string QuantityReal { get; set; }

        [DataMember]
        public string ArticleNumber { get; set; }

        [DataMember]
        public string EmployeeID { get; set; }

        [DataMember]
        public string ReasonID { get; set; }
    }

    [DataContract]
    public class mdlReturOrder
    {
        [DataMember]
        public string ReturNumber { get; set; }

        [DataMember]
        public string CustomerID { get; set; }

        
        [DataMember]
        public string EmployeeID { get; set; }

        [DataMember]
        public string BranchID { get; set; }
  

        [DataMember]
        public string ReturDate { get; set; }

        [DataMember]
        public string ReturStatus { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Signature { get; set; }

        [DataMember]
        public string IsPrint { get; set; }

        [DataMember]
        public string Remark { get; set; }  

        [DataMember]
        public string ReceivedDate { get; set; }

    }

    [DataContract]
    public class mdlReturOrderDetail
    {

        [DataMember]
        public string ReturNumber { get; set; }

        [DataMember]
        public string ProductID { get; set; }

        [DataMember]
        public string UOM { get; set; }

        [DataMember]
        public string Quantity { get; set; }

        [DataMember]
        public string QuantityReal { get; set; }

        [DataMember]
        public string ArticleNumber { get; set; }
    }
  
    //001
    [DataContract]
    public class mdlReturReport
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
        public string ReturNumber { get; set; }

        [DataMember]
        public string ReturDate { get; set; }

        [DataMember]
        public string ProductID { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public int Quantity { get; set; }

        [DataMember]
        public int QuantityReal { get; set; }

        [DataMember]
        public string ReturImage { get; set; } 

        [DataMember]
        public string ReturImagePath { get; set; } 

        [DataMember]
        public string ReceivedDate { get; set; }

        [DataMember]
        public string Remark { get; set; } 

        [DataMember]
        public string Reason { get; set; } 

        [DataMember]
        public string ArticleNumber { get; set; }

        [DataMember]
        public string UOM { get; set; }

        [DataMember]
        public string VehicleID { get; set; }
    }
    

}
