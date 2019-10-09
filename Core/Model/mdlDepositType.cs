using System.Runtime.Serialization;

namespace Core.Model
{
    public class mdlDepositType
    {
        [DataMember]
        public string DepositTypeID { get; set; }

        [DataMember]
        public string DepositTypeName { get; set; }

    }

}