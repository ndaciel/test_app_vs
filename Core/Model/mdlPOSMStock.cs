using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Model
{
    public class mdlPOSMStock
    {
        public string VisitID { get; set; }
        public string POSMID { get; set; }
        public string EmployeeID { get; set; }
        public string BranchID { get; set; }   
        public int Quantity { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }

    }
}
