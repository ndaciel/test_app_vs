using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Core.Manager
{
    public class IdleCounterFacade
    {
        public static Model.mdlIdleCounter CheckIfIdleCounterExist(string employeeID, string branchID)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName = "@employeeID", SqlDbType = SqlDbType.NVarChar, Value = employeeID },
               new SqlParameter() {ParameterName = "@branchID", SqlDbType = SqlDbType.NVarChar, Value = branchID }
            };

            DataTable dt = DataFacade.DTSQLCommand("SELECT EmployeeID,BranchID,StartDate,Longitude,Latitude,Counter,StartDate FROM IdleCounter WHERE EmployeeID = @employeeID AND BranchID = @branchID",sp);

            var mdlIdleCounter = new Model.mdlIdleCounter();
            foreach (DataRow row in dt.Rows)
            {
                
                mdlIdleCounter.EmployeeID = row["EmployeeID"].ToString();
                mdlIdleCounter.BranchID = row["BranchID"].ToString();
                mdlIdleCounter.Longitude = row["Longitude"].ToString();
                mdlIdleCounter.Latitude = row["Latitude"].ToString();
                mdlIdleCounter.Counter = Convert.ToInt32(row["Counter"]);
                mdlIdleCounter.StartDate = row["StartDate"].ToString();
            }

            return mdlIdleCounter;

            //jika false tidak ada data, jika true ada data
        }


        public static double GetIdleRadius(string branchID)
        {
            List<Core.Model.mdlSettings> listSettings = GeneralSettingsFacade.GetCurrentSettings(branchID);


            double radius = 0;

            foreach (var setting in listSettings)
            {
                if (setting.name == "IDLERADIUS")
                {
                    radius = Convert.ToDouble(setting.value);
                }

            }

            return radius;
        }

        public static Model.mdlMobileConfig GetMobileConfigIdleCounter(string branchID)
        {
            

            List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = branchID }
                };

            DataTable dtMobileConfig = Manager.DataFacade.DTSQLCommand(@"SELECT [BranchId]
                                                                                    ,[ID]
                                                                                    ,[Desc]
                                                                                    ,[Value] FROM MobileConfig WHERE BranchID = @BranchID AND ID = 'TIMERTRACKING'", sp); //003
            var lmdlMobileConfig = new Model.mdlMobileConfig();
            foreach (DataRow drMobileConfig in dtMobileConfig.Rows)
            {
               
                lmdlMobileConfig.BranchId = drMobileConfig["BranchId"].ToString();
                lmdlMobileConfig.ID = drMobileConfig["ID"].ToString();
                lmdlMobileConfig.Desc = drMobileConfig["Desc"].ToString();
                lmdlMobileConfig.Value = drMobileConfig["Value"].ToString();
               
            }
            return lmdlMobileConfig;
        }


        public static string InsertIdleCounter(string employeeID, string branchID,string latitude, string longitude, DateTime trackingDate)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName = "@employeeID", SqlDbType = SqlDbType.NVarChar, Value = employeeID },
               new SqlParameter() {ParameterName = "@branchID", SqlDbType = SqlDbType.NVarChar, Value = branchID },
               new SqlParameter() {ParameterName = "@startDate", SqlDbType = SqlDbType.DateTime, Value = trackingDate },
               new SqlParameter() {ParameterName = "@latitude", SqlDbType = SqlDbType.NVarChar, Value = latitude },
               new SqlParameter() {ParameterName = "@longitude", SqlDbType = SqlDbType.NVarChar, Value = longitude },
               new SqlParameter() {ParameterName = "@counter", SqlDbType = SqlDbType.Int, Value = 0 }
            };

            string result = DataFacade.DTSQLVoidCommand("INSERT INTO IdleCounter (EmployeeID,BranchID,StartDate,Longitude,Latitude,Counter) VALUES (@employeeID,@branchID,@startDate,@longitude,@latitude,@counter)", sp);

            return result;

            //jika false tidak ada data, jika true ada data
        }


        public static string InsertIdleLog(string employeeID, string branchID, string deviceID, DateTime trackingDate)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName = "@employeeID", SqlDbType = SqlDbType.NVarChar, Value = employeeID },
               new SqlParameter() {ParameterName = "@branchID", SqlDbType = SqlDbType.NVarChar, Value = branchID },
               new SqlParameter() {ParameterName = "@deviceID", SqlDbType = SqlDbType.NVarChar, Value = deviceID },
               new SqlParameter() {ParameterName = "@timestamp", SqlDbType = SqlDbType.DateTime, Value = trackingDate },
               new SqlParameter() {ParameterName = "@createdBy", SqlDbType = SqlDbType.NVarChar, Value = employeeID }
            };

            string result = DataFacade.DTSQLVoidCommand("INSERT INTO Log_Service (FunctionName,Timestamp,CreatedBy,Result,BranchID,DeviceID,Reason,Size) VALUES ('Idle',@timestamp,@createdBy,'Idle',@branchID,@deviceID,'','')", sp);

            return result;

            //jika false tidak ada data, jika true ada data
        }

        public static string UpdateBaseIdleCounter(string employeeID, string branchID, string latitude, string longitude, DateTime trackingDate)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName = "@employeeID", SqlDbType = SqlDbType.NVarChar, Value = employeeID },
               new SqlParameter() {ParameterName = "@branchID", SqlDbType = SqlDbType.NVarChar, Value = branchID },
               new SqlParameter() {ParameterName = "@startDate", SqlDbType = SqlDbType.DateTime, Value = trackingDate },
               new SqlParameter() {ParameterName = "@latitude", SqlDbType = SqlDbType.NVarChar, Value = latitude },
               new SqlParameter() {ParameterName = "@longitude", SqlDbType = SqlDbType.NVarChar, Value = longitude },
               new SqlParameter() {ParameterName = "@counter", SqlDbType = SqlDbType.Int, Value = 0 }
            };

            string result = DataFacade.DTSQLVoidCommand("UPDATE IdleCounter SET Longitude = @longitude, Latitude = @latitude, Counter = @counter, StartDate = @startDate WHERE EmployeeID = @employeeID AND BranchID = @branchID", sp);

            return result;

            //jika false tidak ada data, jika true ada data
        }

        public static string UpdateIdleCounter(string employeeID, string branchID, string latitude, string longitude, int counter)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName = "@employeeID", SqlDbType = SqlDbType.NVarChar, Value = employeeID },
               new SqlParameter() {ParameterName = "@branchID", SqlDbType = SqlDbType.NVarChar, Value = branchID },
               new SqlParameter() {ParameterName = "@startDate", SqlDbType = SqlDbType.DateTime, Value = DateTime.Now },
               new SqlParameter() {ParameterName = "@latitude", SqlDbType = SqlDbType.NVarChar, Value = employeeID },
               new SqlParameter() {ParameterName = "@longitude", SqlDbType = SqlDbType.NVarChar, Value = employeeID },
               new SqlParameter() {ParameterName = "@counter", SqlDbType = SqlDbType.Int, Value = counter }
            };

            string result = DataFacade.DTSQLVoidCommand("UPDATE IdleCounter SET Counter = @counter WHERE EmployeeID = @employeeID AND BranchID = @branchID", sp);

            return result;

            //jika false tidak ada data, jika true ada data
        }

        //FERNANDES-25jAN2017
        public static List<Model.mdlIdleReport> LoadIdleReport(string lBranchID, DateTime lStartDate, DateTime lEndDate, List<string> lEmployeeIDlist)
        {
            string lParam = string.Empty;

            foreach (var lEmployeeID in lEmployeeIDlist)
            {
                if (lParam == "")
                {
                    lParam = " a.EmployeeID =" + "'" + lEmployeeID + "'";
                }
                else
                {
                    lParam += " OR a.EmployeeID =" + "'" + lEmployeeID + "'";
                }

            }

            lParam = "(" + lParam + ")";

            List<SqlParameter> sp = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = lBranchID },
               new SqlParameter() {ParameterName = "@StartDate", SqlDbType = SqlDbType.DateTime, Value = lStartDate },
               new SqlParameter() {ParameterName = "@EndDate", SqlDbType = SqlDbType.DateTime, Value = lEndDate.AddDays(1) }
            };

            string sql = @"SELECT   a.BranchID, 
	                                b.BranchName, 
                                    a.EmployeeID,
									c.EmployeeName,
                                    a.StartIdle, a.EndIdle,
                                    a.Latitude, a.Longitude, a.Duration, a.Location, a.Status
	                            FROM Log_Idle a    
                                                INNER JOIN Branch b ON b.BranchID = a.BranchID
												INNER JOIN Employee c ON c.EmployeeID = a.EmployeeID
                                WHERE (a.BranchID = @BranchID) AND (a.StartIdle BETWEEN @StartDate and @EndDate) AND " + lParam + "Order by a.StartIdle";


            var mdlIdleReportList = new List<Model.mdlIdleReport>();
            DataTable dtIdle = Manager.DataFacade.DTSQLCommand(sql, sp);

            foreach (DataRow row in dtIdle.Rows)
            {
                var mdlIdleReport = new Model.mdlIdleReport();
                mdlIdleReport.BranchID = row["BranchID"].ToString();
                mdlIdleReport.BranchNm = row["BranchName"].ToString();
                mdlIdleReport.EmployeeID = row["EmployeeID"].ToString();
                mdlIdleReport.EmployeeNm = row["EmployeeName"].ToString();

                mdlIdleReport.StartIdle = row["StartIdle"].ToString();
                mdlIdleReport.EndIdle = row["EndIdle"].ToString();

                if (mdlIdleReport.StartIdle == "" || mdlIdleReport.EndIdle == "")
                {
                    mdlIdleReport.StartIdle = row["StartIdle"].ToString();
                    mdlIdleReport.EndIdle = row["EndIdle"].ToString();
                }
                else
                {
                    mdlIdleReport.StartIdle = Convert.ToDateTime(row["StartIdle"]).ToString("dd-MM-yyyy HH:mm:ss");
                    mdlIdleReport.EndIdle = Convert.ToDateTime(row["EndIdle"]).ToString("dd-MM-yyyy HH:mm:ss");
                }

                mdlIdleReport.Latitude = row["Latitude"].ToString();
                mdlIdleReport.Longitude = row["Longitude"].ToString();
                mdlIdleReport.Location = row["Location"].ToString();

                //Request Location and Update Counter of Request's Method

                //if (mdlIdleReport.Location == "" || mdlIdleReport.Location == null)
                //{
                //    mdlIdleReport.Location = ReverseGeocodingFacade.GetStreetName(mdlIdleReport.Latitude, mdlIdleReport.Longitude);

                //    List<SqlParameter> sp2 = new List<SqlParameter>()
                //    {
                //       new SqlParameter() {ParameterName = "@Location", SqlDbType = SqlDbType.NVarChar, Value = mdlIdleReport.Location },
                //       new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = mdlIdleReport.EmployeeID },
                //       new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = mdlIdleReport.BranchID },
                //       new SqlParameter() {ParameterName = "@StartIdle", SqlDbType = SqlDbType.DateTime, Value = mdlIdleReport.StartIdle },
                //    };

                //    string updatelocation = DataFacade.DTSQLVoidCommand("UPDATE Log_Idle SET Location=@Location WHERE EmployeeID=@EmployeeID AND BranchID=@BranchID AND StartIdle=@StartIdle", sp2);

                //    string DateToday = DateTime.Now.ToString("yyyy-MM-dd");

                //    bool checklogLocationReq = LogFacade.CheckLogLocationReqbyDate(DateToday);
                //    if (checklogLocationReq == true)
                //    {
                //        string insertlogLocationReq = DataFacade.DTSQLVoidCommand(@"INSERT INTO Log_LocationRequest (Date, RequestCounter) " +
                //                                "VALUES ('"+DateToday+"' , '1')", sp2);
                //    }
                //    else
                //    {
                //        var logLocationReq = LogFacade.getLog_LocationReq(DateToday);
                //        foreach ( var log in logLocationReq)
                //        {
                //            int counter = Convert.ToInt32(log.RequestCounter) + 1;

                //            string updatelogLocationReq = DataFacade.DTSQLVoidCommand(@"UPDATE Log_LocationRequest SET RequestCounter= '"+counter+"' "+
                //                                                                            "WHERE Date='"+DateToday+"'", sp2);
                //        }
                        
                //    }

                //}
                //else
                //{
                //}

                mdlIdleReport.Duration = row["Duration"].ToString();
                mdlIdleReport.Status = row["Status"].ToString();

                mdlIdleReportList.Add(mdlIdleReport);
            }
            return mdlIdleReportList;
        }
       
    }
}
