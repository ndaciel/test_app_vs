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
    public class mdlAnswer
    {
        [DataMember]
        public string AnswerID { get; set; }

        [DataMember]
        public string AnswerText { get; set; }

        [DataMember]
        public string QuestionID { get; set; }

        [DataMember]
        public Boolean SubQuestion { get; set; }

        [DataMember]
        public Boolean IsSubQuestion { get; set; }

        [DataMember]
        public Int32 Sequence { get; set; }

        [DataMember]
        public string No { get; set; }

        [DataMember]
        public Boolean IsActive { get; set; }

    }

}
