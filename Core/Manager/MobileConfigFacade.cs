/* documentation
 * 001 nanda 13 Okt 2016
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Core.Manager
{
    public class MobileConfigFacade : Base.Manager
    {
        public static List<Model.mdlMobileConfig> LoadMobileConfig(Model.mdlParam json)
        {
            var lmdlMobileConfigList = new List<Model.mdlMobileConfig>();

            List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = json.BranchID }
                };

            DataTable dtMobileConfig = Manager.DataFacade.DTSQLCommand(@"SELECT [BranchId]
                                                                                    ,[ID]
                                                                                    ,[Desc]
                                                                                    ,[Value] FROM MobileConfig WHERE BranchID = @BranchID", sp); //003

            foreach (DataRow drMobileConfig in dtMobileConfig.Rows)
            {
                var lmdlMobileConfig = new Model.mdlMobileConfig();
                lmdlMobileConfig.BranchId = drMobileConfig["BranchId"].ToString();
                lmdlMobileConfig.ID = drMobileConfig["ID"].ToString();
                lmdlMobileConfig.Desc = drMobileConfig["Desc"].ToString();
                lmdlMobileConfig.Value = drMobileConfig["Value"].ToString();                
                lmdlMobileConfigList.Add(lmdlMobileConfig);
            }
            return lmdlMobileConfigList;
        }

        public static List<Model.MobileConfig> GetMobileConfig()
        {
            var lMobileConfig = DataContext.MobileConfigs.ToList();
            return lMobileConfig;
        }

        public static List<Model.mdlMobileConfig> GetSearch(string keyword)
        {
            var lMobileConfig = DataContext.MobileConfigs.Where(fld => fld.BranchId.Equals(keyword)).OrderBy(fld => fld.BranchId).ToList();

            var mdlMobileConfigList = new List<Model.mdlMobileConfig>();
            foreach (var mobileconfig in lMobileConfig)
            {
                var mdlMobileConfig = new Model.mdlMobileConfig();
                mdlMobileConfig.BranchId = mobileconfig.BranchId;
                mdlMobileConfig.ID = mobileconfig.ID;
                mdlMobileConfig.Desc = mobileconfig.Desc;
                mdlMobileConfig.Value = mobileconfig.Value;
                mdlMobileConfig.TypeValue = mobileconfig.TypeValue;

                mdlMobileConfigList.Add(mdlMobileConfig);
            }

            return mdlMobileConfigList;
        }

        public static List<Model.MobileConfig> GetMobileConfigbyID(string lBranchID)
        {
            var lMobileConfig = DataContext.MobileConfigs.Where(fld => fld.BranchId.Equals(lBranchID)).ToList();
            return lMobileConfig;
        }

        public static Model.MobileConfig GetMobileConfigbyID2(string lBranchID, string lID)
        {
            var lMobileConfig = DataContext.MobileConfigs.FirstOrDefault(fld => fld.BranchId.Equals(lBranchID) && fld.ID.Equals(lID));

            return lMobileConfig;
        }

        public static Model.MobileConfig GetMobileConfigbyTypeValue(string lBranchID, string lTypeValue)
        {
            var lMobileConfig = DataContext.MobileConfigs.FirstOrDefault(fld => fld.BranchId.Equals(lBranchID) && fld.TypeValue.Equals(lTypeValue));
            return lMobileConfig;
        }

        public static Model.mdlResult CheckVersion(Model.mdlParam param)
        {
            var lMobileConfig = DataContext.MobileConfigs.FirstOrDefault(fld => fld.BranchId.Equals(param.BranchID) && fld.ID.Equals("VERSION"));

           
            var mdlResult = new Model.mdlResult();

            mdlResult.Result = lMobileConfig.Value;

            return mdlResult;
        }

        public static List<Model.mdlMobileConfig>  GetMobileConfigbyBranchID()
        {
            var mdlMobileConfigList = new List<Model.mdlMobileConfig>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
            };

            DataTable dtMobileConfig = Manager.DataFacade.DTSQLCommand(@"SELECT Distinct BranchId FROM MobileConfig", sp); //006

            foreach (DataRow row in dtMobileConfig.Rows)
            {
                var mdlMobileConfig = new Model.mdlMobileConfig();
                mdlMobileConfig.BranchId = row["BranchId"].ToString();
                mdlMobileConfigList.Add(mdlMobileConfig);
            }

            return mdlMobileConfigList;
        }

        public static Model.MobileConfig GetMobileConfigforUpdate(string lBranchID, string lID)
        {
            var lMobileConfig = DataContext.MobileConfigs.FirstOrDefault(fld => fld.BranchId.Equals(lBranchID) && fld.ID.Equals(lID));
            return lMobileConfig;
        }

        //001 Penambahan Try Catch
        public static string InsertMobileConfig(List<Model.mdlMobileConfig> lParamlist)
        {
            try
            {
                foreach (var lParam in lParamlist)
                {
                    Model.MobileConfig mdlMobileConfig = new Model.MobileConfig();
                    mdlMobileConfig.BranchId = lParam.BranchId;
                    mdlMobileConfig.ID = lParam.ID;
                    mdlMobileConfig.Desc = lParam.Desc;
                    mdlMobileConfig.Value = lParam.Value;
                    mdlMobileConfig.TypeValue = lParam.TypeValue;  
                    DataContext.MobileConfigs.InsertOnSubmit(mdlMobileConfig);
                    DataContext.SubmitChanges();
                }
                return "SQLSuccess";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        
        public static string  InsertMobileConfigbyID(Model.mdlMobileConfig lParam)
        {
            string lResult = string.Empty;
            var lmdlMobileconfigBranch = GetMobileConfigbyBranchID();
            
            foreach (var lParamBranch in lmdlMobileconfigBranch)
            {

                bool lCheck = CheckMobileConfigExist(lParam.ID, lParamBranch.BranchId);

                if (lCheck == true)
                {
                    string lResult2 = string.Format("ID : {0} Already Exist", lParam.ID);
                    return lResult2;
                }

                List<SqlParameter> sp = new List<SqlParameter>()
                    {
                        new SqlParameter() {ParameterName = "@BranchId", SqlDbType = SqlDbType.NVarChar, Value = lParamBranch.BranchId},
                        new SqlParameter() {ParameterName = "@ID", SqlDbType = SqlDbType.NVarChar, Value = lParam.ID},
                        new SqlParameter() {ParameterName = "@Desc", SqlDbType = SqlDbType.NVarChar, Value = lParam.Desc},
                        new SqlParameter() {ParameterName = "@Value", SqlDbType = SqlDbType.NVarChar, Value = lParam.Value},
                        new SqlParameter() {ParameterName = "@TypeValue", SqlDbType = SqlDbType.NVarChar, Value = lParam.TypeValue}
                    };

                string query = @"INSERT INTO MobileConfig (BranchId, ID, [Desc], Value, TypeValue) VALUES (@BranchId, @ID, @Desc, @Value, @TypeValue)";
                lResult =  Manager.DataFacade.DTSQLVoidCommand(query, sp);
            }
            return lResult;
        }

        public static string UpdateMobileConfig(List<Model.mdlMobileConfig> lParamlist)
        {
            try
            {
                foreach (var lParam in lParamlist)
                {
                    var lMobileConfig = GetMobileConfigbyID2(lParam.BranchId, lParam.ID);
                    lMobileConfig.Desc = lParam.Desc;
                    lMobileConfig.Value = lParam.Value;
                    DataContext.SubmitChanges();
                }
                return "SQLSuccess";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }

        public static string DeleteMobileConfigbyID(string lID)
        {
            try
            {
                var lmdlMobileconfigBranch = GetMobileConfigbyBranchID();
                foreach (var lParamBranch in lmdlMobileconfigBranch)
                {
                    List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@BranchId", SqlDbType = SqlDbType.NVarChar, Value = lParamBranch.BranchId},
                    new SqlParameter() {ParameterName = "@ID", SqlDbType = SqlDbType.NVarChar, Value = lID}
                };

                    string query = @"DELETE FROM MobileConfig where BranchId = @BranchId AND ID = @ID";
                    Manager.DataFacade.DTSQLVoidCommand(query, sp);
                    
                }
                return "SQLSuccess";
            }
                
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public static string DeleteMobileConfig(string lBranchID)
        {
            try
            {
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@BranchId", SqlDbType = SqlDbType.NVarChar, Value = lBranchID}
                };

                string query = @"DELETE FROM MobileConfig where BranchId = @BranchId";
                Manager.DataFacade.DTSQLVoidCommand(query, sp);
                return "SQLSuccess";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public static List<Model.mdlMobileConfig> GetTypeValue()
        {
            var mdlMobileConfigList = new List<Model.mdlMobileConfig>();

            List<SqlParameter> sp = new List<SqlParameter>()
                {
                };

            DataTable dtMobileConfig = Manager.DataFacade.DTSQLCommand(@"SELECT DISTINCT TypeValue FROM MobileConfig", sp); //006

            foreach (DataRow row in dtMobileConfig.Rows)
            {
                var mdlMobileConfig = new Model.mdlMobileConfig();
                mdlMobileConfig.TypeValue = row["TypeValue"].ToString();
                mdlMobileConfigList.Add(mdlMobileConfig);
            }

            return mdlMobileConfigList;

        }

        public static bool CheckMobileConfigExist(string llID, string lBranchID)
        {
            try
            {
                bool lCheck = false;
                string lID2 = string.Empty;

                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@ID", SqlDbType = SqlDbType.NVarChar, Value = llID },
                    new SqlParameter() {ParameterName = "@BranchId", SqlDbType = SqlDbType.NVarChar, Value = lBranchID }
                };

                DataTable dtMobileConfig = Manager.DataFacade.DTSQLCommand(@"SELECT TOP 1 ID FROM MobileConfig WHERE BranchId = @BranchId AND ID = @ID", sp); //006

                foreach (DataRow row in dtMobileConfig.Rows)
                {
                    lID2 = row["ID"].ToString();
                }

                if (lID2 != string.Empty)
                {
                    lCheck = true;
                }

                return lCheck;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static void SetDefaultMobileConfig(string lBranchID)
        {
            var lmdlDefaultconfig = MobileConfigFacade.GetSearch("0000");
            var lmdlMobileconfiglist = new List<Core.Model.mdlMobileConfig>();

            foreach (var lParam in lmdlDefaultconfig)
            {
                var lmdlMobileconfig = new Core.Model.mdlMobileConfig();
                lmdlMobileconfig.BranchId = lBranchID;
                lmdlMobileconfig.ID = lParam.ID;
                lmdlMobileconfig.Desc = lParam.Desc;
                lmdlMobileconfig.Value = lParam.Value;
                lmdlMobileconfig.TypeValue = lParam.TypeValue;  //002 Nanda
                lmdlMobileconfiglist.Add(lmdlMobileconfig);

            }
            MobileConfigFacade.InsertMobileConfig(lmdlMobileconfiglist);
        }
    }
}
