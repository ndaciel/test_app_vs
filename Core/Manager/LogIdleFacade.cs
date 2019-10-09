using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Core.Manager
{
    public class LogIdleFacade
    {
        public static string InsertLogIdle(Model.mdlLogIdleParam lLogIdleParam)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = lLogIdleParam.EmployeeID },
               new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = lLogIdleParam.BranchID },
               new SqlParameter() {ParameterName = "@StartIdle", SqlDbType = SqlDbType.DateTime, Value = lLogIdleParam.StartIdle },
               new SqlParameter() {ParameterName = "@Latitude", SqlDbType = SqlDbType.NVarChar, Value = lLogIdleParam.Latitude },
               new SqlParameter() {ParameterName = "@Longitude", SqlDbType = SqlDbType.NVarChar, Value = lLogIdleParam.Longitude },
               new SqlParameter() {ParameterName = "@Duration", SqlDbType = SqlDbType.NVarChar, Value = lLogIdleParam.Duration },
               new SqlParameter() {ParameterName = "@Location", SqlDbType = SqlDbType.NVarChar, Value = lLogIdleParam.Location },
               new SqlParameter() {ParameterName = "@Status", SqlDbType = SqlDbType.Bit, Value = lLogIdleParam.Status }
            };

            string result = DataFacade.DTSQLVoidCommand("INSERT INTO Log_Idle (EmployeeID,BranchID,StartIdle,Latitude,Longitude,Duration,Location,Status) VALUES (@EmployeeID,@BranchID,@StartIdle,@Latitude,@Longitude,@Duration,@Location,@Status)", sp);

            return result;

            //jika false tidak ada data, jika true ada data
        }

        public static string UpdateLogIdle(Model.mdlLogIdleParam lLogIdleParam)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = lLogIdleParam.EmployeeID },
               new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = lLogIdleParam.BranchID },
               new SqlParameter() {ParameterName = "@StartIdle", SqlDbType = SqlDbType.DateTime, Value = lLogIdleParam.StartIdle },
               new SqlParameter() {ParameterName = "@Latitude", SqlDbType = SqlDbType.NVarChar, Value = lLogIdleParam.Latitude },
               new SqlParameter() {ParameterName = "@Longitude", SqlDbType = SqlDbType.NVarChar, Value = lLogIdleParam.Longitude },
               new SqlParameter() {ParameterName = "@Duration", SqlDbType = SqlDbType.NVarChar, Value = lLogIdleParam.Duration },
               //new SqlParameter() {ParameterName = "@Location", SqlDbType = SqlDbType.NVarChar, Value = lLogIdleParam.Location },
               //new SqlParameter() {ParameterName = "@Status", SqlDbType = SqlDbType.Bit, Value = lLogIdleParam.Status }
            };

//            string result = DataFacade.DTSQLVoidCommand(@"UPDATE Log_Idle SET Latitude = @Latitude, Longitude=@Longitude, Duration=@Duration,
//                                                            Location=@Location 
//                                                            WHERE EmployeeID = @EmployeeID AND BranchID = @BranchID
//                                                                    AND Status=0", sp);

            string result = DataFacade.DTSQLVoidCommand(@"UPDATE Log_Idle SET Latitude = @Latitude, Longitude=@Longitude, Duration=@Duration
                                                            WHERE EmployeeID = @EmployeeID AND BranchID = @BranchID
                                                                    AND Status=0", sp);

            return result;

            //jika false tidak ada data, jika true ada data
        }

        public static string UpdateLogIdleClose(Model.mdlLogIdleParam lLogIdleParam)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = lLogIdleParam.EmployeeID },
               new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = lLogIdleParam.BranchID },
               new SqlParameter() {ParameterName = "@StartIdle", SqlDbType = SqlDbType.DateTime, Value = lLogIdleParam.StartIdle },
               new SqlParameter() {ParameterName = "@Latitude", SqlDbType = SqlDbType.NVarChar, Value = lLogIdleParam.Latitude },
               new SqlParameter() {ParameterName = "@Longitude", SqlDbType = SqlDbType.NVarChar, Value = lLogIdleParam.Longitude },
               new SqlParameter() {ParameterName = "@Duration", SqlDbType = SqlDbType.NVarChar, Value = lLogIdleParam.Duration },
               //new SqlParameter() {ParameterName = "@Location", SqlDbType = SqlDbType.NVarChar, Value = lLogIdleParam.Location },
               //new SqlParameter() {ParameterName = "@EndIdle", SqlDbType = SqlDbType.NVarChar, Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },
               new SqlParameter() {ParameterName = "@EndIdle", SqlDbType = SqlDbType.NVarChar, Value = lLogIdleParam.Now },
               //new SqlParameter() {ParameterName = "@Status", SqlDbType = SqlDbType.Bit, Value = lLogIdleParam.Status }
            };

//            string result = DataFacade.DTSQLVoidCommand(@"UPDATE Log_Idle SET Latitude = @Latitude, Longitude=@Longitude,
//                                                            Location=@Location, Status=1, EndIdle=@EndIdle
//                                                            WHERE EmployeeID = @EmployeeID AND BranchID = @BranchID
//                                                                    AND Status=0", sp);

            string result = DataFacade.DTSQLVoidCommand(@"UPDATE Log_Idle SET Latitude = @Latitude, Longitude=@Longitude,
                                                                    Status=1, EndIdle=@EndIdle, Duration=@Duration
                                                          WHERE EmployeeID = @EmployeeID AND BranchID = @BranchID
                                                                    AND Status=0", sp);

            return result;

            //jika false tidak ada data, jika true ada data
        }

        public static Model.mdlLogIdle GetLogIdle(Model.mdlLogIdleParam lLogIdleParam)
        {

            List<SqlParameter> sp = new List<SqlParameter>()
                {
               new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = lLogIdleParam.EmployeeID },
               new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = lLogIdleParam.BranchID },
               new SqlParameter() {ParameterName = "@StartIdle", SqlDbType = SqlDbType.DateTime, Value = lLogIdleParam.StartIdle }
                };

            DataTable dtLogIdle = Manager.DataFacade.DTSQLCommand(@"SELECT EmployeeID
                                                                                    ,BranchID
                                                                                    ,StartIdle, Latitude, Longitude, Duration, Location
                                                                                    ,Status FROM Log_Idle WHERE EmployeeID = @EmployeeID AND BranchID = @BranchID
                                                                    AND Status=0", sp);

            var lmdlLogIdle = new Model.mdlLogIdle();

            foreach (DataRow row in dtLogIdle.Rows)
            {
                lmdlLogIdle.BranchID = row["BranchID"].ToString();
                lmdlLogIdle.EmployeeID = row["EmployeeID"].ToString();
                lmdlLogIdle.StartIdle = row["StartIdle"].ToString();
                lmdlLogIdle.Latitude = row["Latitude"].ToString();
                lmdlLogIdle.Longitude = row["Longitude"].ToString();
                lmdlLogIdle.Location = row["Location"].ToString();
                lmdlLogIdle.Duration = row["Duration"].ToString();
                lmdlLogIdle.Status = row["Status"].ToString();
            }

            return lmdlLogIdle;
        }

    }
}
