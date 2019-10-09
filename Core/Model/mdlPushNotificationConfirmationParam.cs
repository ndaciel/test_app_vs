using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Model
{
    [DataContract]
    public class mdlPushNotificationConfirmationParam
    {
        [DataMember]
        public string notificationID { get; set; }

        [DataMember]
        public string action { get; set; }
    }
}
