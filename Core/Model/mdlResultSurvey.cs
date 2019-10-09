using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Model
{
    public class mdlResultSurvey
    {
        [DataMember]
        public string SurveyID { get; set; }

        [DataMember]
        public string EmployeeID { get; set; }

        [DataMember]
        public string BranchID { get; set; }

        [DataMember]
        public string VisitID { get; set; }

        [DataMember]
        public string Date { get; set; }

        [DataMember]
        public string StartTime { get; set; }

        [DataMember]
        public string EndTime { get; set; }

        [DataMember]
        public string Latitude { get; set; }

        [DataMember]
        public string Longitude { get; set; }

        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public string QuestionCategoryID { get; set; }

        [DataMember]
        public Decimal CompletedPercentage { get; set; }
    }
}
