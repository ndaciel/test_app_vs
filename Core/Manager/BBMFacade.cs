using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Core.Manager
{
    public class BBMFacade : Base.Manager
    {
        public static bool CheckbyRatioID(string lRatioID)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName = "@RatioID", SqlDbType = SqlDbType.NVarChar, Value = lRatioID },
            };

            DataTable dtBBMRatio = Manager.DataFacade.DTSQLCommand(@"SELECT TOP 1 RatioID 
                                                                   FROM RatioBBM 
                                                                   WHERE RatioID = @RatioID", sp);
            bool lCheck = false;
            if (dtBBMRatio.Rows.Count == 0)
            {
                lCheck = true;
            }

            return lCheck;
        }

        public static Model.mdlResultList InsertBBMRatio(List<Model.mdlBBMRatioParam> lParamlist)
        {
            var mdlResultList = new List<Model.mdlResult>();

            foreach (var lParam in lParamlist)
            {
                var mdlResult = new Model.mdlResult();

                bool lCheckRatioBBM = CheckbyRatioID(lParam.RatioID);
                if (lCheckRatioBBM == false)
                {
                    mdlResult.Result = "|| RatioID : " + lParam.RatioID + " " + "|| IDExist ||";
                    mdlResultList.Add(mdlResult);
                }
                else
                {
                    List<SqlParameter> sp = new List<SqlParameter>()
                    {
                    new SqlParameter() {ParameterName = "@RatioID", SqlDbType = SqlDbType.NVarChar, Value = lParam.RatioID},
                    new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = lParam.BranchID},
                    new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = lParam.EmployeeID},
                    new SqlParameter() {ParameterName = "@VehicleID", SqlDbType = SqlDbType.NVarChar, Value = lParam.VehicleID},
                    new SqlParameter() {ParameterName = "@Km", SqlDbType = SqlDbType.NVarChar, Value = lParam.Km},
                    new SqlParameter() {ParameterName = "@Liter", SqlDbType = SqlDbType.NVarChar, Value = lParam.Liter},
                    new SqlParameter() {ParameterName = "@Harga", SqlDbType = SqlDbType.NVarChar, Value = lParam.Harga},     
                    new SqlParameter() {ParameterName = "@Latitude", SqlDbType = SqlDbType.NVarChar, Value = lParam.Latitude},
                    new SqlParameter() {ParameterName = "@Longitude", SqlDbType = SqlDbType.NVarChar, Value = lParam.Longitude},
                    new SqlParameter() {ParameterName = "@Date", SqlDbType = SqlDbType.NVarChar, Value = lParam.Date}
                    };

                    string query = @"INSERT INTO RatioBBM (RatioID, BranchID, EmployeeID, VehicleID, Km,Liter, Harga, Latitude, Longitude, Date) " +
                                                "VALUES (@RatioID, @BranchID, @EmployeeID, @VehicleID, @Km,@Liter, @Harga, @Latitude, @Longitude, @Date) ";

                    mdlResult.Result = Manager.DataFacade.DTSQLVoidCommand(query, sp);

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
                }
            }

            var mdlResultListnew = new Model.mdlResultList();
            mdlResultListnew.ResultList = mdlResultList;
            return mdlResultListnew;
        }

        public static List<Model.mdlBBMRatioReport> LoadRatioBBMReport(string lBranchID, DateTime lStartDate, DateTime lEndDate, string lVehicleID)
        {

            List<SqlParameter> sp = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = lBranchID },
               new SqlParameter() {ParameterName = "@StartDate", SqlDbType = SqlDbType.DateTime, Value = lStartDate },
               new SqlParameter() {ParameterName = "@EndDate", SqlDbType = SqlDbType.DateTime, Value = lEndDate.AddDays(1) },
               //new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = lEmployeeID },
               new SqlParameter() {ParameterName = "@VehicleID", SqlDbType = SqlDbType.NVarChar, Value = lVehicleID },
            };

            string sql = @"SELECT   a.BranchID, 
	                                c.BranchName, 
                                    a.EmployeeID,
									f.EmployeeName,
                                    a.VehicleID,
	                                a.Date,
                                    a.Km, a.Liter, a.Harga, a.Latitude, a.Longitude
	                            FROM RatioBBM a    
                                                INNER JOIN Branch c ON c.BranchID = a.BranchID
												INNER JOIN Employee f ON a.EmployeeID = f.EmployeeID
                                WHERE (a.BranchID = @BranchID) AND (a.Date BETWEEN @StartDate and @EndDate) AND (a.VehicleID=@VehicleID) ORDER BY a.Date";

            var mdlRatioBBMList = new List<Model.mdlBBMRatioReport>();
            DataTable dtRatioBBM = Manager.DataFacade.DTSQLCommand(sql, sp);

            foreach (DataRow row in dtRatioBBM.Rows)
            {
                var mdlRatioBBMReport = new Model.mdlBBMRatioReport();
                mdlRatioBBMReport.BranchID = row["BranchID"].ToString();
                mdlRatioBBMReport.BranchName = row["BranchName"].ToString();
                mdlRatioBBMReport.EmployeeID = row["EmployeeID"].ToString();
                mdlRatioBBMReport.EmployeeName = row["EmployeeName"].ToString();
                mdlRatioBBMReport.VehicleID = row["VehicleID"].ToString();
                mdlRatioBBMReport.Km = Convert.ToInt32(row["Km"].ToString());

                mdlRatioBBMReport.Liter = Convert.ToDouble(row["Liter"].ToString());

                mdlRatioBBMReport.Harga = Convert.ToInt32(row["Harga"].ToString());

                mdlRatioBBMReport.Latitude = row["Latitude"].ToString();
                mdlRatioBBMReport.Longitude = row["Longitude"].ToString();

                mdlRatioBBMReport.Date = Convert.ToDateTime(row["Date"]).ToString("dd-MM-yyyy");
                mdlRatioBBMReport.Time = Convert.ToDateTime(row["Date"]).ToString("HH:mm:ss");

                mdlRatioBBMList.Add(mdlRatioBBMReport);
            }
            return mdlRatioBBMList;
        }
    }
}
