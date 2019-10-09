using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Model
{
    [DataContract]
    public class mdlUploadJsonParam
    {

        [DataMember]
        public mdlUploadJson UploadJson { get; set; }
    }
    [DataContract]
    public class mdlUploadJson
    {
        [DataMember]
        public List<Core.Model.mdlDeliveryOrderParam> ListDeliveryOrder { get; set; }

        [DataMember]
        public List<Core.Model.mdlDeliveryOrderDetailParam> ListDeliveryOrderDetail { get; set; }

        [DataMember]
        public List<Core.Model.mdlVisitParam> ListVisit { get; set; }

        [DataMember]
        public List<Core.Model.mdlVisitDetailParamNew> ListVisitDetail { get; set; }

        [DataMember]
        public List<Core.Model.mdlDailyCostParam> ListCost { get; set; }

        [DataMember]
        public List<Core.Model.mdlCustomerImageParam> ListCustomerImage { get; set; }

        [DataMember]
        public List<Core.Model.mdlLogVisitParam> ListLogVisit { get; set; }
    }
}
