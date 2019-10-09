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
    public class BranchFacade : Base.Manager
    {
        //001
        //public static Model.mdlBranchList LoadBranch()
        //{
        //    //linq code
        //    //var customerList = DataContext.Customers.Where(fld => fld.PlantID.Equals(json.BranchID)).ToList();

        //    var mdlBranchListnew = new Model.mdlBranchList();
        //    var mdlBranchList = new List<Model.mdlBranch>();

        //    List<SqlParameter> sp = new List<SqlParameter>()
        //    {
        //    };

        //    DataTable dtBranch = Manager.DataFacade.DTSQLCommand("SELECT * FROM Branch", sp);

        //    foreach (DataRow drBranch in dtBranch.Rows)
        //    {
        //        var mdlBranch = new Model.mdlBranch();
        //        mdlBranch.BranchID = drBranch["BranchID"].ToString();
        //        mdlBranch.BranchDescription = drBranch["BranchDescription"].ToString();
        //        mdlBranch.CompanyID = drBranch["CompanyID"].ToString();

        //        mdlBranchList.Add(mdlBranch);
        //    }

        //    mdlBranchListnew.BranchList = mdlBranchList;
        //    return mdlBranchListnew;
        //}

        //002

        public static List<Model.mdlBranch> LoadBranch()
        {
            //linq code
            //var customerList = DataContext.Customers.Where(fld => fld.PlantID.Equals(json.BranchID)).ToList();

            
            var mdlBranchList = new List<Model.mdlBranch>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
            };

            DataTable dtBranch = Manager.DataFacade.DTSQLCommand("SELECT * FROM Branch", sp);

            foreach (DataRow drBranch in dtBranch.Rows)
            {
                var mdlBranch = new Model.mdlBranch();
                mdlBranch.BranchID = drBranch["BranchID"].ToString();
                mdlBranch.BranchDescription = drBranch["BranchName"].ToString();
                mdlBranch.CompanyID = drBranch["CompanyID"].ToString();

                mdlBranchList.Add(mdlBranch);
            }


            return mdlBranchList;
        }

        public static List<string> LoadAllBranch()
        {
            //linq code
            //var customerList = DataContext.Customers.Where(fld => fld.PlantID.Equals(json.BranchID)).ToList();

    

            List<SqlParameter> sp = new List<SqlParameter>()
            {
            };

            DataTable dtBranch = Manager.DataFacade.DTSQLCommand("SELECT * FROM Branch", sp);
            var listBranch = new List<string>();
            foreach (DataRow drBranch in dtBranch.Rows)
            {
               
                listBranch.Add(drBranch["BranchID"].ToString());
            }

            return listBranch;
        }

        public static List<string> LoadBranchByID(string branchid)
        {
            //linq code
            //var customerList = DataContext.Customers.Where(fld => fld.PlantID.Equals(json.BranchID)).ToList();
            branchid = StringFacade.NormalizedBranch(branchid);
            List<SqlParameter> sp = new List<SqlParameter>()
            {
            };

            DataTable dtBranch = Manager.DataFacade.DTSQLCommand("SELECT * FROM Branch where BranchID LIKE '%" + branchid + "%'", sp);
            var listBranch = new List<string>();
            foreach (DataRow drBranch in dtBranch.Rows)
            {
                listBranch.Add(drBranch["BranchID"].ToString());
            }

            return listBranch;
        }

        public static List<Model.mdlBranch> LoadBranch2(string branchid)
        {
            var mdlBranchList = new List<Model.mdlBranch>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
            };

            DataTable dtBranch = Manager.DataFacade.DTSQLCommand("SELECT * FROM Branch where BranchID LIKE '%"+branchid+"%' order by BranchName", sp);

            foreach (DataRow drBranch in dtBranch.Rows)
            {
                var mdlBranch = new Model.mdlBranch();
                mdlBranch.BranchID = drBranch["BranchID"].ToString();
                mdlBranch.BranchName = drBranch["BranchName"].ToString();
                mdlBranch.BranchDescription = drBranch["BranchDescription"].ToString();
                mdlBranch.CompanyID = drBranch["CompanyID"].ToString();
                mdlBranch.Latitude = drBranch["Latitude"].ToString();
                mdlBranch.Longitude = drBranch["Longitude"].ToString();

                mdlBranchList.Add(mdlBranch);
            }

            //mdlBranchListnew.BranchList = mdlBranchList;
            //return mdlBranchListnew;
            return mdlBranchList;
        }

        public static List<Model.mdlBranch> LoadSomeBranch(string branchid)
        {

            branchid = StringFacade.NormalizedBranch(branchid);

            var mdlBranchList = new List<Model.mdlBranch>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
            };

            DataTable dtBranch = Manager.DataFacade.DTSQLCommand("SELECT * FROM Branch where BranchID IN (" + branchid + ") order by BranchName", sp);

            foreach (DataRow drBranch in dtBranch.Rows)
            {
                var mdlBranch = new Model.mdlBranch();
                mdlBranch.BranchID = drBranch["BranchID"].ToString();
                mdlBranch.BranchName = drBranch["BranchName"].ToString();
                mdlBranch.BranchDescription = drBranch["BranchDescription"].ToString();
                mdlBranch.CompanyID = drBranch["CompanyID"].ToString();
                mdlBranch.Latitude = drBranch["Latitude"].ToString();
                mdlBranch.Longitude = drBranch["Longitude"].ToString();

                mdlBranchList.Add(mdlBranch);
            }

            //mdlBranchListnew.BranchList = mdlBranchList;
            //return mdlBranchListnew;
            return mdlBranchList;
        }

        //FOR LINQ MODEL
        public static List<Model.mdlBranch> LoadSomeBranch2(string keyword, string branchid)
        {
            var mdlBranchList = new List<Model.mdlBranch>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
            };

            DataTable dtBranch = Manager.DataFacade.DTSQLCommand("SELECT * FROM Branch where BranchID IN (" + branchid + ") and (BranchName LIKE '%"+keyword+"%')  order by BranchName", sp);

            foreach (DataRow drBranch in dtBranch.Rows)
            {
                var mdlBranch = new Model.mdlBranch();
                mdlBranch.BranchID = drBranch["BranchID"].ToString();
                mdlBranch.BranchName = drBranch["BranchName"].ToString();
                mdlBranch.BranchDescription = drBranch["BranchDescription"].ToString();
                mdlBranch.CompanyID = drBranch["CompanyID"].ToString();

                mdlBranchList.Add(mdlBranch);
            }

            //mdlBranchListnew.BranchList = mdlBranchList;
            //return mdlBranchListnew;
            return mdlBranchList;
        }

        

        

       

        public static List<Model.mdlBranch> GetSearch(string keywordBranch, string branchid)
        {
            var branchlist = DataContext.Branches.Where(fld => fld.BranchID.Contains(branchid) && fld.BranchName.Contains(keywordBranch)).OrderBy(fld => fld.BranchName).ToList();

            var mdlBranchList = new List<Model.mdlBranch>();
            foreach (var branch in branchlist)
            {
                var mdlBranch = new Model.mdlBranch();
                mdlBranch.BranchID = branch.BranchID;
                mdlBranch.BranchName = branch.BranchName;
                mdlBranch.BranchDescription = branch.BranchDescription;
                mdlBranch.CompanyID = branch.CompanyID;

                mdlBranchList.Add(mdlBranch);
            }

            return mdlBranchList;
        }

        //FERNANDES
        public static List<string> AutoComplBranch(string prefixText, int count, string flagfilter)
        {
            List<string> lBranchs = new List<string>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
                 new SqlParameter() {ParameterName = "@SearchText", SqlDbType = SqlDbType.NVarChar, Value = prefixText +'%'}
            };

            //if (flagfilter == "BranchID")
            //{
            DataTable dtEmployee = Manager.DataFacade.DTSQLCommand("SELECT BranchID FROM Branch where BranchID like @SearchText", sp);
            foreach (DataRow drEmployee in dtEmployee.Rows)
            {
                lBranchs.Add(drEmployee["BranchID"].ToString());
            }
            //}
            //else
            //{
            //    DataTable dtEmployee = Manager.DataFacade.DTSQLCommand("SELECT * FROM Employee where EmployeeID like @SearchText", sp);
            //    foreach (DataRow drEmployee in dtEmployee.Rows)
            //    {
            //        lEmployees.Add(drEmployee["EmployeeID"].ToString());
            //    }
            //}


            return lBranchs;
        }

        //public static List<string> AutoComplBranch(string prefixText, int count, string flagfilter)
        //{
        //    List<string> lBranchs = new List<string>();

        //    List<SqlParameter> sp = new List<SqlParameter>()
        //    {
        //         new SqlParameter() {ParameterName = "@SearchText", SqlDbType = SqlDbType.NVarChar, Value = prefixText +'%'}
        //    };

        //    //if (flagfilter == "BranchID")
        //    //{
        //    DataTable dtEmployee = Manager.DataFacade.DTSQLCommand("SELECT BranchID FROM Branch where BranchID like @SearchText", sp);
        //    foreach (DataRow drEmployee in dtEmployee.Rows)
        //    {
        //        lBranchs.Add(drEmployee["BranchID"].ToString());
        //    }
        //    //}
        //    //else
        //    //{
        //    //    DataTable dtEmployee = Manager.DataFacade.DTSQLCommand("SELECT * FROM Employee where EmployeeID like @SearchText", sp);
        //    //    foreach (DataRow drEmployee in dtEmployee.Rows)
        //    //    {
        //    //        lEmployees.Add(drEmployee["EmployeeID"].ToString());
        //    //    }
        //    //}


        //    return lBranchs;
        //}

        //001 Penambahan Try catch
        public static string InsertBranch(string lBranchID, string lBranchName, string lBranchDescription, string lCompanyID)
        {
            try
            {
                Model.Branch mdlBranch = new Model.Branch();
                mdlBranch.BranchID = lBranchID;
                mdlBranch.BranchName = lBranchName;
                mdlBranch.BranchDescription = lBranchDescription;
                mdlBranch.CompanyID = lCompanyID;
                DataContext.Branches.InsertOnSubmit(mdlBranch);
                DataContext.SubmitChanges();
                return "SQLSuccess";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }

        public static string UpdateBranch(string lBranchID, string lBranchName, string lBranchDescription, string lCompanyID)
        {
            try
            {
                var lBranch = GetBranchbyID(lBranchID);
                lBranch.BranchName = lBranchName;
                lBranch.BranchDescription = lBranchDescription;
                lBranch.CompanyID = lCompanyID;
                DataContext.SubmitChanges();
                return "SQLSuccess";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public static string DeleteBranch(string lBranchID)
        {
            try
            {
                var lBranch = GetBranchbyID(lBranchID);
                DataContext.Branches.DeleteOnSubmit(lBranch);
                DataContext.SubmitChanges();
                return "SQLSuccess";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        //001

        public static List<Model.Branch> GetBranch()
        {
            var lBranch = DataContext.Branches.ToList();
            return lBranch;
        }

        public static Model.Branch GetBranchbyID(string lBranchID)
        {
            var lBranch = DataContext.Branches.FirstOrDefault(fld => fld.BranchID.Equals(lBranchID));
            return lBranch;
        }

        public static Model.Branch GetBranchforUpdate(string lBranchID, string lID)
        {
            var lBranch = DataContext.Branches.FirstOrDefault(fld => fld.BranchID.Equals(lBranchID));
            return lBranch;
        }
    }
}
