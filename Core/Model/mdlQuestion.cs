/* documentation
 * 001 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Model
{
    public class mdlQuestion
    {
        [DataMember]
        public string QuestionID { get; set; }

        [DataMember]
        public string QuestionText { get; set; }

        [DataMember]
        public string AnswerTypeID { get; set; }

        [DataMember]
        public Boolean IsSubQuestion { get; set; }

        [DataMember]
        public Int32 Sequence { get; set; }

        [DataMember]
        public string QuestionSetID { get; set; }

        [DataMember]
        public string QuestionCategoryID { get; set; }

        [DataMember]
        public string AnswerID { get; set; }

        [DataMember]
        public string No { get; set; }

        [DataMember]
        public Boolean Mandatory { get; set; }

        [DataMember]
        public Boolean IsActive { get; set; }

        [DataMember]
        public Boolean User { get; set; }
    }

}
