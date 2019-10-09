using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Core.Manager
{
    public class GeneralSettingsFacade
    {
        public static void UpdateSettings(string value,string name, string branchID)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                 new SqlParameter() {ParameterName = "@value", SqlDbType = SqlDbType.NVarChar, Value = value},
                 new SqlParameter() {ParameterName = "@name", SqlDbType = SqlDbType.NVarChar, Value = name},
                 new SqlParameter() {ParameterName = "@branchID", SqlDbType = SqlDbType.NVarChar, Value = branchID}
            };

            DataFacade.DTSQLVoidCommand("UPDATE Settings SET Value = @value WHERE Name = @name AND BranchID = @branchID", sp);
        }

        public static void InsertSettings(string value, string name, string description, string defaultvalue, string branchID)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                 new SqlParameter() {ParameterName = "@value", SqlDbType = SqlDbType.NVarChar, Value = value},
                 new SqlParameter() {ParameterName = "@name", SqlDbType = SqlDbType.NVarChar, Value = name},
                 new SqlParameter() {ParameterName = "@branchID", SqlDbType = SqlDbType.NVarChar, Value = branchID},
                 new SqlParameter() {ParameterName = "@description", SqlDbType = SqlDbType.NVarChar, Value = description},
                 new SqlParameter() {ParameterName = "@defaultvalue", SqlDbType = SqlDbType.NVarChar, Value = defaultvalue}
            };

            DataFacade.DTSQLVoidCommand("INSERT INTO Settings (Value, Name, Description, DefaultValue,BranchID) " +
                                                "VALUES (@value, @name , @description, @defaultvalue, @branchID)", sp);
        }

        public static List<Model.mdlSettings> GetCurrentSettings(string branchID)
        {
            var listSettings = new List<Model.mdlSettings>();
            List<SqlParameter> sp = new List<SqlParameter>()
            {
               
                 new SqlParameter() {ParameterName = "@branchID", SqlDbType = SqlDbType.NVarChar, Value = branchID}
            };

            DataTable dtSettings = DataFacade.DTSQLCommand("SELECT Name,Value FROM Settings WHERE BranchID = @branchID", sp);

            foreach(DataRow row in dtSettings.Rows)
            {
                var mdlSetting = new Model.mdlSettings();
                mdlSetting.name = row["Name"].ToString();
                mdlSetting.value = row["Value"].ToString();
                listSettings.Add(mdlSetting);
            }

            return listSettings;

        }
    }
}
