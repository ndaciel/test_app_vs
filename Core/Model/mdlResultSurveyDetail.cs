using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Model
{
    public class mdlResultSurveyDetail
    {
        [DataMember]
        public string SurveyID { get; set; }

        [DataMember]
        public string QuestionID { get; set; }

        [DataMember]
        public string AnswerID { get; set; }

        [DataMember]
        public string Value { get; set; }
    }
}
