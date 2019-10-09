using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Model
{
    public class mdlLogIdleParam
    {
        public string EmployeeID { get; set; }
        public string BranchID { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Location { get; set; }
        public string StartIdle { get; set; }
        public string Now { get; set; }
        public string Duration { get; set; }
        public string Status { get; set; }
    }

    public class mdlLogIdle
    {
        public string EmployeeID { get; set; }
        public string BranchID { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Location { get; set; }
        public string StartIdle { get; set; }
        public string Duration { get; set; }
        public string Status { get; set; }
    }

}
