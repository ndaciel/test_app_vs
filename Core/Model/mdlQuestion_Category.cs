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
    public class mdlQuestion_Category
    {
        [DataMember]
        public string QuestionCategoryID { get; set; }

        [DataMember]
        public string QuestionCategoryText { get; set; }

    }

}
