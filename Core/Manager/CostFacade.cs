/* documentation
 * 001 18 Okt'16 Fernandes
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Transactions;

namespace Core.Manager
{

    public class CostFacade : Base.Manager
    {
        public static List<Model.mdlCost> LoadCost()
        {
            var mdlCostList = new List<Model.mdlCost>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
            };

            DataTable dtCustomer = Manager.DataFacade.DTSQLCommand("SELECT CostID, CostName, Type FROM Cost", sp);

            foreach (DataRow drCustomer in dtCustomer.Rows)
            {
                var mdlCost = new Model.mdlCost();
                mdlCost.CostID = drCustomer["CostID"].ToString();
                mdlCost.CostName = drCustomer["CostName"].ToString();
                mdlCost.Type = drCustomer["Type"].ToString();
                mdlCostList.Add(mdlCost);
            }
            return mdlCostList;
        }

        public static List<Model.mdlDailyCost> LoadDailyCost(List<Model.mdlDBVisit> paramList)
        {
            var mdlDailyCostList = new List<Model.mdlDailyCost>();

            foreach (var param in paramList)
            {

                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@VisitID", SqlDbType = SqlDbType.NVarChar, Value = param.VisitID },
                };

                DataTable dtDailyCost = Manager.DataFacade.DTSQLCommand(@"SELECT DailyCostID,VisitID,CustomerID,CostID,EmployeeID,WarehouseID,Date,CostValue
                                                                        FROM DailyCost
                                                                        WHERE VisitID=@VisitID", sp);

                foreach (DataRow drDailyCost in dtDailyCost.Rows)
                {
                    var mdlDailyCost = new Model.mdlDailyCost();

                    mdlDailyCost.DailyCostID = drDailyCost["DailyCostID"].ToString();
                    mdlDailyCost.VisitID = drDailyCost["VisitID"].ToString();
                    mdlDailyCost.CustomerID = drDailyCost["CustomerID"].ToString();
                    mdlDailyCost.CostID = drDailyCost["CostID"].ToString();
                    mdlDailyCost.EmployeeID = drDailyCost["EmployeeID"].ToString();
                    mdlDailyCost.WarehouseID = drDailyCost["WarehouseID"].ToString();
                    mdlDailyCost.Date = Convert.ToDateTime(drDailyCost["Date"]).ToString("yyyy-MM-dd HH:mm:ss");
                    mdlDailyCost.CostValue = drDailyCost["CostValue"].ToString();

                    mdlDailyCostList.Add(mdlDailyCost);
                }
            }

            return mdlDailyCostList;
        }

        public static Model.mdlResultList InsertDailyCost(List<Model.mdlDailyCostParam> lParamlist)
        {
            var mdlResultList = new List<Model.mdlResult>();
        
            var mdlResult = new Model.mdlResult();


            //bool lCheckDailyCostID = CheckbyDailyCostID(lParamlist.FirstOrDefault().DailyCostID);

            foreach (var cost in lParamlist)
            {
                bool lCheckDailyCostID = CheckbyDailyCostID(cost.DailyCostID);



                if (lCheckDailyCostID == false)
                {
                    mdlResult.Result = "|| DailyCostID : " + lParamlist.FirstOrDefault().DailyCostID + " " + "|| IDExist ||";
                    mdlResultList.Add(mdlResult);
                }
                else
                {
                    cost.CreatedBy = cost.EmployeeID;
                    cost.CreatedDate = DateTime.Now;

                    cost.LastUpdatedBy = cost.EmployeeID;
                    cost.LastUpdatedDate = DateTime.Now;

                    var listCost = new List<Model.mdlDailyCostParam>();
                    listCost.Add(cost);
                    mdlResult.Result = Manager.DataFacade.DTSQLListInsert(listCost, "DailyCost");


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

        //BulkCopy Method
        //public static Model.mdlResultList InsertCostVisit(List<Model.mdlCostVisit> lParamlist)
        //{
        //    var mdlResultList = new List<Model.mdlResult>();

        //    var mdlResult = new Model.mdlResult();


        //    //bool lCheckDailyCostID = CheckbyDailyCostID(lParamlist.FirstOrDefault().DailyCostID);

        //    foreach (var cost in lParamlist)
        //    {
        //        bool lCheckDailyCostID = CheckCostVisitID(cost.VisitID,cost.CostID);


        //        if (lCheckDailyCostID == false)
        //        {
        //            mdlResult.Result = "|| VisitID : " + cost.VisitID + " and CostID : " + cost.CostID + " || Already Exist ||";
        //            mdlResultList.Add(mdlResult);
        //        }
        //        else
        //        {

        //            var listCost = new List<Model.mdlCostVisit>();
        //            listCost.Add(cost);
        //            mdlResult.Result = Manager.DataFacade.DTSQLListInsert(listCost, "CostVisit");


        //            if (mdlResult.Result == "1")
        //            {
        //            }
        //            else
        //            {
        //                string ResultSubstring;


        //                if (mdlResult.Result.Length > 500)
        //                {
        //                    ResultSubstring = mdlResult.Result.Substring(0, 500);

        //                    mdlResult.Result = ResultSubstring;
        //                }


        //            }


        //            mdlResultList.Add(mdlResult);
        //        }
        //    }



        //    var mdlResultListnew = new Model.mdlResultList();
        //    mdlResultListnew.ResultList = mdlResultList;
        //    return mdlResultListnew;
        //}

        //SQL INSERT METHOD
        public static Model.mdlResultList InsertCostVisit(List<Model.mdlCostVisit> lParamlist)
        {
            var mdlResultList = new List<Model.mdlResult>();
            foreach (var lParam in lParamlist)
            {
                var mdlResult = new Model.mdlResult();

                bool lCheckDailyCostID = CheckCostVisitID(lParam.VisitID, lParam.CostID);

                if (lCheckDailyCostID == false)
                {
                    mdlResult.Result = "|| VisitID : " + lParam.VisitID + " and CostID : " + lParam.CostID + " || Already Exist ||";
                    mdlResultList.Add(mdlResult);
                }
                else
                {
                    List<SqlParameter> sp = new List<SqlParameter>()
                    {
                    new SqlParameter() {ParameterName = "@VisitID", SqlDbType = SqlDbType.NVarChar, Value = lParam.VisitID},
                    new SqlParameter() {ParameterName = "@CostID", SqlDbType = SqlDbType.NVarChar, Value = lParam.CostID},
                    new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = lParam.BranchID},
                    new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = lParam.EmployeeID},
                    new SqlParameter() {ParameterName = "@Value", SqlDbType = SqlDbType.NVarChar, Value = lParam.Value}
                    };

                    string query = @"INSERT INTO CostVisit (VisitID, CostID, Value, EmployeeID, BranchID) " +
                                                "VALUES (@VisitID, @CostID, @Value, @EmployeeID, @BranchID) ";

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

        public static Model.mdlResultList UploadInsertDailyCost(List<Model.mdlDailyCostParam> lParamlist, TransactionScope scope)
        {
            var mdlResultList = new List<Model.mdlResult>();

            var mdlResult = new Model.mdlResult();


            bool lCheckDailyCostID = CheckbyDailyCostID(lParamlist.FirstOrDefault().CostID);
            if (lCheckDailyCostID == false)
            {
                mdlResult.Result = "|| DailyCostID : " + lParamlist.FirstOrDefault().DailyCostID + " " + "|| IDExist ||";
                mdlResultList.Add(mdlResult);
            }
            else
            {


                mdlResult.Result = Manager.DataFacade.DTSQLListInsert(lParamlist, "DailyCost");


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

                    scope.Dispose();


                }


                mdlResultList.Add(mdlResult);
            }


            var mdlResultListnew = new Model.mdlResultList();
            mdlResultListnew.ResultList = mdlResultList;
            return mdlResultListnew;
        }
        //001
        public static List<Model.mdlCostReport> LoadCostReport(string lBranchID, DateTime lStartDate, DateTime lEndDate, List<string> lEmployeeIDlist)
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

            var mdlCostsReportList = new List<Model.mdlCostReport>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = lBranchID },
               //new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = lEmployeeID },
               new SqlParameter() {ParameterName = "@StartDate", SqlDbType = SqlDbType.DateTime, Value = lStartDate },
               new SqlParameter() {ParameterName = "@EndDate", SqlDbType = SqlDbType.DateTime, Value = lEndDate.AddDays(1) }
            };

            string sql = @"SELECT    d.BranchID,
                                     e.BranchName,
                                     a.EmployeeID,
                                     f.EmployeeName,
                                     d.VehicleID,
                                     a.CustomerID,
                                     b.CustomerName,
                                     a.WarehouseID,
                                     g.WarehouseName,
                                     c.CostName,
                                     a.CostValue,
                                     a.Date,
                                     h.ImageBase64
		                     FROM DailyCost a 
                                              INNER JOIN Customer b ON a.CustomerID = b.CustomerID
                                              LEFT JOIN Warehouse g ON a.WarehouseID = g.WarehouseID 
			                                  INNER JOIN Cost c ON a.CostID = c.CostID
			                                  INNER JOIN Visit d ON a.VisitID = d.VisitID
			                                  INNER JOIN Branch e ON d.BranchID = e.BranchID
			                                  INNER JOIN Employee f ON a.EmployeeID = f.EmployeeID
                                              LEFT JOIN CustomerImage h ON h.VisitID=a.VisitID and h.CustomerID=a.CustomerID and h.WarehouseID=a.WarehouseID and h.ImageType=a.CostID
                            WHERE (d.BranchID = @BranchID) AND (a.Date BETWEEN @StartDate and @EndDate) AND" + lParam + "Order by a.Date";

            DataTable dtCost = Manager.DataFacade.DTSQLCommand(sql, sp);

            foreach (DataRow drCustomer in dtCost.Rows)
            {
                var mdlCost = new Model.mdlCostReport();
                mdlCost.BranchID = drCustomer["BranchID"].ToString();
                mdlCost.BranchName = drCustomer["BranchName"].ToString();
                mdlCost.CostName = drCustomer["CostName"].ToString();

                //CultureInfo culture = new CultureInfo("id-ID"); //001
                //mdlCost.CostValue = Decimal.Parse(drCustomer["CostValue"].ToString()).ToString("C", culture); //001
                mdlCost.CostValue = Convert.ToInt32(drCustomer["CostValue"].ToString()); //001

                mdlCost.CustomerID = drCustomer["CustomerID"].ToString();
                mdlCost.WarehouseID = drCustomer["WarehouseID"].ToString();
                mdlCost.EmployeeID = drCustomer["EmployeeID"].ToString();
                mdlCost.EmployeeName = drCustomer["EmployeeName"].ToString();
                mdlCost.VehicleID = drCustomer["VehicleID"].ToString();
                mdlCost.CustomerName = drCustomer["CustomerName"].ToString();
                mdlCost.WarehouseName = drCustomer["WarehouseName"].ToString();

                if (mdlCost.CustomerID == mdlCost.WarehouseID)
                {
                    mdlCost.CustomerID.ToString();
                }
                else
                {
                    mdlCost.CustomerID = mdlCost.CustomerID + " - " + mdlCost.WarehouseID;
                }

                mdlCost.CostImage = drCustomer["ImageBase64"].ToString();
                mdlCost.CostDate = Convert.ToDateTime(drCustomer["Date"]).ToString("dd-MM-yyyy HH:mm:ss");
                if (mdlCost.CostDate == "01-01-2000 00:00:00")
                {
                    mdlCost.CostDate = "-";
                }
                //mdlCost.CostImagePath = drCustomer["ImagePath"].ToString();

                mdlCostsReportList.Add(mdlCost);
            }

            return mdlCostsReportList;
        }

        public static List<Model.mdlJourneyCostReport> LoadJourneyCostReport(string lBranchID, DateTime lStartDate, DateTime lEndDate, List<string> lEmployeeIDlist)
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

            var mdlJurneyCostsReportList = new List<Model.mdlJourneyCostReport>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = lBranchID },
               //new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = lEmployeeID },
               new SqlParameter() {ParameterName = "@StartDate", SqlDbType = SqlDbType.DateTime, Value = lStartDate },
               new SqlParameter() {ParameterName = "@EndDate", SqlDbType = SqlDbType.DateTime, Value = lEndDate.AddDays(1) }
            };

            string sql = @"SELECT    a.BranchID,
                                        e.BranchName,
                                        a.EmployeeID,
                                        f.EmployeeName,
                                        d.VehicleID,
                                        d.VisitDate,
                                        c.CostName,
                                        a.Value,
                                        h.ImageBase64
		                     FROM CostVisit a 
			                                  INNER JOIN Cost c ON a.CostID = c.CostID
			                                  INNER JOIN Visit d ON a.VisitID = d.VisitID
			                                  INNER JOIN Branch e ON a.BranchID = e.BranchID
			                                  INNER JOIN Employee f ON a.EmployeeID = f.EmployeeID
                                              LEFT JOIN CustomerImage h ON h.VisitID=a.VisitID and h.ImageType=a.CostID
                            WHERE (a.BranchID = @BranchID) AND (d.VisitDate BETWEEN @StartDate and @EndDate) AND" + lParam + "Order by d.VisitDate";

            DataTable dtJourneyCost = Manager.DataFacade.DTSQLCommand(sql, sp);

            foreach (DataRow drJourneyCost in dtJourneyCost.Rows)
            {
                var mdlJourneyCost = new Model.mdlJourneyCostReport();

                mdlJourneyCost.BranchID = drJourneyCost["BranchID"].ToString();
                mdlJourneyCost.BranchName = drJourneyCost["BranchName"].ToString();
                mdlJourneyCost.EmployeeID = drJourneyCost["EmployeeID"].ToString();
                mdlJourneyCost.EmployeeName = drJourneyCost["EmployeeName"].ToString();
                mdlJourneyCost.VehicleID = drJourneyCost["VehicleID"].ToString();
                mdlJourneyCost.CostName = drJourneyCost["CostName"].ToString();
                mdlJourneyCost.CostValue = Convert.ToInt32(drJourneyCost["Value"].ToString());
                mdlJourneyCost.CostImage = drJourneyCost["ImageBase64"].ToString();
                mdlJourneyCost.CostDate = Convert.ToDateTime(drJourneyCost["VisitDate"]).ToString("dd-MM-yyyy");

                mdlJurneyCostsReportList.Add(mdlJourneyCost);
            }

            return mdlJurneyCostsReportList;
        }


        public static bool CheckbyDailyCostID(string lDailyCostID)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName = "@DailyCostID", SqlDbType = SqlDbType.NVarChar, Value = lDailyCostID },
            };

            DataTable dtVisit = Manager.DataFacade.DTSQLCommand(@"SELECT TOP 1 DailyCostID
                                                                   FROM DailyCost 
                                                                   WHERE DailyCostID = @DailyCostID", sp);
            bool lCheck = false;
            if (dtVisit.Rows.Count == 0)
            {
                lCheck = true;
            }

            return lCheck;
        }

        public static bool CheckCostVisitID(string visitID, string costID)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName = "@VisitID", SqlDbType = SqlDbType.NVarChar, Value = visitID },
               new SqlParameter() {ParameterName = "@CostID", SqlDbType = SqlDbType.NVarChar, Value = costID }
            };

            DataTable dtVisit = Manager.DataFacade.DTSQLCommand(@"SELECT TOP 1 CostID
                                                                   FROM CostVisit 
                                                                   WHERE VisitID = @VisitID and CostID = @CostID ", sp);
            bool lCheck = false;
            if (dtVisit.Rows.Count == 0)
            {
                lCheck = true;
            }

            return lCheck;
        }

        //------------------------------------------------ Service Facade ----------------------------------------------------//

        public static List<Model.Cost> GetCost()
        {
            var pCost = DataContext.Costs.ToList();
            return pCost;
        }

        public static List<Model.mdlCost> GetSearchCost(string keyword)
        {
            var pCost = DataContext.Costs.Where(fld => fld.CostID.Contains(keyword)).OrderBy(fld => fld.CostID).ToList();
            
            var mdlCostList = new List<Model.mdlCost>();
            foreach (var cost in pCost)
            {
                var mdlCost = new Model.mdlCost();
                mdlCost.CostID = cost.CostID;
                mdlCost.CostName = cost.CostName;
                mdlCost.Type = cost.Type;

                mdlCostList.Add(mdlCost);
            }

            return mdlCostList;
        }

        public static Model.Cost GetCostbyID(string lCostID)
        {
            var pCost = DataContext.Costs.FirstOrDefault(fld => fld.CostID.Equals(lCostID));
            return pCost;
        }

        public static void DeleteCost(string lCostID)
        {
            var pCost = GetCostbyID(lCostID);
            DataContext.Costs.DeleteOnSubmit(pCost);
            DataContext.SubmitChanges();
        }

        public static void UpdateCost(string lCostID, string lCostName, string lCostType)
        {
            var pCost = GetCostbyID(lCostID);
            pCost.CostName = lCostName;
            pCost.Type = lCostType;
            DataContext.SubmitChanges();
        }

        public static void InsertCost(string lCostID, string lCostName, string lCostType)
        {
            Model.Cost pCost = new Model.Cost();
            pCost.CostName = lCostName;
            pCost.CostID = lCostID;
            pCost.Type = lCostType;
            DataContext.Costs.InsertOnSubmit(pCost);
            DataContext.SubmitChanges();
        }
    }

}
