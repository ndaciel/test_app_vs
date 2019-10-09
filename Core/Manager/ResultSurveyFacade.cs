using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;

namespace Core.Manager
{
    public class ResultSurveyFacade
    {
        public static Model.mdlResultList InsertResultSurvey(List<Model.mdlResultSurvey> lResultSurveyParam)
        {
            var mdlResultList = new List<Model.mdlResult>();
            var mdlResult = new Model.mdlResult();

            mdlResult.Result = Manager.DataFacade.DTSQLListInsert(lResultSurveyParam, "ResultSurvey");
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

        public static void DeleteResultSurvey(string surveyID)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
            };
            Manager.DataFacade.DTSQLCommand(@"Delete from ResultSurvey where SurveyID = '" + surveyID + "'", sp);


        }


        public static Model.mdlResultList InsertResultSurveyDetail(List<Model.mdlResultSurveyDetail> lResultSurveyDetailParam)
        {
            var mdlResultList = new List<Model.mdlResult>();

            var mdlResult = new Model.mdlResult();



            mdlResult.Result = Manager.DataFacade.DTSQLListInsert(lResultSurveyDetailParam, "ResultSurveyDetail");
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

        public static void DeleteResultSurveyDetail(string surveyID)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
            };



            Manager.DataFacade.DTSQLCommand(@"Delete from ResultSurveyDetail where SurveyID = '" + surveyID + "'", sp);


        }
    }
}
