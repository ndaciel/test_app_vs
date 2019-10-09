/* documentation
 *001 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Core.Manager;
//using MySql.Data.MySqlClient;

namespace Core.Manager
{
    public class UserFacade : Base.Manager
    {
        public static Model.mdlUser GetUserDetail(string lUserID)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@UserId", SqlDbType = SqlDbType.VarChar, Value = lUserID}
            };

            var mdlUser = new Model.mdlUser();
            DataTable dtUser = Manager.DataFacade.DTSQLCommand(@"SELECT TOP 1 *
                                                                     FROM [dbo].[User] where UserID=@UserId", sp);

            foreach (DataRow drUser in dtUser.Rows)
            {
                mdlUser.UserId = drUser["UserID"].ToString();
                mdlUser.Password = drUser["Password"].ToString();
                mdlUser.EmployeeID = drUser["EmployeeID"].ToString();
                mdlUser.RoleID = drUser["RoleID"].ToString();
            }

            return mdlUser;
        }

        public static bool CheckLogin(string userID, string password)
        {
            var user = GetUserDetail(userID);
            if (user.UserId == null)
                return false;
            else
            {
                //string encrpass = Core.Manager.CryptorEngine.Encode(password);
                string decrPass = Core.Manager.CryptorEngine.Decode(user.Password);
                if (decrPass == password)
                    return true;
                else
                    return false;
            }
        }


        public static Model.User GetUserbyID(string lUserID)
        {
            var pUser = DataContext.Users.FirstOrDefault(fld => fld.UserID.Equals(lUserID));
            return pUser;
        }

        public static List<Model.mdlUser> GetUser(string lKeyword, string lBranchId)
        {
            var pUser = new List<Model.User>();
            if (lBranchId == "")
            {
                pUser = DataContext.Users.Where(fld => fld.UserID.Contains(lKeyword)).ToList();
            }
            else
            {
                pUser = DataContext.Users.Where(fld => fld.BranchID.Equals(lBranchId)).OrderBy(fld => fld.BranchID).ToList();
            }

            var mdlUserList = new List<Model.mdlUser>();
            foreach (var user in pUser)
            {
                var mdlUser = new Model.mdlUser();
                mdlUser.BranchID = user.BranchID;
                mdlUser.UserId = user.UserID;
                mdlUser.Password = user.Password;
                mdlUser.RoleID = user.RoleID;

                mdlUserList.Add(mdlUser);
            }

            return mdlUserList;
        }

        public static List<Model.mdlUser> GetSearchUser(string lkeyword, string branchid)
        {
            //var pUser = DataContext.Users.Where(fld => fld.UserID.Contains(lkeyword) && fld.BranchID.Contains(branchid)).OrderBy(fld => fld.UserID).ToList();
            //return pUser;
            var mdlUserList = new List<Model.mdlUser>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
            };

            DataTable dtUser = Manager.DataFacade.DTSQLCommand(@"SELECT distinct a.* FROM [User] a 
                                                                 inner join (
                                                            select BranchID from Branch where BranchID in ("+branchid+") ) b on a.BranchID like '%' + b.BranchID+'%' WHERE a.UserID LIKE '%"+lkeyword+"%' order by a.UserID", sp);

            foreach (DataRow drUser in dtUser.Rows)
            {
                var mdlUser = new Model.mdlUser();
                mdlUser.BranchID = drUser["BranchID"].ToString();
                mdlUser.UserId = drUser["UserID"].ToString();
                mdlUser.RoleID = drUser["RoleID"].ToString();
                mdlUser.Password = drUser["Password"].ToString();

                mdlUserList.Add(mdlUser);
            }

            return mdlUserList;
        }

        public static void DeleteUser(string lUserId)
        {
            var pUser = GetUserbyID(lUserId);
            DataContext.Users.DeleteOnSubmit(pUser);
            DataContext.SubmitChanges();
        }

        public static void InsertUser(string lUserId, string lUsername, string lPassword, List<string> lBranchIDlist, string lRoleID)
        {
            Model.User pUser = new Model.User();

            string lParamBranch = string.Empty;
            string lParamBranchlist = string.Empty;

            foreach (var lBranchID in lBranchIDlist)
            {
                if (lParamBranch == "")
                {
                    lParamBranch = "'" + lBranchID + "'";
                }
                else
                {
                    lParamBranch += "," + "'" + lBranchID + "'";
                }
            }

            lParamBranchlist = lParamBranch;

            pUser.UserID = lUserId;
        
            pUser.Password = lPassword;
            pUser.BranchID = lParamBranchlist;
            pUser.RoleID = lRoleID;
            DataContext.Users.InsertOnSubmit(pUser);
            DataContext.SubmitChanges();
        }

        public static void UpdateUser(string lUserId, string lUsername, string lPassword, List<string> lBranchIDlist, string lRoleID)
        {
            string lParamBranch = string.Empty;
            string lParamBranchlist = string.Empty;

            foreach (var lBranchID in lBranchIDlist)
            {
                if (lParamBranch == "")
                {
                    lParamBranch = "'" + lBranchID + "'";
                }
                else
                {
                    lParamBranch += "," + "'" + lBranchID + "'";
                }
            }

            lParamBranchlist = lParamBranch;

            var pUser = GetUserbyID(lUserId);
           
            pUser.Password = lPassword;
            pUser.BranchID = lParamBranchlist;
            pUser.RoleID = lRoleID;
            DataContext.SubmitChanges();
        }

        public static void UpdateUserWithoutPass(string lUserId, string lUsername, List<string> lBranchIDlist, string lRoleID)
        {
            string lParamBranch = string.Empty;
            string lParamBranchlist = string.Empty;

            foreach (var lBranchID in lBranchIDlist)
            {
                if (lParamBranch == "")
                {
                    lParamBranch = "'" + lBranchID + "'";
                }
                else
                {
                    lParamBranch += "," + "'" + lBranchID + "'";
                }
            }

            lParamBranchlist = lParamBranch;

            var pUser = GetUserbyID(lUserId);

            pUser.BranchID = lParamBranchlist;
            pUser.RoleID = lRoleID;
            DataContext.SubmitChanges();
        }

        public static List<Model.Role> GetddlRole()
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
            };

            DataTable dtRole = DataFacade.DTSQLCommand("SELECT * FROM Role", sp);
            var mdlRoleList = new List<Model.Role>();

            foreach (DataRow row in dtRole.Rows)
            {
                var mdlRole = new Model.Role();
                mdlRole.RoleID = row["RoleID"].ToString();
                mdlRole.RoleName = row["RoleName"].ToString();
                mdlRoleList.Add(mdlRole);
            }

            return mdlRoleList;
        }

        public static List<Model.Menu> GetMobileMenu(string lRoleID, string lIsAccess)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@RoleID", SqlDbType = SqlDbType.NVarChar, Value = lRoleID},
                new SqlParameter() {ParameterName = "@IsAccess", SqlDbType = SqlDbType.NVarChar, Value = Convert.ToBoolean(lIsAccess)},
            };

            DataTable dtAccess = DataFacade.DTSQLCommand(@"SELECT ModuleID,ModuleType,Title FROM MenuMobile", sp);
            var mdlMenuList = new List<Model.Menu>();

            foreach (DataRow row in dtAccess.Rows)
            {
                var mdlMenu = new Model.Menu();

                mdlMenu.MenuID = row["MenuID"].ToString();
                mdlMenu.MenuName = row["MenuName"].ToString();

                mdlMenuList.Add(mdlMenu);
            }
            return mdlMenuList;
        }

        public static List<Model.Menu> GetAccessMenu(string lRoleID, string lIsAccess)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@RoleID", SqlDbType = SqlDbType.NVarChar, Value = lRoleID},
                new SqlParameter() {ParameterName = "@IsAccess", SqlDbType = SqlDbType.NVarChar, Value = Convert.ToBoolean(lIsAccess)},
            };

            DataTable dtAccess = DataFacade.DTSQLCommand(@"SELECT MenuID,MenuName FROM Menu WHERE MenuID IN (SELECT MenuID FROM AccessRole WHERE RoleID = @RoleID AND IsAccess = @IsAccess AND MenuID LIKE 'M%')", sp);
            var mdlMenuList = new List<Model.Menu>();

            foreach (DataRow row in dtAccess.Rows)
            {
                var mdlMenu = new Model.Menu();

                mdlMenu.MenuID = row["MenuID"].ToString();
                mdlMenu.MenuName = row["MenuName"].ToString();

                mdlMenuList.Add(mdlMenu);
            }
            return mdlMenuList;
        }


        public static List<Model.Menu> GetAccessMobileMenu(string lRoleID, string lIsAccess)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@RoleID", SqlDbType = SqlDbType.NVarChar, Value = lRoleID},
                new SqlParameter() {ParameterName = "@IsAccess", SqlDbType = SqlDbType.NVarChar, Value = Convert.ToBoolean(lIsAccess)},
            };

            DataTable dtAccess = DataFacade.DTSQLCommand(@"SELECT a.RoleID,b.ModuleID,b.ModuleType FROM AccessRoleMobile a INNER JOIN MenuMobile b ON a.MenuID = b.ModuleID WHERE a.RoleID = @RoleID AND IsActive = 1", sp);
            var mdlMenuList = new List<Model.Menu>();

            foreach (DataRow row in dtAccess.Rows)
            {
                var mdlMenu = new Model.Menu();

                mdlMenu.MenuID = row["ModuleID"].ToString();
                mdlMenu.MenuName = row["ModuleID"].ToString();

                mdlMenuList.Add(mdlMenu);
            }
            return mdlMenuList;
        }

        public static List<Model.Menu> GetMobileMenu()
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
            };

            DataTable dtMenu = DataFacade.DTSQLCommand("SELECT ModuleID,ModuleType,Title FROM MenuMobile", sp);
            var mdlMenuList = new List<Model.Menu>();

            foreach (DataRow row in dtMenu.Rows)
            {
                var mdlMenu = new Model.Menu();
                mdlMenu.MenuID = row["ModuleID"].ToString();
                mdlMenu.MenuName = row["ModuleID"].ToString();
                mdlMenuList.Add(mdlMenu);
            }

            return mdlMenuList;
        }


        public static List<Model.Menu> GetMenu()
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
            };

            DataTable dtMenu = DataFacade.DTSQLCommand("SELECT MenuID,MenuName FROM Menu where MenuID LIKE 'M%'", sp);
            var mdlMenuList = new List<Model.Menu>();

            foreach (DataRow row in dtMenu.Rows)
            {
                var mdlMenu = new Model.Menu();
                mdlMenu.MenuID = row["MenuID"].ToString();
                mdlMenu.MenuName = row["MenuName"].ToString();
                mdlMenuList.Add(mdlMenu);
            }

            return mdlMenuList;
        }

        public static List<Model.mdlSubMenu> GetSubMenu(string menuID)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
            };

            DataTable dtSubMenu = DataFacade.DTSQLCommand("SELECT MenuID,MenuName FROM Menu where Type in (select type from menu where menuid = '"+menuID+"') and MenuID <> '"+menuID+"' ", sp);
            var mdlSubMenuList = new List<Model.mdlSubMenu>();

            foreach (DataRow row in dtSubMenu.Rows)
            {
                var mdlSubMenu = new Model.mdlSubMenu();
                mdlSubMenu.menu = row["MenuID"].ToString();
                mdlSubMenu.name = " - " + row["MenuName"].ToString();
                mdlSubMenuList.Add(mdlSubMenu);
            }

            return mdlSubMenuList;
        }

        public static List<Model.mdlSubMenu> GetAccessSubMenu(string lRoleID,string menuID)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
            };

            DataTable dtSubMenu = DataFacade.DTSQLCommand(@"SELECT MenuID,MenuName FROM Menu where Type in (select Type from Menu where menuid = '" + menuID + "') and MenuID <> '" + menuID + @"' AND 
                                                            MenuID IN (SELECT MenuID from AccessRole where IsAccess = 'true' and RoleID = '"+lRoleID+"')", sp);

            var mdlSubMenuList = new List<Model.mdlSubMenu>();

            foreach (DataRow row in dtSubMenu.Rows)
            {
                var mdlSubMenu = new Model.mdlSubMenu();
                mdlSubMenu.menu = row["MenuID"].ToString();
                mdlSubMenu.name = " - " + row["MenuName"].ToString();
                mdlSubMenuList.Add(mdlSubMenu);
            }

            return mdlSubMenuList;
        }

        public static Model.AccessRole GetAccessUserbyID(string lRoleID, string lMenuID)
        {
            var pUserAccess = DataContext.AccessRoles.FirstOrDefault(fld => fld.RoleID.Equals(lRoleID) && fld.MenuID.Equals(lMenuID));
            return pUserAccess;
        }

        public static Model.AccessRoleMobile GetAccessMobileUserbyID(string lRoleID, string lMenuID)
        {
            var pUserAccess = DataContext.AccessRoleMobiles.FirstOrDefault(fld => fld.RoleID.Equals(lRoleID) && fld.MenuID.Equals(lMenuID));
            return pUserAccess;
        }

        public static void UpdateAccessUser(string lRoleID, string lMenuID, string lIsAccess)
        {
            var pUserAccess = GetAccessUserbyID(lRoleID, lMenuID);
            pUserAccess.IsAccess = Convert.ToBoolean(lIsAccess);
            DataContext.SubmitChanges();
        }

        public static void UpdateAccessMobileUser(string lRoleID, string lMenuID, string lIsAccess)
        {
            var pUserAccess = GetAccessMobileUserbyID(lRoleID, lMenuID);
            pUserAccess.IsActive = Convert.ToBoolean(lIsAccess);
            DataContext.SubmitChanges();
        }

        public static void DeleteAccessUser(string lRoleID, string lMenuID, string lIsAccess)
        {
            var deleteAccessMenu =
                from details in DataContext.AccessRoleMobiles
                where (details.RoleID.Equals(lRoleID) && details.MenuID.Equals(lMenuID))
                select details;


            foreach (var delete in deleteAccessMenu)
            {
                DataContext.AccessRoleMobiles.DeleteOnSubmit(delete);
            }

            DataContext.SubmitChanges();
        }

        public static void InsertAccessUser(string lRoleID, string lMenuID, string lIsAccess)
        {
            Model.AccessRole pUserAccess = new Model.AccessRole();
            pUserAccess.RoleID = lRoleID;
            pUserAccess.MenuID = lMenuID;
            pUserAccess.IsAccess = Convert.ToBoolean(lIsAccess);
            DataContext.AccessRoles.InsertOnSubmit(pUserAccess);
            DataContext.SubmitChanges();
        }

        public static void InsertAccessMobileUser(string lRoleID, string lMenuID, string lIsAccess)
        {
            Model.AccessRoleMobile pUserAccess = new Model.AccessRoleMobile();
            pUserAccess.RoleID = lRoleID;
            pUserAccess.MenuID = lMenuID;
            pUserAccess.IsActive = Convert.ToBoolean(lIsAccess);
            DataContext.AccessRoleMobiles.InsertOnSubmit(pUserAccess);
            DataContext.SubmitChanges();
        }

        public static void UpdateAccessUser2(string lRoleID, List<string> lMenuIDlist)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
                    {
                        new SqlParameter() {ParameterName = "@RoleID", SqlDbType = SqlDbType.NVarChar, Value = lRoleID},
                    };

            Manager.DataFacade.DTSQLVoidCommand("UPDATE AccessRole SET IsModify=0 WHERE RoleID=@RoleID", sp);

            foreach (var lMenuID in lMenuIDlist)
            {
                string lParamMenuID = string.Empty;

                lParamMenuID = lMenuID;

                    string query = @"UPDATE AccessRole SET IsModify=1 WHERE RoleID=@RoleID AND MenuID='"+lParamMenuID+"'";

                    Manager.DataFacade.DTSQLVoidCommand(query, sp);
                
            }

            return;
        }

        public static List<Model.mdlSubMenu> GetEditableMenu(string lRoleID, string lIsModify)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@RoleID", SqlDbType = SqlDbType.NVarChar, Value = lRoleID},
                new SqlParameter() {ParameterName = "@IsModify", SqlDbType = SqlDbType.Bit, Value = lIsModify},
            };

            DataTable dtMenu = DataFacade.DTSQLCommand(@"SELECT MenuID,MenuName FROM Menu where MenuID in (select menuid from accessrole where RoleID=@RoleID AND IsModify=@IsModify)", sp);

            var mdlMenuList = new List<Model.mdlSubMenu>();

            foreach (DataRow row in dtMenu.Rows)
            {
                var mdlMenu = new Model.mdlSubMenu();

                mdlMenu.menu = row["MenuID"].ToString();
                mdlMenu.name = row["MenuName"].ToString();
                mdlMenuList.Add(mdlMenu);
            }

            return mdlMenuList;
        }

        public static bool CheckUserLeverage(string lRoleID, string lMenuName)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
               new SqlParameter() {ParameterName = "@RoleID", SqlDbType = SqlDbType.NVarChar, Value = lRoleID },
               new SqlParameter() {ParameterName = "@MenuName", SqlDbType = SqlDbType.NVarChar, Value = lMenuName },
            };

            DataTable dtRole = Manager.DataFacade.DTSQLCommand(@"SELECT TOP 1 RoleID
                                                                   FROM AccessRole 
                                                                   WHERE RoleID=@RoleID and MenuID in (select menuid from menu where MenuName=@MenuName) AND IsModify=1", sp);
            bool lCheck = false;
            if (dtRole.Rows.Count == 1)
            {
                lCheck = true;
            }

            return lCheck;
        }

        public static bool CheckAccessUserbyID(string lRoleID, string lMenuID)
        {
            bool bAccess = true;
            var pUserAccess = DataContext.AccessRoles.FirstOrDefault(fld => fld.RoleID.Equals(lRoleID) && fld.MenuID.Equals(lMenuID));
            if (pUserAccess == null)
            {
                bAccess = false;
            }
            return bAccess;
        }

        public static bool CheckAccessMobileUserbyID(string lRoleID, string lMenuID)
        {
            bool bAccess = true;
            var pUserAccess = DataContext.AccessRoleMobiles.FirstOrDefault(fld => fld.RoleID.Equals(lRoleID) && fld.MenuID.Equals(lMenuID));
            if (pUserAccess == null)
            {
                bAccess = false;
            }
            return bAccess;
        }

        //--------------------------------Facade Menu-----------------------------------------------//

        public static List<Model.Menu> GetSearchMenu(string lRoleID, string lType)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@RoleID", SqlDbType = SqlDbType.NVarChar, Value = lRoleID},
                new SqlParameter() {ParameterName = "@Type", SqlDbType = SqlDbType.NVarChar, Value = lType},
            };

            DataTable dtMenu = DataFacade.DTSQLCommand("SELECT a.MenuUrl,a.MenuName FROM menu a INNER JOIN AccessRole b ON a.MenuID = b.MenuID WHERE b.RoleID = @RoleID AND b.IsAccess = 1 AND a.Type = @Type;", sp);
            var mdlMenuList = new List<Model.Menu>();

            foreach (DataRow row in dtMenu.Rows)
            {
                var mdlMenu = new Model.Menu();
                mdlMenu.MenuUrl = row["MenuUrl"].ToString();
                mdlMenu.MenuName = row["MenuName"].ToString();
                mdlMenuList.Add(mdlMenu);
            }
            return mdlMenuList;
        }
        //--002

        public static List<Model.mdlUserCheckbox> LoadUserlistReport(string lBranch)
        {
            //List<String> listUser = new List<String>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
                 new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = lBranch}
            };


            var lUserlist = new List<Model.mdlUserCheckbox>();
            DataTable dtUser = Manager.DataFacade.DTSQLCommand("SELECT UserID, UserName FROM [User] where BranchID = @BranchID", sp);
            foreach (DataRow drUser in dtUser.Rows)
            {
                var lUser = new Model.mdlUserCheckbox();
                lUser.UserId = drUser["UserID"].ToString();
                lUser.UserName = drUser["UserID"].ToString() + " - " + drUser["UserName"].ToString();
                lUserlist.Add(lUser);
            }
            return lUserlist;
        }

        public static List<Model.mdlUserCheckbox> LoadUserlistReport2(string lUser, string lBranch)
        {
            //List<String> listEmployee = new List<String>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@User", SqlDbType = SqlDbType.NVarChar, Value = "%" + lUser + "%"},
                new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = lBranch}

            };

            var lUserlist = new List<Model.mdlUserCheckbox>();
            DataTable dtUser = Manager.DataFacade.DTSQLCommand("SELECT UserID, UserName FROM [User] WHERE (UserID LIKE @User OR UserName LIKE @User) AND BranchID = @BranchID", sp);

            foreach (DataRow drUser in dtUser.Rows)
            {
                var mdllUser = new Model.mdlUserCheckbox();
                mdllUser.UserId = drUser["UserID"].ToString();
                mdllUser.UserName = drUser["UserID"].ToString() + " - " + drUser["UserName"].ToString();
                lUserlist.Add(mdllUser);
            }
            return lUserlist;
        }

        
    }
}
