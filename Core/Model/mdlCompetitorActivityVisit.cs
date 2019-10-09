using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Model
{
    public class mdlCompetitorActivityVisit
    {
        public string VisitID { get; set; }
        public string CompetitorID { get; set; }
        public string CompetitorProductID { get; set; }
        public string CustomerID { get; set; }
        public string EmployeeID { get; set; }
        public string BranchID { get; set; }
        public string CompetitorActivityTypeID { get; set; }
        public string Value { get; set; }
        public string Notes { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
