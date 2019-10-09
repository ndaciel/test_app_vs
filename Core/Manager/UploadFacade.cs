using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;


namespace Core.Manager
{
    public class UploadFacade
    {
        public static Core.Model.mdlResultList UploadJson(Core.Model.mdlUploadJsonParam lParamlist)
        {
            var mdlResultList = new Model.mdlResultList();
            var listResult = new List<Model.mdlResult>();
            var mdlResult = new Model.mdlResult();

            try
            {
                
                using (TransactionScope scope = new TransactionScope())
                {
                    int err = 0;
                    mdlResultList = DeliveryOrderFacade.UpdateDeliveryOrder(lParamlist.UploadJson.ListDeliveryOrder);
                    mdlResultList = DeliveryOrderFacade.UpdateDeliveryOrderDetail(lParamlist.UploadJson.ListDeliveryOrderDetail);
                    mdlResultList = VisitFacade.InsertVisit(lParamlist.UploadJson.ListVisit);
                    if (mdlResultList.ResultList.FirstOrDefault().Result.Contains("IDExist") || mdlResultList.ResultList.FirstOrDefault().Result == "0")
                    {
                        err = 1;
                    }
                   mdlResultList = VisitFacade.InsertVisitDetail(lParamlist.UploadJson.ListVisitDetail);
                   if (mdlResultList.ResultList.FirstOrDefault().Result.Contains("IDExist") || mdlResultList.ResultList.FirstOrDefault().Result == "0")
                   {
                       err = 1;
                   }
                   mdlResultList = CostFacade.InsertDailyCost(lParamlist.UploadJson.ListCost);
                   if (mdlResultList.ResultList.FirstOrDefault().Result.Contains("IDExist") || mdlResultList.ResultList.FirstOrDefault().Result == "0")
                   {
                       err = 1;
                   }
                   mdlResultList = CustomerImageFacade.InsertCustomerImage(lParamlist.UploadJson.ListCustomerImage);
                   if (mdlResultList.ResultList.FirstOrDefault().Result.Contains("IDExist") || mdlResultList.ResultList.FirstOrDefault().Result == "0")
                   {
                       err = 1;
                   }
                   mdlResultList = LogVisitFacade.InsertLogVisit(lParamlist.UploadJson.ListLogVisit);
                   if (mdlResultList.ResultList.FirstOrDefault().Result.Contains("IDExist") || mdlResultList.ResultList.FirstOrDefault().Result == "0")
                   {
                       err = 1;
                   }
                   if (err == 0)
                   {
                       mdlResult.Result = "1";
                       scope.Complete();
                   }
                   else
                   {
                       mdlResult.Result = "0";
                   }


                  
                }
                
                listResult.Add(mdlResult);
                mdlResultList.ResultList = listResult;

                return mdlResultList;
            }
            catch (TransactionAbortedException ex)
            {

                

                mdlResult.Result = "0";
                listResult.Add(mdlResult);
                mdlResultList.ResultList = listResult;

                return mdlResultList;
            }
        }
    }
}
