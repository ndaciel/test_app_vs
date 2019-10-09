using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Model
{
    public class mdlSisaStockVisit
    {
        public string VisitID { get; set; }
        public string ProductID { get; set; }
        public string CustomerID { get; set; }
        public string EmployeeID { get; set; }
        public string BranchID { get; set; }
        public string UOM { get; set; }
        public string SisaStockTypeID { get; set; }
        public decimal Value { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }

       
    }
}
