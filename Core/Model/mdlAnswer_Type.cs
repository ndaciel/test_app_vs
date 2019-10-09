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
    public class mdlAnswer_Type
    {
        [DataMember]
        public string AnswerTypeID { get; set; }

        [DataMember]
        public string AnswerTypeText { get; set; }

    }

}
