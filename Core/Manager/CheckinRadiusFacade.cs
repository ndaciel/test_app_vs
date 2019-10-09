using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Device.Location;
using System.Configuration;
using System.Globalization;

namespace Core.Manager
{
    public class CheckinRadiusFacade
    {
        public static Model.mdlCheckinCourierRadius CheckinCourierRadius(Core.Model.mdlCheckinCourierRadiusParam param)
        {
            //var getWarehouseCoordinate = GetWarehouseCoordinate(param.CustomerID, param.WarehouseID);

            var listAllCoordinate = GetCustomerCoordinateAll(param.CustomerID);
            //var getMobileConfig = GetMobileConfig(param.BranchID);

            var mdlResult = new Model.mdlCheckinCourierRadius();
            foreach (var coodinate in listAllCoordinate)
            {
                var SCoor = new GeoCoordinate(double.Parse(param.Latitude, CultureInfo.InvariantCulture), double.Parse(param.Longitude, CultureInfo.InvariantCulture));

                var FCoor = new GeoCoordinate(double.Parse(coodinate.Latitude, CultureInfo.InvariantCulture), double.Parse(coodinate.Longitude, CultureInfo.InvariantCulture));
                double distance = RadiusFacade.getDistance(SCoor, FCoor);
                string email = GetEmail(param.CustomerID);

                if (distance <= (double.Parse(param.Radius, CultureInfo.InvariantCulture) + double.Parse(coodinate.Radius, CultureInfo.InvariantCulture)))
                {
                    //string res = UpdateVisitDetailRange(param.VisitID, param.CustomerID, distance, 1);
                    mdlResult.InRange = "1";
                    mdlResult.Distance = distance.ToString();
                    return mdlResult;
                }
                else
                {
                    //string res = UpdateVisitDetailRange(param.VisitID, param.CustomerID, distance, 0);
                    mdlResult.InRange = "0";
                    mdlResult.Distance = distance.ToString();
                    //EmailFacade.SendEmail(email);
                    //EmailFacade.SendEmail(ConfigurationSettings.AppSettings["Email"]);
                }
            }
            //double distance = RadiusFacade.distance_calculate( Convert.ToDouble(param.Latitude),Convert.ToDouble(param.Longitude),Convert.ToDouble(getWarehouseCoordinate.Latitude),Convert.ToDouble(getWarehouseCoordinate.Longitude),'K')*1000;
           




            return mdlResult;






        }

        public static Model.mdlGetWarehouseCoordinate GetWarehouseCoordinate(string customerID, string warehouseID)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                 new SqlParameter() {ParameterName = "@customerID", SqlDbType = SqlDbType.NVarChar, Value = '%' + customerID + '%'},
                 new SqlParameter() {ParameterName = "@warehouseID", SqlDbType = SqlDbType.NVarChar, Value = '%' + warehouseID + '%'}
            };
            string query="";
            if(warehouseID!=""||warehouseID!=null)
                query ="SELECT WarehouseID,CustomerID,Latitude,Longitude,Radius FROM Warehouse where CustomerID LIKE @customerID AND WarehouseID LIKE @warehouseID";
            else
                query = "SELECT '' as WarehouseID,CustomerID,Latitude,Longitude,Radius FROM Customer where CustomerID LIKE @customerID";

            DataTable dtWarehouse = Manager.DataFacade.DTSQLCommand(query, sp);
            
            var mdlWarehouse = new Model.mdlGetWarehouseCoordinate();
            foreach (DataRow row in dtWarehouse.Rows)
            {

                mdlWarehouse.WarehouseID = row["WarehouseID"].ToString();
                mdlWarehouse.CustomerID = row["CustomerID"].ToString();
                mdlWarehouse.Latitude = row["Latitude"].ToString();
                mdlWarehouse.Longitude = row["Longitude"].ToString();
                mdlWarehouse.Radius = row["Radius"].ToString();

            }

            return mdlWarehouse;

        }

        public static List<Model.mdlGetWarehouseCoordinate> GetCustomerCoordinateAll(string customerID)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                 new SqlParameter() {ParameterName = "@customerID", SqlDbType = SqlDbType.NVarChar, Value =  customerID}
                 
            };
            string query = "";
          
            query = "SELECT WarehouseID,CustomerID,Latitude,Longitude,Radius FROM Warehouse where CustomerID = @customerID";
            

            DataTable dtWarehouse = Manager.DataFacade.DTSQLCommand(query, sp);

            var listCoordinate = new List<Model.mdlGetWarehouseCoordinate>();
            foreach (DataRow row in dtWarehouse.Rows)
            {
                if (row["Latitude"].ToString() != "" && row["Longitude"].ToString() != "")
                {
                    var mdlWarehouse = new Model.mdlGetWarehouseCoordinate();
                    mdlWarehouse.WarehouseID = row["WarehouseID"].ToString();
                    mdlWarehouse.CustomerID = row["CustomerID"].ToString();
                    mdlWarehouse.Latitude = row["Latitude"].ToString();
                    mdlWarehouse.Longitude = row["Longitude"].ToString();
                    mdlWarehouse.Radius = row["Radius"].ToString();
                    listCoordinate.Add(mdlWarehouse);
                }
            }



            query = "SELECT '' as WarehouseID,CustomerID,Latitude,Longitude,Radius FROM Customer where CustomerID=@customerID";

            DataTable dtCustomer = Manager.DataFacade.DTSQLCommand(query, sp);

            foreach (DataRow row in dtCustomer.Rows)
            {
                var mdlWarehouse = new Model.mdlGetWarehouseCoordinate();
                mdlWarehouse.WarehouseID = row["WarehouseID"].ToString();
                mdlWarehouse.CustomerID = row["CustomerID"].ToString();
                mdlWarehouse.Latitude = row["Latitude"].ToString();
                mdlWarehouse.Longitude = row["Longitude"].ToString();
                mdlWarehouse.Radius = row["Radius"].ToString();
                listCoordinate.Add(mdlWarehouse);
            }


            return listCoordinate;

        }


        public static string GetEmail(string customerID)
        {
            string email = string.Empty;
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                 new SqlParameter() {ParameterName = "@customerID", SqlDbType = SqlDbType.NVarChar, Value = '%' + customerID + '%'}

            };

            DataTable dtWarehouse = Manager.DataFacade.DTSQLCommand("SELECT Email FROM Customer where CustomerID LIKE @customerID", sp);

            var mdlWarehouse = new Model.mdlGetWarehouseCoordinate();
            foreach (DataRow row in dtWarehouse.Rows)
            {

                email = row["Email"].ToString();
            }

            return email;

        }

        public static Model.mdlMobileConfig GetMobileConfig(string branchID)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                 new SqlParameter() {ParameterName = "@branchID", SqlDbType = SqlDbType.NVarChar, Value = '%' + branchID + '%'},
                 
            };

            DataTable dtMobileConfig = Manager.DataFacade.DTSQLCommand("SELECT ID,Value FROM Warehouse where BranchId LIKE @branchID AND ID = 'RADIUS'", sp);

            var mdlMobileConfig = new Model.mdlMobileConfig();
            foreach (DataRow row in dtMobileConfig.Rows)
            {
                mdlMobileConfig.ID = row["ID"].ToString();
                mdlMobileConfig.Value = row["Value"].ToString();
            }

            return mdlMobileConfig;

        }


        public static string UpdateVisitDetailRange(string visitID, string customerID, double distance, int isInRange)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                 new SqlParameter() {ParameterName = "@customerID", SqlDbType = SqlDbType.NVarChar, Value = customerID },
                 new SqlParameter() {ParameterName = "@visitID", SqlDbType = SqlDbType.NVarChar, Value =  visitID },
                 new SqlParameter() {ParameterName = "@distance", SqlDbType = SqlDbType.Decimal, Value =  distance },
                  new SqlParameter() {ParameterName = "@isInRange", SqlDbType = SqlDbType.Bit, Value = isInRange  }
            };

            string res = Manager.DataFacade.DTSQLVoidCommand("UPDATE VisitDetail SET isInRange = @isInRange, Distance = @distance WHERE VisitID = @visitID AND CustomerID = @customerID", sp);



            return res;

        }


    }
}
