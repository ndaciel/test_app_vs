using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Core.Manager
{
    public class SisaStockTypeFacade
    {
        public static List<Model.mdlSisaStockType> LoadSisaStockType(Model.mdlParam param)
        {
            var listSisaStockType = new List<Model.mdlSisaStockType>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {

            };

            string sql = "SELECT SisaStockTypeID, Description, Category, Seq FROM SisaStockType";
            DataTable dt = DataFacade.DTSQLCommand(sql, sp);

            foreach (DataRow row in dt.Rows)
            {
                var model = new Model.mdlSisaStockType();
                model.SisaStockTypeID = row["SisaStockTypeID"].ToString();
                model.Description = row["Description"].ToString();
                model.Category = row["Category"].ToString();
                model.Seq = row["Seq"].ToString();
                listSisaStockType.Add(model);

            }

            return listSisaStockType;


        }
    }
}
