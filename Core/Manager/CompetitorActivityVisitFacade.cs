using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;

namespace Core.Manager
{
    public class CompetitorActivityVisitFacade
    {
        public static Model.mdlResultList InsertCompetitorActivityVisit(List<Model.mdlCompetitorActivityVisit> lCompetitorActivityVisitParam)
        {
            var mdlResultList = new List<Model.mdlResult>();

            var mdlResult = new Model.mdlResult();



            mdlResult.Result = Manager.DataFacade.DTSQLListInsert(lCompetitorActivityVisitParam, "CompetitorActivityVisit");
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



            var mdlResultListnew = new Model.mdlResultList();
            mdlResultListnew.ResultList = mdlResultList;
            return mdlResultListnew;
        }

        public static void DeleteCompetitorActivityVisit(string visitID)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
            };



            Manager.DataFacade.DTSQLCommand(@"Delete from CompetitorActivityVisit where VisitID = '" + visitID + "'", sp);


        }
    }
}
