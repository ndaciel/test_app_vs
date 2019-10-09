/* documentation
 * 001 17 Okt 2016 fernandes
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Configuration;
using System.Transactions;


namespace Core.Manager
{
    public class DeliveryOrderFacade : Base.Manager
    {
        //001
        //public static List<Model.DeliveryOrder> GetDOByCustomerID(string customerID,string warehouseID)
        //{
        //    var DO = DataContext.DeliveryOrders.Where(fld => fld.CustomerID.Equals(customerID) && fld.WarehouseID.Equals(warehouseID)).OrderByDescending(fld => fld.DODate).ToList();
        //    return DO;
        //}

        public static List<Model.mdlDeliveryOrder> LoadDeliveryOrder(Model.mdlParam json)
        {
            var mdlDeliveryOrderList = new List<Model.mdlDeliveryOrder>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@StartDate", SqlDbType = SqlDbType.DateTime, Value= Convert.ToDateTime(json.Date).Date },
                new SqlParameter() {ParameterName = "@FinishDate", SqlDbType = SqlDbType.DateTime, Value= Convert.ToDateTime(json.Date).Date.AddDays(1) },
                new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = json.EmployeeID },
                new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = json.BranchID }
            };

            DataTable dtDeliveryOrder = DataFacade.DTSQLCommand(@"SELECT  a.DONumber
                    ,a.CallPlanID
                    ,a.DODate
                    ,a.DOStatus
                    ,a.Description
                    ,a.CustomerID
                    ,a.EmployeeID
                    ,a.VehicleID 
                    ,a.BranchID
                    ,a.Signature
                    ,a.IsPrint
                    ,a.Remark
                    ,a.WarehouseID 
                FROM DeliveryOrder a
                INNER JOIN (
		        
				                                 SELECT CallPlanID FROM CallPlan 
                                                 WHERE EmployeeID = @EmployeeID AND BranchID = @BranchID AND IsFinish = 0 AND Date >= @StartDate AND Date < @FinishDate
                               
		                   ) b ON b.CallPlanID = a.CallPlanID
                WHERE DOStatus = 'Shipper'", sp);
            //INNER JOIN  (select * from CallPlanDetail where CallPlanID='C1000001') b ON b.CustomerID = a.CustomerID


            foreach (DataRow row in dtDeliveryOrder.Rows)
            {
                var mdlDeliveryOrder = new Model.mdlDeliveryOrder();
                mdlDeliveryOrder.DONumber = row["DONumber"].ToString();
                mdlDeliveryOrder.CallPlanID = row["CallPlanID"].ToString();
                mdlDeliveryOrder.CustomerID = row["CustomerID"].ToString();
                mdlDeliveryOrder.WarehouseID = row["WarehouseID"].ToString();
                mdlDeliveryOrder.EmployeeID = row["EmployeeID"].ToString();
                mdlDeliveryOrder.VehicleID = row["VehicleID"].ToString(); //006--
                mdlDeliveryOrder.DODate = Convert.ToDateTime(row["DODate"]).ToString("yyyy-MM-dd hh:mm:ss");
                mdlDeliveryOrder.DOStatus = row["DOStatus"].ToString();
                mdlDeliveryOrder.Description = row["Description"].ToString();
                mdlDeliveryOrder.Signature = row["Signature"].ToString();
                mdlDeliveryOrder.IsPrint = row["IsPrint"].ToString();
                mdlDeliveryOrder.Remark = row["Remark"].ToString();
                mdlDeliveryOrder.BranchID = row["BranchID"].ToString();
                mdlDeliveryOrder.VisitID = "";
                mdlDeliveryOrderList.Add(mdlDeliveryOrder);
                
            }
            return mdlDeliveryOrderList;

        }

        public static List<Model.mdlDeliveryOrderDetail> LoadDeliveryOrderDetail(Model.mdlParam json, List<Model.mdlDeliveryOrder> listDO)
        {
            
            List<SqlParameter> sp = new List<SqlParameter>();
            StringBuilder sb = new StringBuilder();
            int count = 1;
            foreach(var DO in listDO)
            {
                
                var sqlParameter = new SqlParameter();
                sqlParameter.ParameterName = "@DoNumbers" + count.ToString();
                if (DO == listDO.Last())
                {
                    sb.Append("@DoNumbers" + count.ToString());
                }
                else
                {
                    sb.Append("@DoNumbers" + count.ToString() + ",");
                }
                sqlParameter.SqlDbType = SqlDbType.NVarChar;
                sqlParameter.Value = DO.DONumber;
                sp.Add(sqlParameter);
                count++;
            }


            var mdlDeliveryOrderDetailList = new List<Model.mdlDeliveryOrderDetail>();
            var deliveryOrder = LoadDeliveryOrder(json);

            //List<SqlParameter> sp = new List<SqlParameter>()
            //{
            //    new SqlParameter() {ParameterName = "@StartDate", SqlDbType = SqlDbType.DateTime, Value= DateTime.Now.Date },
            //    new SqlParameter() {ParameterName = "@FinishDate", SqlDbType = SqlDbType.DateTime, Value= DateTime.Now.Date.AddDays(1) },
            //    new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = json.EmployeeID },
            //    new SqlParameter() {ParameterName = "@DoNumbers", SqlDbType = SqlDbType.NVarChar, Value =  DONumbers},
            //    new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = json.BranchID }
            //};
            

            
            

//            DataTable dtDeliveryOrderDetailList = DataFacade.DTSQLCommand(@"SELECT  DONumber,
//	                ProductID,
//	                UOM,
//	                Quantity,
//	                QuantityReal,
//	                ProductGroup,
//	                LotNumber,
//                    BoxID 
//                FROM DeliveryOrderDetail 
//                WHERE DONumber IN (
//				                SELECT a.DONumber 
//				                FROM DeliveryOrder a
//					                INNER JOIN (
//								                SELECT * FROM CallPlanDetail 
//								                WHERE CallPlanID IN (
//														                SELECT CallPlanID FROM CallPlan 
//														                WHERE EmployeeID = @EmployeeID AND BranchID = @BranchID AND IsFinish = 0 AND Date >= @StartDate
//														             )
//								                ) b ON b.CustomerID = a.CustomerID
//								                WHERE  EmployeeID = @EmployeeID AND BranchID = @BranchID  AND DODate >= @StartDate AND DOStatus = 'Shipper'
//				                    )", sp); 


            DataTable dtDeliveryOrderDetailList = DataFacade.DTSQLCommand(@"SELECT  DONumber,
	                ProductID,
                    UOM,
	                Quantity,
                    QuantityReal,
                    ProductGroup,
	                LotNumber,
                    BoxID,
                    Line 
               FROM DeliveryOrderDetail WHERE DONumber IN (" + sb.ToString() + ") AND Quantity != 0", sp);
            foreach (DataRow row in dtDeliveryOrderDetailList.Rows)
            {
                var mdlDeliveryOrderDetail = new Model.mdlDeliveryOrderDetail();
                mdlDeliveryOrderDetail.DONumber = row["DONumber"].ToString();
                mdlDeliveryOrderDetail.ProductID = row["ProductID"].ToString();
                mdlDeliveryOrderDetail.UOM = row["UOM"].ToString();
                mdlDeliveryOrderDetail.Quantity = row["Quantity"].ToString();
                mdlDeliveryOrderDetail.QuantityReal = row["QuantityReal"].ToString();
                mdlDeliveryOrderDetail.ProductGroup = row["ProductGroup"].ToString();
                mdlDeliveryOrderDetail.LotNumber = row["LotNumber"].ToString();
                mdlDeliveryOrderDetail.BoxID = row["BoxID"].ToString();
                mdlDeliveryOrderDetail.Line = row["Line"].ToString();
                mdlDeliveryOrderDetailList.Add(mdlDeliveryOrderDetail);
            }

            return mdlDeliveryOrderDetailList;
        }

        public static Model.mdlResultList UpdateDeliveryOrder(List<Model.mdlDeliveryOrderParam> lDOParamlist) //005,009 
        {
            var mdlResultList = new List<Model.mdlResult>();

            var listDO = new List<Model.DeliveryOrder>();

            var mdlResult = new Model.mdlResult();

            foreach (var lDOParam in lDOParamlist)
            {
                var mdlDO = new Model.DeliveryOrder();
                //Have To Be Filled
                mdlDO.DONumber = lDOParam.DONumber;
                mdlDO.CallPlanID = lDOParam.CallPlanID;
                mdlDO.CustomerID = lDOParam.CustomerID;
                mdlDO.WarehouseID = lDOParam.WarehouseID;
                //Have To Be Filled
                
                //Updated Data
                mdlDO.DOStatus = lDOParam.DOStatus;
                mdlDO.Signature = lDOParam.Signature;
                mdlDO.IsPrint = Convert.ToBoolean(lDOParam.IsPrint);
                mdlDO.LastDate = DateTime.Now;
                mdlDO.LastUpdateBy = lDOParam.EmployeeID;
                mdlDO.VisitID = lDOParam.VisitID;

                if (lDOParam.SendDate == null || lDOParam.SendDate == "")
                    lDOParam.SendDate = "2000-01-01 00:00:00";

                mdlDO.SendDate = Convert.ToDateTime(lDOParam.SendDate);
                //Updated Data


                //additional
                mdlDO.CreatedDate = DateTime.Now;
                mdlDO.DODate = DateTime.Now;
                mdlDO.Description = "";
                mdlDO.BranchID = lDOParam.BranchID;
                mdlDO.VehicleID = lDOParam.VehicleID;
                mdlDO.EmployeeID = lDOParam.EmployeeID;
                mdlDO.Remark = "";
                mdlDO.CreatedBy = "";
                //additional
                
                listDO.Add(mdlDO);
            }


            var listUpdateCol = new List<string>();
            listUpdateCol.Add("DOStatus");
            listUpdateCol.Add("Signature");
            listUpdateCol.Add("IsPrint");
            listUpdateCol.Add("LastDate");
            listUpdateCol.Add("LastUpdateBy");
            listUpdateCol.Add("VisitID");
            listUpdateCol.Add("SendDate");

            var listOnCol = new List<string>();
            listOnCol.Add("DONumber");
            listOnCol.Add("CallPlanID");
            listOnCol.Add("CustomerID");


            mdlResult.Result = "|| Update DO  || " + Manager.DataFacade.DTSQLListUpdate(listDO, "DeliveryOrder",listUpdateCol,listOnCol);


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

        public static Model.mdlResultList UpdateDeliveryOrderDetail(List<Model.mdlDeliveryOrderDetailParam> lDODetailParamlist) 
        {
           
            var mdlResultList = new List<Model.mdlResult>();

            var mdlResult = new Model.mdlResult();

            var listDODetail = new List<Model.mdlDBDeliveryOrderDetail>();

            foreach (var lDODetailParam in lDODetailParamlist)
            {

                var mdlDODetail = new Model.mdlDBDeliveryOrderDetail();
                mdlDODetail.DONumber = lDODetailParam.DONumber;
                mdlDODetail.ProductID = lDODetailParam.ProductID;
                mdlDODetail.UOM = lDODetailParam.UOM;
                mdlDODetail.Quantity = 0;
                mdlDODetail.QuantityReal = Convert.ToInt32(lDODetailParam.QuantityReal);
                mdlDODetail.ProductGroup = "";
                mdlDODetail.LotNumber = "";
                mdlDODetail.ReasonID = "";
                mdlDODetail.BoxID = "";
                mdlDODetail.Line = lDODetailParam.Line;
                listDODetail.Add(mdlDODetail);
                
            }

            var listUpdateCol = new List<string>();
            listUpdateCol.Add("QuantityReal");
           

            var listOnCol = new List<string>();
            listOnCol.Add("DONumber");
            //listOnCol.Add("ProductID");
            //listOnCol.Add("UOM");
            listOnCol.Add("Line");
      

            mdlResult.Result = Manager.DataFacade.DTSQLListUpdate(listDODetail,"DeliveryOrderDetail",listUpdateCol,listOnCol);

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

        public static Model.mdlResultList UploadUpdateDeliveryOrder(List<Model.mdlDeliveryOrderParam> lDOParamlist,TransactionScope scope) //005,009 
        {
            var mdlResultList = new List<Model.mdlResult>();

            var listDO = new List<Model.DeliveryOrder>();

            var mdlResult = new Model.mdlResult();

            foreach (var lDOParam in lDOParamlist)
            {
                var mdlDO = new Model.DeliveryOrder();
                //Have To Be Filled
                mdlDO.DONumber = lDOParam.DONumber;
                mdlDO.CallPlanID = lDOParam.CallPlanID;
                mdlDO.CustomerID = lDOParam.CustomerID;
                mdlDO.WarehouseID = lDOParam.WarehouseID;
                //Have To Be Filled

                //Updated Data
                mdlDO.DOStatus = lDOParam.DOStatus;
                mdlDO.Signature = lDOParam.Signature;
                mdlDO.IsPrint = Convert.ToBoolean(lDOParam.IsPrint);
                mdlDO.LastDate = DateTime.Now;
                mdlDO.LastUpdateBy = lDOParam.EmployeeID;
                mdlDO.VisitID = lDOParam.VisitID;
                //Updated Data


                //additional
                mdlDO.CreatedDate = DateTime.Now;
                mdlDO.DODate = DateTime.Now;
                mdlDO.Description = "";
                mdlDO.BranchID = lDOParam.BranchID;
                mdlDO.VehicleID = lDOParam.VehicleID;
                mdlDO.EmployeeID = lDOParam.EmployeeID;
                mdlDO.Remark = "";
                mdlDO.CreatedBy = "";
                //additional

                listDO.Add(mdlDO);
            }


            var listUpdateCol = new List<string>();
            listUpdateCol.Add("DOStatus");
            listUpdateCol.Add("Signature");
            listUpdateCol.Add("IsPrint");
            listUpdateCol.Add("LastDate");
            listUpdateCol.Add("LastUpdateBy");
            listUpdateCol.Add("VisitID");

            var listOnCol = new List<string>();
            listOnCol.Add("DONumber");
            listOnCol.Add("CallPlanID");
            listOnCol.Add("CustomerID");


            mdlResult.Result = Manager.DataFacade.DTSQLListUpdate(listDO, "DeliveryOrder", listUpdateCol, listOnCol);


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
                    //
                    
                }
                scope.Dispose();


            }

            mdlResultList.Add(mdlResult);
            var mdlResultListnew = new Model.mdlResultList();
            mdlResultListnew.ResultList = mdlResultList;

            return mdlResultListnew;
        }

        public static Model.mdlResultList UploadUpdateDeliveryOrderDetail(List<Model.mdlDeliveryOrderDetailParam> lDODetailParamlist, TransactionScope scope)
        {

            var mdlResultList = new List<Model.mdlResult>();

            var mdlResult = new Model.mdlResult();

            var listDODetail = new List<Model.mdlDBDeliveryOrderDetail>();

            foreach (var lDODetailParam in lDODetailParamlist)
            {

                var mdlDODetail = new Model.mdlDBDeliveryOrderDetail();
                mdlDODetail.DONumber = lDODetailParam.DONumber;
                mdlDODetail.ProductID = lDODetailParam.ProductID;
                mdlDODetail.UOM = lDODetailParam.UOM;
                mdlDODetail.Quantity = 0;
                mdlDODetail.QuantityReal = Convert.ToInt32(lDODetailParam.QuantityReal);
                mdlDODetail.ProductGroup = "";
                mdlDODetail.LotNumber = "";
                mdlDODetail.ReasonID = "";
                mdlDODetail.BoxID = "";

                listDODetail.Add(mdlDODetail);

            }

            var listUpdateCol = new List<string>();
            listUpdateCol.Add("QuantityReal");


            var listOnCol = new List<string>();
            listOnCol.Add("DONumber");
            listOnCol.Add("ProductID");
            listOnCol.Add("UOM");


            mdlResult.Result = Manager.DataFacade.DTSQLListUpdate(listDODetail, "DeliveryOrderDetail", listUpdateCol, listOnCol);

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
                    //
                    
                }
                scope.Dispose();


            }

            mdlResultList.Add(mdlResult);

            var mdlResultListnew = new Model.mdlResultList();
            mdlResultListnew.ResultList = mdlResultList;

            return mdlResultListnew;

        }

              
        public static Model.mdlDeliveryOrder LoadDeliveryOrderbyDONumber(string lDoNumber)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@DoNumber", SqlDbType = SqlDbType.NVarChar, Value= lDoNumber }
            };

            DataTable dtDeliveryOrder = DataFacade.DTSQLCommand(@"SELECT TOP 1 DONumber
                                                                            ,DODate
                                                                            ,BranchID
                                                                            ,VehicleID
                                                                    From DeliveryOrder WHERE DONumber = @DoNumber", sp);
            var mdlDeliveryOrder = new Model.mdlDeliveryOrder();
            foreach (DataRow row in dtDeliveryOrder.Rows)
            {

                mdlDeliveryOrder.DONumber = row["DONumber"].ToString();
                mdlDeliveryOrder.DODate = Convert.ToDateTime(row["DODate"]).ToString("yyyy-MM-dd hh:mm:ss");
                mdlDeliveryOrder.BranchID = row["BranchID"].ToString();
                mdlDeliveryOrder.VehicleID = row["VehicleID"].ToString();
            }

            return mdlDeliveryOrder;
        }
       
        //001
        public static List<Model.mdlDOReport> LoadDeliveryOrderReport(string lBranchID, DateTime lDODate, DateTime lDOEndDate, List<string> lEmployeeIDlist)
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
               new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = "%"+lBranchID+"%" },
               new SqlParameter() {ParameterName = "@DODate", SqlDbType = SqlDbType.Date, Value = lDODate },
               new SqlParameter() {ParameterName = "@DOEndDate", SqlDbType = SqlDbType.Date, Value = lDOEndDate.AddDays(1) }, 
              
            };

            var mdlDOReportList = new List<Model.mdlDOReport>();

           // a.WarehouseID,
           // i.WarehouseName,
           // (select top 1 ImageBase64 from CustomerImage where DocNumber=a.DONumber) imagebase64,
           //(select top 1 ImagePath from CustomerImage where DocNumber=a.DONumber) imagepath,
            //LEFT JOIN Warehouse i ON i.WarehouseID = a.WarehouseID

            DataTable dtDeliveryOrderReport = Manager.DataFacade.DTSQLCommand(@"SELECT   a.BranchID, 
			                                                                        c.BranchName,
                                                                                    a.EmployeeID,
                                                                                    g.EmployeeName,
                                                                                    f.VehicleID,    
			                                                                        a.CustomerID,
			                                                                        e.CustomerName,
                                                                                    a.WarehouseID,
                                                                                    i.WarehouseName,
                                                                                    
			                                                                        a.DONumber,
                                                                                    a.Remark,
                                                                                    a.DODate,
                                                                                    a.LastDate,
																					    
			                                                                        b.ProductID,
                                                                                    d.ArticleNumber, 
			                                                                        d.ProductName,
                                                                                    b.LotNumber,
			                                                                        b.Quantity,
                                                                                    b.UOM,
			                                                                        b.QuantityReal,
                                                                                    h.Value
			                                                                       
				                                                            FROM DeliveryOrder a
				                                                            INNER JOIN DeliveryOrderDetail b ON b.DONumber = a.DONumber
				                                                            INNER JOIN Branch c ON c.BranchID = a.BranchID 
				                                                            INNER JOIN Customer e ON e.CustomerID = a.CustomerID
				                                                            INNER JOIN Product d ON d.ProductID = b.ProductID
                                                                            INNER JOIN Employee g ON g.EmployeeID = a.EmployeeID
                                                                            LEFT JOIN Reason h ON h.ReasonID = b.ReasonID
                                                                            LEFT JOIN Visit f ON f.VisitID = a.VisitID
                                                                            LEFT JOIN Warehouse i ON i.WarehouseID = a.WarehouseID
                                                                            
				                                                            Where (a.BranchID LIKE @BranchID) AND (a.LastDate between @DODate and @DOEndDate) AND " + lParam + "Order by a.LastDate", sp); //018

            foreach (DataRow row in dtDeliveryOrderReport.Rows)
            {
                var mdlDOReport = new Model.mdlDOReport();
                mdlDOReport.BranchID = row["BranchID"].ToString();
                mdlDOReport.BranchName = row["BranchName"].ToString();

                mdlDOReport.CustomerID = row["CustomerID"].ToString();
                mdlDOReport.CustomerName = row["CustomerName"].ToString();
                mdlDOReport.WarehouseID = row["WarehouseID"].ToString();
                mdlDOReport.WarehouseName = row["WarehouseName"].ToString();

                if (mdlDOReport.CustomerID == mdlDOReport.WarehouseID)
                {
                    mdlDOReport.CustomerID.ToString();
                }
                else
                {
                    mdlDOReport.CustomerID = mdlDOReport.CustomerID + " - " + mdlDOReport.WarehouseID;
                }

                mdlDOReport.DONumber = row["DONumber"].ToString();
                mdlDOReport.ProductID = row["ProductID"].ToString();
                mdlDOReport.ArticleNumber = row["ArticleNumber"].ToString();
                mdlDOReport.ProductName = row["ProductName"].ToString();
                mdlDOReport.LotNumber = row["LotNumber"].ToString();

                //mdlDOReport.UOM = row["UOM"].ToString(); fernandes
                mdlDOReport.Quantity = row["Quantity"].ToString() + " " + row["UOM"].ToString();
                
                string qtyReal = ProductUOMFacade.ConvertMultiUOM(mdlDOReport.ProductID,Convert.ToInt32(row["QuantityReal"]));
                mdlDOReport.QuantityReal = qtyReal;
                //mdlDOReport.ImageBase64 = row["ImageBase64"].ToString();
                //mdlDOReport.ImagePath = row["imagepath"].ToString();
                mdlDOReport.DODate = Convert.ToDateTime(row["DODate"]).ToString();
                mdlDOReport.DeliveryDate = Convert.ToDateTime(row["LastDate"]).ToString("dd-MM-yyyy HH:mm:ss");

                mdlDOReport.EmployeeID = row["EmployeeID"].ToString();
                mdlDOReport.EmployeeName = row["EmployeeName"].ToString();
                mdlDOReport.VehicleID = row["VehicleID"].ToString();
                mdlDOReport.Remark = row["Remark"].ToString(); 
                mdlDOReport.Reason = row["Value"].ToString(); 

                mdlDOReportList.Add(mdlDOReport);
            }
            return mdlDOReportList;
        }

        public static List<Model.mdlDOReport> LoadDeliveryOrderPartialReport(string lBranchID, DateTime lDODate, DateTime lDOEndDate, List<string> lEmployeeIDlist)
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
               new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = "%"+lBranchID+"%" },
               new SqlParameter() {ParameterName = "@DODate", SqlDbType = SqlDbType.Date, Value = lDODate },
               new SqlParameter() {ParameterName = "@DOEndDate", SqlDbType = SqlDbType.Date, Value = lDOEndDate.AddDays(1) }, 
              
            };

            var mdlDOReportList = new List<Model.mdlDOReport>();

            // a.WarehouseID,
            // i.WarehouseName,
            // (select top 1 ImageBase64 from CustomerImage where DocNumber=a.DONumber) imagebase64,
            //(select top 1 ImagePath from CustomerImage where DocNumber=a.DONumber) imagepath,
            //LEFT JOIN Warehouse i ON i.WarehouseID = a.WarehouseID

            DataTable dtDeliveryOrderReport = Manager.DataFacade.DTSQLCommand(@"SELECT   a.BranchID, 
			                                                                        c.BranchName,
                                                                                    a.EmployeeID,
                                                                                    g.EmployeeName,
                                                                                    f.VehicleID,    
			                                                                        a.CustomerID,
			                                                                        e.CustomerName,
                                                                                    a.WarehouseID,
                                                                                    i.WarehouseName,
                                                                                    
			                                                                        a.DONumber,
                                                                                    a.Remark,
                                                                                    a.DODate,
                                                                                    a.LastDate,
																					    
			                                                                        b.ProductID,
                                                                                    d.ArticleNumber, 
			                                                                        d.ProductName,
                                                                                    b.LotNumber,
			                                                                        b.Quantity,
                                                                                    b.UOM,
			                                                                        b.QuantityReal,
                                                                                    h.Value
			                                                                       
				                                                            FROM DeliveryOrder a
				                                                            INNER JOIN DeliveryOrderDetail b ON b.DONumber = a.DONumber
				                                                            INNER JOIN Branch c ON c.BranchID = a.BranchID 
				                                                            INNER JOIN Customer e ON e.CustomerID = a.CustomerID
				                                                            INNER JOIN Product d ON d.ProductID = b.ProductID
                                                                            INNER JOIN Employee g ON g.EmployeeID = a.EmployeeID
                                                                            LEFT JOIN Reason h ON h.ReasonID = b.ReasonID
                                                                            LEFT JOIN Visit f ON f.VisitID = a.VisitID
                                                                            LEFT JOIN Warehouse i ON i.WarehouseID = a.WarehouseID
                                                                            
				                                                            Where (a.BranchID LIKE @BranchID) AND (a.LastDate between @DODate and @DOEndDate) AND " + lParam + "Order by a.LastDate", sp); //018

            foreach (DataRow row in dtDeliveryOrderReport.Rows)
            {
                var mdlDOReport = new Model.mdlDOReport();
                //mdlDOReport.UOM = row["UOM"].ToString(); fernandes
                mdlDOReport.Quantity = row["Quantity"].ToString() + " " + row["UOM"].ToString();
                mdlDOReport.ProductID = row["ProductID"].ToString();

                string qtyReal = ProductUOMFacade.ConvertMultiUOM(mdlDOReport.ProductID, Convert.ToInt32(row["QuantityReal"]));

                if (qtyReal.Trim() == mdlDOReport.Quantity)
                {
                }
                else
                {
                    mdlDOReport.BranchID = row["BranchID"].ToString();
                    mdlDOReport.BranchName = row["BranchName"].ToString();

                    mdlDOReport.CustomerID = row["CustomerID"].ToString();
                    mdlDOReport.CustomerName = row["CustomerName"].ToString();
                    mdlDOReport.WarehouseID = row["WarehouseID"].ToString();
                    mdlDOReport.WarehouseName = row["WarehouseName"].ToString();

                    if (mdlDOReport.CustomerID == mdlDOReport.WarehouseID)
                    {
                        mdlDOReport.CustomerID.ToString();
                    }
                    else
                    {
                        mdlDOReport.CustomerID = mdlDOReport.CustomerID + " - " + mdlDOReport.WarehouseID;
                    }

                    mdlDOReport.DONumber = row["DONumber"].ToString();
                    mdlDOReport.ProductID = row["ProductID"].ToString();
                    mdlDOReport.ArticleNumber = row["ArticleNumber"].ToString();
                    mdlDOReport.ProductName = row["ProductName"].ToString();
                    mdlDOReport.LotNumber = row["LotNumber"].ToString();

                    //mdlDOReport.UOM = row["UOM"].ToString(); fernandes
                    mdlDOReport.Quantity = row["Quantity"].ToString() + " " + row["UOM"].ToString();

                    mdlDOReport.QuantityReal = qtyReal;
                    //mdlDOReport.ImageBase64 = row["ImageBase64"].ToString();
                    //mdlDOReport.ImagePath = row["imagepath"].ToString();
                    mdlDOReport.DODate = Convert.ToDateTime(row["DODate"]).ToString();
                    mdlDOReport.DeliveryDate = Convert.ToDateTime(row["LastDate"]).ToString("dd-MM-yyyy HH:mm:ss");

                    mdlDOReport.EmployeeID = row["EmployeeID"].ToString();
                    mdlDOReport.EmployeeName = row["EmployeeName"].ToString();
                    mdlDOReport.VehicleID = row["VehicleID"].ToString();
                    mdlDOReport.Remark = row["Remark"].ToString();
                    mdlDOReport.Reason = row["Value"].ToString();

                    mdlDOReportList.Add(mdlDOReport);
                }
                
            }
            return mdlDOReportList;
        }

        public static List<Model.mdlDeliveryOrderDetail> UnvalidDOProduct(List<Model.mdlDeliveryOrder> listDO)
        {
            var mdlDODetailList = new List<Model.mdlDeliveryOrderDetail>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
            };

            string lParam = string.Empty;

            foreach (var DO in listDO)
            {
                if (lParam == "")
                {
                    lParam = "'"+DO.DONumber+"'";
                }
                else
                {
                    lParam += ",'"+DO.DONumber+"'";
                }
            }

            lParam = "(" + lParam + ")";

            DataTable dtDODetail = Manager.DataFacade.DTSQLCommand(@"select DONumber, ProductID from DeliveryOrderDetail 
                                                                where DONumber in "+lParam+" and ProductID not in (select ProductID from ProductUOM)", sp);

            foreach (DataRow row in dtDODetail.Rows)
            {
                var mdlDODetail = new Model.mdlDeliveryOrderDetail();
                mdlDODetail.DONumber = row["DONumber"].ToString();
                mdlDODetail.ProductID = row["ProductID"].ToString();

                mdlDODetailList.Add(mdlDODetail);
            }
            return mdlDODetailList;
        }

    }
}
