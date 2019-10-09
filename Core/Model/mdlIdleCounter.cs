using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Model
{
    public class mdlIdleCounter
    {
        public string EmployeeID { get; set; }
        public string BranchID { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public int Counter { get; set; }
        public string StartDate { get; set; }
    }

    public class mdlIdleReport
    {
        public string EmployeeID { get; set; }
        public string EmployeeNm { get; set; }
        public string BranchID { get; set; }
        public string BranchNm { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Location { get; set; }
        public string StartIdle { get; set; }
        public string EndIdle { get; set; }
        public string Duration { get; set; }
        public string Status { get; set; }
    }

    public class mdlReportIdleList
    {
        public List<mdlIdleReport> mdlIdleTrackingList { get; set; }
    }

}
