using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Core.Manager
{
    public class POSMFacade
    {
        public static List<Model.mdlPOSM> LoadPOSMProduct()
        {
            var listPOSMProduct = new List<Model.mdlPOSM>();
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                
            };

            DataTable dtMenu = Manager.DataFacade.DTSQLCommand(@"SELECT [POSMID],[POSMName],[CreatedDate],[CreatedBy] FROM [POSMProduct]", sp);

            foreach (DataRow row in dtMenu.Rows)
            {
                var model = new Model.mdlPOSM();
                model.POSMID = row["POSMID"].ToString();
                model.POSMName = row["POSMName"].ToString();
                listPOSMProduct.Add(model);
            }

            return listPOSMProduct;
        }
    }
}
