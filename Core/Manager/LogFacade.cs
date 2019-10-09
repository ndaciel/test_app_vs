/* documentation
 *001 
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace Core.Manager
{
    public class LogFacade
    {
        
        public static string InsertLog(string lFunctionName, string lCreatedBy, string lResult, string branchID, string deviceID, string reason, string size)
        {
            List<SqlParameter> sp = new List<SqlParameter>()

            {
                
                new SqlParameter() {ParameterName = "@Createdby", SqlDbType = SqlDbType.VarChar, Value = lCreatedBy},
                new SqlParameter() {ParameterName = "@TimeStamp", SqlDbType = SqlDbType.DateTime, Value = DateTime.Now},
                new SqlParameter() {ParameterName = "@FunctionName", SqlDbType = SqlDbType.VarChar, Value = lFunctionName},
                new SqlParameter() {ParameterName = "@Result", SqlDbType = SqlDbType.VarChar, Value = lResult},
                new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.VarChar, Value = branchID},
                new SqlParameter() {ParameterName = "@DeviceID", SqlDbType = SqlDbType.VarChar, Value = deviceID},
                new SqlParameter() {ParameterName = "@Reason", SqlDbType = SqlDbType.VarChar, Value = reason},
                new SqlParameter() {ParameterName = "@Size", SqlDbType = SqlDbType.VarChar, Value = size}
            };

            string result = DataFacade.DTSQLVoidCommand("INSERT INTO Log_Service (FunctionName,Timestamp,CreatedBy,Result, BranchID, DeviceID, Reason, Size) VALUES (@FunctionName,@Timestamp,@Createdby,@Result,@BranchID, @DeviceID, @Reason, @Size)", sp);

            return result;
        }

        public static bool CheckLogLocationReqbyDate(string lDate)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName = "@Date", SqlDbType = SqlDbType.NVarChar, Value = lDate },
            };

            DataTable dtLogLocationReq = Manager.DataFacade.DTSQLCommand(@"SELECT TOP 1 Date
                                                                   FROM Log_LocationRequest 
                                                                   WHERE Date = @Date", sp);
            bool lCheck = false;
            if (dtLogLocationReq.Rows.Count == 0)
            {
                lCheck = true;
            }

            return lCheck;
            //klau true berarti datanya belum ada
        }

        public static List<Model.mdlLogLocationReq> getLog_LocationReq(string lDate)
        {
            var mdlLogLocationReqList = new List<Model.mdlLogLocationReq>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@Date", SqlDbType = SqlDbType.NVarChar, Value= lDate },

            };

            DataTable dtLog = DataFacade.DTSQLCommand(@"SELECT TOP 1 Date,RequestCounter From Log_LocationRequest
                                                        WHERE Date = @Date", sp);

            foreach (DataRow row in dtLog.Rows)
            {
                var mdlLogLocationReq = new Model.mdlLogLocationReq();

                mdlLogLocationReq.Date = row["Date"].ToString();
                mdlLogLocationReq.RequestCounter = row["RequestCounter"].ToString();

                mdlLogLocationReqList.Add(mdlLogLocationReq);
            }

            return mdlLogLocationReqList;
        }

        public static string InsertLogSMS_Service(String lUrl, String lBody, String lResult)
        {
            var resObject = JsonConvert.DeserializeObject<Model.mdlResultSmsGateway>(lResult);
            string newIDLogSMS = GenerateIDFacade.GenerateIDSMSLog(String.Format("LSMS/{0}/", DateTime.Now.ToString("yyyyMM")));

            List<SqlParameter> sp = new List<SqlParameter>() {
                new SqlParameter() {ParameterName = "@LogSMSID", SqlDbType = SqlDbType.VarChar, Value = newIDLogSMS},
                new SqlParameter() {ParameterName = "@Url", SqlDbType = SqlDbType.VarChar, Value = lUrl},
                new SqlParameter() {ParameterName = "@Body", SqlDbType = SqlDbType.VarChar, Value = lBody},
                new SqlParameter() {ParameterName = "@ResultCode", SqlDbType = SqlDbType.VarChar, Value = resObject.code},
                new SqlParameter() {ParameterName = "@ResultStatus", SqlDbType = SqlDbType.VarChar, Value = resObject.status},
                new SqlParameter() {ParameterName = "@ResultMessage", SqlDbType = SqlDbType.VarChar, Value = resObject.message},
                new SqlParameter() {ParameterName = "@ResultMsgid", SqlDbType = SqlDbType.VarChar, Value = resObject.msgid}

            };

            string result = DataFacade.DTSQLVoidCommand("" +
                "INSERT INTO Log_Sms (LogSMSID, Url, Body, ResultCode, ResultStatus, ResultMessage, ResultMsgid) " + 
                "VALUES (@LogSMSID, @Url, @Body, @ResultCode, @ResultStatus, @ResultMessage, @ResultMsgid) ", sp);

            return result;
        }

    }
}
