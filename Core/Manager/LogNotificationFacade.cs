using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Core.Manager
{
    public class LogNotificationFacade
    {
        public static string InsertAndroidKeyLog(string notificationID, string employeeID, string branchID, string action)
        {
            

            List<SqlParameter> sp = new List<SqlParameter>()
            {
                 new SqlParameter() {ParameterName = "@notificationID", SqlDbType = SqlDbType.NVarChar, Value = notificationID},
                 new SqlParameter() {ParameterName = "@employeeID", SqlDbType = SqlDbType.NVarChar, Value = employeeID},
                 new SqlParameter() {ParameterName = "@branchID", SqlDbType = SqlDbType.NVarChar, Value = branchID},
                 new SqlParameter() {ParameterName = "@createdDate", SqlDbType = SqlDbType.DateTime, Value = DateTime.Now},
                 new SqlParameter() {ParameterName = "@action", SqlDbType = SqlDbType.NVarChar, Value = action}
            };


            string res = Manager.DataFacade.DTSQLVoidCommand("INSERT INTO Log_Notification (NotificationID,EmployeeID,BranchID,Action,Sent,Recieved,CreatedDate) VALUES (@notificationID,@employeeID,@branchID,@action,1,0,@createdDate)", sp);

            return res;
        }
    }
}
