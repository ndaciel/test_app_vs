using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Core.Manager
{
    public class UserConfigFacade
    {
        public static void UploadUid(Core.Model.mdlSetDeviceIDParam param)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                 new SqlParameter() {ParameterName = "@DeviceID", SqlDbType = SqlDbType.NVarChar, Value = param.deviceID},
                 new SqlParameter() {ParameterName = "@Uid", SqlDbType = SqlDbType.NVarChar, Value = param.Uid}
            };

            String query = @"BEGIN TRAN
                                   UPDATE USERCONFIG 
                                        SET 
                                        Uid = @Uid,
                                        LastUpdateBy = @DeviceID,
                                        LastUpdateDate = GetDate()
                                   WHERE DeviceID = @DeviceID 

                                   IF @@rowcount = 0
                                   BEGIN
                                      INSERT INTO USERCONFIG 
                                            (DeviceID,
                                                EmployeeID,
                                                BranchID,
                                                BranchName,
                                                VehicleNumber,
                                                IpLocal,
                                                PortLocal,
                                                IpPublic,
                                                PortPublic,
                                                IpAlternative,
                                                PortAlternative,
                                                Password,
                                                CreatedBy,
                                                LastUpdateBy,
                                                CreatedDate,
                                                LastUpdateDate,
                                                Uid,
                                                isActive
                                                ) 
                                            VALUES 
                                            (@DeviceID,
                                                '',
                                                '',
                                                '',
                                                '',
                                                '',
                                                '',
                                                '',
                                                '',
                                                '',
                                                '',
                                                '',
                                                @DeviceID,
                                                @DeviceID,
                                                GetDate(),
                                                GetDate(),
                                                @Uid,
                                                '0'
                                                )
                                   END
                                COMMIT TRAN";
            string res = Manager.DataFacade.DTSQLVoidCommand(query, sp);
        }

        public static Core.Model.mdlSetDeviceID SetUserConfig(Core.Model.mdlSetDeviceIDParam param)
        {
            if (param.Uid == null)
            {
                param.Uid = "";
            }


            DataTable dt = new DataTable();
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@Uid", SqlDbType = SqlDbType.NVarChar, Value = param.Uid },
                new SqlParameter() {ParameterName = "@deviceID", SqlDbType = SqlDbType.NVarChar, Value = param.deviceID },
            };

            dt = DataFacade.DTSQLCommand(@"SELECT * FROM UserConfig WHERE Uid = @Uid  OR deviceID = @deviceID", sp);

            var mdlUserConfig = new Model.mdlSetDeviceID();
            foreach (DataRow row in dt.Rows)
            {

                mdlUserConfig.EmployeeID = row["EmployeeID"].ToString();
                mdlUserConfig.BranchID = row["BranchID"].ToString();
                mdlUserConfig.BranchName = row["BranchName"].ToString();
                mdlUserConfig.VehicleNumber = row["VehicleNumber"].ToString();
                mdlUserConfig.IpLocal = row["IpLocal"].ToString();
                mdlUserConfig.PortLocal = row["PortLocal"].ToString();
                mdlUserConfig.IpPublic = row["IpPublic"].ToString();
                mdlUserConfig.PortPublic = row["PortPublic"].ToString();
                mdlUserConfig.IpAlternative = row["IpAlternative"].ToString();
                mdlUserConfig.PortAlternative = row["PortAlternative"].ToString();
                mdlUserConfig.Password = row["Password"].ToString();
                mdlUserConfig.Uid = row["Uid"].ToString();
            }



            if (mdlUserConfig.EmployeeID == "" | mdlUserConfig.EmployeeID == null)
            {
                mdlUserConfig.Result = "0";
                mdlUserConfig.Role = "";
            }
            else
            {
                mdlUserConfig.Result = "1";
                mdlUserConfig.Role = getUserRole(mdlUserConfig.EmployeeID);
            }
            return mdlUserConfig;

        }

        public static string getUserRole(string EmployeeID)
        {
            var mdlUserConfigList = new List<Model.mdlUserConfig>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@employeeID", SqlDbType = SqlDbType.VarChar, Value = EmployeeID }
                
            };

            DataTable dtUserConfig = Manager.DataFacade.DTSQLCommand("SELECT EmployeeTypeID FROM Employee WHERE EmployeeID = @employeeID", sp);
            string empID = "";
            foreach (DataRow drUserConfig in dtUserConfig.Rows)
            {
                empID = drUserConfig["EmployeeTypeID"].ToString();
            }

            return empID;
        }

        public static List<Model.mdlUserConfig> LoadUserConfig(string keyword, string branchid)
        {
            var mdlUserConfigList = new List<Model.mdlUserConfig>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@keyword", SqlDbType = SqlDbType.VarChar, Value = "%" +keyword+ "%" },
                new SqlParameter() {ParameterName = "@branchid", SqlDbType = SqlDbType.VarChar, Value = "%" +branchid+ "%" }
            };

            DataTable dtUserConfig = Manager.DataFacade.DTSQLCommand("SELECT * FROM UserConfig WHERE (DeviceID LIKE @keyword OR EmployeeID LIKE @keyword) AND (BranchID LIKE @branchid) ORDER BY BranchID,EmployeeID ASC", sp);

            foreach (DataRow drUserConfig in dtUserConfig.Rows)
            {
                var mdlUserConfig = new Model.mdlUserConfig();
                mdlUserConfig.DeviceID = drUserConfig["DeviceID"].ToString();
                mdlUserConfig.EmployeeID = drUserConfig["EmployeeID"].ToString();
                mdlUserConfig.BranchID = drUserConfig["BranchID"].ToString();
                mdlUserConfig.BranchName = drUserConfig["BranchName"].ToString();
                mdlUserConfig.VehicleNumber = drUserConfig["VehicleNumber"].ToString();
                mdlUserConfig.IpLocal = drUserConfig["IpLocal"].ToString();
                mdlUserConfig.PortLocal = drUserConfig["PortLocal"].ToString();
                mdlUserConfig.IpPublic = drUserConfig["IpPublic"].ToString();
                mdlUserConfig.PortPublic = drUserConfig["PortPublic"].ToString();
                mdlUserConfig.IpAlternative = drUserConfig["IpAlternative"].ToString();
                mdlUserConfig.PortAlternative = drUserConfig["PortAlternative"].ToString();

                mdlUserConfigList.Add(mdlUserConfig);
            }

            return mdlUserConfigList;
        }

        public static List<Model.mdlUserConfig> LoadUserConfigByCertainBranch(string keyword, string branchid)
        {
            var mdlUserConfigList = new List<Model.mdlUserConfig>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@keyword", SqlDbType = SqlDbType.VarChar, Value = "%" +keyword+ "%" },
                new SqlParameter() {ParameterName = "@branchid", SqlDbType = SqlDbType.VarChar, Value = "%" +branchid+ "%" }
            };

            DataTable dtUserConfig = Manager.DataFacade.DTSQLCommand("SELECT * FROM UserConfig WHERE (DeviceID LIKE @keyword OR EmployeeID LIKE @keyword) AND (BranchID IN ("+branchid+")) ORDER BY BranchID,EmployeeID ASC", sp);

            foreach (DataRow drUserConfig in dtUserConfig.Rows)
            {
                var mdlUserConfig = new Model.mdlUserConfig();
                mdlUserConfig.DeviceID = drUserConfig["DeviceID"].ToString();
                mdlUserConfig.EmployeeID = drUserConfig["EmployeeID"].ToString();
                mdlUserConfig.BranchID = drUserConfig["BranchID"].ToString();
                mdlUserConfig.BranchName = drUserConfig["BranchName"].ToString();
                mdlUserConfig.VehicleNumber = drUserConfig["VehicleNumber"].ToString();
                mdlUserConfig.IpLocal = drUserConfig["IpLocal"].ToString();
                mdlUserConfig.PortLocal = drUserConfig["PortLocal"].ToString();
                mdlUserConfig.IpPublic = drUserConfig["IpPublic"].ToString();
                mdlUserConfig.PortPublic = drUserConfig["PortPublic"].ToString();
                mdlUserConfig.IpAlternative = drUserConfig["IpAlternative"].ToString();
                mdlUserConfig.PortAlternative = drUserConfig["PortAlternative"].ToString();

                mdlUserConfigList.Add(mdlUserConfig);
            }

            return mdlUserConfigList;
        }

        public static Model.mdlUserConfig LoadUserConfigbyDeviceID(string DeviceID)
        {
            var mdlUserConfig = new Model.mdlUserConfig();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@DeviceID", SqlDbType = SqlDbType.VarChar, Value = DeviceID },
            };

            DataTable dtUserConfig = Manager.DataFacade.DTSQLCommand("SELECT * FROM UserConfig WHERE DeviceID = @DeviceID", sp);

            foreach (DataRow drUserConfig in dtUserConfig.Rows)
            {
                mdlUserConfig.DeviceID = drUserConfig["DeviceID"].ToString();
                mdlUserConfig.EmployeeID = drUserConfig["EmployeeID"].ToString();
                mdlUserConfig.BranchID = drUserConfig["BranchID"].ToString();
                mdlUserConfig.BranchName = drUserConfig["BranchName"].ToString();
                mdlUserConfig.VehicleNumber = drUserConfig["VehicleNumber"].ToString();
                mdlUserConfig.IpLocal = drUserConfig["IpLocal"].ToString();
                mdlUserConfig.PortLocal = drUserConfig["PortLocal"].ToString();
                mdlUserConfig.IpPublic = drUserConfig["IpPublic"].ToString();
                mdlUserConfig.PortPublic = drUserConfig["PortPublic"].ToString();
                mdlUserConfig.IpAlternative = drUserConfig["IpAlternative"].ToString();
                mdlUserConfig.PortAlternative = drUserConfig["PortAlternative"].ToString();
                mdlUserConfig.Password = drUserConfig["Password"].ToString();
            }

            return mdlUserConfig;
        }

        public static string DeleteUserConfig(string DeviceID, string EmployeeID)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
                    {
                        new SqlParameter() {ParameterName = "@DeviceID", SqlDbType = SqlDbType.NVarChar, Value = DeviceID},
                        new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = EmployeeID},
                    };

            string query = @"DELETE FROM UserConfig WHERE DeviceID = @DeviceID AND EmployeeID = @EmployeeID";

            string result = Manager.DataFacade.DTSQLVoidCommand(query, sp);

            return result;
        }

        public static string InsertUserConfig(string DeviceID, string BranchID, string BranchNm, string EmployeeID, string VehicleNumber, string IpLocal, string PortLocal, string IpPublic, string PortPublic, string IpAlt, string PortAlt, string password, string datetime, string user)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
                    {
                        new SqlParameter() {ParameterName = "@DeviceID", SqlDbType = SqlDbType.NVarChar, Value = DeviceID},
                        new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = BranchID},
                        new SqlParameter() {ParameterName = "@BranchNm", SqlDbType = SqlDbType.NVarChar, Value = BranchNm},
                        new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = EmployeeID},
                        new SqlParameter() {ParameterName = "@VehicleNumber", SqlDbType = SqlDbType.NVarChar, Value = VehicleNumber},
                        new SqlParameter() {ParameterName = "@IpLocal", SqlDbType = SqlDbType.NVarChar, Value = IpLocal},
                        new SqlParameter() {ParameterName = "@PortLocal", SqlDbType = SqlDbType.NVarChar, Value = PortLocal},
                        new SqlParameter() {ParameterName = "@IpPublic", SqlDbType = SqlDbType.NVarChar, Value = IpPublic},
                        new SqlParameter() {ParameterName = "@PortPublic", SqlDbType = SqlDbType.NVarChar, Value = PortPublic},
                        new SqlParameter() {ParameterName = "@IpAlt", SqlDbType = SqlDbType.NVarChar, Value = IpAlt},
                        new SqlParameter() {ParameterName = "@PortAlt", SqlDbType = SqlDbType.NVarChar, Value = PortAlt},
                         new SqlParameter() {ParameterName = "@Password", SqlDbType = SqlDbType.NVarChar, Value = password},
                         new SqlParameter() {ParameterName = "@Date", SqlDbType = SqlDbType.NVarChar, Value = datetime},
                         new SqlParameter() {ParameterName = "@User", SqlDbType = SqlDbType.NVarChar, Value = user},
                    };

            string query = @"INSERT INTO UserConfig (DeviceID, BranchID, BranchName, EmployeeID, VehicleNumber, IpLocal, PortLocal, IpPublic, PortPublic, IpAlternative, PortAlternative, Password, CreatedBy, LastUpdateBy, CreatedDate, LastUpdateDate) " +
                                                "VALUES (@DeviceID, @BranchID, @BranchNm, @EmployeeID, @VehicleNumber, @IpLocal, @PortLocal, @IpPublic, @PortPublic, @IpAlt, @PortAlt, @Password, @User, @User, @Date, @Date) ";

            string result = Manager.DataFacade.DTSQLVoidCommand(query, sp);

            return result;
        }

        public static string UpdateUserConfig(string DeviceID, string BranchID, string BranchNm, string EmployeeID, string VehicleNumber, string IpLocal, string PortLocal, string IpPublic, string PortPublic, string IpAlt, string PortAlt, string password, string datetime, string user)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
                    {
                        new SqlParameter() {ParameterName = "@DeviceID", SqlDbType = SqlDbType.NVarChar, Value = DeviceID},
                        new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = BranchID},
                        new SqlParameter() {ParameterName = "@BranchNm", SqlDbType = SqlDbType.NVarChar, Value = BranchNm},
                        new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = EmployeeID},
                        new SqlParameter() {ParameterName = "@VehicleNumber", SqlDbType = SqlDbType.NVarChar, Value = VehicleNumber},
                        new SqlParameter() {ParameterName = "@IpLocal", SqlDbType = SqlDbType.NVarChar, Value = IpLocal},
                        new SqlParameter() {ParameterName = "@PortLocal", SqlDbType = SqlDbType.NVarChar, Value = PortLocal},
                        new SqlParameter() {ParameterName = "@IpPublic", SqlDbType = SqlDbType.NVarChar, Value = IpPublic},
                        new SqlParameter() {ParameterName = "@PortPublic", SqlDbType = SqlDbType.NVarChar, Value = PortPublic},
                        new SqlParameter() {ParameterName = "@IpAlt", SqlDbType = SqlDbType.NVarChar, Value = IpAlt},
                        new SqlParameter() {ParameterName = "@PortAlt", SqlDbType = SqlDbType.NVarChar, Value = PortAlt},
                         new SqlParameter() {ParameterName = "@Password", SqlDbType = SqlDbType.NVarChar, Value = password},
                         new SqlParameter() {ParameterName = "@Date", SqlDbType = SqlDbType.NVarChar, Value = datetime},
                         new SqlParameter() {ParameterName = "@User", SqlDbType = SqlDbType.NVarChar, Value = user},
                    };

            string query = @"UPDATE UserConfig SET BranchID=@BranchID, BranchName=@BranchNm, EmployeeID=@EmployeeID, VehicleNumber=@VehicleNumber, IpLocal=@IpLocal, 
                                PortLocal=@PortLocal, IpPublic=@IpPublic, PortPublic=@PortPublic, IpAlternative=@IpAlt, PortAlternative=@PortAlt, Password=@Password, LastUpdateBy=@User, LastUpdateDate=@Date  
                                WHERE DeviceID=@DeviceID";

            string result = Manager.DataFacade.DTSQLVoidCommand(query, sp);

            return result;
        }

    }
}
