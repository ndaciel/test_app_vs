/* documentation
 *001 nanda  13 okt 2016
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Core.Manager
{

    public class CompanyFacade : Base.Manager
    {
        public static Model.mdlCompanyList LoadCompany()
        {
            var mdlCompanyListnew = new Model.mdlCompanyList();
            var mdlCompanyList = new List<Model.mdlCompany>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
            };

            DataTable dtCompany = Manager.DataFacade.DTSQLCommand("SELECT * FROM Company", sp);

            foreach (DataRow drCompany in dtCompany.Rows)
            {
                var mdlCompany = new Model.mdlCompany();
                mdlCompany.CompanyID = drCompany["CompanyID"].ToString();
                mdlCompany.CompanyName = drCompany["CompanyName"].ToString();

                mdlCompanyList.Add(mdlCompany);
            }

            mdlCompanyListnew.CompanyList = mdlCompanyList;
            return mdlCompanyListnew;
        }

        //--001 
        public static List<Model.mdlCompany> LoadCompany2()
        {
            var mdlCompanyList = new List<Model.mdlCompany>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
            };

            DataTable dtCompany = Manager.DataFacade.DTSQLCommand("SELECT * FROM Company", sp);

            foreach (DataRow drCompany in dtCompany.Rows)
            {
                var mdlCompany = new Model.mdlCompany();
                mdlCompany.CompanyID = drCompany["CompanyID"].ToString();
                mdlCompany.CompanyName = drCompany["CompanyName"].ToString();

                mdlCompanyList.Add(mdlCompany);
            }


            return mdlCompanyList;
        }
        //--001
    }
}
