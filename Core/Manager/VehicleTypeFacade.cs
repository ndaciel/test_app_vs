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
    public class VehicleTypeFacade : Base.Manager
    {
       
        public static Model.mdlVehicleTypeList LoadVehicleType()
        {
            //var customerList = DataContext.Customers.Where(fld => fld.PlantID.Equals(json.BranchID)).ToList();
            var mdlVehicleTypeListnew = new Model.mdlVehicleTypeList();
            var mdlVehicleTypeList = new List<Model.mdlVehicleType>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
            };

            DataTable dtVehicleType = Manager.DataFacade.DTSQLCommand("SELECT * FROM VehicleType", sp);

            foreach (DataRow drVehicleType in dtVehicleType.Rows)
            {
                var mdlVehicleType = new Model.mdlVehicleType();

                mdlVehicleType.VehicleTypeID = drVehicleType["VehicleTypeID"].ToString();
                mdlVehicleType.VehicleTypeName = drVehicleType["VehicleTypeName"].ToString();

                mdlVehicleTypeList.Add(mdlVehicleType);
            }

            mdlVehicleTypeListnew.VehicleTypeList = mdlVehicleTypeList;
            return mdlVehicleTypeListnew;

        }
        
    }
}
