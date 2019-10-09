using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Core.Manager
{
    public class AnswerFacade
    {
        public static List<Model.mdlAnswer> LoadAnswer()
        {
            var listAnswer = new List<Model.mdlAnswer>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {

            };

            string sql = "SELECT AnswerID, AnswerText, QuestionID, SubQuestion, IsSubQuestion, Sequence, No, IsActive FROM Answer";
            DataTable dt = DataFacade.DTSQLCommand(sql, sp);

            foreach (DataRow row in dt.Rows)
            {
                var model = new Model.mdlAnswer();
                model.AnswerID = row["AnswerID"].ToString();
                model.AnswerText = row["AnswerText"].ToString();
                model.QuestionID = row["QuestionID"].ToString();
                model.SubQuestion = Convert.ToBoolean(row["SubQuestion"].ToString());
                model.IsSubQuestion = Convert.ToBoolean(row["IsSubQuestion"].ToString());
                model.Sequence = Convert.ToInt32(row["Sequence"].ToString());
                model.No = row["No"].ToString();
                model.IsActive = Convert.ToBoolean(row["IsActive"].ToString());
                listAnswer.Add(model);

            }

            return listAnswer;


        }


        public static List<Model.mdlAnswer_Type> LoadAnswerType()
        {
            var listAnswerType = new List<Model.mdlAnswer_Type>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {

            };

            string sql = "SELECT AnswerTypeID, AnswerTypeText FROM AnswerType";
            DataTable dt = DataFacade.DTSQLCommand(sql, sp);

            foreach (DataRow row in dt.Rows)
            {
                var model = new Model.mdlAnswer_Type();
                model.AnswerTypeID = row["AnswerTypeID"].ToString();
                model.AnswerTypeText = row["AnswerTypeText"].ToString();

                listAnswerType.Add(model);

            }

            return listAnswerType;


        }
    }
}
