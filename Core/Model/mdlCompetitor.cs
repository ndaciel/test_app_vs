using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Model
{
    public class mdlCompetitor
    {
        public string CompetitorID { get; set; }
        public string CompetitorName { get; set; }
    }

    public class mdlCompetitorProduct
    {
        public string CompetitorID { get; set; }
        public string CompetitorProductID { get; set; }
        public string CompetitorProductName { get; set; }

    }

    public class mdlCompetitorActivity
    {
        public string ActivityID { get; set; }
        public string CompetitorID { get; set; }
        public string ActivityName { get; set; }
    }


}
