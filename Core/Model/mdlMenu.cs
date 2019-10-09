using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Model
{
    public class mdlMenu
    {
        public string role { get; set; }
        public string html { get; set; }
        public string menu { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string type { get; set; }
    }

    public class mdlSubMenu
    {
        public string menu { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string type { get; set; }
    }

    public class mdlMenu2
    {
        public string menuID { get; set; }
        public string type { get; set; }
    }

    public class mdlMenuMobile
    {
        public string Role { get; set; }
        public string ModuleID { get; set; }
        public string ModuleType { get; set; }
        public string IsActive { get; set; }
        public string Title { get; set; }
    }

    public class mdlLoginMenu
    {
        public List<Model.mdlMenuMobile> ListMenu { get; set; }

    }
}
