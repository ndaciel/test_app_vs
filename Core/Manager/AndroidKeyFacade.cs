using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.Data;
using System.Data.SqlClient;

namespace Core.Manager
{
    //christopher
    public class AndroidKeyFacade
    {
        public static string InsertAndroidKey(Model.mdlSaveAndroidKeyParam param)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                 new SqlParameter() {ParameterName = "@employeeID", SqlDbType = SqlDbType.NVarChar, Value = param.EmployeeID},
                 new SqlParameter() {ParameterName = "@branchID", SqlDbType = SqlDbType.NVarChar, Value = param.BranchID},
                 new SqlParameter() {ParameterName = "@androidKey", SqlDbType = SqlDbType.Text, Value = param.AndroidKey}
            };


            string res = Manager.DataFacade.DTSQLVoidCommand("INSERT INTO EmployeeAndroidKey (EmployeeID,AndroidKey,BranchID) VALUES (@employeeID,@androidKey,@branchID)", sp);

            return res;
        }

        

        public static string UpdateAndroidKey(Model.mdlSaveAndroidKeyParam param)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                 new SqlParameter() {ParameterName = "@employeeID", SqlDbType = SqlDbType.NVarChar, Value = param.EmployeeID},
                 new SqlParameter() {ParameterName = "@branchID", SqlDbType = SqlDbType.NVarChar, Value = param.BranchID},
                 new SqlParameter() {ParameterName = "@androidKey", SqlDbType = SqlDbType.Text, Value = param.AndroidKey}
            };


            string res = Manager.DataFacade.DTSQLVoidCommand("UPDATE EmployeeAndroidKey SET AndroidKey = @androidKey WHERE EmployeeID = @employeeID AND BranchID = @branchID", sp);

            return res;
        }

        public static Model.mdlAndroidKey CheckEmployee(Model.mdlSaveAndroidKeyParam param)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                 new SqlParameter() {ParameterName = "@employeeID", SqlDbType = SqlDbType.NVarChar, Value = param.EmployeeID},
                 new SqlParameter() {ParameterName = "@branchID", SqlDbType = SqlDbType.NVarChar, Value = param.BranchID}
                 
            };


            DataTable dtAndroidKey = Manager.DataFacade.DTSQLCommand("SELECT EmployeeID,BranchID FROM EmployeeAndroidKey WHERE EmployeeID = @employeeID AND BranchID = @branchID ", sp);
            var mdlEmployee = new Model.mdlAndroidKey();


            foreach (DataRow row in dtAndroidKey.Rows)
            {

                mdlEmployee.EmployeeID = row["EmployeeID"].ToString();
                mdlEmployee.BranchID = row["BranchID"].ToString();

            }

            return mdlEmployee;


        }
    }
}
