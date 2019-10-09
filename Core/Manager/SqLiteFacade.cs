using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Core.Model;
//using System.Net.Http;
using System.Configuration;
using System.Web;

namespace Core.Manager
{
    public class SqlLiteFacade
    {
        private static string UploadSqlLitePath = ConfigurationManager.AppSettings["UploadSqlLitePath"];
        public static Model.mdlResult UploadSqlLite(Stream data)
        {
            var mdlResult = new mdlResult();

            HttpMultipartParser parser = new HttpMultipartParser(data, "file");
            try
            {
                if (parser.Success)
                {
                    // Save the image somewhere
                    // File.WriteAllBytes("C://inetpub//wwwroot//TIKIServiceMysql//SqlLite//" + parser.Filename, parser.FileContents);
                    File.WriteAllBytes(UploadSqlLitePath + parser.Filename, parser.FileContents);

                    mdlResult.Result = "SUCCESS";
                }
                else
                {
                    mdlResult.Result = "ERROR: Terjadi Kesalahan Convert File SqlLite";
                }

            }
            catch (Exception ex)
            {
                String lEx = ex.ToString().Substring(0, 200);
                mdlResult.Result = "ERROR : " + lEx;
            }

            return mdlResult;
        }
    }
}
