using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Core.Manager
{
    public class DepositTypeFacade
    {
        public static List<Model.mdlDepositType> LoadDepositType()
        {
            var listDepositType = new List<Model.mdlDepositType>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {

            };

            string sql = "SELECT DepositTypeID, DepositTypeName FROM DepositType";
            DataTable dt = DataFacade.DTSQLCommand(sql, sp);

            foreach (DataRow row in dt.Rows)
            {
                var model = new Model.mdlDepositType();
                model.DepositTypeID = row["DepositTypeID"].ToString();
                model.DepositTypeName = row["DepositTypeName"].ToString();
                listDepositType.Add(model);

            }

            return listDepositType;


        }
    }
}
