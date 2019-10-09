using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Model
{
    public class mdlPush
    {
        public mdlNotification notification { get; set; }
        public List<string> registration_ids { get; set; }
        public string priority { get; set; }
        public mdlData data { get; set; }
        public bool delay_while_idle { get; set; }
        public bool content_available { get; set; }
    }

    public class mdlNotification
    {
        public string title { get; set; }
        public string body { get; set; }

        //public string text { get; set; }
    }

    public class mdlData
    {
        public string title { get; set; }
        public string msg { get; set; }
        public string notificationid { get; set; }

    }

    public class mdlFCMReturn
    {
        public int success { get; set; }
        public List<mdlFCMResult> results { get; set; }
    
    }

    public class mdlFCMResult 
    {
        public string error { get; set; }
    }

}
