/* documentation
 *001 18 oKT'16 FERNANDES
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Core.Manager
{
    
    public class ReturFacade : Base.Manager
    {
        
        public static List<Model.mdlReturOrder> LoadReturOrder(Model.mdlParam json)
        {
            var mdlReturOrderList = new List<Model.mdlReturOrder>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@DateStart", SqlDbType = SqlDbType.DateTime, Value = DateTime.Now.Date },
                //new SqlParameter() {ParameterName = "@DateFinish", SqlDbType = SqlDbType.DateTime, Value = DateTime.Now.AddDays(1).Date },
                new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = json.EmployeeID },
                new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = json.BranchID }
            };
             
            DataTable dtReturOrder = Manager.DataFacade.DTSQLCommand("SELECT ReturNumber,CustomerID,EmployeeID,BranchID,ReturDate,ReturStatus,Description,Signature,IsPrint,Remark,ReceivedDate FROM ReturOrder WHERE EmployeeID = @EmployeeID AND BranchID = @BranchID AND ReturDate <= @DateStart AND ReturStatus = '' AND CONVERT(varchar, Signature) = '' AND Remark = ''", sp);


            foreach (DataRow row in dtReturOrder.Rows)
            {
                var mdlReturOrder = new Model.mdlReturOrder();
                mdlReturOrder.ReturNumber = row["ReturNumber"].ToString();
                mdlReturOrder.CustomerID = row["CustomerID"].ToString();
                mdlReturOrder.EmployeeID = row["EmployeeID"].ToString(); 
                mdlReturOrder.BranchID = row["BranchID"].ToString();
                mdlReturOrder.ReturDate = Convert.ToDateTime(row["ReturDate"]).ToString("yyyy-MM-dd hh:mm:ss");
                mdlReturOrder.ReturStatus = row["ReturStatus"].ToString();
                mdlReturOrder.Description = row["Description"].ToString();
                mdlReturOrder.Signature = row["Signature"].ToString();
                mdlReturOrder.IsPrint = row["IsPrint"].ToString();
                mdlReturOrder.Remark = row["Remark"].ToString();
                mdlReturOrder.ReceivedDate = Convert.ToDateTime(row["ReceivedDate"]).ToString("yyyy-MM-dd hh:mm:ss");
                mdlReturOrderList.Add(mdlReturOrder);
            }
            return mdlReturOrderList;
        }


        public static List<Model.mdlReturOrderDetail> LoadReturOrderDetail(Model.mdlParam json)
        {
            var mdlReturOrderDetailList = new List<Model.mdlReturOrderDetail>();
            var returOrder = LoadReturOrder(json).ToList();
            var returOrderDetail = new List<Model.mdlReturOrderDetail>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@DateStart", SqlDbType = SqlDbType.DateTime, Value = DateTime.Now.Date },
                //new SqlParameter() {ParameterName = "@DateFinish", SqlDbType = SqlDbType.DateTime, Value = DateTime.Now.AddDays(1).Date },
                new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = json.EmployeeID },
                new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = json.BranchID }
            };

            DataTable dtDetailOrderDetail = Manager.DataFacade.DTSQLCommand("SELECT ReturNumber,ProductID,UOM,Quantity,QuantityReal,ArticleNumber FROM ReturOrderDetail WHERE ReturNumber IN  (SELECT ReturNumber FROM ReturOrder WHERE EmployeeID = @EmployeeID AND BranchID = @BranchID AND ReturDate <= @DateStart AND ReturStatus = '' AND CONVERT(varchar, Signature) = '' AND Remark = '')", sp);
            
            foreach (DataRow row in dtDetailOrderDetail.Rows)
            {
                var mdlReturDetail = new Model.mdlReturOrderDetail();
                mdlReturDetail.ReturNumber = row["ReturNumber"].ToString();
                mdlReturDetail.ProductID = row["ProductID"].ToString();
                mdlReturDetail.UOM = row["UOM"].ToString();
                mdlReturDetail.Quantity = row["Quantity"].ToString();
                mdlReturDetail.QuantityReal = row["QuantityReal"].ToString();
                mdlReturDetail.ArticleNumber = row["ArticleNumber"].ToString();   
                mdlReturOrderDetailList.Add(mdlReturDetail);
            }
            return mdlReturOrderDetailList;
        }
        
        public static Model.mdlResultList UpdateReturOrder(List<Model.mdlReturOrderParam> lParamlist)
        {
            var mdlResultList = new List<Model.mdlResult>();

            foreach (var lParam in lParamlist)
            {
                if (lParam.ReturStatus == "")
                {
                    var mdlResult = new Model.mdlResult();
                    mdlResult.Result = "|| " + "Update Retur Order : " + lParam.ReturNumber + " and CustomerID  : " + lParam.CustomerID + " || " + " Cancelled";
                    mdlResultList.Add(mdlResult);
                }
                else
                {
                    var mdlResult = new Model.mdlResult();
                    List<SqlParameter> sp = new List<SqlParameter>()
                    {
                        new SqlParameter() {ParameterName = "@ReturNumber", SqlDbType = SqlDbType.NVarChar, Value = lParam.ReturNumber},
                        new SqlParameter() {ParameterName = "@CustomerID", SqlDbType = SqlDbType.NVarChar, Value = lParam.CustomerID},
                        new SqlParameter() {ParameterName = "@ReturStatus", SqlDbType = SqlDbType.NVarChar, Value = lParam.ReturStatus},
                        new SqlParameter() {ParameterName = "@ReturDate", SqlDbType = SqlDbType.DateTime, Value = lParam.ReturDate},
                        new SqlParameter() {ParameterName = "@Description", SqlDbType = SqlDbType.NVarChar, Value = lParam.Description },
                        new SqlParameter() {ParameterName = "@Signature", SqlDbType = SqlDbType.NVarChar, Value = lParam.Signature },
                        new SqlParameter() {ParameterName = "@IsPrint", SqlDbType = SqlDbType.NVarChar, Value = lParam.IsPrint },
                        new SqlParameter() {ParameterName = "@Remark", SqlDbType = SqlDbType.NVarChar, Value = lParam.Remark },  //003
                        new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = lParam.EmployeeID },
                        new SqlParameter() {ParameterName = "@VisitID", SqlDbType = SqlDbType.NVarChar, Value = lParam.VisitID },
                        new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = lParam.BranchID },
                        new SqlParameter() {ParameterName = "@ReceivedDate", SqlDbType = SqlDbType.DateTime, Value = lParam.ReceivedDate }
                     };


                    string query = @"UPDATE ReturOrder SET
                                            CustomerID = @CustomerID,
                                            ReturStatus = @ReturStatus,
                                            ReturDate = @ReturDate,
                                            Description = @Description,
                                            Signature = @Signature,
                                            IsPrint = @IsPrint,
                                            Remark = @Remark,
                                            EmployeeID = @EmployeeID,
                                            VisitID = @VisitID,
                                            BranchID = @BranchID, 
                                            ReceivedDate = @ReceivedDate
                                        WHERE ReturNumber = @ReturNumber "; 


                    mdlResult.Result = "|| " + "Update Retur Order : " + lParam.ReturNumber + " and CustomerID  : " + lParam.CustomerID + " || " + Manager.DataFacade.DTSQLVoidCommand(query, sp);


                    
                    if (mdlResult.Result.Contains("SQLSuccess") == true) 
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

        public static Model.mdlResultList UpdateReturOrderDetail(List<Model.mdlReturOrderDetailParam> lParamlist)
        {
            var mdlResultList = new List<Model.mdlResult>();

            foreach (var lParam in lParamlist)
            {
                if (lParam.QuantityReal == "0" || lParam.QuantityReal == "")
                {
                    var mdlResult = new Model.mdlResult();
                    mdlResult.Result = "|| " + "Update Retur Order Detail : " + lParam.ReturNumber + " and ProductID  : " + lParam.ProductID + " || " + " Cancelled";
                    mdlResultList.Add(mdlResult);
                }
                else
                {
                    var mdlResult = new Model.mdlResult();
                    List<SqlParameter> sp = new List<SqlParameter>()
                    {
                    
                        new SqlParameter() {ParameterName = "@ReturNumber", SqlDbType = SqlDbType.NVarChar, Value = lParam.ReturNumber},
                        new SqlParameter() {ParameterName = "@ProductID", SqlDbType = SqlDbType.NVarChar, Value = lParam.ProductID},
                        new SqlParameter() {ParameterName = "@UOM", SqlDbType = SqlDbType.NVarChar, Value = lParam.UOM},
                        new SqlParameter() {ParameterName = "@Quantity", SqlDbType = SqlDbType.Decimal, Value = lParam.Quantity},
                        new SqlParameter() {ParameterName = "@QuantityReal", SqlDbType = SqlDbType.Decimal, Value = lParam.QuantityReal},
                        new SqlParameter() {ParameterName = "@ArticleNumber", SqlDbType = SqlDbType.NVarChar, Value = lParam.ArticleNumber},
                        new SqlParameter() {ParameterName = "@ReasonID", SqlDbType = SqlDbType.NVarChar, Value = lParam.ReasonID }
                    };


                    string query = @"UPDATE ReturOrderDetail SET
                                                UOM = @UOM,
                                                Quantity=@Quantity,
                                                QuantityReal=@QuantityReal,
                                                ArticleNumber=@ArticleNumber,
                                                ReasonID = @ReasonID
                                            WHERE  ReturNumber = @ReturNumber AND ProductID = @ProductID";


                    mdlResult.Result = "|| " + "Update Retur Order " + lParam.ReturNumber + " || " + lParam.ProductID + " || " + Manager.DataFacade.DTSQLVoidCommand(query, sp);

                    
                    if (mdlResult.Result.Contains("SQLSuccess") == true)
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
        
        //001
        public static List<Model.mdlReturReport> LoadReturReport(string lBranchID, DateTime lStartDate,DateTime lEndDate, List<string> lEmployeeIDlist)
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
               new SqlParameter() {ParameterName = "@StartDate", SqlDbType = SqlDbType.Date, Value = lStartDate },
               new SqlParameter() {ParameterName = "@EndDate", SqlDbType = SqlDbType.Date, Value = lEndDate.AddDays(1) } 
            };

            var mdlReturReportList = new List<Model.mdlReturReport>();

            DataTable dtReturReport = Manager.DataFacade.DTSQLCommand(@"SELECT   a.BranchID, 
		                                                                    c.BranchName,
                                                                            a.EmployeeID,
                                                                            g.EmployeeName,
                                                                            f.VehicleID,    
		                                                                    a.CustomerID,
		                                                                    e.CustomerName,
                                                                            a.WarehouseID,
                                                                            i.WarehouseName, 
		                                                                    a.ReturNumber,
                                                                            a.Remark, 
		                                                                    a.ReturDate,   
		                                                                    b.ProductID,
                                                                            d.ArticleNumber, 
		                                                                    d.ProductName,
		                                                                    b.Quantity,
                                                                            b.UOM,
		                                                                    b.QuantityReal,
                                                                            h.Value,
                                                                            (select top 1 ImageBase64 from CustomerImage where DocNumber=a.ReturNumber) ImageBase64,
                                                                            (select top 1 ImagePath from CustomerImage where DocNumber=a.ReturNumber) ImagePath,
                                                                            a.ReceivedDate

		                                                                    FROM ReturOrder a
		                                                                    INNER JOIN ReturOrderDetail b ON b.ReturNumber = a.ReturNumber
		                                                                    INNER JOIN Branch c ON c.BranchID = a.BranchID 
		                                                                    INNER JOIN Customer e ON e.CustomerID = a.CustomerID
		                                                                    INNER JOIN Product d ON d.ProductID = b.ProductID
		                                                                    INNER JOIN Employee g ON g.EmployeeID = a.EmployeeID
                                                                            LEFT JOIN Reason h ON h.ReasonID = b.ReasonID
                                                                            LEFT JOIN Visit f ON f.VisitID = a.VisitID
                                                                            LEFT JOIN Warehouse i ON i.WarehouseID = a.WarehouseID
		                                                                    Where (a.BranchID = @BranchID) AND (a.ReceivedDate BETWEEN @StartDate and @EndDate) AND" + lParam, sp); //012
             
            
            foreach (DataRow row in dtReturReport.Rows)
            {
                var mdlReturReport = new Model.mdlReturReport();
                mdlReturReport.BranchID = row["BranchID"].ToString();
                mdlReturReport.BranchName = row["BranchName"].ToString();
                mdlReturReport.CustomerID = row["CustomerID"].ToString();
                mdlReturReport.CustomerName = row["CustomerName"].ToString();
                mdlReturReport.WarehouseID = row["WarehouseID"].ToString();
                mdlReturReport.WarehouseName = row["WarehouseName"].ToString();
                mdlReturReport.ReturNumber = row["ReturNumber"].ToString();
                mdlReturReport.ProductID = row["ProductID"].ToString();
                mdlReturReport.ArticleNumber = row["ArticleNumber"].ToString();
                mdlReturReport.ProductName = row["ProductName"].ToString();
                mdlReturReport.Quantity = Convert.ToInt32(row["Quantity"]);
                mdlReturReport.UOM = row["UOM"].ToString();
                mdlReturReport.QuantityReal = Convert.ToInt32(row["QuantityReal"]);
                mdlReturReport.ReturDate = Convert.ToDateTime(row["ReturDate"]).ToString();
                mdlReturReport.EmployeeID = row["EmployeeID"].ToString();
                mdlReturReport.EmployeeName = row["EmployeeName"].ToString();
                mdlReturReport.VehicleID = row["VehicleID"].ToString();
                mdlReturReport.ReturImage = row["ImageBase64"].ToString();
                mdlReturReport.ReturImagePath = row["ImagePath"].ToString();
                mdlReturReport.ReceivedDate = Convert.ToDateTime(row["ReceivedDate"]).ToString();
                mdlReturReport.Remark = row["Remark"].ToString(); 
                mdlReturReport.Reason = row["Value"].ToString(); 

                mdlReturReportList.Add(mdlReturReport);
            }
            return mdlReturReportList;
        }


        public static Model.mdlResultList InsertReturOrder(List<Model.mdlReturOrderParam> lParamlist)
        {
            var mdlResultList = new List<Model.mdlResult>();
            foreach (var lParam in lParamlist)
            {
                var mdlResult = new Model.mdlResult();
                
                bool lCheckReturNumber = CheckbyReturNumber(lParam.ReturNumber);
                if (lCheckReturNumber == false)
                {
                    mdlResult.Result = " || ReturNumber : " + lParam.ReturNumber + " " + "|| IDExist ||";
                    mdlResultList.Add(mdlResult);
                }
                else
                {
                    List<SqlParameter> sp = new List<SqlParameter>()
                    {
                        new SqlParameter() {ParameterName = "@ReturNumber", SqlDbType = SqlDbType.NVarChar, Value = lParam.ReturNumber},
                        new SqlParameter() {ParameterName = "@CustomerID", SqlDbType = SqlDbType.NVarChar, Value = lParam.CustomerID},
                        new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = lParam.EmployeeID},
                        new SqlParameter() {ParameterName = "@VisitID", SqlDbType = SqlDbType.NVarChar, Value = lParam.VisitID},
                        new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = lParam.BranchID},
                        new SqlParameter() {ParameterName = "@ReturDate", SqlDbType = SqlDbType.Date, Value = lParam.ReturDate},
                        new SqlParameter() {ParameterName = "@ReturStatus", SqlDbType = SqlDbType.NVarChar, Value = lParam.ReturStatus},     
                        new SqlParameter() {ParameterName = "@Description", SqlDbType = SqlDbType.NVarChar, Value = lParam.Description}, 
                        new SqlParameter() {ParameterName = "@Signature", SqlDbType = SqlDbType.NVarChar, Value = lParam.Signature},
                        new SqlParameter() {ParameterName = "@IsPrint", SqlDbType = SqlDbType.NVarChar, Value = lParam.IsPrint},
                        new SqlParameter() {ParameterName = "@Remark", SqlDbType = SqlDbType.NVarChar, Value = lParam.Remark},
                        new SqlParameter() {ParameterName = "@ReceivedDate", SqlDbType = SqlDbType.DateTime, Value = lParam.ReceivedDate},
                        new SqlParameter() {ParameterName = "@IsNewRetur", SqlDbType = SqlDbType.NVarChar, Value = lParam.IsNewRetur} 
                    };

                    string query = @"INSERT INTO ReturOrder (ReturNumber, CustomerID, EmployeeID, VisitID, BranchID, ReturDate, ReturStatus, Description, Signature, IsPrint, Remark, ReceivedDate, IsNewRetur) " +
                                                "VALUES (@ReturNumber, @CustomerID, @EmployeeID, @VisitID, @BranchID, @ReturDate, @ReturStatus, @Description, @Signature, @IsPrint, @Remark, @ReceivedDate, @IsNewRetur)"; 

                    mdlResult.Result = "|| " + "Insert Retur Number " + lParam.ReturNumber + " || " + Manager.DataFacade.DTSQLVoidCommand(query, sp);

                    if (mdlResult.Result.Contains("SQLSuccess") == true) 
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

        public static bool CheckbyReturNumber(string lReturNumber)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName = "@ReturNumber", SqlDbType = SqlDbType.NVarChar, Value = lReturNumber },
            };

            DataTable dtReturOrder = Manager.DataFacade.DTSQLCommand(@"SELECT TOP 1 ReturNumber 
                                                                   FROM ReturOrder 
                                                                   WHERE ReturNumber = @ReturNumber", sp);
            bool lCheck = false;
            if (dtReturOrder.Rows.Count == 0)
            {
                lCheck = true;
            }

            return lCheck;
        }

        public static Model.mdlResultList InsertReturOrderDetail(List<Model.mdlReturOrderDetailParam> lParamlist)
        {
            var mdlResultList = new List<Model.mdlResult>();

            foreach (var lParam in lParamlist)
            {
                if (lParam.Quantity == "" || lParam.Quantity == null)
                    lParam.Quantity = "0";

                var mdlResult = new Model.mdlResult();

                bool lCheckReturNumberAndProductID = CheckbyReturNumberAndProductID(lParam.ReturNumber, lParam.ProductID);
                if (lCheckReturNumberAndProductID == false)
                {
                    mdlResult.Result = " || ReturNumber : " +lParam.ReturNumber+ " and ProductID : " +lParam.ProductID+ " " + "|| IDExist ||"; 
                    mdlResultList.Add(mdlResult);
                }
                else
                {
                    List<SqlParameter> sp = new List<SqlParameter>()
                    {
                        new SqlParameter() {ParameterName = "@ReturNumber", SqlDbType = SqlDbType.NVarChar, Value = lParam.ReturNumber},
                        new SqlParameter() {ParameterName = "@ProductID", SqlDbType = SqlDbType.NVarChar, Value = lParam.ProductID},
                        new SqlParameter() {ParameterName = "@UOM", SqlDbType = SqlDbType.NVarChar, Value = lParam.UOM},
                        new SqlParameter() {ParameterName = "@Quantity", SqlDbType = SqlDbType.Decimal, Value = lParam.Quantity},
                        new SqlParameter() {ParameterName = "@QuantityReal", SqlDbType = SqlDbType.Decimal, Value = lParam.QuantityReal},
                        new SqlParameter() {ParameterName = "@ArticleNumber", SqlDbType = SqlDbType.NVarChar, Value = lParam.ArticleNumber},
                        new SqlParameter() {ParameterName = "@ReasonID", SqlDbType = SqlDbType.NVarChar, Value = lParam.ReasonID}
                    };

                    string query = @"INSERT INTO ReturOrderDetail (ReturNumber, ProductID, UOM, Quantity, QuantityReal, ArticleNumber,ReasonID) " +
                                                "VALUES (@ReturNumber, @ProductID, @UOM, @Quantity, @QuantityReal, @ArticleNumber, @ReasonID)";

                    mdlResult.Result = " || ReturNumber : " + lParam.ReturNumber + " and ProductID : " + lParam.ProductID + " || " + Manager.DataFacade.DTSQLVoidCommand(query, sp);

                    if (mdlResult.Result.Contains("SQLSuccess") == true) 
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

        public static bool CheckbyReturNumberAndProductID(string lReturNumber, string lProductID)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName = "@ReturNumber", SqlDbType = SqlDbType.NVarChar, Value = lReturNumber },
               new SqlParameter() {ParameterName = "@ProductID", SqlDbType = SqlDbType.NVarChar, Value = lProductID }
            };

            DataTable dtReturOrderDetail = Manager.DataFacade.DTSQLCommand(@"SELECT TOP 1 ReturNumber,ProductID 
                                                                   FROM ReturOrderDetail 
                                                                   WHERE ReturNumber = @ReturNumber AND ProductID = @ProductID", sp);
            bool lCheck = false;
            if (dtReturOrderDetail.Rows.Count == 0)
            {
                lCheck = true;
            }

            return lCheck;
        }
        

    }
   
}
