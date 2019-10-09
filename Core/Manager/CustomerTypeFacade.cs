/* documentation
 *001 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Core.Manager
{
    public class CustomerTypeFacade:Base.Manager
    {
        public static List<Model.mdlCustomerType> LoadCustomerType()
        {
            var mdlCustomerTypeList = new List<Model.mdlCustomerType>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
            };

            DataTable dtCustomerType = Manager.DataFacade.DTSQLCommand("SELECT * FROM CustomerType", sp);

            foreach (DataRow drCustomerType in dtCustomerType.Rows)
            {
                var mdlCustomerType = new Model.mdlCustomerType();
                mdlCustomerType.CustomerTypeID = drCustomerType["CustomerTypeID"].ToString();
                mdlCustomerType.CustomerTypeName = drCustomerType["CustomerTypeName"].ToString();
                mdlCustomerType.Description = drCustomerType["Description"].ToString();
              
                mdlCustomerTypeList.Add(mdlCustomerType);
            }

            return mdlCustomerTypeList;

        }   
    }
}
