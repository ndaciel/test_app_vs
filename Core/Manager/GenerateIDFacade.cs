using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Manager
{
    public class GenerateIDFacade
    {
        public static String GenerateIDSMSLog(String pLIKE)
        {
            string lTime = DateTime.Now.ToString("ddhhmmssff");
            string newID = pLIKE + lTime;

            return newID;
        }
    }
}
