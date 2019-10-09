/* documentation
 * 001 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Core.Manager
{
    public class TrackingFacade
    {

        public static Model.mdlResultList InsertTracking(Model.mdlTrackingParam lParam)
        {
            var newMdlResultList = new Model.mdlResultList();
            var mdlResultList = new List<Model.mdlResult>();
            var mdlResult = new Model.mdlResult();

            bool lCheckImageID = CheckbyTrackingID(lParam.TrackingID);
            if (lCheckImageID == false)
            {
                mdlResult.Result = "|| TrackingID : " + lParam.TrackingID + " " + "|| IDExist ||"; //006
            }
            else
            {

                List<SqlParameter> sp = new List<SqlParameter>()
                    {
                        new SqlParameter() {ParameterName = "@TrackingID", SqlDbType = SqlDbType.NVarChar, Value = lParam.TrackingID},
                        new SqlParameter() {ParameterName = "@TrackingDate", SqlDbType = SqlDbType.NVarChar, Value = lParam.TrackingDate},
                        new SqlParameter() {ParameterName = "@VehicleID", SqlDbType = SqlDbType.NVarChar, Value = lParam.VehicleID},
                        new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = lParam.EmployeeID},
                        new SqlParameter() {ParameterName = "@Longitude", SqlDbType = SqlDbType.NVarChar, Value = lParam.Longitude},     
                        new SqlParameter() {ParameterName = "@Latitude", SqlDbType = SqlDbType.NVarChar, Value = lParam.Latitude}, 
                        new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = lParam.BranchID},    
                    };

                string query = @"INSERT INTO LiveTracking (TrackingID, TrackingDate, VehicleID, EmployeeID, Longitude, Latitude, BranchID) " +
                                            "VALUES (@TrackingID, @TrackingDate, @VehicleID, @EmployeeID, @Longitude, @Latitude, @BranchID) ";

                mdlResult.Result = "|| " + "Insert Tracking ID " + lParam.TrackingID + " || " + Manager.DataFacade.DTSQLVoidCommand(query, sp);


                if (mdlResult.Result.Contains("1") == true)
                {
                    mdlResultList.Add(mdlResult);
                }
                else
                {
                    string ResultSubstring;

                    if (mdlResult.Result.Length > 500)
                    {
                        ResultSubstring = mdlResult.Result.Substring(0, 500);

                        //mdlResult.Result = ResultSubstring;
                        mdlResultList.Add(mdlResult);
                    }

                }

            }

            newMdlResultList.ResultList = mdlResultList;

            return newMdlResultList;
        }


        public static bool CheckbyTrackingID(string lTrackingID)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName = "@TrackingID", SqlDbType = SqlDbType.NVarChar, Value = lTrackingID },
            };

            DataTable dtVisit = Manager.DataFacade.DTSQLCommand(@"SELECT TOP 1 TrackingID 
                                                                   FROM LiveTracking 
                                                                   WHERE TrackingID = @TrackingID", sp);
            bool lCheck = false;
            if (dtVisit.Rows.Count == 0)
            {
                lCheck = true;
            }

            return lCheck;
        }


        public static List<Model.mdlJourneyTracking> GetJourneyTracking(string lBranchID, DateTime lDate, string lEmployeeID)
        {

            string[] splitEmployee = lEmployeeID.Split('-');
            string lEmployee2 = splitEmployee.FirstOrDefault().Trim();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = lBranchID },
               new SqlParameter() {ParameterName = "@Date", SqlDbType = SqlDbType.NVarChar, Value = lDate.ToString("MM-dd-yyyy") }, 
               new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = lEmployee2 }
            };

            var mdlJourneyTrackingList = new List<Model.mdlJourneyTracking>();

            DataTable dtJourneyTracking = Manager.DataFacade.DTSQLCommand(@"SELECT TrackingDate, Longitude, Latitude
                                                                        FROM LiveTracking
                                                                        WHERE (BranchID = @BranchID) AND ( (CONVERT(CHAR(10),TrackingDate,110) = @Date) ) AND (EmployeeID = @EmployeeID) ", sp); //008

            foreach (DataRow row in dtJourneyTracking.Rows)
            {
                var mdlJourneyTracking = new Model.mdlJourneyTracking();
                mdlJourneyTracking.TrackingDate = DateTime.Parse(row["TrackingDate"].ToString()).ToString("hh:mm tt");
                mdlJourneyTracking.Longitude = row["Longitude"].ToString();
                mdlJourneyTracking.Latitude = row["Latitude"].ToString();

                mdlJourneyTracking.StreetName = ReverseGeocodingFacade.GetStreetName(mdlJourneyTracking.Latitude, mdlJourneyTracking.Longitude);

                mdlJourneyTrackingList.Add(mdlJourneyTracking);
            }
            return mdlJourneyTrackingList;
        }

        public static List<Model.mdlLiveTracking> GetLiveTracking(string lBranchID, List<string> lEmployeeIDlist)
        {
            string lParam = string.Empty;
            if (lBranchID != "")
            {
                lParam = "WHERE (a.BranchID = @BranchID) AND ( (CONVERT(CHAR(10),a.TrackingDate,110) = @Date) )";
            }

            string lParam2 = string.Empty;

            if (lEmployeeIDlist.Count != 0)
            {
                foreach (var lEmployeeID in lEmployeeIDlist)
                {
                    if (lParam2 == "")
                    {
                        lParam2 = "a.EmployeeID =" + "'" + lEmployeeID + "'";
                    }
                    else
                    {
                        lParam2 += " OR a.EmployeeID =" + "'" + lEmployeeID + "'";
                    }

                }
                lParam2 = " AND (" + lParam2 + ")";
            }




            List<SqlParameter> sp = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = lBranchID },
               new SqlParameter() {ParameterName = "@Date", SqlDbType = SqlDbType.NVarChar, Value = DateTime.Now.ToString("MM-dd-yyyy")  }
               //new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = lEmployeeID.Substring(0,4) }
            };

            var mdlLiveTrackingList = new List<Model.mdlLiveTracking>();

            DataTable dtLiveTracking = Manager.DataFacade.DTSQLCommand(@"select a.* from LiveTracking a
                                                                            inner join
                                                                            (select max(trackingdate) trackingdate, EmployeeID  
                                                                            from LiveTracking group by EmployeeID) b on a.EmployeeID=b.EmployeeID and a.trackingdate = b.trackingdate " + lParam + lParam2, sp);

            foreach (DataRow row in dtLiveTracking.Rows)
            {
                var mdlLiveTracking = new Model.mdlLiveTracking();
                mdlLiveTracking.TrackingDate = DateTime.Parse(row["TrackingDate"].ToString()).ToString("hh:mm tt");
                mdlLiveTracking.Longitude = row["Longitude"].ToString();
                mdlLiveTracking.Latitude = row["Latitude"].ToString();
                mdlLiveTracking.EmployeeID = row["EmployeeID"].ToString();
                mdlLiveTracking.VehicleID = row["VehicleID"].ToString();
                mdlLiveTracking.BranchID = row["BranchID"].ToString();

                mdlLiveTracking.StreetName = ReverseGeocodingFacade.GetStreetName(mdlLiveTracking.Latitude, mdlLiveTracking.Longitude);

                mdlLiveTrackingList.Add(mdlLiveTracking);
            }
            return mdlLiveTrackingList;
        }


        public static List<Model.mdlLiveTracking> GetIdleTracking(string lBranchID, string lEmployeeID)
        {
            string lParam = string.Empty;
            if (lBranchID != "")
            {
                lParam = "WHERE (a.BranchID = @BranchID) AND ( (CONVERT(CHAR(10),a.TrackingDate,110) = @Date) )";
            }

            string lParam2 = string.Empty;




            lParam2 = "a.EmployeeID =" + "'" + lEmployeeID.Substring(0, 4) + "'";







            List<SqlParameter> sp = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = lBranchID },
               new SqlParameter() {ParameterName = "@Date", SqlDbType = SqlDbType.NVarChar, Value = DateTime.Now.ToString("MM-dd-yyyy")  }
               //new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = lEmployeeID.Substring(0,4) }
            };

            var mdlLiveTrackingList = new List<Model.mdlLiveTracking>();

            DataTable dtLiveTracking = Manager.DataFacade.DTSQLCommand(@"select a.* from LiveTracking a
                                                                            inner join
                                                                            (select max(trackingdate) trackingdate, EmployeeID  
                                                                            from LiveTracking group by EmployeeID) b on a.EmployeeID=b.EmployeeID and a.trackingdate = b.trackingdate " + lParam + lParam2, sp);

            foreach (DataRow row in dtLiveTracking.Rows)
            {
                var mdlLiveTracking = new Model.mdlLiveTracking();
                mdlLiveTracking.TrackingDate = DateTime.Parse(row["TrackingDate"].ToString()).ToString("hh:mm tt");
                mdlLiveTracking.Longitude = row["Longitude"].ToString();
                mdlLiveTracking.Latitude = row["Latitude"].ToString();
                mdlLiveTracking.EmployeeID = row["EmployeeID"].ToString();
                mdlLiveTracking.VehicleID = row["VehicleID"].ToString();
                mdlLiveTracking.BranchID = row["BranchID"].ToString();

                mdlLiveTracking.StreetName = ReverseGeocodingFacade.GetStreetName(mdlLiveTracking.Latitude, mdlLiveTracking.Longitude);

                mdlLiveTrackingList.Add(mdlLiveTracking);
            }
            return mdlLiveTrackingList;
        }

    }
}
