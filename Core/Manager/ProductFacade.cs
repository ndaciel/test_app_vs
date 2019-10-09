/* documentation
 * 001 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;


namespace Core.Manager
{
    public class ProductFacade:Base.Manager
    {

        public static List<Model.mdlProduct> LoadProduct(Model.mdlParam json)
        {
            var mdlProductList = new List<Model.mdlProduct>();
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@Date", SqlDbType = SqlDbType.DateTime, Value = Convert.ToDateTime(json.Date).Date },
                new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = json.EmployeeID },
                new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = json.BranchID }
            };


            DataTable dtProduct = Manager.DataFacade.DTSQLCommand(@"SELECT ProductID,ProductName,ProductWeight,UOM,DNR_Code,SAP_Code,Price FROM Product", sp);
            foreach (DataRow row in dtProduct.Rows)
            {
                var mdlProduct = new Model.mdlProduct();
                mdlProduct.ProductID = row["ProductID"].ToString();
                mdlProduct.ProductName = row["ProductName"].ToString();
                mdlProduct.ProductType = "";
                mdlProduct.ProductGroup = "";
                mdlProduct.ProductWeight = row["ProductWeight"].ToString();
                mdlProduct.UOM = row["UOM"].ToString();
                mdlProduct.ArticleNumber = "";
                mdlProductList.Add(mdlProduct);
            }

            //var mdlProductListnew = new Model.mdlProductList();
            //mdlProductListnew.ProductList = mdlProductList;

            return mdlProductList;
        }

        public static List<Model.mdlProductUOM> LoadProductUOM(List<Model.mdlDeliveryOrderDetail> listDODetail)
        {
            var mdlProductUOMList = new List<Model.mdlProductUOM>();

            List<SqlParameter> sp = new List<SqlParameter>();
            StringBuilder sb = new StringBuilder();
            int count = 1;

            foreach(var DO in listDODetail)
            {
                var sqlParameter = new SqlParameter();
                sqlParameter.ParameterName = "@ProducIDs" + count.ToString();
                if (DO == listDODetail.Last())
                {
                    sb.Append("@ProducIDs" + count.ToString());
                }
                else
                {
                    sb.Append("@ProducIDs" + count.ToString() + ",");
                }
                sqlParameter.SqlDbType = SqlDbType.NVarChar;
                sqlParameter.Value = DO.ProductID;
                sp.Add(sqlParameter);
                count++;
            }


            //live Code
            if (listDODetail.Count > 0)
            {
                DataTable dtProductUOM = Manager.DataFacade.DTSQLCommand(@"(select ProductID, UOM,BaseUOM,Quantity from ProductUOM WHERE ProductID IN (" + sb.ToString() + @"))
                                                                        union
                                                                     (select distinct ProductID,BaseuOM,BaseUOM,1 as Quantity from ProductUOM WHERE ProductID IN (" + sb.ToString() + "))", sp);
                foreach (DataRow row in dtProductUOM.Rows)
                {
                    var mdlProductUOM = new Model.mdlProductUOM();
                    mdlProductUOM.ProductID = row["ProductID"].ToString();
                    mdlProductUOM.UOM = row["UOM"].ToString();
                    mdlProductUOM.BaseUOM = row["BaseUOM"].ToString();
                    mdlProductUOM.Quantity = row["Quantity"].ToString();
                    mdlProductUOMList.Add(mdlProductUOM);
                }
            }
            return mdlProductUOMList;
        }
    }
}
