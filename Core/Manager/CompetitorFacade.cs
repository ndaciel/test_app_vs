using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Core.Manager
{
    public class CompetitorFacade
    {
        public static List<Model.mdlCompetitor> LoadCompetitor(Model.mdlParam param)
        {
            var listCompetitor = new List<Model.mdlCompetitor>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
               
            };

            string sql = "SELECT CompetitorID,CompetitorName FROM Competitor";
            DataTable dt = DataFacade.DTSQLCommand(sql,sp);

            foreach (DataRow row in dt.Rows)
            {
                var model = new Model.mdlCompetitor();
                model.CompetitorID = row["CompetitorID"].ToString();
                model.CompetitorName = row["CompetitorName"].ToString();
                listCompetitor.Add(model);

            }

            return listCompetitor;
            

        }

        public static List<Model.mdlCompetitorActivity> LoadCompetitorActivity(Model.mdlParam param)
        {
            var listCompetitorActivity = new List<Model.mdlCompetitorActivity>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {

            };

            string sql = "SELECT [ActivityID],[CompetitorID],[ActivityName] FROM [CompetitorActivity]";
            DataTable dt = DataFacade.DTSQLCommand(sql, sp);

            foreach (DataRow row in dt.Rows)
            {
                var model = new Model.mdlCompetitorActivity();
                model.ActivityID = row["ActivityID"].ToString();
                model.CompetitorID = row["CompetitorID"].ToString();
                model.ActivityName = row["ActivityName"].ToString();
                listCompetitorActivity.Add(model);

            }

            return listCompetitorActivity;


        }

        public static List<Model.mdlCompetitorProduct> LoadCompetitorProduct(Model.mdlParam param,List<Model.mdlCompetitor> listCompetitor)
        {

            List<SqlParameter> sp = new List<SqlParameter>();
            StringBuilder sb = new StringBuilder();
            int count = 1;


            foreach (var com in listCompetitor)
            {

                var sqlParameter = new SqlParameter();
                sqlParameter.ParameterName = "@ComID" + count.ToString();
                if (com == listCompetitor.Last())
                {
                    sb.Append("@ComID" + count.ToString());
                }
                else
                {
                    sb.Append("@ComID" + count.ToString() + ",");
                }

                sqlParameter.SqlDbType = SqlDbType.NVarChar;
                sqlParameter.Value = com.CompetitorID;
                sp.Add(sqlParameter);
                count++;
                
            }

            var listCompetitorProduct = new List<Model.mdlCompetitorProduct>();
            string sql = @"SELECT CompetitorID,CompetitorProductID,CompetitorProductName FROM CompetitorProduct WHERE CompetitorID IN ("+sb.ToString()+")";
            DataTable dt = DataFacade.DTSQLCommand(sql, sp);

            foreach (DataRow row in dt.Rows)
            {
                var model = new Model.mdlCompetitorProduct();
                model.CompetitorID = row["CompetitorID"].ToString();
                model.CompetitorProductID = row["CompetitorProductID"].ToString();
                model.CompetitorProductName = row["CompetitorProductName"].ToString();
                listCompetitorProduct.Add(model);

            }

            return listCompetitorProduct;
        }




    }
}
