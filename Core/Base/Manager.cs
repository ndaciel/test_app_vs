using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.Linq;


namespace Core.Base
{
    using Core.Model;
    using Core.Common;
    public abstract class Manager
    {

        public static LINQDataContext DataContext
        {
            get
            {
                //return DataContextFactory.GetScopedDataContext<MangaCloudAppContext>(null, ApplicationData.ConnectionStrings);
                return DataContextFactory.GetScopedDataContext<LINQDataContext>(null, ApplicationData.ConnectionStrings);
                

            }
        }

    }
}
