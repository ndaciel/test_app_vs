/* documentation
 * 001 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Model
{
    public class mdlJson
    {
        [DataMember]
        public List<mdlCustomer> CustomerList { get; set; }

        [DataMember]
        public List<mdlCustomerType> CustomerTypeList { get; set; }

        
        [DataMember]
        public List<mdlDeliveryOrder> DeliveryOrderList { get; set; }

        [DataMember]
        public List<mdlDeliveryOrderDetail> DeliveryOrderDetailList { get; set; }

        [DataMember]
        public List<mdlProduct> ProductList { get; set; }

        [DataMember]
        public List<mdlReturOrder> ReturOrderList { get; set; }  

        [DataMember]
        public List<mdlReturOrderDetail> ReturOrderDetailList { get; set; } 

        [DataMember]
        public List<mdlCallPlan> CallPlanList { get; set; }
        [DataMember]
        public List<mdlCallPlanDetail> CallPlanDetailList { get; set; } 

        [DataMember]
        public List<mdlCost> CostList { get; set; } 
        [DataMember]
        public List<mdlDailyMsg> DailyMessageList { get; set; } 

        [DataMember]
        public List<mdlReason> ReasonList { get; set; } 
        [DataMember]
        public List<mdlProductUOM> ProductUOMList { get; set; }

        //christopher
        [DataMember]
        public List<mdlMobileConfig> MobileConfigList { get; set; } //009
        //christopher
        [DataMember]
        public List<mdlWarehouse> WarehouseList { get; set; }

        //fernandes
        [DataMember]
        public string Result { get; set; }

        [DataMember]
        public List<mdlDeliveryOrderDetail> UnvalidDOProductList { get; set; }

        [DataMember]
        public List<mdlDBVisit> Visit { get; set; }

        [DataMember]
        public List<mdlDBVisitDetail2> VisitDetailList { get; set; }

        [DataMember]
        public List<mdlDailyCost> DailyCostList { get; set; }

        //wyeth
        [DataMember]
        public List<Model.mdlPromo> PromoList { get; set; }

        [DataMember]
        public List<Model.mdlCompetitorActivity> CompetitorActivityList { get; set; }

        [DataMember]
        public List<Model.mdlCompetitor> CompetitorList { get; set; }

        [DataMember]
        public List<Model.mdlSisaStockType> SisaStockTypeList { get; set; }

        [DataMember]
        public List<Model.mdlCompetitorProduct> CompetitorProductList { get; set; }

        [DataMember]
        public List<Model.mdlMenuMobile> MenuMobileList { get; set; }

        [DataMember]
        public List<Model.mdlPOSM> POSMProductList { get; set; }

        [DataMember]
        public List<Model.mdlCompetitorActivityType> CompetitorActivityTypeList { get; set; }

        [DataMember]
        public List<Model.mdlPhotoType> PhotoTypeList { get; set; }

        [DataMember]
        public List<Model.mdlQuestion> QuestionList { get; set; }

        [DataMember]
        public List<Model.mdlAnswer> AnswerList { get; set; }

        [DataMember]
        public List<Model.mdlAnswer_Type> AnswerTypeList { get; set; }

        [DataMember]
        public List<Model.mdlQuestion_Set> QuestionSetList { get; set; }

        [DataMember]
        public List<Model.mdlQuestion_Category> QuestionCategoryList { get; set; }

        [DataMember]
        public List<Model.mdlDepositType> DepositTypeList { get; set; }
        
    }

    public class mdlJsonList
    {
        public List<mdlJson> mdlJson { get; set; }
    }
}
