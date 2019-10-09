using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Core.Manager
{
    public class ProductUOMFacade
    {
        public static String ConvertMultiUOM(string productID, int Quantity)
        {
            //linq code
            //var customerList = DataContext.Customers.Where(fld => fld.PlantID.Equals(json.BranchID)).ToList();

            var mdlBranchListnew = new Model.mdlBranchList();
            var mdlBranchList = new List<Model.mdlBranch>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
                  new SqlParameter() {ParameterName = "@ProductID", SqlDbType = SqlDbType.NVarChar, Value = productID }
            };

            DataTable dtProductUOM = Manager.DataFacade.DTSQLCommand("SELECT PRODUCTID, UOM, BASEUOM, QUANTITY FROM PRODUCTUOM WHERE PRODUCTID=@ProductID order by quantity desc", sp);

            int sisa = Quantity;
            string result = "";
            string baseUOM = "";
            foreach (DataRow drProductUOM in dtProductUOM.Rows)
            {
                baseUOM = drProductUOM["BaseUOM"].ToString();
                int qtyUOM = Convert.ToInt32(drProductUOM["Quantity"]);
                if (sisa >= qtyUOM)
                {
                    result = (sisa / qtyUOM).ToString() + " " + drProductUOM["UOM"];
                    sisa = sisa % qtyUOM;
                }
                else if (sisa == 0)
                {
                    result = "0";
                }
            }
            if (sisa > 0)
                result = result + " " + sisa.ToString() + " " + baseUOM; 

            
            return result;
        }
    }
}
