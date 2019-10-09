/* documentation
 *001 nanda 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Core.Manager
{
    public class EmployeeTypeFacade : Base.Manager
    {
        public static Model.mdlEmployeeTypeList LoadEmployeeType()
        {
            var mdlEmployeeTypeListnew = new Model.mdlEmployeeTypeList();
            var mdlEmployeeTypeList = new List<Model.mdlEmployeeType>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
            };

            DataTable dtEmployeeType = Manager.DataFacade.DTSQLCommand("SELECT * FROM EmployeeType", sp);

            foreach (DataRow drEmployeeType in dtEmployeeType.Rows)
            {
                var mdlEmployeeType = new Model.mdlEmployeeType();
                mdlEmployeeType.EmployeeTypeID = drEmployeeType["EmployeeTypeID"].ToString();
                mdlEmployeeType.EmployeeTypeName = drEmployeeType["EmployeeTypeName"].ToString();
                mdlEmployeeType.Description = drEmployeeType["Description"].ToString();

                mdlEmployeeTypeList.Add(mdlEmployeeType);
            }

            mdlEmployeeTypeListnew.EmployeeTypeList = mdlEmployeeTypeList;
            return mdlEmployeeTypeListnew;
        }
    }
}
