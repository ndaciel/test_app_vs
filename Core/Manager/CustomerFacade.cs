/* documentation
 *001 - 14 Okt'16 - fernandes
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Core.Manager
{
    public class CustomerFacade : Base.Manager
    {

        public static string CreateNewCustomerID()
        {
            List<SqlParameter> sp = new List<SqlParameter>() {
               
            };

            string query = "select top 1 CustomerID from Customer order by CustomerID desc";

            DataTable dt = DataFacade.DTSQLCommand(query, sp);

            string lastID = "";
            foreach (DataRow row in dt.Rows)
            {
                lastID = row["CustomerID"].ToString();
            }

            string[] stringArr = lastID.Split('-');

            int part3 = Convert.ToInt32(stringArr[2]);
            string newpart =  (part3 + 1).ToString();


            return stringArr[0] + "-" + stringArr[1] + "-" + newpart;



        }

        public static string AddNewCustomer(string customerName,string address,string customerType,string city,string countryReg,string account,string distributor,string employeeID)
        {
            List<SqlParameter> sp = new List<SqlParameter>() {
                new SqlParameter() {ParameterName = "@CustomerID", SqlDbType = SqlDbType.VarChar, Value = CreateNewCustomerID() },
                new SqlParameter() {ParameterName = "@CustomerName", SqlDbType = SqlDbType.VarChar, Value = customerName },
                new SqlParameter() {ParameterName = "@CustomerAddress", SqlDbType = SqlDbType.VarChar, Value = address },
                new SqlParameter() {ParameterName = "@Phone", SqlDbType = SqlDbType.VarChar, Value = "" },
                new SqlParameter() {ParameterName = "@Email", SqlDbType = SqlDbType.VarChar, Value = "" },
                
                new SqlParameter() {ParameterName = "@PIC", SqlDbType = SqlDbType.VarChar, Value = "" },
                new SqlParameter() {ParameterName = "@CustomerTypeID", SqlDbType = SqlDbType.VarChar, Value = customerType },
                new SqlParameter() {ParameterName = "@Latitude", SqlDbType = SqlDbType.VarChar, Value = "" },
                new SqlParameter() {ParameterName = "@Longitude", SqlDbType = SqlDbType.VarChar, Value = "" },
                new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.VarChar, Value = city },
                new SqlParameter() {ParameterName = "@Radius", SqlDbType = SqlDbType.Decimal, Value = 0 },
                new SqlParameter() {ParameterName = "@City", SqlDbType = SqlDbType.VarChar, Value = city },
                new SqlParameter() {ParameterName = "@CountryRegionCode", SqlDbType = SqlDbType.VarChar, Value = countryReg },
                new SqlParameter() {ParameterName = "@Blocked", SqlDbType = SqlDbType.Bit, Value = false },
                new SqlParameter() {ParameterName = "@Account", SqlDbType = SqlDbType.VarChar, Value = account },
                new SqlParameter() {ParameterName = "@Distributor", SqlDbType = SqlDbType.VarChar, Value = distributor },
                new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.VarChar, Value = employeeID }
            
            };

            string query = @"INSERT INTO [dbo].[Customer]
           ([CustomerID]
           ,[CustomerName]
           ,[CustomerAddress]
           ,[Phone]
           ,[Email]
           ,[PIC]
           ,[CustomerTypeID]
           ,[Latitude]
           ,[Longitude]
           ,[BranchID]
           ,[Radius]
           ,[City]
           ,[CountryRegionCode]
           ,[Blocked]
           ,[Account]
           ,[Distributor]
           ,[EmployeeID])
     VALUES
           (@CustomerID,@CustomerName,@CustomerAddress,@Phone,@Email,@PIC,@CustomerTypeID,@Latitude,@Longitude,@BranchID,@Radius,@City,@CountryRegionCode,@Blocked,@Account,@Distributor,@EmployeeID)";



           return DataFacade.DTSQLVoidCommand(query, sp);


        }

        public static List<Model.CustomerType> LoadCustomerType()
        {
            return DataContext.CustomerTypes.ToList();
        }

        public static List<Model.Account> LoadAccount()
        {
            return DataContext.Accounts.ToList();
        }

        public static List<Model.Distributor> LoadDistributor()
        {
            return DataContext.Distributors.ToList();
        }

        public static List<Model.mdlCustomerForAddNew> LoadCustomersForAddnew(int pageindex, int pagesize)
        {
            List<SqlParameter> sp = new List<SqlParameter>() {
                new SqlParameter() {ParameterName = "@index", SqlDbType = SqlDbType.Int, Value = pageindex },
                new SqlParameter() {ParameterName = "@size", SqlDbType = SqlDbType.Int, Value = pagesize }
            
            };
            var mdlCustomerList = new List<Model.mdlCustomerForAddNew>();

            string query = @"select (select TOP 1 RowNumber from  (select ROW_NUMBER() OVER( Order By CustomerID ASC) as RowNumber,* from Customer) a ORDER BY RowNumber DESC) as total,* from (select ROW_NUMBER() OVER( Order By CustomerID ASC) as RowNumber,* from Customer) a WHERE RowNumber BETWEEN ((@index-1)*@size)+1 AND (@index)*@size;";

            DataTable dtCustomer = Manager.DataFacade.DTSQLCommand(query,sp);

            foreach (DataRow row in dtCustomer.Rows)
            {
                var model = new Model.mdlCustomerForAddNew();
                model.RowNumber = row["RowNumber"].ToString();
                model.Total = row["total"].ToString();
                model.CustomerID = row["CustomerID"].ToString();
                model.CustomerName = row["CustomerName"].ToString();
                model.CustomerAddress = row["CustomerAddress"].ToString();
                model.CustomerTypeID = row["CustomerTypeID"].ToString();
                model.BranchID = row["BranchID"].ToString();
                model.City = row["City"].ToString();
                model.CountryRegionCode = row["CountryRegionCode"].ToString();
                model.Account = row["Account"].ToString();
                model.Distributor = row["Distributor"].ToString();
                model.EmployeeID = row["EmployeeID"].ToString();

                mdlCustomerList.Add(model);
                
            }

            return mdlCustomerList;

        }


        public static List<Model.mdlCustomer> LoadCustomer(Model.mdlParam json)
        {

            var mdlCustomerList = new List<Model.mdlCustomer>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@Date", SqlDbType = SqlDbType.DateTime, Value = Convert.ToDateTime(json.Date).Date },
                new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = json.EmployeeID },
                new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = json.BranchID }
            };

            DataTable dtCustomer = Manager.DataFacade.DTSQLCommand(@"SELECT * FROM Customer", sp);
            foreach (DataRow drCustomer in dtCustomer.Rows)
            {
                var mdlCustomer = new Model.mdlCustomer();
                mdlCustomer.CustomerID = drCustomer["CustomerID"].ToString();
                mdlCustomer.CustomerName = drCustomer["CustomerName"].ToString();
                mdlCustomer.CustomerAddress = drCustomer["CustomerAddress"].ToString();
                mdlCustomer.Phone = drCustomer["Phone"].ToString();
                mdlCustomer.Email = drCustomer["Email"].ToString();
                mdlCustomer.PIC = drCustomer["PIC"].ToString();
                mdlCustomer.CustomerTypeID = drCustomer["CustomerTypeID"].ToString();
                mdlCustomer.Latitude = drCustomer["Latitude"].ToString();
                mdlCustomer.Longitude = drCustomer["Longitude"].ToString();
                mdlCustomer.BranchID = drCustomer["BranchID"].ToString();
                mdlCustomer.Radius = drCustomer["Radius"].ToString();
                mdlCustomer.City = drCustomer["City"].ToString();
                mdlCustomer.CountryRegionCode = drCustomer["CountryRegionCode"].ToString();
                //mdlCustomer.Blocked = drCustomer["Blocked"].ToString();
                mdlCustomer.Account = drCustomer["Account"].ToString();
                mdlCustomer.Channel = drCustomer["Channel"].ToString();
                mdlCustomer.Distributor = drCustomer["Distributor"].ToString();
                mdlCustomer.EmployeeID = drCustomer["EmployeeID"].ToString();
                mdlCustomer.Mobilephone1 = drCustomer["Mobilephone1"].ToString();
                mdlCustomer.Mobilephone2 = drCustomer["Mobilephone2"].ToString();
                mdlCustomer.BankAccountNumber = drCustomer["BankAccountNumber"].ToString();
                mdlCustomerList.Add(mdlCustomer);
            }
            return mdlCustomerList;
        }

        public static List<Model.Customer> GetSearch(string keyword,string branchid)
        {
            var customer = DataContext.Customers.Where(fld => (fld.CustomerID.Contains(keyword) || fld.CustomerName.Contains(keyword) || fld.CustomerAddress.Contains(keyword)) && fld.BranchID.Contains(branchid)).OrderBy(fld => fld.CustomerName).ToList();
            return customer;
        }

        public static List<Model.mdlCustomer> GetSearchCustomerByBranch(string keyword, string branchid)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
            };

            var mdlCustList = new List<Model.mdlCustomer>();

            DataTable dtCust = Manager.DataFacade.DTSQLCommand(@"SELECT   CustomerID, 
			                                                                        CustomerName,
                                                                                    CustomerAddress, BranchID
                                                                                    from Customer
				                                                            Where BranchID IN (" + branchid + ") and (CustomerID LIKE '%" + keyword + "%' or CustomerName LIKE '%"+keyword+"%')", sp);

            foreach (DataRow row in dtCust.Rows)
            {
                var mdlCust = new Model.mdlCustomer();
                mdlCust.CustomerID = row["CustomerID"].ToString();
                mdlCust.CustomerName = row["CustomerName"].ToString();
                mdlCust.CustomerAddress = row["CustomerAddress"].ToString();
                mdlCust.BranchID = row["BranchID"].ToString();

                mdlCustList.Add(mdlCust);
            }
            return mdlCustList;
        }

        //untuk SettingKoordinat.aspx
        public static List<Model.Customer> GetSearch2(string ddlcabang, string keyword)
        {
            var customer = DataContext.Customers.Where(fld => fld.BranchID.Equals(ddlcabang)).Where(fld => fld.CustomerID.ToLower().Contains(keyword.ToLower()) || fld.CustomerName.ToLower().Contains(keyword.ToLower()) || fld.CustomerAddress.ToLower().Contains(keyword.ToLower())).OrderBy(fld => fld.CustomerName).ToList();
            return customer;
        }

        public static Model.Customer GetCustomerDetail(string keyword)
        {
            var customer = DataContext.Customers.FirstOrDefault(fld => fld.CustomerID.Contains(keyword));
            return customer;
        }

        //001
        public static void UpdateLongLatCustomer(string lCustomerID, string lLongitude, string lLatitude, decimal lRadius)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@CustomerID", SqlDbType = SqlDbType.NVarChar, Value = lCustomerID },
                    new SqlParameter() {ParameterName = "@Longitude", SqlDbType = SqlDbType.NVarChar, Value = lLongitude },
                    new SqlParameter() {ParameterName = "@Latitude", SqlDbType = SqlDbType.NVarChar, Value = lLatitude },
                    new SqlParameter() {ParameterName = "@Radius", SqlDbType = SqlDbType.Decimal, Value = lRadius }
                };

            string query = @"UPDATE Customer SET 
                                            Longitude = @Longitude, 
                                            Latitude = @Latitude,
                                            Radius = @Radius
                                            WHERE CustomerID = @CustomerID";

            Manager.DataFacade.DTSQLVoidCommand(query, sp);

            return;
        }

        public static List<Model.mdlCustomer> GetCustomerCoordinate(string lBranchID)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = lBranchID },
            };

            var mdlCustomerList = new List<Model.mdlCustomer>();

            DataTable dtCustomer = Manager.DataFacade.DTSQLCommand(@"SELECT CustomerID, CustomerName, CustomerAddress, Phone, PIC, Latitude, Longitude
                                                                        FROM Customer
                                                                        WHERE BranchID=@BranchID AND Latitude <> '' AND Latitude <> '0' AND Latitude IS NOT NULL
                                                                                                 AND Longitude <> '' AND Longitude <> '0' AND Longitude IS NOT NULL", sp);

            foreach (DataRow row in dtCustomer.Rows)
            {
                var mdlCustomer = new Model.mdlCustomer();
                mdlCustomer.CustomerID = row["CustomerID"].ToString();
                mdlCustomer.CustomerName = row["CustomerName"].ToString();
                mdlCustomer.CustomerAddress = row["CustomerAddress"].ToString();
                mdlCustomer.Phone = row["Phone"].ToString();
                mdlCustomer.PIC = row["PIC"].ToString();
                mdlCustomer.Latitude = row["Latitude"].ToString();
                mdlCustomer.Longitude = row["Longitude"].ToString();

                mdlCustomerList.Add(mdlCustomer);
            }
            return mdlCustomerList;
        }

        public static List<Model.mdlCustomer2> LoadCustomernWarehouse(string customerID)
        {

            var mdlCustomerList = new List<Model.mdlCustomer2>();

            List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@CustomerID", SqlDbType = SqlDbType.NVarChar, Value = customerID},
                };

            DataTable dtCustomer = Manager.DataFacade.DTSQLCommand(@"select a.customerid,a.customername from customer a where a.customerid=@CustomerID
                                                                    union all
                                                                    select b.warehouseid,b.warehousename from Warehouse b where b.customerid=@CustomerID", sp);

            foreach (DataRow drCustomer in dtCustomer.Rows)
            {
                var mdlCustomer = new Model.mdlCustomer2();
                mdlCustomer.CustomerID = drCustomer["CustomerID"].ToString();
                mdlCustomer.CustomerName = drCustomer["CustomerName"] + " - " + drCustomer["CustomerID"].ToString();

                mdlCustomerList.Add(mdlCustomer);
            }
            return mdlCustomerList;
        }

        public static List<Model.mdlCustomer2> LoadCustomernWarehouseAddr(string ID, string customerid)
        {

            var mdlCustomerList = new List<Model.mdlCustomer2>();

            List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@ID", SqlDbType = SqlDbType.NVarChar, Value = ID},
                    new SqlParameter() {ParameterName = "@CustomerID", SqlDbType = SqlDbType.NVarChar, Value = customerid},
                };

            DataTable dtCustomer = Manager.DataFacade.DTSQLCommand(@"select a.CustomerAddress, 'customer' as CustomerType from customer a where a.customerid=@ID
                                                                    union all
                                                                    select b.WarehouseAddress, 'warehouse' as CustomerType from Warehouse b where b.warehouseid=@ID and b.CustomerID=@CustomerID", sp);

            foreach (DataRow drCustomer in dtCustomer.Rows)
            {
                var mdlCustomer = new Model.mdlCustomer2();
                mdlCustomer.CustomerAddress = drCustomer["CustomerAddress"].ToString();
                mdlCustomer.CustomerType = drCustomer["CustomerType"].ToString();

                mdlCustomerList.Add(mdlCustomer);
            }
            return mdlCustomerList;
        }

        //FERNANDES
        public static Model.mdlCustomerLicense LoadCustomerLicenseKey(string customerid)
        {

            var mdlCustomerLicense = new Model.mdlCustomerLicense();

            List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@CustomerID", SqlDbType = SqlDbType.NVarChar, Value = customerid},
                };

            DataTable dtCustomer = Manager.DataFacade.DTSQLCommand(@"SELECT LicenseKey FROM CustomerLicense WHERE CustomerID=@CustomerID", sp);

            foreach (DataRow drCustomer in dtCustomer.Rows)
            {
                mdlCustomerLicense.LicenseKey = drCustomer["LicenseKey"].ToString();
            }

            return mdlCustomerLicense;
        }

        //FERNANDES
        public static List<Model.mdlCustomer> GetCustomerorWarehouseCoordinate(List<Model.mdlVisitTracking> lParamlist)
        {
            var mdlCustomerList = new List<Model.mdlCustomer>();

            foreach (var lParam in lParamlist)
            {
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                   new SqlParameter() {ParameterName = "@CustomerID", SqlDbType = SqlDbType.NVarChar, Value = lParam.CustomerID},
                   new SqlParameter() {ParameterName = "@WarehouseID", SqlDbType = SqlDbType.NVarChar, Value = lParam.WarehouseID}
                };

                if (lParam.CustomerID == lParam.WarehouseID)
                {
                    DataTable dtCustomer = Manager.DataFacade.DTSQLCommand(@"SELECT CustomerID, CustomerName, Latitude, Longitude,CustomerAddress
                                                                            FROM Customer
                                                                            WHERE CustomerID=@CustomerID AND Latitude <> '' AND Latitude <> '0' AND Latitude IS NOT NULL
                                                                                                     AND Longitude <> '' AND Longitude <> '0' AND Longitude IS NOT NULL", sp);

                    foreach (DataRow row in dtCustomer.Rows)
                    {
                        var mdlCustomer = new Model.mdlCustomer();
                        mdlCustomer.CustomerID = row["CustomerID"].ToString();
                        mdlCustomer.CustomerName = row["CustomerName"].ToString();
                        mdlCustomer.CustomerAddress = row["CustomerAddress"].ToString();
                        mdlCustomer.Latitude = row["Latitude"].ToString();
                        mdlCustomer.Longitude = row["Longitude"].ToString();

                        mdlCustomerList.Add(mdlCustomer);
                    }
                }
                else
                {
                    DataTable dtCustomer = Manager.DataFacade.DTSQLCommand(@"SELECT WarehouseID, WarehouseName, Latitude, Longitude, WarehouseAddress
                                                                            FROM Warehouse
                                                                            WHERE CustomerID=@CustomerID AND WarehouseID=@WarehouseID AND Latitude <> '' AND Latitude <> '0' AND Latitude IS NOT NULL
                                                                                                     AND Longitude <> '' AND Longitude <> '0' AND Longitude IS NOT NULL", sp);

                    foreach (DataRow row in dtCustomer.Rows)
                    {
                        var mdlCustomer = new Model.mdlCustomer();
                        mdlCustomer.CustomerID = row["WarehouseID"].ToString();
                        mdlCustomer.CustomerName = row["WarehouseName"].ToString();
                        mdlCustomer.CustomerAddress = row["WarehouseAddress"].ToString();
                        mdlCustomer.Latitude = row["Latitude"].ToString();
                        mdlCustomer.Longitude = row["Longitude"].ToString();

                        mdlCustomerList.Add(mdlCustomer);
                    }
                }
                
            }

            return mdlCustomerList;
       }

    }
}
