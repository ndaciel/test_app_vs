
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;
using System.Globalization;

namespace Core.Manager
{
    public class VisitFacade : Base.Manager
    {
        public static List<Model.mdlDBVisit> LoadVisit(List<Model.mdlCallPlan> paramList)
        {
            var mdlVisitList = new List<Model.mdlDBVisit>();

            foreach (var param in paramList)
            {

                List<SqlParameter> sp = new List<SqlParameter>()
                {
                   new SqlParameter() {ParameterName = "@CallPlanID", SqlDbType = SqlDbType.NVarChar, Value = param.CallPlanID },
                };


                DataTable dtVisit = Manager.DataFacade.DTSQLCommand(@"SELECT  VisitID,
                                                                                BranchID,
                                                                                EmployeeID, VehicleID, VisitDate, isStart,
                                                                                isFinish, StartDate, EndDate, KMStart, KMFinish
                                                                       FROM Visit 
                                                                       WHERE VisitID=@CallPlanID", sp);

                foreach (DataRow row in dtVisit.Rows)
                {
                    var mdlVisit = new Model.mdlDBVisit();

                    mdlVisit.EmployeeID = row["EmployeeID"].ToString();
                    mdlVisit.BranchID = row["BranchID"].ToString();
                    mdlVisit.VisitID = row["VisitID"].ToString();
                    mdlVisit.VehicleID = row["VehicleID"].ToString();
                    mdlVisit.VisitDate = Convert.ToDateTime(row["VisitDate"]).ToString("yyyy-MM-dd");
                    mdlVisit.isStart = Convert.ToBoolean(row["isStart"].ToString());
                    mdlVisit.StartDate = Convert.ToDateTime(row["StartDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                    mdlVisit.EndDate = Convert.ToDateTime(row["EndDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                    mdlVisit.isFinish = Convert.ToBoolean(row["isFinish"].ToString());
                    mdlVisit.KMStart = row["KMStart"].ToString();
                    mdlVisit.KMFinish = row["KMFinish"].ToString();

                    mdlVisitList.Add(mdlVisit);
                }
            }


            return mdlVisitList;
        }

        public static List<Model.mdlDBVisitDetail2> LoadVisitDetail(List<Model.mdlDBVisit> paramList)
        {
            var visitDetailList = new List<Model.mdlDBVisitDetail2>();

            foreach (var param in paramList)
            {

                List<SqlParameter> sp = new List<SqlParameter>()
                {
                   new SqlParameter() {ParameterName = "@VisitID", SqlDbType = SqlDbType.NVarChar, Value = param.VisitID },
                };

                DataTable dtVisitDetail = Manager.DataFacade.DTSQLCommand(@"select a.CallPlanID,a.CustomerID,a.WarehouseID, b.isStart, b.isFinish,b.isVisit,b.StartDate,b.EndDate,
		                                                                b.ReasonID,b.ReasonDescription,b.Longitude,b.Latitude,b.isDeliver,b.isInRange,
		                                                                b.Distance,b.isInRangeCheckout,b.Duration,b.DistanceCheckout,b.LatitudeCheckOut,
		                                                                b.LongitudeCheckOut,a.Sequence
                                                                from CallPlanDetail a
                                                                left join VisitDetail b on b.VisitID = a.CallPlanID and b.CustomerID=a.CustomerID and b.WarehouseID=a.WarehouseID
                                                                where a.CallPlanID = @VisitID", sp);


                    foreach (DataRow row in dtVisitDetail.Rows)
                    {
                        var mdlVisitDetail = new Model.mdlDBVisitDetail2();

                        mdlVisitDetail.VisitID = row["CallPlanID"].ToString();
                        mdlVisitDetail.CustomerID = row["CustomerID"].ToString();
                        mdlVisitDetail.WarehouseID = row["WarehouseID"].ToString();
                        mdlVisitDetail.Seq = Convert.ToInt32(row["Sequence"].ToString());
                        mdlVisitDetail.StartDate = row["StartDate"].ToString();

                        if (mdlVisitDetail.StartDate == "" || mdlVisitDetail.StartDate == null)
                        {
                            mdlVisitDetail.isStart = false;
                            mdlVisitDetail.isFinish = false;
                            mdlVisitDetail.isVisit = false;
                            mdlVisitDetail.LatitudeCheckOut = "0";
                            mdlVisitDetail.LongitudeCheckOut = "0";
                            mdlVisitDetail.isDeliver = 0;
                            mdlVisitDetail.isInRange = false;
                            mdlVisitDetail.isInRangeCheckout = false;
                            mdlVisitDetail.Distance = 0;
                            mdlVisitDetail.Duration = "0";
                            mdlVisitDetail.DistanceCheckout = 0;

                            mdlVisitDetail.StartDate = "2000-01-01 00:00:00";
                            mdlVisitDetail.EndDate = "2000-01-01 00:00:00";

                            mdlVisitDetail.ReasonID = "";
                            mdlVisitDetail.ReasonDescription = "";
                            mdlVisitDetail.Latitude = "";
                            mdlVisitDetail.Longitude = "";
                        }
                        else
                        {
                            mdlVisitDetail.isStart = Convert.ToBoolean(row["isStart"].ToString());
                            mdlVisitDetail.isFinish = Convert.ToBoolean(row["isFinish"].ToString());
                            mdlVisitDetail.isVisit = Convert.ToBoolean(row["isVisit"].ToString());
                            mdlVisitDetail.LatitudeCheckOut = row["LatitudeCheckOut"].ToString();
                            mdlVisitDetail.LongitudeCheckOut = row["LongitudeCheckOut"].ToString();
                            mdlVisitDetail.isDeliver = Convert.ToInt32(row["isDeliver"].ToString());
                            mdlVisitDetail.isInRange = Convert.ToBoolean(row["isInRange"].ToString());
                            mdlVisitDetail.isInRangeCheckout = Convert.ToBoolean(row["isInRangeCheckout"].ToString());
                            mdlVisitDetail.Distance = Convert.ToDouble(row["Distance"].ToString());
                            mdlVisitDetail.Duration = row["Duration"].ToString();
                            mdlVisitDetail.DistanceCheckout = Convert.ToDouble(row["DistanceCheckout"].ToString());

                            mdlVisitDetail.StartDate = Convert.ToDateTime(row["StartDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                            mdlVisitDetail.EndDate = Convert.ToDateTime(row["EndDate"]).ToString("yyyy-MM-dd HH:mm:ss");

                            mdlVisitDetail.ReasonID = row["ReasonID"].ToString();
                            mdlVisitDetail.ReasonDescription = row["ReasonDescription"].ToString();
                            mdlVisitDetail.Latitude = row["Latitude"].ToString();
                            mdlVisitDetail.Longitude = row["Longitude"].ToString();
                        }

                        visitDetailList.Add(mdlVisitDetail);
                }
            }

            return visitDetailList;
        }

        public static int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public static Model.mdlResultList UploadVisit(List<Model.mdlVisitParam> lVisitParamlist)
        {
            var mdlResultList = new List<Model.mdlResult>();
            var mdlResult = new Model.mdlResult();

            //var listVisit = new List<Model.mdlDBVisit>();
            String ErrVisitlist = "";
            foreach (var lVisitParam in lVisitParamlist)
            {
                //Update Callplan karena sudah selesai di kunjungi
                if (lVisitParam.isFinish == true)
                {
                    CallPlanFacade.UpdateVisitbyIsFinish(lVisitParamlist.FirstOrDefault().VisitID);
                }

                //Insert dan update data Visit
                var week = GetIso8601WeekOfYear(Convert.ToDateTime(lVisitParam.VisitDate));
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@VisitID", SqlDbType = SqlDbType.NVarChar, Value = lVisitParam.VisitID },
                    new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = lVisitParam.BranchID},
                    new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = lVisitParam.EmployeeID },
                    new SqlParameter() {ParameterName = "@VehicleID", SqlDbType = SqlDbType.NVarChar, Value = lVisitParam.VehicleID },
                    new SqlParameter() {ParameterName = "@VisitDate", SqlDbType = SqlDbType.NVarChar, Value = lVisitParam.VisitDate },
                    new SqlParameter() {ParameterName = "@VisitWeek", SqlDbType = SqlDbType.NVarChar, Value = week },
                    new SqlParameter() {ParameterName = "@isStart", SqlDbType = SqlDbType.NVarChar, Value = lVisitParam.isStart },
                    new SqlParameter() {ParameterName = "@isFinish", SqlDbType = SqlDbType.NVarChar, Value = lVisitParam.isFinish },
                    new SqlParameter() {ParameterName = "@StartDate", SqlDbType = SqlDbType.NVarChar, Value = lVisitParam.StartDate },
                    new SqlParameter() {ParameterName = "@EndDate", SqlDbType = SqlDbType.NVarChar, Value = lVisitParam.EndDate },
                    new SqlParameter() {ParameterName = "@KMStart", SqlDbType = SqlDbType.NVarChar, Value = lVisitParam.KMStart },
                    new SqlParameter() {ParameterName = "@KMFinish", SqlDbType = SqlDbType.NVarChar, Value = lVisitParam.KMFinish },
                    new SqlParameter() {ParameterName = "@deviceid", SqlDbType = SqlDbType.NVarChar, Value = lVisitParam.deviceID }
                };


                String query = @"BEGIN TRAN
                                   UPDATE Visit SET 
                                                VisitID = @VisitID,
                                                BranchID = @BranchID,
                                                EmployeeID = @EmployeeID,
                                                VehicleID = @VehicleID,
                                                VisitDate = @VisitDate,
                                                VisitWeek = @VisitWeek,
                                                isStart = @isStart,
                                                isFinish = @isFinish,
                                                StartDate = @StartDate,
                                                EndDate = @EndDate,
                                                KMStart = @KMStart,
                                                KMFinish = @KMFinish,
                                                LastUpdatedBy = @deviceid,
                                                LastUpdatedDate = GETDATE(),
                                                rkDelete = 0
                                   WHERE VisitID = @VisitID 

                                   IF @@rowcount = 0
                                   BEGIN
                                      INSERT INTO Visit (
                                                VisitID,
                                                BranchID,
                                                EmployeeID,
                                                VehicleID,
                                                VisitDate,
                                                VisitWeek,
                                                isStart,
                                                isFinish,
                                                StartDate,
                                                EndDate,
                                                KMStart,
                                                KMFinish,
                                                CreatedBy,
                                                CreatedDate,
                                                LastUpdatedBy,
                                                LastUpdatedDate,
                                                rkDelete
                                      ) 
                                      VALUES (
                                                @VisitID,
                                                @BranchID,
                                                @EmployeeID,
                                                @VehicleID,
                                                @VisitDate,
                                                @VisitWeek,
                                                @isStart,
                                                @isFinish,
                                                @StartDate,
                                                @EndDate,
                                                @KMStart,
                                                @KMFinish,
                                                @deviceid,
                                                GETDATE(),
                                                @deviceid,
                                                GETDATE(),
                                                0
                                            )
                                   END
                                COMMIT TRAN";
                String resultVst = DataFacade.DTSQLVoidCommand(query, sp);
                if (resultVst.Equals("0"))
                {
                    mdlResult.Result = "0";
                    ErrVisitlist += lVisitParam.VisitID + ",";
                }
            }

            //mdlResult.Result = Manager.DataFacade.DTSQLListInsert(listVisit, "Visit");
            if (mdlResult.Result == "0")
            {
                mdlResult.Result = "0";
                mdlResult.ResultValue = "Terdapat Visit yang gagal di upload = " + ErrVisitlist;

            }
            else
            {
                mdlResult.Result = "1";
                mdlResult.ResultValue = "Success";
            }

            mdlResultList.Add(mdlResult);

            var mdlResultListnew = new Model.mdlResultList();
            mdlResultListnew.ResultList = mdlResultList;
            return mdlResultListnew;
        }

        public static Model.mdlResultList InsertVisit(List<Model.mdlVisitParam> lVisitParamlist)
        {
            var mdlResultList = new List<Model.mdlResult>();

            var mdlResult = new Model.mdlResult();

            var listVisit = new List<Model.mdlDBVisit>();
            foreach (var temp in lVisitParamlist)
            {
                var mdlVisit = new Model.mdlDBVisit();
                mdlVisit.VisitID = temp.VisitID;
                mdlVisit.BranchID = temp.BranchID;
                mdlVisit.EmployeeID = temp.EmployeeID;
                mdlVisit.VehicleID = temp.VehicleID;
                mdlVisit.VisitDate = temp.VisitDate;

                var week  = GetIso8601WeekOfYear(Convert.ToDateTime(temp.VisitDate));
                mdlVisit.VisitWeek = week;

                mdlVisit.isStart = temp.isStart;
                mdlVisit.isFinish = temp.isFinish;

                mdlVisit.StartDate = temp.StartDate;
                mdlVisit.EndDate = temp.EndDate;

                mdlVisit.KMStart = temp.KMStart;
                mdlVisit.KMFinish = temp.KMFinish;

                mdlVisit.CreatedBy = temp.EmployeeID;
                mdlVisit.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                mdlVisit.LastUpdatedBy = temp.EmployeeID;
                mdlVisit.LastUpdatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                mdlVisit.rkDelete = 0;

                //var visit = GetVisitByID(mdlVisit.VisitID);
                //if (visit != null)
                //{
                //    //mdlResult.Result = "|| VisitID : " + lVisitParamlist.FirstOrDefault().VisitID + " " + "|| IDExist ||";
                //    //mdlResultList.Add(mdlResult);
                //    DateTime EndDate = DateTime.ParseExact(mdlVisit.EndDate, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                //    DateTime Now = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

                //    visit.EndDate = EndDate;
                //    visit.isFinish = mdlVisit.isFinish;
                //    visit.KMFinish = Convert.ToInt32(mdlVisit.KMFinish);
                //    visit.LastUpdatedBy = mdlVisit.EmployeeID;
                //    visit.LastUpdatedDate = Now;
                //    DataContext.SubmitChanges();

                //    if (visit.isFinish == true)
                //    {
                //        CallPlanFacade.UpdateVisitbyIsFinish(lVisitParamlist.FirstOrDefault().VisitID);
                //    }
                //}
                //else
                //{
                //    listVisit.Add(mdlVisit);
                //}
                listVisit.Add(mdlVisit);
                if (mdlVisit.isFinish == true)
                {
                    CallPlanFacade.UpdateVisitbyIsFinish(lVisitParamlist.FirstOrDefault().VisitID);
                }
            }





            //var mdlVisitList = new List<Model.Visit>();
            //foreach(var temp in lVisitParamlist)
            //{
            //    var mdlVisit = new Model.Visit();
            //    mdlVisit.VisitID = temp.VisitID;
            //    mdlVisit.BranchID = temp.BranchID;
            //    mdlVisit.EmployeeID = temp.EmployeeID;
            //    mdlVisit.VehicleID = temp.VehicleID;
            //    mdlVisit.VisitDate = Convert.ToDateTime(temp.VisitDate);
            //    mdlVisit.isStart = Convert.ToBoolean(temp.isStart);
            //    mdlVisit.isFinish = Convert.ToBoolean(temp.isFinish);
            //    mdlVisit.StartDate = Convert.ToDateTime(temp.StartDate);
            //    mdlVisit.EndDate = Convert.ToDateTime(temp.EndDate);
            //    mdlVisit.KMStart = Convert.ToInt32(temp.KMStart);
            //    mdlVisit.KMStart = Convert.ToInt32(temp.KMFinish);
            //    mdlVisitList.Add(mdlVisit);
            //}

            mdlResult.Result = Manager.DataFacade.DTSQLListInsert(listVisit, "Visit");
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



            var mdlResultListnew = new Model.mdlResultList();
            mdlResultListnew.ResultList = mdlResultList;
            return mdlResultListnew;
        }

        public static Model.mdlResultList InsertVisitDetail(List<Model.mdlVisitDetailParamNew> lVisitDetailParamlist)
        {
            var mdlResultList = new List<Model.mdlResult>();

            var mdlResult = new Model.mdlResult();


            var listVisitDetail = new List<Model.mdlDBVisitDetail>();
            String ErrVisitlist = "";
            foreach (var temp in lVisitDetailParamlist)
            {
                var mdlVisitDetail = new Model.mdlDBVisitDetail();
                mdlVisitDetail.VisitID = temp.VisitID;
                mdlVisitDetail.CustomerID = temp.CustomerID;
                mdlVisitDetail.WarehouseID = temp.WarehouseID;
                bool tempBool = false;
                if (temp.isStart == "1")
                    tempBool = true;
                mdlVisitDetail.isStart = tempBool;

                tempBool = false;
                if (temp.isFinish == "1")
                    tempBool = true;
                mdlVisitDetail.isFinish = tempBool;

                tempBool = false;
                if (temp.isVisit == "1")
                    tempBool = true;
                mdlVisitDetail.isVisit = tempBool;

                mdlVisitDetail.StartDate = Convert.ToDateTime(temp.StartDate);
                mdlVisitDetail.EndDate = Convert.ToDateTime(temp.EndDate);
                mdlVisitDetail.ReasonID = temp.ReasonID;
                mdlVisitDetail.ReasonDescription = temp.ReasonDescription;
                mdlVisitDetail.Longitude = temp.Longitude;
                mdlVisitDetail.Latitude = temp.Latitude;

                if (temp.LatitudeCheckOut == null || temp.LatitudeCheckOut == "")
                {
                    mdlVisitDetail.LatitudeCheckOut = "0";
                    mdlVisitDetail.LongitudeCheckOut = "0";
                }
                else
                {
                    mdlVisitDetail.LatitudeCheckOut = temp.LatitudeCheckOut;
                    mdlVisitDetail.LongitudeCheckOut = temp.LongitudeCheckOut;
                }

                mdlVisitDetail.isDeliver = Convert.ToInt32(temp.isDeliver);

                tempBool = false;
                if (temp.isInRange == "1")
                    tempBool = true;
                mdlVisitDetail.isInRange = tempBool;
                mdlVisitDetail.Distance = Convert.ToDouble(temp.Distance);

                mdlVisitDetail.CreatedBy = temp.EmployeeID;
                mdlVisitDetail.CreatedDate = DateTime.Now;

                mdlVisitDetail.LastUpdatedBy = temp.EmployeeID;
                mdlVisitDetail.LastUpdatedDate = DateTime.Now;

                tempBool = false;
                if (temp.isInRangeCheckout == "1")
                    tempBool = true;
                mdlVisitDetail.isInRangeCheckout = tempBool;
                mdlVisitDetail.Duration = temp.Duration;
                mdlVisitDetail.DistanceCheckout = Convert.ToDouble(temp.DistanceCheckout);
                mdlVisitDetail.ReasonSequence = temp.ReasonSequence;


                bool lCheckVisitDetailID = CheckbyVisitDetailID(mdlVisitDetail.VisitID, mdlVisitDetail.CustomerID, mdlVisitDetail.WarehouseID);
                if (lCheckVisitDetailID == true)
                    listVisitDetail.Add(mdlVisitDetail);

                if (temp.CustomerID == temp.WarehouseID)
                {
                    var customer = CustomerFacade.GetCustomerDetail(temp.CustomerID);
                    if (customer.Latitude == "" || customer.Latitude == null || customer.Latitude == "0")
                        customer.Latitude = temp.Latitude;
                    if (customer.Longitude == "" || customer.Longitude == null || customer.Longitude == "0")
                        customer.Longitude = temp.Longitude;
                    DataContext.SubmitChanges();
                }
                else
                { //FERNANDES -- update longlat warehouse
                    var warehouse = WarehouseFacade.GetWarehouseDetail(temp.WarehouseID, temp.CustomerID);
                    if (warehouse != null)
                    {
                        if (warehouse.Latitude == "" || warehouse.Latitude == null || warehouse.Latitude == "0")
                            warehouse.Latitude = temp.Latitude;
                        if (warehouse.Longitude == "" || warehouse.Longitude == null || warehouse.Longitude == "0")
                            warehouse.Longitude = temp.Longitude;
                        DataContext.SubmitChanges();
                    }
                }


                //Insert dan update data Visit Detail
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@VisitID", SqlDbType = SqlDbType.NVarChar, Value = mdlVisitDetail.VisitID },
                    new SqlParameter() {ParameterName = "@CustomerID", SqlDbType = SqlDbType.NVarChar, Value = mdlVisitDetail.CustomerID},
                    new SqlParameter() {ParameterName = "@WarehouseID", SqlDbType = SqlDbType.NVarChar, Value = mdlVisitDetail.WarehouseID },
                    new SqlParameter() {ParameterName = "@isStart", SqlDbType = SqlDbType.NVarChar, Value = mdlVisitDetail.isStart },
                    new SqlParameter() {ParameterName = "@isFinish", SqlDbType = SqlDbType.NVarChar, Value = mdlVisitDetail.isFinish },
                    new SqlParameter() {ParameterName = "@StartDate", SqlDbType = SqlDbType.DateTime, Value = mdlVisitDetail.StartDate },
                    new SqlParameter() {ParameterName = "@EndDate", SqlDbType = SqlDbType.DateTime, Value = mdlVisitDetail.EndDate },
                    new SqlParameter() {ParameterName = "@ReasonID", SqlDbType = SqlDbType.NVarChar, Value = mdlVisitDetail.ReasonID },
                    new SqlParameter() {ParameterName = "@ReasonDescription", SqlDbType = SqlDbType.NVarChar, Value = mdlVisitDetail.ReasonDescription },
                    new SqlParameter() {ParameterName = "@Longitude", SqlDbType = SqlDbType.NVarChar, Value = mdlVisitDetail.Longitude },
                    new SqlParameter() {ParameterName = "@Latitude", SqlDbType = SqlDbType.NVarChar, Value = mdlVisitDetail.Latitude },
                    new SqlParameter() {ParameterName = "@isDeliver", SqlDbType = SqlDbType.NVarChar, Value = mdlVisitDetail.isDeliver },
                    new SqlParameter() {ParameterName = "@isInRange", SqlDbType = SqlDbType.NVarChar, Value = mdlVisitDetail.isInRange },
                    new SqlParameter() {ParameterName = "@Distance", SqlDbType = SqlDbType.NVarChar, Value = mdlVisitDetail.Distance },
                    new SqlParameter() {ParameterName = "@isInRangeCheckout", SqlDbType = SqlDbType.NVarChar, Value = mdlVisitDetail.isInRangeCheckout },
                    new SqlParameter() {ParameterName = "@Duration", SqlDbType = SqlDbType.NVarChar, Value = mdlVisitDetail.Duration },
                    new SqlParameter() {ParameterName = "@DistanceCheckout", SqlDbType = SqlDbType.NVarChar, Value = mdlVisitDetail.DistanceCheckout },
                    new SqlParameter() {ParameterName = "@LatitudeCheckOut", SqlDbType = SqlDbType.NVarChar, Value = mdlVisitDetail.LatitudeCheckOut },
                    new SqlParameter() {ParameterName = "@LongitudeCheckOut", SqlDbType = SqlDbType.NVarChar, Value = mdlVisitDetail.LongitudeCheckOut },
                    new SqlParameter() {ParameterName = "@isVisit", SqlDbType = SqlDbType.NVarChar, Value = mdlVisitDetail.isVisit },
                    new SqlParameter() {ParameterName = "@CreatedBy", SqlDbType = SqlDbType.NVarChar, Value = mdlVisitDetail.CreatedBy },
                    new SqlParameter() {ParameterName = "@LastUpdatedBy", SqlDbType = SqlDbType.NVarChar, Value = mdlVisitDetail.LastUpdatedBy },
                    new SqlParameter() {ParameterName = "@ReasonSequence", SqlDbType = SqlDbType.NVarChar, Value = mdlVisitDetail.ReasonSequence }
                };


                String query = @"BEGIN TRAN
                                   UPDATE VisitDetail SET 
                                                isStart = @isStart,
                                                isFinish = @isFinish,
                                                StartDate = @StartDate,
                                                EndDate = @EndDate,
                                                ReasonID = @ReasonID,
                                                ReasonDescription = @ReasonDescription,
                                                Longitude = @Longitude,
                                                Latitude = @Latitude,
                                                isDeliver = @isDeliver,
                                                isInRange = @isInRange,
                                                Distance = @Distance,
                                                isInRangeCheckout = @isInRangeCheckout,
                                                Duration = @Duration,
                                                DistanceCheckout = @DistanceCheckout,
                                                LatitudeCheckOut = @LatitudeCheckOut,
                                                LongitudeCheckOut = @LongitudeCheckOut,
                                                isVisit = @isVisit,
                                                LastUpdatedBy = @LastUpdatedBy,
                                                LastUpdatedDate = GETDATE(),
                                                ReasonSequence = @ReasonSequence
                                   WHERE VisitID = @VisitID AND CustomerID = @CustomerID AND
                                                WarehouseID = @WarehouseID

                                   IF @@rowcount = 0
                                   BEGIN
                                      INSERT INTO VisitDetail (
                                                VisitID,
                                                CustomerID,
                                                WarehouseID,
                                                isStart,
                                                isFinish,
                                                StartDate,
                                                EndDate,
                                                ReasonID,
                                                ReasonDescription,
                                                Longitude,
                                                Latitude,
                                                isDeliver,
                                                isInRange,
                                                Distance,
                                                isInRangeCheckout,
                                                Duration,
                                                DistanceCheckout,
                                                LatitudeCheckOut,
                                                LongitudeCheckOut,
                                                isVisit,
                                                CreatedBy,
                                                CreatedDate,
                                                LastUpdatedBy,
                                                LastUpdatedDate,
                                                ReasonSequence
                                      ) 
                                      VALUES (
                                                @VisitID,
                                                @CustomerID,
                                                @WarehouseID,
                                                @isStart,
                                                @isFinish,
                                                @StartDate,
                                                @EndDate,
                                                @ReasonID,
                                                @ReasonDescription,
                                                @Longitude,
                                                @Latitude,
                                                @isDeliver,
                                                @isInRange,
                                                @Distance,
                                                @isInRangeCheckout,
                                                @Duration,
                                                @DistanceCheckout,
                                                @LatitudeCheckOut,
                                                @LongitudeCheckOut,
                                                @isVisit,
                                                @CreatedBy,
                                                GETDATE(),
                                                @LastUpdatedBy,
                                                GETDATE(),
                                                @ReasonSequence
                                            )
                                   END
                                COMMIT TRAN";
                String resultVst = DataFacade.DTSQLVoidCommand(query, sp);
                if (resultVst.Equals("0"))
                {
                    mdlResult.Result = "0";
                    ErrVisitlist += mdlVisitDetail.VisitID + ",";
                }
            }

            if (mdlResult.Result == "0")
            {
                mdlResult.Result = "0";
                mdlResult.ResultValue = "Terdapat Visit Detail yang gagal di upload = " + ErrVisitlist;
            }
            else
            {
                mdlResult.Result = "1";
                mdlResult.ResultValue = "Success";
            }

            mdlResultList.Add(mdlResult);

            var mdlResultListnew = new Model.mdlResultList();
            mdlResultListnew.ResultList = mdlResultList;
            return mdlResultListnew;
        }

        public static Model.mdlResultList UploadInsertVisitDetail(List<Model.mdlVisitDetailParam> lVisitDetailParamlist, TransactionScope scope)
        {
            var mdlResultList = new List<Model.mdlResult>();

            var mdlResult = new Model.mdlResult();


            var listVisitDetail = new List<Model.mdlDBVisitDetail>();
            foreach (var temp in lVisitDetailParamlist)
            {
                var mdlVisitDetail = new Model.mdlDBVisitDetail();
                mdlVisitDetail.VisitID = temp.VisitID;
                mdlVisitDetail.CustomerID = temp.CustomerID;
                mdlVisitDetail.WarehouseID = temp.VisitID;
                mdlVisitDetail.isStart = Convert.ToBoolean(temp.isStart);
                mdlVisitDetail.isFinish = Convert.ToBoolean(temp.isFinish);
                mdlVisitDetail.StartDate = Convert.ToDateTime(temp.StartDate);
                mdlVisitDetail.EndDate = Convert.ToDateTime(temp.EndDate);
                mdlVisitDetail.ReasonID = temp.ReasonID;
                mdlVisitDetail.ReasonDescription = temp.ReasonDescription;
                mdlVisitDetail.Longitude = temp.Longitude;
                mdlVisitDetail.Latitude = temp.Latitude;
                mdlVisitDetail.isDeliver = temp.isDeliver;
                mdlVisitDetail.isInRange = Convert.ToBoolean(temp.isInRange);
                mdlVisitDetail.Distance = Convert.ToDouble(temp.Distance);
                listVisitDetail.Add(mdlVisitDetail);

            }

            bool lCheckVisitDetailID = CheckbyVisitDetailID(lVisitDetailParamlist.FirstOrDefault().VisitID, lVisitDetailParamlist.FirstOrDefault().CustomerID, lVisitDetailParamlist.FirstOrDefault().WarehouseID);
            if (lCheckVisitDetailID == false)
            {
                mdlResult.Result = "|| VisitID : " + lVisitDetailParamlist.FirstOrDefault().VisitID + " And CustomerID : " + lVisitDetailParamlist.FirstOrDefault().CustomerID + " " + "|| IDExist ||";
                mdlResultList.Add(mdlResult);
            }
            else
            {

                mdlResult.Result = Manager.DataFacade.DTSQLListInsert(listVisitDetail, "VisitDetail");


                if (mdlResult.Result.Contains("1") == true)
                {
                }
                else
                {
                    string ResultSubstring;

                    if (mdlResult.Result.Length > 500)
                    {
                        ResultSubstring = mdlResult.Result.Substring(0, 500);

                        mdlResult.Result = ResultSubstring;
                        //

                    }
                    scope.Dispose();

                }

                mdlResultList.Add(mdlResult);
            }


            var mdlResultListnew = new Model.mdlResultList();
            mdlResultListnew.ResultList = mdlResultList;
            return mdlResultListnew;
        }

        //001
        public static List<Model.mdlVisitReport> LoadVisitReport(string lBranchID, DateTime lStartDate, DateTime lEndDate, List<string> lEmployeeIDlist)
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
               //new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = lEmployeeID },
               new SqlParameter() {ParameterName = "@StartDate", SqlDbType = SqlDbType.DateTime, Value = lStartDate },
               new SqlParameter() {ParameterName = "@EndDate", SqlDbType = SqlDbType.DateTime, Value = lEndDate }
            };

            //KONDISI Jika mau ditampilkan foto
            //,
            //h.ImageBase64,
            //h.ImagePath

            //LEFT JOIN (select ImageType,ImageBase64,ImagePath,VisitID,CustomerID,WarehouseID from CustomerImage where ImageType='Visit') h ON h.VisitID = a.VisitID AND h.CustomerID = b.CustomerID AND h.WarehouseID = b.WarehouseID

            string sql = @"SELECT   a.BranchID, 
	                                c.BranchName, 
                                    a.EmployeeID,
									f.EmployeeName,
                                    a.VehicleID,
                                    b.CustomerID,
                                    b.WarehouseID, 
                                    i.WarehouseName,
	                                e.CustomerName,
	                                b.StartDate,
	                                b.EndDate,
	                                b.isStart,
	                                b.isFinish,
	                                b.ReasonID,
                                    b.isInRange,
                                    b.isInRangeCheckout,
                                    g.Value,
                                    b.Duration
	                            FROM Visit a    INNER JOIN Branch c ON c.BranchID = a.BranchID
												INNER JOIN Employee f ON a.EmployeeID = f.EmployeeID
                                                INNER JOIN VisitDetail b ON a.VisitID = b.VisitID 
                                                LEFT JOIN Customer e ON e.CustomerID = b.CustomerID 
                                                LEFT JOIN Warehouse i ON i.WarehouseID = b.WarehouseID
                                                LEFT JOIN Reason g ON g.ReasonID = b.ReasonID
                                WHERE (a.BranchID = @BranchID) AND (a.VisitDate BETWEEN @StartDate and @EndDate) AND " + lParam + "ORDER BY b.StartDate";
            
            //d.DONumber, 
            //d.DOStatus,
            //LEFT JOIN DeliveryOrder d ON b.VisitID=d.VisitID and b.CustomerID=d.CustomerID

            var mdlVisitList = new List<Model.mdlVisitReport>();
            DataTable dtVisit = Manager.DataFacade.DTSQLCommand(sql, sp);

            foreach (DataRow row in dtVisit.Rows)
            {
                var mdlVisitReport = new Model.mdlVisitReport();
                mdlVisitReport.BranchID = row["BranchID"].ToString();
                mdlVisitReport.BranchName = row["BranchName"].ToString();
                mdlVisitReport.CustomerID = row["CustomerID"].ToString();
                mdlVisitReport.CustomerName = row["CustomerName"].ToString();
                mdlVisitReport.WarehouseID = row["WarehouseID"].ToString();
                mdlVisitReport.WarehouseName = row["WarehouseName"].ToString();

                if (mdlVisitReport.CustomerID == mdlVisitReport.WarehouseID)
                {
                    mdlVisitReport.CustomerID.ToString();
                }
                else
                {
                    mdlVisitReport.CustomerID = mdlVisitReport.CustomerID + " - " + mdlVisitReport.WarehouseID;
                }

                //mdlVisitReport.DONumber = row["DONumber"].ToString();
                //mdlVisitReport.DOStatus = row["DOStatus"].ToString();
                mdlVisitReport.EndDate = Convert.ToDateTime(row["EndDate"]).ToString("dd-MM-yyyy HH:mm:ss");

                mdlVisitReport.isFinish = row["isFinish"].ToString();
                if (mdlVisitReport.isFinish == "True")
                {
                    mdlVisitReport.isFinish = "Ya";
                }
                else
                {
                    mdlVisitReport.isFinish = "Tidak";
                }
                

                mdlVisitReport.isStart = row["isStart"].ToString();
                if (mdlVisitReport.isStart == "True")
                {
                    mdlVisitReport.isStart = "Ya";
                }
                else
                {
                    mdlVisitReport.isStart = "Tidak";
                    mdlVisitReport.isFinish = "Tidak";
                }
                
                mdlVisitReport.Reason = row["Value"].ToString();
                mdlVisitReport.StartDate = Convert.ToDateTime(row["StartDate"]).ToString("dd-MM-yyyy HH:mm:ss");

                if (mdlVisitReport.StartDate == "01-01-2000 00:00:00")
                {
                    mdlVisitReport.StartDate = "-";
                    mdlVisitReport.EndDate = "-";
                }

                mdlVisitReport.EmployeeID = row["EmployeeID"].ToString();
                mdlVisitReport.EmployeeName = row["EmployeeName"].ToString();
                mdlVisitReport.VehicleID = row["VehicleID"].ToString();
                //mdlVisitReport.ImageBase64 = row["ImageBase64"].ToString();
                //mdlVisitReport.ImagePath = row["ImagePath"].ToString();
                mdlVisitReport.isInRange = row["isInRange"].ToString();
                if (mdlVisitReport.isInRange == "True")
                {
                    mdlVisitReport.isInRange = "Ya";
                }
                else
                {
                    mdlVisitReport.isInRange = "Tidak";
                }

                mdlVisitReport.isInRangeCheckOut = row["isInRangeCheckout"].ToString();
                if (mdlVisitReport.isInRangeCheckOut == "True")
                {
                    mdlVisitReport.isInRangeCheckOut = "Ya";
                }
                else
                {
                    mdlVisitReport.isInRangeCheckOut = "Tidak";
                }
                //DateTime tStartDate = DateTime.Parse(row["StartDate"].ToString());
                //DateTime tEndDate = DateTime.Parse(row["EndDate"].ToString());
                //TimeSpan lduration = tEndDate - tStartDate;
                //mdlVisitReport.Duration = lduration.ToString();
                mdlVisitReport.Duration = row["Duration"].ToString();

                mdlVisitList.Add(mdlVisitReport);
            }
            return mdlVisitList;
        }

        public static bool CheckbyVisitID(string lVisitID)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName = "@VisitID", SqlDbType = SqlDbType.NVarChar, Value = lVisitID },
            };

            DataTable dtVisit = Manager.DataFacade.DTSQLCommand(@"SELECT TOP 1 VisitID
                                                                   FROM Visit 
                                                                   WHERE VisitID = @VisitID", sp);
            bool lCheck = false;
            if (dtVisit.Rows.Count == 0)
            {
                lCheck = true;
            }

            return lCheck;
        }

        public static bool CheckbyVisitDetailID(string lVisitID, string lCustomerID, string warehouseID)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName = "@VisitID", SqlDbType = SqlDbType.NVarChar, Value = lVisitID },
               new SqlParameter() {ParameterName = "@CustomerID", SqlDbType = SqlDbType.NVarChar, Value = lCustomerID },
               new SqlParameter() {ParameterName = "@WarehouseID", SqlDbType = SqlDbType.NVarChar, Value = warehouseID }
            };

            DataTable dtVisit = Manager.DataFacade.DTSQLCommand(@"SELECT TOP 1 VisitID
                                                                   FROM VisitDetail 
                                                                   WHERE VisitID = @VisitID AND CustomerID = @CustomerID AND WarehouseID=@WarehouseID", sp);
            bool lCheck = false;
            if (dtVisit.Rows.Count == 0)
            {
                lCheck = true;
            }

            return lCheck;
        }

        public static Model.Visit GetVisitByID(string visitID)
        {
            return DataContext.Visits.FirstOrDefault(fld => fld.VisitID.Equals(visitID));
        }

//        public static Model.mdlDBVisit GetVisitByID(string visitID)
//        {
//            List<SqlParameter> sp = new List<SqlParameter>()
//            {
//            };

//            DataTable dtVisit = Manager.DataFacade.DTSQLCommand(@"SELECT TOP 1   
//                                                                   VisitID,EndDate,KMFinish,isFinish,LastUpdatedDate,LastUpdatedBy 
//                                                                   FROM Visit 
//                                                                   WHERE VisitID='"+visitID+"' ", sp);

//            var mdlVisit = new Model.mdlDBVisit();

//            foreach (DataRow row in dtVisit.Rows)
//            {
//                mdlVisit.VisitID = row["VisitID"].ToString();
//                mdlVisit.EndDate = row["EndDate"].ToString();
//                mdlVisit.isFinish = Convert.ToBoolean(row["isFinish"].ToString());
//                mdlVisit.KMFinish = row["KMFinish"].ToString();
//                mdlVisit.LastUpdatedBy = row["LastUpdatedBy"].ToString();
//                mdlVisit.LastUpdatedDate = row["LastUpdatedDate"].ToString();
//            }

//            return mdlVisit;
//        }

        public static List<Model.mdlVisitTracking> GetVisitTracking(string lBranchID, DateTime lStartDate, DateTime lEndDate, List<string> lEmployeeIDlist)
        {
            string lParam = string.Empty;

            foreach (var lEmployeeID in lEmployeeIDlist)
            {
                if (lParam == "")
                {
                    lParam = "a.EmployeeID =" + "'" + lEmployeeID + "'";
                }
                else
                {
                    lParam += "OR a.EmployeeID =" + "'" + lEmployeeID + "'";
                }

            }

            lParam = "(" + lParam + ")";

            List<SqlParameter> sp = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = lBranchID },
               new SqlParameter() {ParameterName = "@StartDate", SqlDbType = SqlDbType.DateTime, Value = lStartDate },
               new SqlParameter() {ParameterName = "@EndDate", SqlDbType = SqlDbType.DateTime, Value = lEndDate },
            };

            var mdlVisitTrackingList = new List<Model.mdlVisitTracking>();

            DataTable dtVisitTracking = Manager.DataFacade.DTSQLCommand(@"SELECT b.CustomerName, c.Longitude, c.Latitude, c.StartDate, c.EndDate, a.EmployeeID, c.CustomerID, c.Duration, c.WarehouseID
                                                                        FROM Visit a
                                                                        INNER JOIN VisitDetail c ON c.VisitID = a.VisitID
                                                                        INNER JOIN Customer b ON b.CustomerID = c.CustomerID
                                                                        INNER JOIN Branch d ON d.BranchID = a.BranchID
                                                                        WHERE (a.BranchID = @BranchID) AND (a.VisitDate BETWEEN @StartDate and @EndDate) AND (c.Longitude <> '' and c.Latitude <> '') AND" + lParam + "Order By c.StartDate", sp);

            foreach (DataRow row in dtVisitTracking.Rows)
            {
                var mdlVisitTracking = new Model.mdlVisitTracking();
                mdlVisitTracking.CustomerID = row["CustomerID"].ToString();
                mdlVisitTracking.Customer = row["CustomerName"].ToString();
                mdlVisitTracking.Longitude = row["Longitude"].ToString();
                mdlVisitTracking.Latitude = row["Latitude"].ToString();
                mdlVisitTracking.Start = Convert.ToDateTime(row["StartDate"]).ToString("dd-MM-yyyy HH:mm:ss");
                mdlVisitTracking.End = Convert.ToDateTime(row["EndDate"]).ToString("dd-MM-yyyy HH:mm:ss");

                mdlVisitTracking.StreetName = ReverseGeocodingFacade.GetStreetName(mdlVisitTracking.Latitude, mdlVisitTracking.Longitude);
                mdlVisitTracking.EmployeeID = row["EmployeeID"].ToString();

                //DateTime tStartDate = DateTime.Parse(row["StartDate"].ToString());
                //DateTime tEndDate = DateTime.Parse(row["EndDate"].ToString());
                //TimeSpan lduration = tEndDate - tStartDate;
                //mdlVisitTracking.Duration = lduration.ToString();

                mdlVisitTracking.Duration = row["Duration"].ToString();
                mdlVisitTracking.WarehouseID = row["WarehouseID"].ToString();

                mdlVisitTrackingList.Add(mdlVisitTracking);
            }
            return mdlVisitTrackingList;
        }

        //001
        public static List<Model.mdlKoordinatKunjungan> GetKoordinatKunjungan(DateTime lStartDate, DateTime lEndDate, string lCustomerID)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName = "@CustomerID", SqlDbType = SqlDbType.NVarChar, Value = lCustomerID },
               new SqlParameter() {ParameterName = "@StartDate", SqlDbType = SqlDbType.Date, Value = lStartDate },
               new SqlParameter() {ParameterName = "@EndDate", SqlDbType = SqlDbType.Date, Value = lEndDate },
            };

            var mdlKoordinatKunjunganList = new List<Model.mdlKoordinatKunjungan>();

            DataTable dtKoordinatKunjungan = Manager.DataFacade.DTSQLCommand(@"select convert(varchar,a.VisitDate,106) Date,b.Latitude,b.Longitude,a.VisitDate,b.CustomerId
                                                                                from Visit a inner join VisitDetail b
                                                                                on a.VisitID=b.VisitID
                                                                                where (a.VisitDate >= @StartDate and a.VisitDate<=@EndDate) and (b.CustomerId = @CustomerID) and (b.Latitude <> '' or b.Latitude <> null or b.Latitude <> 'null') and (b.WarehouseID = @CustomerID)", sp);

            foreach (DataRow row in dtKoordinatKunjungan.Rows)
            {
                var mdlKoordinatKunjungan = new Model.mdlKoordinatKunjungan();
                mdlKoordinatKunjungan.Date = row["Date"].ToString();
                mdlKoordinatKunjungan.Latitude = row["Latitude"].ToString();
                mdlKoordinatKunjungan.Longitude = row["Longitude"].ToString();
                mdlKoordinatKunjungan.VisitDate = row["VisitDate"].ToString();
                mdlKoordinatKunjungan.CustomerID = row["CustomerID"].ToString();

                mdlKoordinatKunjunganList.Add(mdlKoordinatKunjungan);
            }
            return mdlKoordinatKunjunganList;
        }

        //001
        public static List<Model.mdlKoordinatKunjunganGudang> GetKoordinatKunjunganGudang(DateTime lStartDate, DateTime lEndDate, string lWarehouseID, string lCustomerID)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName = "@WarehouseID", SqlDbType = SqlDbType.NVarChar, Value = lWarehouseID },
               new SqlParameter() {ParameterName = "@StartDate", SqlDbType = SqlDbType.Date, Value = lStartDate },
               new SqlParameter() {ParameterName = "@EndDate", SqlDbType = SqlDbType.Date, Value = lEndDate },
               new SqlParameter() {ParameterName = "@CustomerID", SqlDbType = SqlDbType.NVarChar, Value = lCustomerID },
            };

            var mdlKoordinatKunjunganGudangList = new List<Model.mdlKoordinatKunjunganGudang>();

            DataTable dtKoordinatKunjunganGudang = Manager.DataFacade.DTSQLCommand(@"select convert(varchar,a.VisitDate,106) Date,b.Latitude,b.Longitude,a.VisitDate,b.WarehouseID 
                                                                                from Visit a inner join VisitDetail b 
                                                                                on a.VisitID=b.VisitID
                                                                                where (a.VisitDate >= @StartDate and a.VisitDate<=@EndDate) and (b.WarehouseID = @WarehouseID) and (b.Latitude <> '' or b.Latitude <> null or b.Latitude <> 'null') and (b.CustomerID=@CustomerID)", sp);

            foreach (DataRow row in dtKoordinatKunjunganGudang.Rows)
            {
                var mdlKoordinatKunjunganGudang = new Model.mdlKoordinatKunjunganGudang();
                mdlKoordinatKunjunganGudang.Date = row["Date"].ToString();
                mdlKoordinatKunjunganGudang.Latitude = row["Latitude"].ToString();
                mdlKoordinatKunjunganGudang.Longitude = row["Longitude"].ToString();
                mdlKoordinatKunjunganGudang.VisitDate = row["VisitDate"].ToString();
                mdlKoordinatKunjunganGudang.WarehouseID = row["WarehouseID"].ToString();

                mdlKoordinatKunjunganGudangList.Add(mdlKoordinatKunjunganGudang);
            }
            return mdlKoordinatKunjunganGudangList;
        }

    }
}
