using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Model
{
    [DataContract]
    public class mdlResult
    {
        [DataMember]
        public string Result { get; set; }

        [DataMember]
        public string ResultValue { get; set; }

    }

    [DataContract]
    public class mdlResultList
    {
        [DataMember]
        public List<mdlResult> ResultList { get; set; }
    }
    [DataContract]
    public class mdlResultSV
    {
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string StatusCode { get; set; }
        [DataMember]
        public string StatusMessage { get; set; }
        [DataMember]
        public Model.mdlSetDeviceID Value { get; set; }


    }

    public class mdlResultSvc
    {
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string StatusCode { get; set; }
        [DataMember]
        public string StatusMessage { get; set; }
        [DataMember]
        public Object Value { get; set; }


    }

    public class mdlResultSmsGateway
    {
        [DataMember]
        public string code { get; set; }

        [DataMember]
        public string status { get; set; }

        [DataMember]
        public string message { get; set; }

        [DataMember]
        public string msgid { get; set; }

    }
}
