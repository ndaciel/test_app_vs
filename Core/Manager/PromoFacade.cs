using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Core.Manager
{
    public class PromoFacade
    {

        public static List<Model.mdlPromo> LoadPromo(Model.mdlParam param)
        {
            var listPromo = new List<Model.mdlPromo>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {

            };

            string sql = "SELECT PromoID,PromoName, PromoCategory FROM Promo";
            DataTable dt = DataFacade.DTSQLCommand(sql, sp);

            foreach (DataRow row in dt.Rows)
            {
                var model = new Model.mdlPromo();
                model.PromoID = row["PromoID"].ToString();
                model.PromoName = row["PromoName"].ToString();
                model.PromoCategory = row["PromoCategory"].ToString();
                listPromo.Add(model);

            }

            return listPromo;


        }
    }
}
