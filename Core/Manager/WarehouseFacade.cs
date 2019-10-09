using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Core.Manager
{
    public class WarehouseFacade : Base.Manager
    {

        public static List<Model.Warehouse> GetSearch(string CustomerID, string keyword)
        {
            var warehouse = DataContext.Warehouses.Where(fld => fld.CustomerID.Equals(CustomerID)).Where(fld => fld.WarehouseID.Contains(keyword) || fld.WarehouseName.Contains(keyword) || fld.WarehouseAddress.Contains(keyword)).OrderBy(fld => fld.WarehouseName).ToList();
            return warehouse;
        }

        public static Model.Warehouse GetWarehouseDetail(string keyword, string customerid)
        {
            var warehouse = DataContext.Warehouses.FirstOrDefault(fld => fld.WarehouseID.Contains(keyword) && fld.CustomerID.Contains(customerid));
            return warehouse;
        }

        public static void UpdateLongLatWarehouse(string lWarehouseID, string lLongitude, string lLatitude, decimal lRadius, string lCustomerID)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@WarehouseID", SqlDbType = SqlDbType.NVarChar, Value = lWarehouseID },
                    new SqlParameter() {ParameterName = "@Longitude", SqlDbType = SqlDbType.NVarChar, Value = lLongitude },
                    new SqlParameter() {ParameterName = "@Latitude", SqlDbType = SqlDbType.NVarChar, Value = lLatitude },
                    new SqlParameter() {ParameterName = "@Radius", SqlDbType = SqlDbType.Decimal, Value = lRadius },
                    new SqlParameter() {ParameterName = "@CustomerID", SqlDbType = SqlDbType.NVarChar, Value = lCustomerID },
                };

            string query = @"UPDATE Warehouse SET 
                                            Longitude = @Longitude, 
                                            Latitude = @Latitude,
                                            Radius = @Radius
                                            WHERE WarehouseID = @WarehouseID and CustomerID = @CustomerID";

            Manager.DataFacade.DTSQLVoidCommand(query, sp);

            return;
        }

        public static List<Model.mdlWarehouse> LoadWarehouse(List<Model.mdlCallPlan> listParam)
        {

            var lmdlWarehouseList = new List<Model.mdlWarehouse>();

            List<SqlParameter> sp = new List<SqlParameter>();
            //List<SqlParameter> sp = new List<SqlParameter>()
            //    {
            //        new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = param.BranchID },
            //        new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = param.EmployeeID },
            //        new SqlParameter() {ParameterName = "@Date", SqlDbType = SqlDbType.DateTime, Value = DateTime.Now.Date }
            //    };

            string callplanID = "";
            foreach (var param in listParam)
            {
                callplanID = callplanID + param.CallPlanID + ",";

            }
            callplanID = callplanID.Substring(0, callplanID.Length - 1);


            //            DataTable dtWarehouse= Manager.DataFacade.DTSQLCommand(@"SELECT c.* FROM CallPlan a INNER JOIN CallPlanDetail b ON a.CallPlanID= b.CallPlanID
            //						   INNER JOIN Warehouse c ON b.WarehouseID = c.WarehouseID WHERE a.EmployeeID = @EmployeeID and a.BranchID = @BranchID and a.IsFinish = 0 and a.Date >= @Date", sp); //003

            DataTable dtWarehouse = Manager.DataFacade.DTSQLCommand(@"select distinct a.WarehouseID, a.warehousename, a.warehouseaddress,a.radius,a.CustomerID,a.Latitude,a.Longitude,a.Phone,a.Contact from Warehouse a
inner join CallPlanDetail b on a.CustomerID=b.CustomerID
inner join CallPlan c on b.callplanid=c.callplanid
where c.callplanid in ( '" + callplanID + "')", sp); //003

            foreach (DataRow drWarehouse in dtWarehouse.Rows)
            {
                var lmdlWarehouse = new Model.mdlWarehouse();
                lmdlWarehouse.WarehouseID = drWarehouse["WarehouseID"].ToString();
                lmdlWarehouse.WarehouseName = drWarehouse["WarehouseName"].ToString();
                lmdlWarehouse.WarehouseAddress = drWarehouse["WarehouseAddress"].ToString();
                lmdlWarehouse.Latitude = drWarehouse["Latitude"].ToString();
                lmdlWarehouse.Longitude = drWarehouse["Longitude"].ToString();
                lmdlWarehouse.CustomerID = drWarehouse["CustomerID"].ToString();
                lmdlWarehouse.Radius = drWarehouse["Radius"].ToString();
                lmdlWarehouse.Phone = drWarehouse["Phone"].ToString();
                lmdlWarehouse.Pic = drWarehouse["Contact"].ToString();

                lmdlWarehouseList.Add(lmdlWarehouse);
            }
            return lmdlWarehouseList;

        }

        public static List<Model.mdlWarehouse> GetWarehouseCoordinate(string lBranchID)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = lBranchID },
            };

            var mdlWarehouseList = new List<Model.mdlWarehouse>();

            DataTable dtWarehouse = Manager.DataFacade.DTSQLCommand(@"SELECT WarehouseID, WarehouseName, WarehouseAddress, Phone, Contact, Latitude, Longitude, CustomerID
                                                                        FROM Warehouse
                                                                        WHERE BranchID=@BranchID AND Latitude <> '' AND Latitude <> '0' AND Latitude IS NOT NULL
                                                                                                 AND Longitude <> '' AND Longitude <> '0' AND Longitude IS NOT NULL", sp);

            foreach (DataRow row in dtWarehouse.Rows)
            {
                var mdlWarehouse = new Model.mdlWarehouse();
                mdlWarehouse.WarehouseID = row["WarehouseID"].ToString();
                mdlWarehouse.WarehouseName = row["WarehouseName"].ToString();
                mdlWarehouse.CustomerID = row["CustomerID"].ToString();
                mdlWarehouse.WarehouseAddress = row["WarehouseAddress"].ToString();
                mdlWarehouse.Phone = row["Phone"].ToString();
                mdlWarehouse.Pic = row["Contact"].ToString();
                mdlWarehouse.Latitude = row["Latitude"].ToString();
                mdlWarehouse.Longitude = row["Longitude"].ToString();

                mdlWarehouseList.Add(mdlWarehouse);
            }
            return mdlWarehouseList;
        }

    }
}
