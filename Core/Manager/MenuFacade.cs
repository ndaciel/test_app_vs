using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;

namespace Core.Manager
{
    public class MenuFacade : Base.Manager
    {
        public static Model.mdlMenu2 LoadMenuId(string Item)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
            };

            var mdlMenu = new Model.mdlMenu2();
            DataTable dtMenu = Manager.DataFacade.DTSQLCommand(@"SELECT MenuID,Type
                                                                   FROM Menu 
                                                                   WHERE MenuName='"+Item+"'", sp);

            foreach (DataRow row in dtMenu.Rows)
            {
                mdlMenu.menuID = row["MenuID"].ToString();
                mdlMenu.type = row["Type"].ToString();
            }

            return mdlMenu;
        }


        public static List<Model.mdlMenuMobile> LoadMenuMobile(string role)
        {

            var listMenu = new List<Model.mdlMenuMobile>();
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@role", SqlDbType = SqlDbType.VarChar, Value = role },
            };

            ///////////////change
            DataTable dtMenu = Manager.DataFacade.DTSQLCommand(@"select a.RoleID,b.ModuleID,b.ModuleType,a.IsActive,b.Title from AccessRoleMobile a inner join MenuMobile b ON a.MenuID = b.ModuleID WHERE a.RoleID = @role", sp);

            foreach (DataRow row in dtMenu.Rows)
            {
                var model = new Model.mdlMenuMobile();
                model.Role = row["RoleID"].ToString();
                model.ModuleID = row["ModuleID"].ToString();
                model.ModuleType = row["ModuleType"].ToString();
                model.IsActive = row["IsActive"].ToString();
                model.Title = row["Title"].ToString();
                listMenu.Add(model);
            }

            return listMenu;
        }

        public static List<Model.mdlSubMenu> LoadSubMenu(string menuID, string menutype)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
            };

            var mdlSubMenuList = new List<Model.mdlSubMenu>();

            if (menuID.StartsWith("M"))
            {
                DataTable dtSubMenu = Manager.DataFacade.DTSQLCommand(@"SELECT MenuName, MenuID
                                                                   FROM Menu 
                                                                   WHERE MenuID='" + menuID + "' or Type='" + menutype + "'", sp);

                foreach (DataRow row in dtSubMenu.Rows)
                {
                    var mdlSubMenu = new Model.mdlSubMenu();

                    mdlSubMenu.name = row["MenuName"].ToString();
                    mdlSubMenu.menu = row["MenuID"].ToString();
                    if (mdlSubMenu.menu.StartsWith("M"))
                    {
                        mdlSubMenu.name = row["MenuName"].ToString();
                    }
                    else
                    {
                        mdlSubMenu.name = " - " + row["MenuName"].ToString();
                    }

                    mdlSubMenuList.Add(mdlSubMenu);
                }
            }
            else
            {
                DataTable dtSubMenu = Manager.DataFacade.DTSQLCommand(@"SELECT MenuName, MenuID
                                                                   FROM Menu 
                                                                   WHERE (MenuID='" + menuID + "' AND Type='" + menutype + "') OR (MenuID LIKE 'M%' AND Type='"+menutype+"')", sp);

                foreach (DataRow row in dtSubMenu.Rows)
                {
                    var mdlSubMenu = new Model.mdlSubMenu();

                    mdlSubMenu.name = row["MenuName"].ToString();
                    mdlSubMenu.menu = row["MenuID"].ToString();
                    if (mdlSubMenu.menu.StartsWith("M"))
                    {
                        mdlSubMenu.name = row["MenuName"].ToString();
                    }
                    else
                    {
                        mdlSubMenu.name = " - " + row["MenuName"].ToString();
                    }

                    mdlSubMenuList.Add(mdlSubMenu);
                }
            }

            return mdlSubMenuList;
        }

    }
}
