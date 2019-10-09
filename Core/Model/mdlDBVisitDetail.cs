using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Model
{
    public class mdlDBVisitDetail
    {

        public string VisitID { get; set; }


        public string CustomerID { get; set; }


        public string WarehouseID { get; set; }


        public bool isStart { get; set; }


        public bool isFinish { get; set; }

        public DateTime StartDate { get; set; }


        public DateTime EndDate { get; set; }


        public string ReasonID { get; set; }

        public string ReasonDescription { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public int isDeliver { get; set; }
        public bool isInRange { get; set; }

        public double Distance { get; set; }

        public bool isInRangeCheckout { get; set; }

        public string Duration { get; set; }

        public double DistanceCheckout { get; set; }

        public string LatitudeCheckOut { get; set; }

        public string LongitudeCheckOut { get; set; }

        public bool isVisit { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string LastUpdatedBy { get; set; }

        public DateTime LastUpdatedDate { get; set; }

        public string ReasonSequence { get; set; }
    }

    public class mdlDBVisitDetail2
    {

        public string VisitID { get; set; }


        public string CustomerID { get; set; }


        public string WarehouseID { get; set; }


        public bool isStart { get; set; }

        public bool isFinish { get; set; }

        public bool isVisit { get; set; }

        public string StartDate { get; set; }


        public string EndDate { get; set; }


        public string ReasonID { get; set; }

        public string ReasonDescription { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public int isDeliver { get; set; }
        public bool isInRange { get; set; }

        public double Distance { get; set; }

        public bool isInRangeCheckout { get; set; }

        public string Duration { get; set; }

        public double DistanceCheckout { get; set; }

        public string LatitudeCheckOut { get; set; }

        public string LongitudeCheckOut { get; set; }

        public string CreatedBy { get; set; }

        public string CreatedDate { get; set; }

        public string LastUpdatedBy { get; set; }

        public string LastUpdatedDate { get; set; }

        public int Seq { get; set; }
    }
}
