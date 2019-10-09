using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml.Linq;

namespace Core.Common
{
    public class ApplicationData
    {
        //public static string GetConnectionSetting()
        //{
        //    string connectionString = "";
        //    XDocument doc = XDocument.Load(@"C:\MobileAdapter(Faber)\ISol.MobileSFA.Adapter.Bin\MobileSFA.bin\xmlServerConfig.xml");
        //    var dbServer = doc.Descendants("szDbServer").FirstOrDefault().Value;
        //    var dbName = doc.Descendants("szDbName").FirstOrDefault().Value;
        //    var userID = doc.Descendants("szDbUserId").FirstOrDefault().Value;
        //    var password = doc.Descendants("szDbPassword").FirstOrDefault().Value;
        //    connectionString = "Data Source="+dbServer+";Initial Catalog="+dbName+";User ID="+userID+";Password="+password;
        //    return connectionString;
        //}
        public static string ConnectionStrings = ConfigurationManager.ConnectionStrings["MainConnectionString"].ToString();
        //public static string ConnectionStrings = GetConnectionSetting();
    }
}
