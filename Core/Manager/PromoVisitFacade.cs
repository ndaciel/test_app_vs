using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Manager
{
    public class PromoVisitFacade
    {
        public static Model.mdlResultList InsertPromoVisit(List<Model.mdlPromoVisit> lPromoVisitParam)
        {
            var mdlResultList = new List<Model.mdlResult>();

            var mdlResult = new Model.mdlResult();



            mdlResult.Result = Manager.DataFacade.DTSQLListInsert(lPromoVisitParam, "PromoVisit");
            if (mdlResult.Result == "1")
            {
            }
            else
            {
                string ResultSubstring;

                if (mdlResult.Result.Length > 500)
                {
                    ResultSubstring = mdlResult.Result.Substring(0, 500);

                    mdlResult.Result = ResultSubstring;
                }
            }

            mdlResultList.Add(mdlResult);



            var mdlResultListnew = new Model.mdlResultList();
            mdlResultListnew.ResultList = mdlResultList;
            return mdlResultListnew;
        }
    }
}
