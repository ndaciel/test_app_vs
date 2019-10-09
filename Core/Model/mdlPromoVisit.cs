using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Model
{
    public class mdlPromoVisit
    {
        public string VisitID { get; set; }
        public string ProductID { get; set; }
        public string CustomerID { get; set; }
        public string PromoID { get; set; }
        public string EmployeeID { get; set; }
        public string BranchID { get; set; }       
        public string Image { get; set; }
        public string Notes { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }

    }
}
