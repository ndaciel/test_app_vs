/* documentation
 * 001 17 Okt'16 fernandes
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.IO;
using System.Configuration;
using System.Web;
using System.Transactions;

namespace Core.Manager
{
    public class CustomerImageFacade
    {
        public static Model.mdlResultList InsertCustomerImage(List<Model.mdlCustomerImageParam> lParamlist)
        {
            var mdlResultList = new List<Model.mdlResult>();
            string UploadImagePath = ConfigurationManager.AppSettings["imagePath"];
            DateTime dateTime = DateTime.UtcNow.Date;
            string day = dateTime.ToString("dd");
            string month = dateTime.ToString("MM");
            string year = dateTime.ToString("yyyy");
            foreach (var lParam in lParamlist)
            {
                var mdlResult = new Model.mdlResult();

                bool lCheckImageID = CheckbyCustomerImageID(lParam.ImageID);
                if (lCheckImageID == false)
                {
                    DeleteCustomerImage(lParam.ImageID);
                    //mdlResult.Result = "|| ImageID : " + lParam.ImageID + " " + "|| IDExist ||"; //008
                    //mdlResultList.Add(mdlResult);
                }

                //WRITE IMAGE TO SPECIFIC FOLDER
                //byte[] data = System.Convert.FromBase64String(lParam.ImageBase64);

                //System.IO.MemoryStream ms = new System.IO.MemoryStream(data);
                //System.Drawing.Image img = System.Drawing.Image.FromStream(ms);

                //string url = "C:\\inetpub\\wwwroot\\FABER_Transport\\Images\\CustomerImage\\" + lParam.ImageType + "\\" + lParam.ImageID + ".jpg";
                //File.WriteAllBytes(url, data);
                string newUploadImagePath = UploadImagePath.Replace("//", "\\") + year + "\\" + month + "\\" + day + "\\" + lParam.ImageBase64;
                if (lParam.Longitude == null)
                    lParam.Longitude = "0";
                if (lParam.Latitude == null)
                    lParam.Latitude = "0";

                List<SqlParameter> sp = new List<SqlParameter>();

                    sp = new List<SqlParameter>()
                    {
                    new SqlParameter() {ParameterName = "@ImageID", SqlDbType = SqlDbType.NVarChar, Value = lParam.ImageID},
                    new SqlParameter() {ParameterName = "@ImageDate", SqlDbType = SqlDbType.NVarChar, Value = lParam.ImageDate},
                    new SqlParameter() {ParameterName = "@ImageBase64", SqlDbType = SqlDbType.NVarChar, Value = lParam.ImageBase64},
                    new SqlParameter() {ParameterName = "@ImageType", SqlDbType = SqlDbType.NVarChar, Value = lParam.ImageType},
                    new SqlParameter() {ParameterName = "@CustomerID", SqlDbType = SqlDbType.NVarChar, Value = lParam.CustomerID},
                    new SqlParameter() {ParameterName = "@WarehouseID", SqlDbType = SqlDbType.NVarChar, Value = lParam.WarehouseID},
                    new SqlParameter() {ParameterName = "@DocNumber", SqlDbType = SqlDbType.NVarChar, Value = lParam.DocNumber},   
                    new SqlParameter() {ParameterName = "@VisitID", SqlDbType = SqlDbType.NVarChar, Value = lParam.VisitID},      
                    new SqlParameter() {ParameterName = "@ImagePath", SqlDbType = SqlDbType.NVarChar, Value = ""},
                    new SqlParameter() {ParameterName = "@Longitude", SqlDbType = SqlDbType.NVarChar, Value = lParam.Longitude}, 
                    new SqlParameter() {ParameterName = "@Latitude", SqlDbType = SqlDbType.NVarChar, Value = lParam.Latitude}
                    };
     

                string query = @"INSERT INTO CustomerImage (ImageID, ImageDate, ImageBase64, ImageType, CustomerID,WarehouseID, DocNumber, VisitID, ImagePath, Longitude, Latitude) " +
                                            "VALUES (@ImageID, @ImageDate, @ImageBase64, @ImageType, @CustomerID,@WarehouseID, @DocNumber, @VisitID, @ImagePath, @Longitude, @Latitude) "; //004

                mdlResult.Result = Manager.DataFacade.DTSQLVoidCommand(query, sp); //010

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

            var mdlResultListnew = new Model.mdlResultList();
            mdlResultListnew.ResultList = mdlResultList;
            return mdlResultListnew;
        }

        public static Model.mdlResultList UploadInsertCustomerImage(List<Model.mdlCustomerImageParam> lParamlist, TransactionScope scope)
        {
            var mdlResultList = new List<Model.mdlResult>();
            foreach (var lParam in lParamlist)
            {
                var mdlResult = new Model.mdlResult();

                bool lCheckImageID = CheckbyCustomerImageID(lParam.ImageID);
                if (lCheckImageID == false)
                {
                    DeleteCustomerImage(lParam.ImageID);
                    //mdlResult.Result = "|| ImageID : " + lParam.ImageID + " " + "|| IDExist ||"; //008
                    //mdlResultList.Add(mdlResult);
                }
                else
                {
                    //WRITE IMAGE TO SPECIFIC FOLDER
                    //byte[] data = System.Convert.FromBase64String(lParam.ImageBase64);

                    //System.IO.MemoryStream ms = new System.IO.MemoryStream(data);
                    //System.Drawing.Image img = System.Drawing.Image.FromStream(ms);

                    //string url = "C:\\inetpub\\wwwroot\\FABER_Transport\\Images\\CustomerImage\\" + lParam.ImageType + "\\" + lParam.ImageID + ".jpg";
                    //File.WriteAllBytes(url, data);

                    List<SqlParameter> sp = new List<SqlParameter>()
                    {
                    new SqlParameter() {ParameterName = "@ImageID", SqlDbType = SqlDbType.NVarChar, Value = lParam.ImageID},
                    new SqlParameter() {ParameterName = "@ImageDate", SqlDbType = SqlDbType.NVarChar, Value = lParam.ImageDate},
                    new SqlParameter() {ParameterName = "@ImageBase64", SqlDbType = SqlDbType.NVarChar, Value = lParam.ImageBase64},
                    new SqlParameter() {ParameterName = "@ImageType", SqlDbType = SqlDbType.NVarChar, Value = lParam.ImageType},
                    new SqlParameter() {ParameterName = "@CustomerID", SqlDbType = SqlDbType.NVarChar, Value = lParam.CustomerID},
                    new SqlParameter() {ParameterName = "@WarehouseID", SqlDbType = SqlDbType.NVarChar, Value = lParam.WarehouseID},
                    new SqlParameter() {ParameterName = "@DocNumber", SqlDbType = SqlDbType.NVarChar, Value = lParam.DocNumber},     //004
                    new SqlParameter() {ParameterName = "@VisitID", SqlDbType = SqlDbType.NVarChar, Value = lParam.VisitID},       //002
                    new SqlParameter() {ParameterName = "@ImagePath", SqlDbType = SqlDbType.NVarChar, Value = ""}
                    };

                    string query = @"INSERT INTO CustomerImage (ImageID, ImageDate, ImageBase64, ImageType, CustomerID,WarehouseID, DocNumber, VisitID, ImagePath) " +
                                                "VALUES (@ImageID, @ImageDate, @ImageBase64, @ImageType, @CustomerID,@WarehouseID, @DocNumber, @VisitID, @ImagePath) "; //004

                    mdlResult.Result = Manager.DataFacade.DTSQLVoidCommand(query, sp); //010

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
            }

            var mdlResultListnew = new Model.mdlResultList();
            mdlResultListnew.ResultList = mdlResultList;
            return mdlResultListnew;
        }
        public static List<Model.mdlCustomerImageReport> LoadCustomerImageReport(string lBranchID, DateTime lStartDate, DateTime lEndDate, List<string> lEmployeeIDlist, List<string> lImgTypelist)
        {
            //Fill the lParam by the Value of EmployeeID CheckBoxList
            string lParam = string.Empty;
            foreach (var lEmployeeID in lEmployeeIDlist)
            {
                if (lParam == "")
                {
                    lParam = " d.EmployeeID =" + "'" + lEmployeeID + "'";
                }
                else
                {
                    lParam += " OR d.EmployeeID =" + "'" + lEmployeeID + "'";
                }

            }
            lParam = "(" + lParam + ")";

            //Fill the lParam2 by the Value of ImageType CheckBoxList
            string lParam2 = string.Empty;
            List<string> listType = new List<string>();
            foreach (var lImgType in lImgTypelist)
            {
                if (lImgType == "VISIT")
                {
                    listType.Add("OutOfRangeVisitCheckIn");
                    listType.Add("OutOfRangeVisitCheckOut");
                    listType.Add("ReasonVisit");
                }

                else if (lImgType == "COST")
                {
                    var listCost = CostFacade.GetCost().Select(x => x.CostID);
                    listType.AddRange(listCost);
                }


            }

            foreach (var type in listType)
            {
                if (lParam2 == "")
                {
                    lParam2 = " a.ImageType =" + "'" + type + "'";
                }
                else
                {
                    lParam2 += " OR a.ImageType =" + "'" + type + "'";
                }
            }
            lParam2 = "AND (" + lParam2 + ")";

            List<SqlParameter> sp = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName = "@StartDate", SqlDbType = SqlDbType.Date, Value = lStartDate },
               new SqlParameter() {ParameterName = "@EndDate", SqlDbType = SqlDbType.Date, Value = lEndDate.AddDays(1) }, //010
               new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = lBranchID }
            };

            var mdlCustImageReportList = new List<Model.mdlCustomerImageReport>();

            //001
            DataTable dtCustomerImageReport = Manager.DataFacade.DTSQLCommand(@"SELECT    d.BranchID,
                                                                                 e.BranchName,
                                                                                 d.EmployeeID,
                                                                                 f.EmployeeName,
                                                                                 d.VehicleID,
                                                                                 a.CustomerID,
                                                                                 b.CustomerName,
                                                                                 a.WarehouseID,
                                                                                 c.WarehouseName,
                                                                                 a.DocNumber,
                                                                                 a.VisitID,
                                                                                 a.ImageDate,
                                                                                 a.ImageBase64,
                                                                                 a.ImagePath,
									                                             a.ImageType,
                                                                                 g.CostName
		                                                                 FROM CustomerImage a 
											                                LEFT JOIN Customer b ON a.CustomerID = b.CustomerID
                                                                            LEFT JOIN Warehouse c ON a.WarehouseID = c.WarehouseID
			                                                                  INNER JOIN Visit d ON a.VisitID = d.VisitID
			                                                                  INNER JOIN Branch e ON d.BranchID = e.BranchID
			                                                                  INNER JOIN Employee f ON d.EmployeeID = f.EmployeeID
                                                                              LEFT JOIN Cost g on g.CostID = a.ImageType
                                                                            WHERE (d.BranchID = @BranchID) AND (a.ImageDate BETWEEN @StartDate and @EndDate) AND" + lParam + lParam2 + "Order by a.ImageDate", sp);

            foreach (DataRow row in dtCustomerImageReport.Rows)
            {
                var mdlCustomerImageReport = new Model.mdlCustomerImageReport();
                mdlCustomerImageReport.BranchID = row["BranchID"].ToString();
                mdlCustomerImageReport.BranchName = row["BranchName"].ToString();
                mdlCustomerImageReport.EmployeeID = row["EmployeeID"].ToString();
                mdlCustomerImageReport.EmployeeName = row["EmployeeName"].ToString();
                mdlCustomerImageReport.VehicleID = row["VehicleID"].ToString();
                mdlCustomerImageReport.DocNumber = row["DocNumber"].ToString();
                mdlCustomerImageReport.VisitID = row["VisitID"].ToString();

                mdlCustomerImageReport.CustomerID = row["CustomerID"].ToString();
                mdlCustomerImageReport.CustomerName = row["CustomerName"].ToString();
                mdlCustomerImageReport.WarehouseID = row["WarehouseID"].ToString();
                mdlCustomerImageReport.WarehouseName = row["WarehouseName"].ToString();

                if (mdlCustomerImageReport.CustomerID == mdlCustomerImageReport.WarehouseID)
                {
                    mdlCustomerImageReport.CustomerID.ToString();
                }
                else
                {
                    mdlCustomerImageReport.CustomerID = mdlCustomerImageReport.CustomerID + " - " + mdlCustomerImageReport.WarehouseID;
                }

                mdlCustomerImageReport.ImageDate = Convert.ToDateTime(row["ImageDate"]).ToString("dd-MM-yyyy HH:mm:ss");
                mdlCustomerImageReport.ImageBase64 = row["ImageBase64"].ToString();
                mdlCustomerImageReport.ImagePath = row["ImagePath"].ToString();
                if (row["CostName"].ToString() != "")
                    mdlCustomerImageReport.ImageType = row["CostName"].ToString();
                else
                    mdlCustomerImageReport.ImageType = row["ImageType"].ToString();

                mdlCustImageReportList.Add(mdlCustomerImageReport);
            }
            return mdlCustImageReportList;
        }

        public static bool CheckbyCustomerImageID(string lImageID)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName = "@ImageID", SqlDbType = SqlDbType.NVarChar, Value = lImageID },
            };

            DataTable dtVisit = Manager.DataFacade.DTSQLCommand(@"SELECT TOP 1 ImageID 
                                                                   FROM CustomerImage 
                                                                   WHERE ImageID = @ImageID", sp);
            bool lCheck = false;
            if (dtVisit.Rows.Count == 0)
            {
                lCheck = true;
            }

            return lCheck;
        }

        public static void DeleteCustomerImage(string imageID)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
            };



            Manager.DataFacade.DTSQLCommand(@"Delete from CustomerImage where ImageID = '" + imageID + "'", sp);


        }
    }
}
