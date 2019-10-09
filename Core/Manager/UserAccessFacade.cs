using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Core.Manager
{
    public class UserAccessFacade
    {
        public static List<Model.mdlMenu> GetMenu(string roleID)
        {
            var listMenu = new List<Model.mdlMenu>();
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@RoleID", SqlDbType = SqlDbType.NVarChar, Value = roleID}
            };


            DataTable dtProduct = Manager.DataFacade.DTSQLCommand("select a.RoleID,a.MenuID,b.MenuName,b.MenuUrl,b.Type from AccessRole a inner join Menu b on a.MenuID = b.MenuID where a.RoleID = @RoleID AND b.MenuID LIKE 'M%' AND a.IsAccess = 1", sp);
            foreach (DataRow row in dtProduct.Rows)
            {
                var mdlMenu = new Model.mdlMenu();
                if (row["MenuUrl"].ToString() == "")
                {
                    mdlMenu.html = @"<li><a><i class='fa fa-edit'></i> " + row["MenuName"].ToString() + " <span class='fa fa-chevron-down'></span></a>";
                    
                }
                else
                {
                    mdlMenu.html = @"<li><a href='" + row["MenuUrl"].ToString() + "'><i class='fa fa-home'></i> " + row["MenuName"].ToString() + " </a></li>";
                }
                mdlMenu.role = row["RoleID"].ToString();
                mdlMenu.menu = row["MenuID"].ToString();
                mdlMenu.name = row["MenuName"].ToString();
                mdlMenu.url = row["MenuUrl"].ToString();
                mdlMenu.type = row["Type"].ToString();
                listMenu.Add(mdlMenu);
            }

            return listMenu;
        }


        public static List<Model.mdlSubMenu> GetSubMenu(string type, string menuID, string roleID)
        {
            var listMenu = new List<Model.mdlSubMenu>();
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@MenuID", SqlDbType = SqlDbType.NVarChar, Value = menuID},
                new SqlParameter() {ParameterName = "@Type", SqlDbType = SqlDbType.NVarChar, Value = type},
                new SqlParameter() {ParameterName = "@RoleID", SqlDbType = SqlDbType.NVarChar, Value = roleID}
            };


            DataTable dtProduct = Manager.DataFacade.DTSQLCommand("select a.RoleID,a.MenuID,b.MenuName,b.MenuUrl,b.Type from AccessRole a inner join Menu b on a.MenuID = b.MenuID where a.RoleID = @RoleID AND b.MenuID LIKE 'S%' AND a.IsAccess = 1 AND Type = @Type AND MenuUrl != ''", sp);
            foreach (DataRow row in dtProduct.Rows)
            {
                var mdlMenu = new Model.mdlSubMenu();
               
                mdlMenu.menu = row["MenuID"].ToString();
                mdlMenu.name = row["MenuName"].ToString();
                mdlMenu.url = row["MenuUrl"].ToString();
                mdlMenu.type = row["Type"].ToString();
                listMenu.Add(mdlMenu);
            }

            return listMenu;
        }

        public static void InsertUserRole(string RoleID, string RoleNm)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@RoleID", SqlDbType = SqlDbType.NVarChar, Value = RoleID},
                new SqlParameter() {ParameterName = "@RoleNm", SqlDbType = SqlDbType.NVarChar, Value = RoleNm}
            };


            Manager.DataFacade.DTSQLCommand(@"INSERT INTO Role (RoleID, RoleName) " +
                                            "VALUES (@RoleID, @RoleNm)", sp);

            return;
        }

    }
}
