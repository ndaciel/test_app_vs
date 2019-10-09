using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Core.Manager
{
    public class QuestionFacade
    {
        public static List<Model.mdlQuestion> LoadQuestion(Model.mdlParam json)
        {
            var listQuestion = new List<Model.mdlQuestion>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@RoleID", SqlDbType = SqlDbType.VarChar, Value = json.Role}
            };

            string sql = @"SELECT a.QuestionID, a.QuestionText, a.AnswerTypeID, a.IsSubQuestion, a.Sequence, a.QuestionSetID, a.QuestionCategoryID, 
                            a.AnswerID, a.No, a.Mandatory, a.IsActive 
                            FROM Question a
                            INNER JOIN Question_Role b ON b.QuestionID = a.QuestionID
                            WHERE b.RoleID = @RoleID";
            DataTable dt = DataFacade.DTSQLCommand(sql, sp);

            foreach (DataRow row in dt.Rows)
            {
                var model = new Model.mdlQuestion();
                model.QuestionID = row["QuestionID"].ToString();
                model.QuestionText = row["QuestionText"].ToString();
                model.AnswerTypeID = row["AnswerTypeID"].ToString();
                model.IsSubQuestion = Convert.ToBoolean(row["IsSubQuestion"].ToString());
                model.Sequence = Convert.ToInt32(row["Sequence"].ToString());
                model.QuestionSetID = row["QuestionSetID"].ToString();
                model.QuestionCategoryID = row["QuestionCategoryID"].ToString();
                model.AnswerID = row["AnswerID"].ToString();
                model.No = row["No"].ToString();
                model.Mandatory = Convert.ToBoolean(row["Mandatory"].ToString());
                model.IsActive = Convert.ToBoolean(row["IsActive"].ToString());
                listQuestion.Add(model);

            }

            return listQuestion;


        }

        public static List<Model.mdlQuestion_Set> LoadQuestionSet()
        {
            var listQuestionSet = new List<Model.mdlQuestion_Set>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {

            };

            string sql = "SELECT QuestionSetID, QuestionSetText FROM Question_Set WHERE IsActive=1";
            DataTable dt = DataFacade.DTSQLCommand(sql, sp);

            foreach (DataRow row in dt.Rows)
            {
                var model = new Model.mdlQuestion_Set();
                model.QuestionSetID = row["QuestionSetID"].ToString();
                model.QuestionSetText = row["QuestionSetText"].ToString();

                listQuestionSet.Add(model);

            }

            return listQuestionSet;


        }

        public static List<Model.mdlQuestion_Category> LoadQuestionCategory()
        {
            var listQuestionCategory = new List<Model.mdlQuestion_Category>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {

            };

            string sql = "SELECT QuestionCategoryID, QuestionCategoryText FROM Question_Category";
            DataTable dt = DataFacade.DTSQLCommand(sql, sp);

            foreach (DataRow row in dt.Rows)
            {
                var model = new Model.mdlQuestion_Category();
                model.QuestionCategoryID = row["QuestionCategoryID"].ToString();
                model.QuestionCategoryText = row["QuestionCategoryText"].ToString();

                listQuestionCategory.Add(model);

            }

            return listQuestionCategory;


        }
    }
}
