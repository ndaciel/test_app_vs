using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Model
{
    public class mdlDBDeliveryOrderDetail
    {
        public string DONumber { get; set; }

        public string ProductID { get; set; }

        public string UOM { get; set; }

        public int Quantity { get; set; }

        public int QuantityReal { get; set; }

        public string ProductGroup { get; set; }

        public string LotNumber { get; set; }

        public string ReasonID { get; set; }

        public string BoxID { get; set; }

        public string Line { get; set; }
    }
}
