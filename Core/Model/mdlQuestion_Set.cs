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
    public class mdlQuestion_Set
    {
        [DataMember]
        public string QuestionSetID { get; set; }

        [DataMember]
        public string QuestionSetText { get; set; }

        [DataMember]
        public Boolean Role { get; set; }
    }

}
