/* documentation
 * 001 - 13 Okt'16 - fernandes
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Model
{

    [DataContract]
    public class mdlVisitParam
    {
        [DataMember]
        public string VisitID { get; set; }

        [DataMember]
        public string BranchID { get; set; }

        [DataMember]
        public string EmployeeID { get; set; }

        [DataMember]
        public string VehicleID { get; set; }

        [DataMember]
        public string VisitDate { get; set; }

        [DataMember]
        public bool isStart { get; set; }

        [DataMember]
        public bool isFinish { get; set; }

        [DataMember]
        public string StartDate { get; set; }

        [DataMember]
        public string EndDate { get; set; }

        [DataMember]
        public string KMStart { get; set; }

        [DataMember]
        public string KMFinish { get; set; }

        [DataMember]
        public string deviceID { get; set; }
    }

    public class mdlDBVisit
    {

        public string VisitID { get; set; }


        public string BranchID { get; set; }


        public string EmployeeID { get; set; }


        public string VehicleID { get; set; }


        public string VisitDate { get; set; }

        public int VisitWeek { get; set; }

        public bool isStart { get; set; }

        public bool isFinish { get; set; }

        public string StartDate { get; set; }


        public string EndDate { get; set; }


        public string KMStart { get; set; }

        public string KMFinish { get; set; }

        public string CreatedBy { get; set; }

        public string CreatedDate { get; set; }

        public string LastUpdatedBy { get; set; }

        public string LastUpdatedDate { get; set; }

        public int rkDelete { get; set; }
    }


    [DataContract]
    public class mdlVisitDetailParam
    {
        [DataMember]
        public string VisitID { get; set; }

        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public string WarehouseID { get; set; }

        [DataMember]
        public bool isStart { get; set; }

        [DataMember]
        public bool isFinish { get; set; }

        [DataMember]
        public string StartDate { get; set; }

        [DataMember]
        public string EndDate { get; set; }

        [DataMember]
        public string ReasonID { get; set; }

        [DataMember]
        public string ReasonDescription { get; set; }

        [DataMember]
        public string Longitude { get; set; }


        [DataMember]
        public string Latitude { get; set; }



        [DataMember]
        public string EmployeeID { get; set; }

        [DataMember]
        public int isDeliver { get; set; }


        [DataMember]
        public bool isInRange { get; set; }

        [DataMember]
        public string Distance { get; set; }

        [DataMember]
        public bool isInRangeCheckout { get; set; }

        [DataMember]
        public decimal Duration { get; set; }

        [DataMember]
        public string DistanceCheckout { get; set; }
    }


    [DataContract]
    public class mdlVisitDetailParamNew
    {
        [DataMember]
        public string VisitID { get; set; }

        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public string isStart { get; set; }

        [DataMember]
        public string isFinish { get; set; }

        [DataMember]
        public string isVisit { get; set; }

        [DataMember]
        public string StartDate { get; set; }

        [DataMember]
        public string EndDate { get; set; }

        [DataMember]
        public string ReasonID { get; set; }

        [DataMember]
        public string ReasonDescription { get; set; }

        [DataMember]
        public string Latitude { get; set; }

        [DataMember]
        public string Longitude { get; set; }

        [DataMember]
        public string EmployeeID { get; set; }

        [DataMember]
        public string isDeliver { get; set; }

        [DataMember]
        public string WarehouseID { get; set; }

        [DataMember]
        public string isInRange { get; set; }

        [DataMember]
        public string isInRangeCheckout { get; set; }

        [DataMember]
        public string Distance { get; set; }

        [DataMember]
        public string Duration { get; set; }

        [DataMember]
        public string DistanceCheckout { get; set; }

        [DataMember]
        public string BranchID { get; set; }

        [DataMember]
        public string deviceID { get; set; }

        [DataMember]
        public string LatitudeCheckOut { get; set; }

        [DataMember]
        public string LongitudeCheckOut { get; set; }

        [DataMember]
        public string ReasonSequence { get; set; }
    }


    [DataContract]
    public class mdlVisit
    {
        [DataMember]
        public string EmployeeID { get; set; }

        [DataMember]
        public string BranchID { get; set; }

        [DataMember]
        public string VisitID { get; set; }

        [DataMember]
        public string VehicleID { get; set; }

        [DataMember]
        public string VisitDate { get; set; }

        [DataMember]
        public string isStart { get; set; }

        [DataMember]
        public string StartDate { get; set; }

        [DataMember]
        public string EndDate { get; set; }

        [DataMember]
        public string isFinish { get; set; }

        [DataMember]
        public string KMStart { get; set; }

        [DataMember]
        public string KMFinish { get; set; }
    }

    [DataContract]
    public class mdlVisitDetail
    {
        [DataMember]
        public string VisitID { get; set; }

        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public string isStart { get; set; }

        [DataMember]
        public string isFinish { get; set; }

        [DataMember]
        public string StartDate { get; set; }

        [DataMember]
        public string EndDate { get; set; }

        [DataMember]
        public string ReasonID { get; set; }

        [DataMember]
        public string ReasonDescription { get; set; }

        [DataMember]
        public string Latitude { get; set; }

        [DataMember]
        public string Longitude { get; set; }

        [DataMember]
        public string EmployeeID { get; set; }

        [DataMember]
        public string isDeliver { get; set; }
    }

    //001
    [DataContract]
    public class mdlVisitReport
    {
        [DataMember]
        public string BranchID { get; set; }

        [DataMember]
        public string BranchName { get; set; }

        [DataMember]
        public string DONumber { get; set; }

        [DataMember]
        public string DOStatus { get; set; }

        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string WarehouseID { get; set; }

        [DataMember]
        public string WarehouseName { get; set; }

        [DataMember]
        public string VisitDate { get; set; }

        [DataMember]
        public string StartDate { get; set; }

        [DataMember]
        public string EndDate { get; set; }

        [DataMember]
        public string isStart { get; set; }

        [DataMember]
        public string isFinish { get; set; }

        [DataMember]
        public string isInRange { get; set; }

        [DataMember]
        public string isInRangeCheckOut { get; set; }

        [DataMember]
        public string Reason { get; set; }

        [DataMember]
        public string EmployeeID { get; set; }

        [DataMember]
        public string EmployeeName { get; set; }

        //[DataMember]
        //public string ImageBase64 { get; set; }

        //[DataMember]
        //public string ImagePath { get; set; }

        [DataMember]
        public string Duration { get; set; }

        [DataMember]
        public string VehicleID { get; set; }

        [DataMember]
        public int KMStart { get; set; }

        [DataMember]
        public int KMEnd { get; set; }

        [DataMember]
        public string DistancebyGPS { get; set; }

    }

    [DataContract]
    public class mdlVisitTracking
    {
        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public string Customer { get; set; }

        [DataMember]
        public string Latitude { get; set; }

        [DataMember]
        public string Longitude { get; set; }

        [DataMember]
        public string Start { get; set; }

        [DataMember]
        public string End { get; set; }

        [DataMember]
        public string Duration { get; set; }

        [DataMember]
        public string StreetName { get; set; }

        [DataMember]
        public string EmployeeID { get; set; }

        [DataMember]
        public string WarehouseID { get; set; }
    }

    [DataContract]
    public class mdlKoordinatKunjungan
    {
        [DataMember]
        public string Date { get; set; }

        [DataMember]
        public string Latitude { get; set; }

        [DataMember]
        public string Longitude { get; set; }

        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public string VisitDate { get; set; }

        [DataMember]
        public string BranchID { get; set; }
    }

    //001
    [DataContract]
    public class mdlKoordinatKunjunganGudang
    {
        [DataMember]
        public string Date { get; set; }

        [DataMember]
        public string Latitude { get; set; }

        [DataMember]
        public string Longitude { get; set; }

        [DataMember]
        public string WarehouseID { get; set; }

        [DataMember]
        public string VisitDate { get; set; }

        [DataMember]
        public string BranchID { get; set; }
    }

}
