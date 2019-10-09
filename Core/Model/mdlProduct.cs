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

    [DataContract]
    public class mdlProduct
    {
        [DataMember]
        public string ProductID { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public string ProductType { get; set; }

        [DataMember]
        public string ProductGroup { get; set; }

        [DataMember]
        public string ProductWeight { get; set; }

        [DataMember]
        public string UOM { get; set; }

        [DataMember]
        public string ArticleNumber { get; set; }

    }

    [DataContract]
    public class mdlProductParam
    {
        [DataMember]
        public string ProductID { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public string ProductType { get; set; }

        [DataMember]
        public string ProductGroup { get; set; }

        [DataMember]
        public string ProductWeight { get; set; }

        [DataMember]
        public string UOM { get; set; }

        [DataMember]
        public string ArticleNumber { get; set; }
    }


    [DataContract]
    public class mdlProductUOM
    {
        [DataMember]
        public string ProductID { get; set; }

        //christopher
        [DataMember]
        public string BaseUOM { get; set; }
        //christopher
        [DataMember]
        public string Quantity { get; set; }

        [DataMember]
        public string UOM { get; set; }
    }


    public class mdlProductList
    {
        public List<mdlProduct> ProductList { get; set; }
    }


    public class mdlProductUOMList
    {
        public List<mdlProductUOM> ProductUOMList { get; set; }
    }


}
