using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Core.Manager
{
    public class PhotoTypeFacade
    {
        public static List<Model.mdlPhotoType> LoadPhotoType()
        {
        
            var mdlPhotoTypeList = new List<Model.mdlPhotoType>();

            List<SqlParameter> sp = new List<SqlParameter>()
            {
            };

            DataTable dtPhotoType = Manager.DataFacade.DTSQLCommand("SELECT * FROM PhotoType", sp);

            foreach (DataRow drPhotoType in dtPhotoType.Rows)
            {
                var mdlPhotoType = new Model.mdlPhotoType();
                mdlPhotoType.PhotoTypeID = drPhotoType["PhotoTypeID"].ToString();
                mdlPhotoType.Category = drPhotoType["Category"].ToString();
                mdlPhotoType.Value = drPhotoType["Value"].ToString();

                mdlPhotoTypeList.Add(mdlPhotoType);
            }

           
            return mdlPhotoTypeList;
        }
    }
}
