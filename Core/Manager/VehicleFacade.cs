using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Core.Manager
{
    public class VehicleFacade : Base.Manager
    {
        public static Model.mdlResult InsertVehicle(Model.mdlVehicleParam lParam)
        {
           
            var mdlResult = new Model.mdlResult();
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@VehicleID", SqlDbType = SqlDbType.NVarChar, Value = lParam.VehicleID },
                new SqlParameter() {ParameterName = "@VehicleName", SqlDbType = SqlDbType.NVarChar, Value = lParam.VehicleName },
                new SqlParameter() {ParameterName = "@VehicleNumber", SqlDbType = SqlDbType.NVarChar, Value = lParam.VehicleNumber },
                new SqlParameter() {ParameterName = "@STNKDate", SqlDbType = SqlDbType.NVarChar, Value = lParam.STNKDate },
                new SqlParameter() {ParameterName = "@YearManufacturing", SqlDbType = SqlDbType.NVarChar, Value = lParam.YearManufacturing },
                new SqlParameter() {ParameterName = "@KIRNumber", SqlDbType = SqlDbType.NVarChar, Value = lParam.KIRNumber },
                new SqlParameter() {ParameterName = "@EngineNumber", SqlDbType = SqlDbType.NVarChar, Value = lParam.EngineNumber },
                new SqlParameter() {ParameterName = "@VehicleTypeID", SqlDbType = SqlDbType.NVarChar, Value = lParam.VehicleTypeID },
                new SqlParameter() {ParameterName = "@SubVehicleTypeID", SqlDbType = SqlDbType.NVarChar, Value = lParam.SubVehicleTypeID },
                new SqlParameter() {ParameterName = "@PlantID", SqlDbType = SqlDbType.NVarChar, Value = lParam.PlantID },
                new SqlParameter() {ParameterName = "@CapacityVolume", SqlDbType = SqlDbType.NVarChar, Value = lParam.CapacityVolume },
                new SqlParameter() {ParameterName = "@CapacityWeight", SqlDbType = SqlDbType.NVarChar, Value = lParam.CapacityWeight },
                new SqlParameter() {ParameterName = "@Brand", SqlDbType = SqlDbType.NVarChar, Value = lParam.Brand }
            };

            string query = "INSERT INTO Vehicle (VehicleID,VehicleName,VehicleNumber,STNKDate,YearManufacturing,KIRNumber,EngineNumber,VehicleTypeID,SubVehicleTypeID,PlantID,CapacityVolume,CapacityWeight,Brand) " +
                                             "VALUES (@VehicleID,@VehicleName,@VehicleNumber,@STNKDate,@YearManufacturing,@KIRNumber,@EngineNumber,@VehicleTypeID,@SubVehicleTypeID,@PlantID,@CapacityVolume,@CapacityWeight,@Brand) ";
            mdlResult.Result = "|| " + "Insert Vehicle " + lParam.VehicleID + " || " + Manager.DataFacade.DTSQLVoidCommand(query, sp);

            return mdlResult;
        }

        public static Model.mdlVehicleList LoadVehicle()
        {
            //var customerList = DataContext.Customers.Where(fld => fld.PlantID.Equals(json.BranchID)).ToList();
            var mdlVehicleListnew = new Model.mdlVehicleList();
            var mdlVehicleList = new List<Model.mdlVehicle>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
            };

            DataTable dtVehicle = Manager.DataFacade.DTSQLCommand("SELECT * FROM Vehicle", sp);

            foreach (DataRow drVehicle in dtVehicle.Rows)
            {
                var mdlVehicle = new Model.mdlVehicle();

                mdlVehicle.VehicleID = drVehicle["VehicleID"].ToString();
                mdlVehicle.VehicleName = drVehicle["VehicleName"].ToString();
                mdlVehicle.VehicleNumber = drVehicle["VehicleNumber"].ToString();
                mdlVehicle.STNKDate = drVehicle["STNKDate"].ToString();
                mdlVehicle.YearManufacturing = drVehicle["YearManufacturing"].ToString();
                mdlVehicle.KIRNumber = drVehicle["KIRNumber"].ToString();
                mdlVehicle.EngineNumber = drVehicle["EngineNumber"].ToString();
                mdlVehicle.VehicleTypeID = drVehicle["VehicleTypeID"].ToString();
                mdlVehicle.SubVehicleTypeID = drVehicle["SubVehicleTypeID"].ToString();
                mdlVehicle.PlantID = drVehicle["PlantID"].ToString();
                mdlVehicle.CapacityVolume = drVehicle["CapacityVolume"].ToString();
                mdlVehicle.CapacityWeight = drVehicle["CapacityWeight"].ToString();
                mdlVehicle.Brand = drVehicle["Brand"].ToString();

                mdlVehicleList.Add(mdlVehicle);
            }

            mdlVehicleListnew.VehicleList = mdlVehicleList;
            return mdlVehicleListnew;

        }

        public static Model.mdlResult UpdateVehicle(Model.mdlVehicleParam lParam)
        {
            var mdlResult = new Model.mdlResult();
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@VehicleID", SqlDbType = SqlDbType.NVarChar, Value = lParam.VehicleID },
                new SqlParameter() {ParameterName = "@VehicleName", SqlDbType = SqlDbType.NVarChar, Value = lParam.VehicleName },
                new SqlParameter() {ParameterName = "@VehicleNumber", SqlDbType = SqlDbType.NVarChar, Value = lParam.VehicleNumber },
                new SqlParameter() {ParameterName = "@STNKDate", SqlDbType = SqlDbType.NVarChar, Value = lParam.STNKDate },
                new SqlParameter() {ParameterName = "@YearManufacturing", SqlDbType = SqlDbType.NVarChar, Value = lParam.YearManufacturing },
                new SqlParameter() {ParameterName = "@KIRNumber", SqlDbType = SqlDbType.NVarChar, Value = lParam.KIRNumber },
                new SqlParameter() {ParameterName = "@EngineNumber", SqlDbType = SqlDbType.NVarChar, Value = lParam.EngineNumber },
                new SqlParameter() {ParameterName = "@VehicleTypeID", SqlDbType = SqlDbType.NVarChar, Value = lParam.VehicleTypeID },
                new SqlParameter() {ParameterName = "@SubVehicleTypeID", SqlDbType = SqlDbType.NVarChar, Value = lParam.SubVehicleTypeID },
                new SqlParameter() {ParameterName = "@PlantID", SqlDbType = SqlDbType.NVarChar, Value = lParam.PlantID },
                new SqlParameter() {ParameterName = "@CapacityVolume", SqlDbType = SqlDbType.NVarChar, Value = lParam.CapacityVolume },
                new SqlParameter() {ParameterName = "@CapacityWeight", SqlDbType = SqlDbType.NVarChar, Value = lParam.CapacityWeight },
                new SqlParameter() {ParameterName = "@Brand", SqlDbType = SqlDbType.NVarChar, Value = lParam.Brand }
            };

            string query = "UPDATE Vehicle SET VehicleName = @VehicleName, VehicleNumber = @VehicleNumber, STNKDate = @STNKDate, YearManufacturing = @YearManufacturing, KIRNumber = @KIRNumber, EngineNumber = @EngineNumber, VehicleTypeID = @VehicleTypeID, SubVehicleTypeID = @SubVehicleTypeID, PlantID = @PlantID, CapacityVolume = @CapacityVolume, CapacityWeight = @CapacityWeight, Brand = @Brand WHERE VehicleID = @VehicleID";
            mdlResult.Result = "|| " + "Update Vehicle " + lParam.VehicleID + " || " + Manager.DataFacade.DTSQLVoidCommand(query, sp);

            return mdlResult;
        }

        public static Model.mdlResult DeleteVehicle(Model.mdlVehicleParam lParam)
        {
            var mdlResult = new Model.mdlResult();
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@VehicleID", SqlDbType = SqlDbType.NVarChar, Value = lParam.VehicleID }
            };

            string query = "Delete From Vehicle WHERE VehicleID = @VehicleID";
            mdlResult.Result = "|| " + "Delete Vehicle " + lParam.VehicleID + " || " + Manager.DataFacade.DTSQLVoidCommand(query, sp);

            return mdlResult;
        }

        //FERNANDES
        public static List<string> AutoComplVehicleNumberUserConf(string prefixText, int count, string flagfilter)
        {
            List<string> lVehicleNumbers = new List<string>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
                 new SqlParameter() {ParameterName = "@SearchText", SqlDbType = SqlDbType.NVarChar, Value = "%" + prefixText + "%"},
            };

            DataTable dtVehicleNumber = Manager.DataFacade.DTSQLCommand("SELECT VehicleNumber FROM Vehicle where VehicleNumber like @SearchText", sp);
            foreach (DataRow drVehicleNumber in dtVehicleNumber.Rows)
            {
                lVehicleNumbers.Add(drVehicleNumber["VehicleNumber"].ToString());
            }

            return lVehicleNumbers;
        }

        //load by model param
        public static Model.mdlVehicleList LoadVehicleByBranch(Model.mdlVehicleBranchParam lParam)
        {
            var mdlVehicleListnew = new Model.mdlVehicleList();
            var mdlVehicleList = new List<Model.mdlVehicle>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = lParam.BranchID},
            };

            DataTable dtVehicle = Manager.DataFacade.DTSQLCommand("select vehicleid,VehicleNumber from Vehicle where VehicleNumber <> '' and SUBSTRING(VehicleID,5,3) = @BranchID", sp);

            foreach (DataRow drVehicle in dtVehicle.Rows)
            {
                var mdlVehicle = new Model.mdlVehicle();

                mdlVehicle.VehicleID = drVehicle["VehicleID"].ToString();
                mdlVehicle.VehicleNumber = drVehicle["VehicleNumber"].ToString();

                mdlVehicleList.Add(mdlVehicle);
            }

            mdlVehicleListnew.VehicleList = mdlVehicleList;
            return mdlVehicleListnew;

        }

        //load by string param
        public static List<Model.mdlVehicle> LoadVehicleByBranch(string BranchID)
        {
            var mdlVehicleList = new List<Model.mdlVehicle>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = BranchID},
            };

            DataTable dtVehicle = Manager.DataFacade.DTSQLCommand("select vehicleid,VehicleNumber from Vehicle where VehicleNumber <> '' and SUBSTRING(VehicleID,5,3) = @BranchID ORDER BY VehicleNumber", sp);

            foreach (DataRow drVehicle in dtVehicle.Rows)
            {
                var mdlVehicle = new Model.mdlVehicle();

                mdlVehicle.VehicleID = drVehicle["VehicleID"].ToString();
                mdlVehicle.VehicleNumber = drVehicle["VehicleNumber"].ToString();

                mdlVehicleList.Add(mdlVehicle);
            }

            return mdlVehicleList;

        }

    }
}
