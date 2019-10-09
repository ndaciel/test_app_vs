using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.IO;
using System.Configuration;
using System.Web;
using System.Transactions;

namespace Core.Manager
{
    public class LogVisitFacade
    {
        public static Model.mdlResultList InsertLogVisit(List<Model.mdlLogVisitParam> lParamlist)
        {
            var mdlResultList = new List<Model.mdlResult>();
            foreach (var lParam in lParamlist)
            {
                var mdlResult = new Model.mdlResult();



                List<SqlParameter> sp = new List<SqlParameter>()
                    {
                    new SqlParameter() {ParameterName = "@VisitID", SqlDbType = SqlDbType.NVarChar, Value = lParam.VisitID},
                    new SqlParameter() {ParameterName = "@CustomerID", SqlDbType = SqlDbType.NVarChar, Value = lParam.CustomerID},
                    new SqlParameter() {ParameterName = "@Date", SqlDbType = SqlDbType.NVarChar, Value = lParam.Date},
                    new SqlParameter() {ParameterName = "@Type", SqlDbType = SqlDbType.NVarChar, Value = lParam.Type},
                    
                    };

                string query = @"INSERT INTO LogVisit (VisitID, CustomerID, Date, Type) " +
                                            "VALUES (@VisitID, @CustomerID, @Date, @Type) "; //004

                mdlResult.Result = Manager.DataFacade.DTSQLVoidCommand(query, sp); //010

                if (mdlResult.Result == "1")
                {
                }
                else
                {
                    string ResultSubstring;

                    if (mdlResult.Result.Length > 500)
                    {
                        ResultSubstring = mdlResult.Result.Substring(0, 500);

                        mdlResult.Result = ResultSubstring;
                    }
                }

                mdlResultList.Add(mdlResult);
            }


            var mdlResultListnew = new Model.mdlResultList();
            mdlResultListnew.ResultList = mdlResultList;
            return mdlResultListnew;
        }
    }
}
