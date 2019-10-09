/* documentation
 *001 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Configuration;

namespace Core.Manager
{
    public class DailyMessageFacade : Base.Manager
    {

       

        public static List<Model.mdlDailyMsg> LoadDailyMessage2(Model.mdlParam json)
        {
            var mdlDailyMsgList = new List<Model.mdlDailyMsg>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@Date", SqlDbType = SqlDbType.DateTime, Value= DateTime.Now.Date },
                //new SqlParameter() {ParameterName = "@FinishDate", SqlDbType = SqlDbType.DateTime, Value = DateTime.Now.Date.AddDays(1) },
                new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = json.EmployeeID },
                new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = json.BranchID }

            };

            DataTable dtDailyMsg = DataFacade.DTSQLCommand(@"
                SELECT
	                a.MessageID,
	                a.MessageName,
	                a.MessageDesc,
	                CONCAT('//img//DailyMessage//', c.ImageBase64) as MessageImg,
	                a.CreatedBy,
	                a.Date
                FROM (
	                SELECT DISTINCT
		                a.MessageID
	                FROM DailyMessage a
	                INNER JOIN DailyMessageDetail b ON a.MessageID = b.MessageID 
                    WHERE a.Date <= @Date AND a.EndDate >= @Date AND b.BranchID = @BranchID AND b.EmployeeID = @EmployeeID
                ) AS qr
                INNER JOIN DailyMessage a ON a.MessageID = qr.MessageID
                LEFT JOIN CustomerImage c ON c.ImageID = a.MessageImg", sp);

            foreach (DataRow row in dtDailyMsg.Rows)
            {
                var mdlDailyMsg = new Model.mdlDailyMsg();

                mdlDailyMsg.MessageID = row["MessageID"].ToString();
                mdlDailyMsg.MessageName = row["MessageName"].ToString();
                mdlDailyMsg.MessageDesc = row["MessageDesc"].ToString();
                mdlDailyMsg.MessageImg = row["MessageImg"].ToString();
                mdlDailyMsg.BranchID = "";
                mdlDailyMsg.EndDate = "";
                mdlDailyMsg.Date = Convert.ToDateTime(row["Date"]).ToString("yyyy-MM-dd hh:mm:ss");
                mdlDailyMsg.CreatedBy = row["CreatedBy"].ToString();

                mdlDailyMsgList.Add(mdlDailyMsg);
            }
            return mdlDailyMsgList;
        }

        public static List<Model.mdlDailyMsg> LoadDailyMessage(Model.mdlParam json)
        {
            var mdlDailyMsgList = new List<Model.mdlDailyMsg>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@Date", SqlDbType = SqlDbType.DateTime, Value= DateTime.Now.Date },
                new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = json.BranchID }
            };

            DataTable dtDailyMsg = DataFacade.DTSQLCommand(@"SELECT MessageID
                                                                            ,MessageName
                                                                            ,MessageDesc
                                                                            ,MessageImg
                                                                            ,CreatedBy
                                                                            ,Date 
                                                                    From DailyMessage WHERE Date <= @Date AND EndDate >= @Date AND BranchID = @BranchID", sp);

            foreach (DataRow row in dtDailyMsg.Rows)
            {
                var mdlDailyMsg = new Model.mdlDailyMsg();
                mdlDailyMsg.MessageID = row["MessageID"].ToString();
                mdlDailyMsg.MessageName = row["MessageName"].ToString();
                mdlDailyMsg.MessageDesc = row["MessageDesc"].ToString();
                mdlDailyMsg.MessageImg = row["MessageImg"].ToString();
                mdlDailyMsg.Date = Convert.ToDateTime(row["Date"]).ToString("yyyy-MM-dd");
                mdlDailyMsg.CreatedBy = row["CreatedBy"].ToString();

                mdlDailyMsgList.Add(mdlDailyMsg);
            }
            return mdlDailyMsgList;
        }

        public static List<Model.mdlDailyMsg> GetSearch(string keyword, DateTime keyworddate, string branchid)
        {
            branchid = StringFacade.NormalizedBranch(branchid);
            var mdlDailyMsgList = new List<Model.mdlDailyMsg>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@ValidDate", SqlDbType = SqlDbType.Date, Value= keyworddate},
                new SqlParameter() {ParameterName = "@Keyword", SqlDbType = SqlDbType.NVarChar, Value = "%"+keyword+"%" },
                new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = "%"+branchid+"%" }
            };

            DataTable dtDailyMsg = DataFacade.DTSQLCommand(@"SELECT distinct(a.MessageID)
                                                                            ,a.MessageName
                                                                            ,a.Date
                                                                            ,a.EndDate
                                                                            ,a.CreatedBy 
                                                                    From DailyMessage a
                                                                    LEFT JOIN DailyMessageDetail b on b.MessageID = a.MessageID
                                                                    WHERE (a.Date <= @ValidDate AND a.EndDate >= @ValidDate) AND (a.MessageID like @Keyword OR MessageName like @Keyword)
                                                                            AND a.MessageID IN (SELECT MessageID from DailyMessageDetail where BranchID LIKE @BranchID)", sp);

            foreach (DataRow row in dtDailyMsg.Rows)
            {
                var mdlDailyMsg = new Model.mdlDailyMsg();
                mdlDailyMsg.MessageID = row["MessageID"].ToString();
                mdlDailyMsg.MessageName = row["MessageName"].ToString();
                mdlDailyMsg.Date = Convert.ToDateTime(row["Date"]).ToString("dd/MM/yyyy");
                mdlDailyMsg.EndDate = Convert.ToDateTime(row["EndDate"]).ToString("dd/MM/yyyy");
                mdlDailyMsg.CreatedBy = row["CreatedBy"].ToString();

                mdlDailyMsgList.Add(mdlDailyMsg);
            }
            return mdlDailyMsgList;
        }

        public static List<Model.mdlDailyMsg> GetSearchbySomeBranch(string keyword, DateTime keyworddate, string branchid)
        {

            branchid = StringFacade.NormalizedBranch(branchid);
            var mdlDailyMsgList = new List<Model.mdlDailyMsg>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@ValidDate", SqlDbType = SqlDbType.Date, Value= keyworddate},
                new SqlParameter() {ParameterName = "@Keyword", SqlDbType = SqlDbType.NVarChar, Value = "%"+keyword+"%" },
                new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = branchid }
            };

            DataTable dtDailyMsg = DataFacade.DTSQLCommand(@"SELECT distinct(a.MessageID)
                                                                            ,a.MessageName
                                                                            ,a.Date
                                                                            ,a.EndDate
                                                                            ,a.CreatedBy 
                                                                    From DailyMessage a
                                                                    LEFT JOIN DailyMessageDetail b on b.MessageID = a.MessageID
                                                                    WHERE (a.Date <= @ValidDate AND a.EndDate >= @ValidDate) AND (a.MessageID like @Keyword OR MessageName like @Keyword)
                                                                    AND a.MessageID IN (SELECT MessageID from DailyMessageDetail where BranchID IN ("+branchid+"))", sp);

            foreach (DataRow row in dtDailyMsg.Rows)
            {
                var mdlDailyMsg = new Model.mdlDailyMsg();
                mdlDailyMsg.MessageID = row["MessageID"].ToString();
                mdlDailyMsg.MessageName = row["MessageName"].ToString();
                mdlDailyMsg.Date = Convert.ToDateTime(row["Date"]).ToString("dd/MM/yyyy");
                mdlDailyMsg.EndDate = Convert.ToDateTime(row["EndDate"]).ToString("dd/MM/yyyy");
                mdlDailyMsg.CreatedBy = row["CreatedBy"].ToString();

                mdlDailyMsgList.Add(mdlDailyMsg);
            }
            return mdlDailyMsgList;
        }

        public static List<Model.mdlDailyMsgRole> GetData(string keyword, string keyword2, string branchid)
        {
            var mdlDailyMsgRoleList = new List<Model.mdlDailyMsgRole>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@MessageID", SqlDbType = SqlDbType.NVarChar, Value= keyword},
                new SqlParameter() {ParameterName = "@Keyword", SqlDbType = SqlDbType.NVarChar, Value= "%"+keyword2+"%"},
                new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value= "%"+branchid+"%"}
            };

            DataTable dtDailyMsgDetail = DataFacade.DTSQLCommand(@"SELECT a.MessageID
                                                                            ,a.EmployeeID
                                                                                ,b.EmployeeName
                                                                            ,a.BranchID
                                                                    From DailyMessageDetail a
                                                                    INNER JOIN Employee b ON b.EmployeeID = a.EmployeeID
                                                                    WHERE (a.MessageID = @MessageID) AND (a.EmployeeID LIKE @Keyword OR b.EmployeeName LIKE @Keyword) AND a.BranchID LIKE @BranchID AND a.EmployeeID <> '' ORDER BY BranchID", sp);

            foreach (DataRow row in dtDailyMsgDetail.Rows)
            {
                var mdlDailyMsgRole = new Model.mdlDailyMsgRole();
                mdlDailyMsgRole.MessageID = row["MessageID"].ToString();
                mdlDailyMsgRole.EmployeeID = row["EmployeeID"].ToString();
                mdlDailyMsgRole.BranchID = row["BranchID"].ToString();
                mdlDailyMsgRole.EmployeeName = row["EmployeeName"].ToString();

                mdlDailyMsgRoleList.Add(mdlDailyMsgRole);
            }
            return mdlDailyMsgRoleList;
        }

        public static List<Model.mdlDailyMsgRole> GetDataBySomeBranch(string keyword, string keyword2, string branchid)
        {
            var mdlDailyMsgRoleList = new List<Model.mdlDailyMsgRole>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@MessageID", SqlDbType = SqlDbType.NVarChar, Value= keyword},
                new SqlParameter() {ParameterName = "@Keyword", SqlDbType = SqlDbType.NVarChar, Value= "%"+keyword2+"%"},
                new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value= "%"+branchid+"%"}
            };

            DataTable dtDailyMsgDetail = DataFacade.DTSQLCommand(@"SELECT a.MessageID
                                                                            ,a.EmployeeID
                                                                                ,b.EmployeeName
                                                                            ,a.BranchID
                                                                    From DailyMessageDetail a
                                                                    INNER JOIN Employee b ON b.EmployeeID = a.EmployeeID
                                                                    WHERE (a.MessageID = @MessageID) AND (a.EmployeeID LIKE @Keyword OR b.EmployeeName LIKE @Keyword) AND a.BranchID IN ("+branchid+") AND a.EmployeeID <> '' ORDER BY BranchID", sp);

            foreach (DataRow row in dtDailyMsgDetail.Rows)
            {
                var mdlDailyMsgRole = new Model.mdlDailyMsgRole();
                mdlDailyMsgRole.MessageID = row["MessageID"].ToString();
                mdlDailyMsgRole.EmployeeID = row["EmployeeID"].ToString();
                mdlDailyMsgRole.BranchID = row["BranchID"].ToString();
                mdlDailyMsgRole.EmployeeName = row["EmployeeName"].ToString();

                mdlDailyMsgRoleList.Add(mdlDailyMsgRole);
            }
            return mdlDailyMsgRoleList;
        }


        public static Model.DailyMessage GetDailyMessageByID(string MsgID)
        {
            var DailyMsg = DataContext.DailyMessages.FirstOrDefault(fld => fld.MessageID.Equals(MsgID));
            return DailyMsg;
        }

        public static void DeleteDailyMessage(string MsgID)
        {
            var DailyMsg = GetDailyMessageByID(MsgID);

            DataContext.DailyMessages.DeleteOnSubmit(DailyMsg);
            DataContext.SubmitChanges();
        }

        public static void DeleteMessageDetail(string MsgID, string EmployeeID, string BranchID)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
                    {
                        new SqlParameter() {ParameterName = "@MessageID", SqlDbType = SqlDbType.NVarChar, Value = MsgID},
                        new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = EmployeeID},
                        new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = BranchID}
                    };

            string query = @"DELETE FROM DailyMessageDetail WHERE MessageID = @MessageID AND EmployeeID = @EmployeeID 
                                        AND BranchID = @BranchID";

            Manager.DataFacade.DTSQLVoidCommand(query, sp);

            return;
        }


        public static void UpdateDailyMessage(string MsgID, string MsgNm, DateTime date, DateTime enddate, string description, string Createdby, string MsgImg, string MsgImgPath)
        {
            var DailyMsg = GetDailyMessageByID(MsgID);
            DailyMsg.MessageID = MsgID;
            DailyMsg.MessageName = MsgNm;
            DailyMsg.Date = date;
            DailyMsg.EndDate = enddate;
            DailyMsg.MessageDesc = description;
            DailyMsg.MessageImg = MsgImg;
            DailyMsg.ImgPath = MsgImgPath;
            DailyMsg.CreatedBy = Createdby;
            DataContext.SubmitChanges();
        }

 
        public static void InsertDailyMessage(string MsgID, string MsgNm, DateTime date, DateTime enddate, string description, string Createdby, string MsgImg, string MsgImgPath)
        {
            Model.DailyMessage DailyMsg = new Model.DailyMessage();
            DailyMsg.MessageID = MsgID;
            DailyMsg.MessageName = MsgNm;
            DailyMsg.Date = date;
            DailyMsg.EndDate = enddate;
            DailyMsg.MessageDesc = description;
            DailyMsg.CreatedBy = Createdby;
            DailyMsg.MessageImg = MsgImg;
            DailyMsg.ImgPath = MsgImgPath;

            DataContext.DailyMessages.InsertOnSubmit(DailyMsg);
            DataContext.SubmitChanges();
        }

        public static void InsertDailyMessageDetail(string MsgID, string BranchID, List<string> lEmployeeIDlist)
        {
            foreach (var lEmployeeID in lEmployeeIDlist)
            {
                string lParamEmployee = string.Empty;

                lParamEmployee = lEmployeeID;

                bool lCheckDMDetail = CheckInsert(MsgID, BranchID, lParamEmployee);
                if (lCheckDMDetail == false)
                {
                }
                else
                {
                    List<SqlParameter> sp = new List<SqlParameter>()
                    {
                        new SqlParameter() {ParameterName = "@MessageID", SqlDbType = SqlDbType.NVarChar, Value = MsgID},
                        new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = BranchID},
                        new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = lParamEmployee}
                    };

                    string query = @"INSERT INTO DailyMessageDetail (MessageID, EmployeeID, BranchID) " +
                                                "VALUES (@MessageID, @EmployeeID , @BranchID)";

                    Manager.DataFacade.DTSQLVoidCommand(query, sp);
                }
            }

            return;
        }

        public static bool CheckInsert(string lMessageID, string lBranchID, string lEmployeeID)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName = "@MessageID", SqlDbType = SqlDbType.NVarChar, Value = lMessageID },
               new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = lEmployeeID },
               new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = lBranchID },
            };

            DataTable dtDMDetail = Manager.DataFacade.DTSQLCommand(@"SELECT TOP 1 * 
                                                                   FROM DailyMessageDetail 
                                                                   WHERE MessageID = @MessageID AND EmployeeID = @EmployeeID AND BranchID = @BranchID", sp);
            bool lCheck = false;
            if (dtDMDetail.Rows.Count == 0)
            {
                lCheck = true;
            }

            return lCheck;
        }

    }
}
