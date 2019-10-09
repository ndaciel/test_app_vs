using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Core.Manager
{
    public class CompetitorActivityTypeFacade
    {
        public static List<Model.mdlCompetitorActivityType> LoadCompetitorActivityType(Model.mdlParam param)
        {
            var listCompetitorActivityType = new List<Model.mdlCompetitorActivityType>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {

            };

            string sql = "SELECT CompetitorActivityTypeID, Description, Category, Seq FROM CompetitorActivityType";
            DataTable dt = DataFacade.DTSQLCommand(sql, sp);

            foreach (DataRow row in dt.Rows)
            {
                var model = new Model.mdlCompetitorActivityType();
                model.CompetitorActivityTypeID = row["CompetitorActivityTypeID"].ToString();
                model.Description = row["Description"].ToString();
                model.Category = row["Category"].ToString();
                model.Seq = row["Seq"].ToString();
                listCompetitorActivityType.Add(model);

            }

            return listCompetitorActivityType;


        }
    }
}
