using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Device.Location;
using System.Globalization;

namespace Core.Manager
{
    public class ReportJourneyTrackingFacade
    {
        public static List<Model.mdlTrackingJourney> GetTrackingCoordinate(string employeeID, string branchID, DateTime date)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@DateStart", SqlDbType = SqlDbType.DateTime, Value= date},
                new SqlParameter() {ParameterName = "@DateFinish", SqlDbType = SqlDbType.DateTime, Value= date.AddDays(1)},
                //new SqlParameter() {ParameterName = "@FinishDate", SqlDbType = SqlDbType.DateTime, Value = DateTime.Now.Date.AddDays(1) },
                new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = employeeID },
                new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = branchID}

            };

            DataTable dtDailyMsg = DataFacade.DTSQLCommand(@"SELECT TrackingDate,VehicleID,Latitude,Longitude FROM LiveTracking WHERE TrackingDate >= @DateStart AND TrackingDate < @DateFinish AND EmployeeID=@EmployeeID AND BranchID = @BranchID Order by TrackingDate", sp);

            var mdlTrackingList = new List<Model.mdlTrackingJourney>();

            foreach(DataRow row in dtDailyMsg.Rows)
            {
                var mdlTracking = new Model.mdlTrackingJourney();
                mdlTracking.vehicleID = row["VehicleID"].ToString();
                mdlTracking.time = Convert.ToDateTime(row["TrackingDate"]).ToString("H:mm:ss");
                mdlTracking.latitude = row["Latitude"].ToString();
                mdlTracking.longitude = row["Longitude"].ToString();
                mdlTrackingList.Add(mdlTracking);
            }

            return mdlTrackingList;

        }

        //Multiple Employee and to get total distance from employee's journey, above is for the single employee
        public static List<Model.mdlVisitReport> GetTrackingCoordinate2(List<string> lEmployeeIDlist, string branchID, DateTime date, DateTime dateEnd)
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
                new SqlParameter() {ParameterName = "@DateStart", SqlDbType = SqlDbType.DateTime, Value= date},
                new SqlParameter() {ParameterName = "@DateFinish", SqlDbType = SqlDbType.DateTime, Value= dateEnd.AddDays(1)},
                new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = branchID}

            };
            
            DataTable dtVisit = DataFacade.DTSQLCommand(@"SELECT a.BranchID,a.EmployeeID,a.VehicleID,a.StartDate,a.EndDate,a.KMStart,a.KMFinish,
                                                                    b.BranchName, c.EmployeeName, a.VisitDate
                                                                FROM Visit a
                                                                INNER JOIN Branch b ON b.BranchID = a.BranchID
                                                                INNER JOIN Employee c on c.EmployeeID = a.EmployeeID
                                                                WHERE (a.VisitDate BETWEEN @DateStart and @DateFinish) and (a.BranchID=@BranchID) and "+ lParam + "Order By c.EmployeeName,a.StartDate", sp);


            var mdlVisitList = new List<Model.mdlVisitReport>();
            foreach (DataRow row in dtVisit.Rows)
            {
                var mdlVisit = new Model.mdlVisitReport();
                mdlVisit.BranchID = row["BranchID"].ToString();
                mdlVisit.BranchName = row["BranchName"].ToString();
                mdlVisit.EmployeeID = row["EmployeeID"].ToString();
                mdlVisit.EmployeeName = row["EmployeeName"].ToString();
                mdlVisit.VehicleID = row["VehicleID"].ToString();
                mdlVisit.VisitDate = Convert.ToDateTime(row["VisitDate"]).ToString("dd-MM-yyyy");
                mdlVisit.StartDate = Convert.ToDateTime(row["StartDate"]).ToString("dd-MM-yyyy HH:mm:ss");
                mdlVisit.EndDate = Convert.ToDateTime(row["EndDate"]).ToString("dd-MM-yyyy HH:mm:ss");
                mdlVisit.KMStart = Convert.ToInt32(row["KMStart"].ToString());
                mdlVisit.KMEnd = Convert.ToInt32(row["KMFinish"].ToString());

//                DataTable dtJourneyDistance = DataFacade.DTSQLCommand(@"SELECT Latitude,Longitude FROM LiveTracking 
//                                                                        WHERE TrackingDate >= '"+Convert.ToDateTime(mdlVisit.VisitDate).ToString("yyyy-MM-dd")+"' AND TrackingDate < '"+Convert.ToDateTime(mdlVisit.EndDate).AddDays(1).ToString("yyyy-MM-dd")+"' AND BranchID = '"+mdlVisit.BranchID+"' AND EmployeeID = '"+mdlVisit.EmployeeID+"' Order by TrackingDate", sp);

                DataTable dtJourneyDistance = DataFacade.DTSQLCommand(@"SELECT Latitude,Longitude FROM LiveTracking 
                                                                        WHERE TrackingDate >= '" + Convert.ToDateTime(row["VisitDate"]).ToString("yyyy-MM-dd") + "' AND TrackingDate < '" + Convert.ToDateTime(row["EndDate"]).AddDays(1).ToString("yyyy-MM-dd") + "' AND BranchID = '" + mdlVisit.BranchID + "' AND EmployeeID = '" + mdlVisit.EmployeeID + "' Order by TrackingDate", sp);
                var mdlTracking = new Model.mdlTrackingJourney();

                double Total = 0;
                foreach (DataRow rowJourney in dtJourneyDistance.Rows)
                {
                    if (mdlTracking.latitude == null || mdlTracking.longitude == null)
                    {
                        mdlTracking.latitude = rowJourney["Latitude"].ToString();
                        mdlTracking.longitude = rowJourney["Longitude"].ToString();
                    }
                    //if (rowJourney == dtJourneyDistance.Rows[dtJourneyDistance.Rows.Count - 1]) -->> // to get the last data in looping
                    else
                    {
                        mdlTracking.latitude2 = rowJourney["Latitude"].ToString();
                        mdlTracking.longitude2 = rowJourney["Longitude"].ToString();

                        var SCoor = new GeoCoordinate(double.Parse(mdlTracking.latitude, CultureInfo.InvariantCulture), double.Parse(mdlTracking.longitude, CultureInfo.InvariantCulture));
                        var FCoor = new GeoCoordinate(double.Parse(mdlTracking.latitude2, CultureInfo.InvariantCulture), double.Parse(mdlTracking.longitude2, CultureInfo.InvariantCulture));
                        double distance = RadiusFacade.getDistance(SCoor, FCoor);

                        Total = Total + distance;

                        mdlTracking.latitude = mdlTracking.latitude2;
                        mdlTracking.longitude = mdlTracking.longitude2;

                    }
                    
                  }

                mdlVisit.DistancebyGPS = Convert.ToString(Math.Ceiling(Total / 1000));
                
                mdlVisitList.Add(mdlVisit);
                }

            return mdlVisitList;

        }

        public static List<Model.mdlVisitJourney> GetVisitCoordinate(string employeeID, string branchID, DateTime date)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@Date", SqlDbType = SqlDbType.DateTime, Value= date},
                //new SqlParameter() {ParameterName = "@FinishDate", SqlDbType = SqlDbType.DateTime, Value = DateTime.Now.Date.AddDays(1) },
                new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = employeeID },
                new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = branchID}

            };

            DataTable dtVisit = DataFacade.DTSQLCommand(@"select a.VisitID,b.CustomerID,c.CustomerName,b.StartDate,b.EndDate,b.Latitude,b.Longitude,b.isInRange,b.ReasonID,d.Value from Visit a 
                                                            inner join VisitDetail b ON a.VisitID = b.VisitID
                                                            inner join Customer c ON b.CustomerID = c.CustomerID
                                                            left join Reason d on b.ReasonID = d.ReasonID
                                                            WHERE a.VisitDate >= @Date AND a.VisitDate < DATEADD(dd,1,@Date) AND a.EmployeeID = @EmployeeID AND a.BranchID = @BranchID ORDER BY StartDate", sp);

            var mdlVisitList = new List<Model.mdlVisitJourney>();

            foreach (DataRow row in dtVisit.Rows)
            {
                var mdlVisit = new Model.mdlVisitJourney();
                mdlVisit.visitID = row["VisitID"].ToString();
                mdlVisit.customerID = row["CustomerID"].ToString();
                mdlVisit.customerName = row["CustomerName"].ToString();
                mdlVisit.startTime = Convert.ToDateTime(row["StartDate"]).ToString("H:mm:ss");
                mdlVisit.finishTime = Convert.ToDateTime(row["EndDate"]).ToString("H:mm:ss");
                mdlVisit.latitude = row["Latitude"].ToString();
                mdlVisit.longitude = row["Longitude"].ToString();
                if(Convert.ToInt16(row["isInRange"]) == 1)
                {
                    mdlVisit.isInRange = "YES";
                }
                else
                {
                    mdlVisit.isInRange = "NO";
                }
                
                mdlVisit.reasonID = row["ReasonID"].ToString();
                if (mdlVisit.reasonID != "")
                {
                    mdlVisit.reasonName = row["Value"].ToString();
                }
                else
                {
                    mdlVisit.reasonName = "";
                }
                
                mdlVisitList.Add(mdlVisit);
            }

            return mdlVisitList;

        }

        

    }
}
