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
    public class ReportIdleTrackingFacade
    {
        public static List<Model.mdlTrackingIdle> GetTrackingList(string employeeID, string branchID, DateTime date)
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

            var mdlTrackingList = new List<Model.mdlTrackingIdle>();

            foreach (DataRow row in dtDailyMsg.Rows)
            {
                var mdlTracking = new Model.mdlTrackingIdle();
                mdlTracking.vehicleID = row["VehicleID"].ToString();
                mdlTracking.time = Convert.ToDateTime(row["TrackingDate"]);
                mdlTracking.latitude = row["Latitude"].ToString();
                mdlTracking.longitude = row["Longitude"].ToString();
                mdlTrackingList.Add(mdlTracking);
            }

            return mdlTrackingList;
        }





        public static Model.mdlReportIdleList GetIdleTrackingList(string employeeID, string branchID, DateTime date, List<Core.Model.mdlSettings> listSettings)
        {
            //20Feb2017 - FERNANDES
            List<SqlParameter> sp = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = branchID },
               new SqlParameter() {ParameterName = "@StartDate", SqlDbType = SqlDbType.Date, Value = date },
               new SqlParameter() {ParameterName = "@EndDate", SqlDbType = SqlDbType.Date, Value = date.AddDays(1) },
               new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = employeeID },
            };

            var mdlIdleTrackingListnew = new Model.mdlReportIdleList();

            var mdlIdleTrackingChildList = new List<Model.mdlIdleReport>();

            DataTable dtIdleTracking = Manager.DataFacade.DTSQLCommand(@"SELECT a.StartIdle, a.EndIdle, a.Latitude, a.Longitude, a.Duration, a.Location
                                                                        FROM Log_Idle a
                                                                        WHERE (a.BranchID = @BranchID) AND (a.StartIdle BETWEEN @StartDate and @EndDate) AND (a.Longitude <> '' and a.Latitude <> '') AND (a.EmployeeID=@EmployeeID)", sp);

            foreach (DataRow row in dtIdleTracking.Rows)
            {
                var mdlIdleTracking = new Model.mdlIdleReport();
                mdlIdleTracking.StartIdle = row["StartIdle"].ToString();
                mdlIdleTracking.EndIdle = row["EndIdle"].ToString();
                mdlIdleTracking.Longitude = row["Longitude"].ToString();
                mdlIdleTracking.Latitude = row["Latitude"].ToString();
                mdlIdleTracking.Duration = row["Duration"].ToString();
                mdlIdleTracking.Location = row["Location"].ToString();

                mdlIdleTrackingChildList.Add(mdlIdleTracking);
            }

            mdlIdleTrackingListnew.mdlIdleTrackingList = mdlIdleTrackingChildList;

            return mdlIdleTrackingListnew;

            //Christopher

            //int minutes = 0;
            //double radius = 0;

            //foreach (var setting in listSettings)
            //{
            //    if (setting.name == "IDLERADIUS")
            //    {
            //        radius = Convert.ToDouble(setting.value);
            //    }
            //    else if (setting.name == "IDLETIME")
            //    {
            //        minutes = Convert.ToInt32(setting.value);
            //    }
            //}

            //var mdlTrackingList = GetTrackingList(employeeID, branchID, date);


            //var listMdlTrackingList = new List<Model.mdlTrackingIdleList>();

            //var finalmdlTrackingList = new Model.mdlTrackingIdleListFinal();
            
            //var listMdlTrackingIdle = new List<Model.mdlTrackingIdle>();
            //var mdlTracking = new Model.mdlTrackingIdle();

            //DateTime minTime = mdlTrackingList.Min(fld => fld.time);
            //DateTime maxTime = mdlTrackingList.Max(fld => fld.time);

            //DateTime startTime = minTime;

           
            //while(startTime < maxTime)
            //{
            //    var mdlTrackingIdleList = new Model.mdlTrackingIdleList();
            //    List<Model.mdlTrackingIdle> newlistMdlTrackingIdle = mdlTrackingList.Where(fld => fld.time >= startTime && fld.time <= startTime.AddMinutes(minutes)).ToList();

            //    var firstCoor = new GeoCoordinate();

            //    List<Model.newMdlTrackingIdle> finalListMdlTrackingIdle = new List<Model.newMdlTrackingIdle>();
            //    foreach (var temp in newlistMdlTrackingIdle)
            //    {
            //        var newMdlTracking = new Model.newMdlTrackingIdle();
            //        newMdlTracking.vehicleID = temp.vehicleID;
            //        newMdlTracking.time = temp.time.ToString("H:mm:ss");
            //        newMdlTracking.latitude = temp.latitude;
            //        newMdlTracking.longitude = temp.longitude;

            //        double latitude = double.Parse(temp.latitude, CultureInfo.InvariantCulture);
            //        double longitude = double.Parse(temp.longitude, CultureInfo.InvariantCulture);

            //        if (temp == newlistMdlTrackingIdle.First())
            //        {
                        
            //            firstCoor = new GeoCoordinate(latitude, longitude);
            //            newMdlTracking.isIdle = "1";
            //        }
            //        else
            //        {
                        
                        
            //            var coor = new GeoCoordinate(latitude, longitude);
            //            double distance = RadiusFacade.getDistance(firstCoor, coor);
            //            if (distance > radius)
            //            {
            //                newMdlTracking.isIdle = "0";
            //            }
            //            else
            //            {
            //                newMdlTracking.isIdle = "1";
            //            }
            //        }
                    
            //        finalListMdlTrackingIdle.Add(newMdlTracking);
                    
            //    }

            //    mdlTrackingIdleList.trackingIdleList = finalListMdlTrackingIdle;
            //    listMdlTrackingList.Add(mdlTrackingIdleList);

            //    var lastOnGroup = newlistMdlTrackingIdle.Last();
            //    int indexOfLastOnGroup = mdlTrackingList.FindIndex(fld => fld.time == lastOnGroup.time);
            //    DateTime newStartTime;
            //    if (indexOfLastOnGroup != mdlTrackingList.Count - 1)
            //    {
            //        newStartTime = mdlTrackingList[indexOfLastOnGroup + 1].time;
            //    }
            //    else
            //    {
            //        newStartTime = mdlTrackingList[indexOfLastOnGroup].time;
            //    }

            //    startTime = newStartTime;

            //}

            //finalmdlTrackingList.finalTrackingIdleList = listMdlTrackingList;


            //return finalmdlTrackingList;
        }

    }
}
