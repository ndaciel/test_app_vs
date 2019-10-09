using System;
using System.Runtime.Serialization;
namespace Core.Model {

    public class mdlDepositParam {

        [DataMember]
        public String DepositID { get; set; }

        [DataMember]
        public String VisitID { get; set; }

        [DataMember]
        public String EmployeeID { get; set; }

        [DataMember]
        public String Status { get; set; }

        [DataMember]
        public String ReceivedDate { get; set; }

        [DataMember]
        public String Description { get; set; }

        [DataMember]
        public String CustomerID { get; set; }

        [DataMember]
        public String CustomerName { get; set; }

        [DataMember]
        public String CreatedBy { get; set; }

        //[DataMember]
        //public String CreatedDate { get; set; }

        [DataMember]
        public String LastUpdateBy { get; set; }

        //[DataMember]
        //public String LastDate { get; set; }

        [DataMember]
        public String Note { get; set; }

        [DataMember]
        public double Amount { get; set; }

        [DataMember]
        public String Seq { get; set; }

        [DataMember]
        public String DepositTypeID { get; set; }

        [DataMember]
        public string CustomerMobilephone1 { get; set; }

        [DataMember]
        public string BankAccountNumber { get; set; }

    }

    //public class mdlDeposit {

    //    [DataMember]
    //    public String DepositID { get; set; }

    //    [DataMember]
    //    public String VisitID { get; set; }

    //    [DataMember]
    //    public String EmployeeID { get; set; }

    //    [DataMember]
    //    public String Status { get; set; }

    //    [DataMember]
    //    public String ReceivedDate { get; set; }

    //    [DataMember]
    //    public String Description { get; set; }

    //    [DataMember]
    //    public String CustomerID { get; set; }

    //    [DataMember]
    //    public String CreatedBy { get; set; }

    //    [DataMember]
    //    public String CreatedDate { get; set; }

    //    [DataMember]
    //    public String LastUpdateBy { get; set; }

    //    [DataMember]
    //    public String LastDate { get; set; }

    //}

    //public class mdlDepositDetail {

    //    [DataMember]
    //    public String DepositID { get; set; }

    //    [DataMember]
    //    public String Note { get; set; }

    //    [DataMember]
    //    public String Amount { get; set; }

    //    [DataMember]
    //    public String Seq { get; set; }

    //    [DataMember]
    //    public String DepositTypeID { get; set; }

    //}

}