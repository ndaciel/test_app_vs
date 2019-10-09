/* documentation
 * 001 17 okt 2016 fernandes
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace Core.Manager
{
    public class CallPlanFacade : Base.Manager
    {

        public static Model.CallPlan GetCallPlan(string callplanID)
        {
            var callplan = DataContext.CallPlans.FirstOrDefault(fld => fld.CallPlanID.Equals(callplanID));
            return callplan;
        }

        public static Model.CallPlanDetail GetCallPlanByCPDetailID(string callplanDetailID)
        {
            var callplanbycpdetailID = DataContext.CallPlanDetails.FirstOrDefault(fld => fld.CPDetailID.Equals(callplanDetailID));
            return callplanbycpdetailID;
        }

        public static List<Model.CallPlan> GetCallPlanMove(string EmployeeID, string callPlanId, string date, string branchid)
        {
            var callplanall = DataContext.CallPlans.Where(fld => fld.CallPlanID != callPlanId && fld.Date.Equals(date) && fld.EmployeeID.Contains(EmployeeID) && fld.BranchID.Contains(branchid)).ToList();


            return callplanall;
        }

        public static List<Model.CallPlan> GetCallPlanMoveByBranch(string EmployeeID, string callPlanId, string date, string branchid)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
            };

            var mdlCPList = new List<Model.CallPlan>();

            DataTable dtCP = Manager.DataFacade.DTSQLCommand(@"SELECT   CallPlanID, 
			                                                                        EmployeeID,
                                                                                    Date, VehicleID, BranchID
                                                                                    from CallPlan
				                                                            Where CallPlanID <> '" + callPlanId + "' and BranchID IN (" + branchid + ") and Date = '" + date + "' and EmployeeID LIKE '%" + EmployeeID + "%'", sp);

            foreach (DataRow row in dtCP.Rows)
            {
                var mdlCP = new Model.CallPlan();
                mdlCP.EmployeeID = row["EmployeeID"].ToString();
                mdlCP.Date = Convert.ToDateTime(row["Date"].ToString());
                mdlCP.VehicleID = row["VehicleID"].ToString();
                mdlCP.BranchID = row["BranchID"].ToString();
                mdlCP.CallPlanID = row["CallPlanID"].ToString();

                mdlCPList.Add(mdlCP);
            }
            return mdlCPList;
        }

        public static Model.CallPlanDetail GetCallPlanDetailToDel(string callplandetailID)
        {
            var cpDetaildel = DataContext.CallPlanDetails.FirstOrDefault(fld => fld.CPDetailID.Equals(callplandetailID));
            return cpDetaildel;
        }

        public static List<Model.CallPlan> GetSearch(string keyword, string keyworddate, string branchid)
        {
            var callplan = DataContext.CallPlans.Where(fld => fld.EmployeeID.Contains(keyword) && fld.Date.Equals(keyworddate) && fld.BranchID.Contains(branchid)).OrderByDescending(fld => fld.Date).ToList();
            return callplan;
        }

        public static List<Model.CallPlan> GetSearchCPByBranch(string keyword, string keyworddate, string branchid)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
            };

            var mdlCPList = new List<Model.CallPlan>();

            DataTable dtCP = Manager.DataFacade.DTSQLCommand(@"SELECT   CallPlanID, 
			                                                                        EmployeeID,
                                                                                    Date, VehicleID, BranchID
                                                                                    from CallPlan
				                                                            Where BranchID IN (" + branchid + ") and Date='" + keyworddate + "' and EmployeeID LIKE '%" + keyword + "%'", sp);

            foreach (DataRow row in dtCP.Rows)
            {
                var mdlCP = new Model.CallPlan();
                mdlCP.EmployeeID = row["EmployeeID"].ToString();
                mdlCP.CallPlanID = row["CallPlanID"].ToString();
                mdlCP.Date = Convert.ToDateTime(row["Date"].ToString());
                mdlCP.BranchID = row["BranchID"].ToString();
                mdlCP.VehicleID = row["VehicleID"].ToString();

                mdlCPList.Add(mdlCP);
            }
            return mdlCPList;
        }

        public static void DeleteCPDetail(string callPlanDetailID)
        {
            var cpDetail = GetCallPlanDetailToDel(callPlanDetailID);

            DataContext.CallPlanDetails.DeleteOnSubmit(cpDetail);
            DataContext.SubmitChanges();

        }

        //001
        public static void InsertCPDetail(string callplanID, string customerID, string warehouseID, int sequence)
        {
            Model.CallPlanDetail cpDetail = new Model.CallPlanDetail();
            cpDetail.CallPlanID = callplanID;
            cpDetail.CustomerID = customerID;
            cpDetail.Sequence = sequence;
            cpDetail.WarehouseID = warehouseID;
            DataContext.CallPlanDetails.InsertOnSubmit(cpDetail);
            DataContext.SubmitChanges();
        }

        public static void UpdateCPDetail(string cpDetailID, string callplanid)
        {
            var cpDetail = GetCallPlanByCPDetailID(cpDetailID);
            cpDetail.CallPlanID = callplanid;

            DataContext.SubmitChanges();
        }

        //001
        public static void EditCPDetail(string cpDetailID, string callplanid, string customerid, int seq, string warehouseid)
        {
            var cpDetail = GetCallPlanByCPDetailID(cpDetailID);
            cpDetail.CallPlanID = callplanid;
            cpDetail.CustomerID = customerid;
            cpDetail.Sequence = seq;
            cpDetail.WarehouseID = warehouseid;

            DataContext.SubmitChanges();
        }

        //001
        public static Model.CallPlanDetail GetSameCustomer(string customerID, string callplanID, string warehouseID)
        {
            var cpdetail = DataContext.CallPlanDetails.FirstOrDefault(fld => fld.CustomerID.Equals(customerID) && fld.CallPlanID.Equals(callplanID) && fld.WarehouseID.Equals(warehouseID));
            return cpdetail;
        }



        //------------------------------------------------ Service Facade ----------------------------------------------------//

        //001 pergantian facade dr linq ke sql query
        public static List<Model.mdlCallPlanDetail2> GetCallPlanDetail(string lCallplanID, string keyword)
        {
            //var cpDetail = DataContext.CallPlanDetails.Where(fld => fld.CallPlanID.Equals(callplanID)).Where(fld => fld.CustomerID.Contains(keyword) || fld.Customer.CustomerName.Contains(keyword) || fld.WarehouseID.Contains(keyword)).OrderBy(fld => fld.Sequence).ToList();
            //return cpDetail;

            var mdlCallPlanDetailList = new List<Model.mdlCallPlanDetail2>();
            if (lCallplanID == null || keyword == null)
            {
                return mdlCallPlanDetailList;
            }

            List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@CallplanID", SqlDbType = SqlDbType.NVarChar, Value = lCallplanID },
                    new SqlParameter() {ParameterName = "@keyword", SqlDbType = SqlDbType.NVarChar, Value = "%" + keyword + "%" }
                };

            DataTable dtcallplandetail = Manager.DataFacade.DTSQLCommand(@"SELECT a.CPDetailID,
                                                                                    a.CallPlanID,
                                                                                    a.CustomerID,
                                                                                    a.WarehouseID,
		                                                                            b.CustomerName,
                                                                                    a.[Sequence]
                                                                                    FROM CallPlanDetail a LEFT JOIN Customer b ON a.CustomerID = b.CustomerID
								                                          WHERE a.CallPlanID = @CallplanID AND ( b.CustomerID LIKE @keyword OR a.WarehouseID lIKE @keyword) 
                                                                          ORDER BY a.[Sequence]", sp); //006

            foreach (DataRow row in dtcallplandetail.Rows)
            {
                var mdlCallPlanDetail = new Model.mdlCallPlanDetail2();
                mdlCallPlanDetail.CPDetailID = row["CPDetailID"].ToString();
                mdlCallPlanDetail.CallPlanID = row["CallPlanID"].ToString();
                mdlCallPlanDetail.CustomerID = row["CustomerID"].ToString();
                mdlCallPlanDetail.CustomerName = row["CustomerName"].ToString();
                mdlCallPlanDetail.WarehouseID = row["WarehouseID"].ToString();
                mdlCallPlanDetail.Sequence = row["Sequence"].ToString();

                mdlCallPlanDetailList.Add(mdlCallPlanDetail);
            }

            return mdlCallPlanDetailList;

        }

        //fernandes
        public static bool CheckExistingCP(Model.mdlParam json)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@Date", SqlDbType = SqlDbType.DateTime, Value = Convert.ToDateTime(json.Date).Date },
                new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = json.EmployeeID },
                new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = json.BranchID }
            };

            DataTable dtExistingCP = Manager.DataFacade.DTSQLCommand(@"SELECT CallPlanID
                                                                   FROM CallPlan 
                                                                   WHERE EmployeeID = @EmployeeID and BranchID = @BranchID and Date >= @Date and Date < DATEADD(day,1,@Date) ", sp);
            bool lCheck = false;
            if (dtExistingCP.Rows.Count == 0)
            {
                lCheck = true;
            }

            return lCheck;
            //klau true berarti datanya belum ada
        }

        public static bool CheckIsFinishedCP(Model.mdlParam json)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@Date", SqlDbType = SqlDbType.DateTime, Value = Convert.ToDateTime(json.Date).Date },
                new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = json.EmployeeID },
                new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = json.BranchID }
            };

            DataTable dtIsFinishedCP = Manager.DataFacade.DTSQLCommand(@"SELECT CallPlanID
                                                                   FROM CallPlan 
                                                                   WHERE EmployeeID = @EmployeeID and BranchID = @BranchID and IsFinish = 0 and Date >= @Date and Date < DATEADD(day,1,@Date) ", sp);
            bool lCheck = true;
            if (dtIsFinishedCP.Rows.Count == 0)
            {
                lCheck = false;
            }

            return lCheck;
            //klau false berarti datanya belum ada
        }

        public static List<Model.mdlCallPlan> LoadCallPlan(Model.mdlParam json)
        {
            var mdlcallplanList = new List<Model.mdlCallPlan>();

            bool Check = CheckExistingCP(json);
            if (Check == false)
            {
                bool CheckFinishedCP = CheckIsFinishedCP(json);

                if (CheckFinishedCP == false)
                {
                    var mdlcallplan = new Model.mdlCallPlan();

                    mdlcallplan.Result = "FINISH";
                    mdlcallplanList.Add(mdlcallplan);
                }
                else
                {
                    List<SqlParameter> sp = new List<SqlParameter>()
                    {
                        new SqlParameter() {ParameterName = "@Date", SqlDbType = SqlDbType.DateTime, Value = Convert.ToDateTime(json.Date).Date },
                        new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = json.EmployeeID },
                        new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = json.BranchID }
                    };

                    DataTable dtcallplan = Manager.DataFacade.DTSQLCommand(@"SELECT CallPlanID
                          ,a.EmployeeID
                          ,a.BranchID
                          ,Date
                          ,VehicleID
                          ,b.EmployeeName Helper1
                          ,c.EmployeeName Helper2

                          FROM CallPlan a
						  left join Employee b on a.Helper1 = b.EmployeeID
						  left join Employee c on c.EmployeeID=a.Helper2  
                          WHERE a.EmployeeID = @EmployeeID and a.BranchID = @BranchID and IsFinish = 0 and Date >= @Date and Date < DATEADD(day,1,@Date) ", sp);

                    foreach (DataRow row in dtcallplan.Rows)
                    {
                        var mdlcallplan = new Model.mdlCallPlan();
                        mdlcallplan.CallPlanID = row["CallPlanID"].ToString();
                        mdlcallplan.EmployeeID = row["EmployeeID"].ToString();
                        mdlcallplan.BranchID = row["BranchID"].ToString();
                        mdlcallplan.Date = Convert.ToDateTime(row["Date"]).ToString("yyyy-MM-dd hh:mm:ss");
                        mdlcallplan.VehicleID = row["VehicleID"].ToString();
                        mdlcallplan.Helper1 = row["Helper1"].ToString();
                        mdlcallplan.Helper2 = row["Helper2"].ToString();
                        mdlcallplan.Result = "SUCCESS";

                        List<SqlParameter> sp3 = new List<SqlParameter>()
                    {
                        
                        new SqlParameter() {ParameterName = "@CallPlanID", SqlDbType = SqlDbType.NVarChar, Value = mdlcallplan.CallPlanID }
                    };
                        Manager.DataFacade.DTSQLVoidCommand(@"update callplan set isDownload = 1 where callplanid=@CallPlanID", sp3);


                        List<SqlParameter> sp2 = new List<SqlParameter>()
                    {
                       
                        new SqlParameter() {ParameterName = "@VehicleID", SqlDbType = SqlDbType.NVarChar, Value =mdlcallplan.VehicleID }
                        
                    };
                        DataTable dtVisit = Manager.DataFacade.DTSQLCommand(@"select top 1 VisitDate,KMFinish from Visit where VehicleID=@VehicleID
and visitdate < GETDATE()
order by VisitDate desc", sp2);
                        if (dtVisit.Rows.Count > 0)
                            mdlcallplan.KMAkhir = dtVisit.Rows[0]["KMFinish"].ToString();
                        mdlcallplanList.Add(mdlcallplan);
                    }
                }
            }
            else
            {
                var mdlcallplan = new Model.mdlCallPlan();
                mdlcallplan.Result = "FAILED";

                mdlcallplanList.Add(mdlcallplan);
            }


            return mdlcallplanList;
        }

        public static List<Model.mdlCallPlanDetail> LoadCallPlanDetail(Model.mdlParam json)
        {
            var mdlCallPlanDetailList = new List<Model.mdlCallPlanDetail>();

            List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@Date", SqlDbType = SqlDbType.DateTime, Value = Convert.ToDateTime(json.Date).Date },
                    new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = json.EmployeeID },
                    new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = json.BranchID }
                };

            DataTable dtcallplandetail = Manager.DataFacade.DTSQLCommand(@"SELECT 
                                  CallPlanID
                                  ,a.CustomerID
                                  ,Sequence
                                  ,WarehouseID
                                  ,[Time]
								  
                                  FROM CallPlanDetail a
								  inner join customer b on a.CustomerID=b.CustomerID
                                  WHERE CallPlanID IN (SELECT CallPlanID FROM CallPlan WHERE EmployeeID = @EmployeeID and BranchID = @BranchID and IsFinish = 0 and Date >= @Date and Date < DATEADD(day,1,@Date))", sp); //006

            foreach (DataRow row in dtcallplandetail.Rows)
            {
                var mdlCallPlanDetail = new Model.mdlCallPlanDetail();
                mdlCallPlanDetail.CallPlanID = row["CallPlanID"].ToString();
                //mdlCallPlanDetail.CPDetailID = row["CPDetailID"].ToString();
                DateTime time = DateTime.ParseExact(row["Time"].ToString(), "HH:mm:ss", CultureInfo.InvariantCulture);

                mdlCallPlanDetail.Time = time.ToString("HH:mm:ss");
                mdlCallPlanDetail.CPDetailID = "";
                mdlCallPlanDetail.CustomerID = row["CustomerID"].ToString();
                mdlCallPlanDetail.Sequence = row["Sequence"].ToString();
                mdlCallPlanDetail.WarehouseID = row["WarehouseID"].ToString();


                var week =VisitFacade.GetIso8601WeekOfYear(Convert.ToDateTime(json.Date));
                List<SqlParameter> spCheck = new List<SqlParameter>()
                    {
                        
                        new SqlParameter() {ParameterName = "@Week", SqlDbType = SqlDbType.Int, Value = week },
                        new SqlParameter() {ParameterName = "@CustomerID", SqlDbType = SqlDbType.NVarChar, Value = mdlCallPlanDetail.CustomerID }
                       
                    };
                DataTable dtCheckVisit = Manager.DataFacade.DTSQLCommand(@"select a.VisitID,CustomerID from Visit a
inner join VisitDetail b on a.VisitID = b.VisitID where b.isDeliver=1 and b.CustomerID= @CustomerID and a.VisitWeek= @Week ", spCheck);

                if (dtCheckVisit.Rows.Count > 0)
                {
                    mdlCallPlanDetail.BPStock = true;
                }
                else
                {
                    mdlCallPlanDetail.BPStock = false;
                }

                List<SqlParameter> sp3 = new List<SqlParameter>()
                    {
                        
                        new SqlParameter() {ParameterName = "@CallPlanID", SqlDbType = SqlDbType.NVarChar, Value = mdlCallPlanDetail.CallPlanID },
                        new SqlParameter() {ParameterName = "@CustomerID", SqlDbType = SqlDbType.NVarChar, Value = mdlCallPlanDetail.CustomerID }
                       
                    };
                Manager.DataFacade.DTSQLVoidCommand(@"update callplandetail set isDownload = 1 where CallPlanID = @CallPlanID AND CustomerID = @CustomerID", sp3);

                mdlCallPlanDetailList.Add(mdlCallPlanDetail);
            }

            return mdlCallPlanDetailList;
        }

        public static string UpdateVisitbyIsFinish(string lCallPlanID)
        {
            var mdlResult = new Model.mdlResult();
            List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@CallPlanID", SqlDbType = SqlDbType.NVarChar, Value = lCallPlanID}
                };

            string query = @"UPDATE CallPlan SET IsFinish = 1 WHERE CallPlanID = @CallPlanID";

            string Result = Manager.DataFacade.DTSQLVoidCommand(query, sp);
            return Result;
        }

    }
}
