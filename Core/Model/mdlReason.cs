/* documentation
 *001 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Model
{
    public class mdlReason
    {
        [DataMember]
        public string ReasonID { get; set; }

        [DataMember]
        public string ReasonType { get; set; }

        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public Boolean Role { get; set; }
    }

    public class mdlReasonList
    {
        public List<mdlReason> ReasonList { get; set; }
    }


}
