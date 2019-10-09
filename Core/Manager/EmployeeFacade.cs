/* documentation
 * 001 20 Okt'16 fernandes
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Core.Manager
{
    public class EmployeeFacade : Base.Manager
    {
       //GETSEARCH BY LINQ
        //public static List<Model.Employee> GetSearch(string keyword, string branch)
        //{
        //    var lEmployee = DataContext.Employees.Where(fld => (fld.EmployeeName.Contains(keyword) || fld.EmployeeID.Contains(keyword)) && fld.BranchID.Contains(branch)).ToList();
        //    return lEmployee;
        //}

        public static bool CheckExistingEmployee(string branchID, string employeeID)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@DateNow", SqlDbType = SqlDbType.DateTime, Value = DateTime.Now.Date },
            };

            DataTable dtExistingEmp = Manager.DataFacade.DTSQLCommand(@"SELECT TOP 1 EmployeeID
                                                                   FROM Employee
                                                                   WHERE BranchID = '" + branchID + "' and EmployeeID = '" + employeeID + "' and (OutDate = '2000-01-01' or OutDate = '1900-01-01' or OutDate >= @DateNow)", sp);
            bool lCheck = false;
            if (dtExistingEmp.Rows.Count == 0)
            {
                lCheck = true;
            }

            return lCheck;
            //klau true berarti datanya belum ada
        }

        public static List<Model.mdlEmployee> GetSearchAllEmployee(string keyword, string branch)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@DateNow", SqlDbType = SqlDbType.DateTime, Value = DateTime.Now.Date },
            };

            var mdlEmployeeList = new List<Model.mdlEmployee>();

            DataTable dtEmployee = Manager.DataFacade.DTSQLCommand(@"SELECT   EmployeeID, 
			                                                                        EmployeeName,
                                                                                    EmployeeTypeID
                                                                                    from Employee
				                                                            Where BranchID LIKE '%" + branch + "%' and (EmployeeID LIKE '%" + keyword + "%' or EmployeeName LIKE '%" + keyword + "%') and (OutDate = '2000-01-01' or OutDate = '1900-01-01' or OutDate >= @DateNow) AND EmployeeTypeID='0'", sp);

            foreach (DataRow row in dtEmployee.Rows)
            {
                var mdlEmployee = new Model.mdlEmployee();
                mdlEmployee.EmployeeID = row["EmployeeID"].ToString();
                mdlEmployee.EmployeeName = row["EmployeeName"].ToString();
                mdlEmployee.EmployeeTypeID = row["EmployeeTypeID"].ToString();

                mdlEmployeeList.Add(mdlEmployee);
            }
            return mdlEmployeeList;
        }

        public static List<Model.mdlEmployee> GetSearchEmployeeByBranch(string keyword, string branch)
        {
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                 new SqlParameter() {ParameterName = "@DateNow", SqlDbType = SqlDbType.DateTime, Value = DateTime.Now.Date },
            };

            var mdlEmployeeList = new List<Model.mdlEmployee>();

            DataTable dtEmployee = Manager.DataFacade.DTSQLCommand(@"SELECT   EmployeeID, 
			                                                                        EmployeeName,
                                                                                    EmployeeTypeID
                                                                                    from Employee
				                                                            Where BranchID IN (" + branch + ") and (EmployeeID LIKE '%" + keyword + "%' or EmployeeName LIKE '%" + keyword + "%') and (OutDate = '2000-01-01' or OutDate = '1900-01-01' or OutDate >= @DateNow) AND EmployeeTypeID='0'", sp);

            foreach (DataRow row in dtEmployee.Rows)
            {
                var mdlEmployee = new Model.mdlEmployee();
                mdlEmployee.EmployeeID = row["EmployeeID"].ToString();
                mdlEmployee.EmployeeName = row["EmployeeName"].ToString();
                mdlEmployee.EmployeeTypeID = row["EmployeeTypeID"].ToString();

                mdlEmployeeList.Add(mdlEmployee);
            }
            return mdlEmployeeList;
        }

        public static Model.Employee GetEmployeeByID(string employeeID)
        {
            var lEmployee = DataContext.Employees.FirstOrDefault(fld => fld.EmployeeID.Equals(employeeID));
            return lEmployee;
        }
       

        //-------------------------------------------- Service Facade --------------------------------------------------------//


        public static List<string> AutoComplEmployee(string prefixText, int count, string flagfilter)
        {
            List<string> lEmployees = new List<string>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
                 new SqlParameter() {ParameterName = "@SearchText", SqlDbType = SqlDbType.NVarChar, Value = prefixText + "%"}
            };

           
            DataTable dtEmployee = Manager.DataFacade.DTSQLCommand("SELECT * FROM Employee where EmployeeID like @SearchText", sp);
            foreach (DataRow drEmployee in dtEmployee.Rows)
            {
                lEmployees.Add(drEmployee["EmployeeID"].ToString() + " - " + drEmployee["EmployeeName"].ToString());
            }
            
            return lEmployees;
        }

        //FERNANDES
        public static List<string> AutoComplEmployeeUserConf(string prefixText, int count, string flagfilter, string contextKey)
        {
            List<string> lEmployees = new List<string>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
                 new SqlParameter() {ParameterName = "@SearchText", SqlDbType = SqlDbType.NVarChar, Value = "%" + prefixText + "%"},
                 new SqlParameter() {ParameterName = "@SearchText2", SqlDbType = SqlDbType.NVarChar, Value = "%" + contextKey + "%"},
                 new SqlParameter() {ParameterName = "@DateNow", SqlDbType = SqlDbType.DateTime, Value = DateTime.Now.Date },
            };

            DataTable dtEmployee = Manager.DataFacade.DTSQLCommand("SELECT * FROM Employee where (EmployeeID like @SearchText or EmployeeName like @SearchText) AND (BranchID like @SearchText2) and (OutDate = '2000-01-01' or OutDate = '1900-01-01' or OutDate >= @DateNow) AND EmployeeTypeID='0'", sp);

            foreach (DataRow drEmployee in dtEmployee.Rows)
            {
                lEmployees.Add(drEmployee["EmployeeID"].ToString() + " - " + drEmployee["EmployeeName"].ToString());
            }

            return lEmployees;
        }
        
        public static Model.mdlEmployeeList LoadEmployee()
        {
            var mdlEmployeeListnew = new Model.mdlEmployeeList();
            var mdlEmployeeList = new List<Model.mdlEmployee>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
            };

            DataTable dtEmployee = Manager.DataFacade.DTSQLCommand("SELECT * FROM Employee", sp);

            foreach (DataRow drEmployee in dtEmployee.Rows)
            {
                var mdlEmployee = new Model.mdlEmployee();
                mdlEmployee.EmployeeID = drEmployee["EmployeeID"].ToString();
                mdlEmployee.EmployeeName = drEmployee["EmployeeName"].ToString();
                mdlEmployee.EmployeeTypeID = drEmployee["EmployeeTypeID"].ToString();
                mdlEmployeeList.Add(mdlEmployee);
            }

            mdlEmployeeListnew.EmployeeList = mdlEmployeeList;
            return mdlEmployeeListnew;
        }

        public static List<Model.mdlEmployee> LoadEmployeelistReport(string lBranch)
        {
            List<String> listEmployee = new List<String>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
                 new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = lBranch},
                 new SqlParameter() {ParameterName = "@DateNow", SqlDbType = SqlDbType.DateTime, Value = DateTime.Now.Date },
            };


            var lEmployeelist = new List<Model.mdlEmployee>();
            DataTable dtEmployee = Manager.DataFacade.DTSQLCommand("SELECT EmployeeID, EmployeeName FROM Employee where BranchID = @BranchID and (OutDate = '2000-01-01' or OutDate = '1900-01-01' or OutDate >= @DateNow) AND EmployeeTypeID='0'", sp);
            foreach (DataRow drEmployee in dtEmployee.Rows)
            {
                var lEmployee = new Model.mdlEmployee();
                lEmployee.EmployeeID = drEmployee["EmployeeID"].ToString();
                lEmployee.EmployeeName = drEmployee["EmployeeID"].ToString() + " - " + drEmployee["EmployeeName"].ToString();
                lEmployeelist.Add(lEmployee);
            }
            return lEmployeelist;
        }

        //001
        public static List<Model.mdlEmployee> LoadEmployeelistReport2(string lEmployee, string lBranch)
        {
            List<String> listEmployee = new List<String>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@Employee", SqlDbType = SqlDbType.NVarChar, Value = "%" + lEmployee + "%"},
                new SqlParameter() {ParameterName = "@BranchID", SqlDbType = SqlDbType.NVarChar, Value = lBranch},
                new SqlParameter() {ParameterName = "@DateNow", SqlDbType = SqlDbType.DateTime, Value = DateTime.Now.Date },

            };

            var lEmployeelist = new List<Model.mdlEmployee>();
            DataTable dtEmployee = Manager.DataFacade.DTSQLCommand("SELECT EmployeeID, EmployeeName FROM Employee WHERE (EmployeeID LIKE @Employee OR EmployeeName LIKE @Employee) AND BranchID = @BranchID and (OutDate = '2000-01-01' or OutDate = '1900-01-01' or OutDate >= @DateNow) AND EmployeeTypeID='0'", sp);

            foreach (DataRow drEmployee in dtEmployee.Rows)
            {
                var mdllEmployee = new Model.mdlEmployee();
                mdllEmployee.EmployeeID = drEmployee["EmployeeID"].ToString();
                mdllEmployee.EmployeeName = drEmployee["EmployeeID"].ToString() + " - " + drEmployee["EmployeeName"].ToString();
                lEmployeelist.Add(mdllEmployee);
            }
            return lEmployeelist;
        }
    }
}
