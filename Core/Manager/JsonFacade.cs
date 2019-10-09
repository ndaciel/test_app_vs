/* documentation
 * 001 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Core.Manager;


namespace Core.Manager
{
    public class JsonFacade : Base.Manager
    {

        //006 OPTIMAZE
        public static Model.mdlJsonList LoadJson(Model.mdlParam json)
        {
            //001 
            var mdlJson = new Model.mdlJson();
            var mdlJsonList = new List<Model.mdlJson>();

            var mdlCallPlan = CallPlanFacade.LoadCallPlan(json);
            var mdlCallPlanDetail = CallPlanFacade.LoadCallPlanDetail(json);

            //--006
            var mdlCustomerList = new List<Model.mdlCustomer>();
            var mdlCustomerTypeList = new List<Model.mdlCustomerType>();
            //mdlProductList = new List<Model.mdlProduct>();  //005 Close Code

            //--002
            var mdlCost = new List<Model.mdlCost>();
            var mdlDeliveryOrderList = new List<Model.mdlDeliveryOrder>();
            var mdlDeliveryOrderDetailList = new List<Model.mdlDeliveryOrderDetail>();
            //002--

            var mdlProductList = new List<Model.mdlProduct>();

            var mdlDailyMsgList = new List<Model.mdlDailyMsg>(); //003
            var mdlDailyCostList = new List<Model.mdlDailyCost>();

            var mdlReturOrderList = new List<Model.mdlReturOrder>();
            var mdlReturOrderDetailList = new List<Model.mdlReturOrderDetail>();

            var mdlReasonList = new List<Model.mdlReason>();

            var mdlProductUOMList = new List<Model.mdlProductUOM>(); //007

          
            var mdlMobileConfigList = new List<Model.mdlMobileConfig>();

  
            var mdlWarehouseList = new List<Model.mdlWarehouse>();

            var mdlUnvalidDOProductList = new List<Model.mdlDeliveryOrderDetail>();

            var mdlVisit = new List<Model.mdlDBVisit>();

            var mdlVisitDetailList = new List<Model.mdlDBVisitDetail2>();

            //Wyeth
            var mdlPromoList = new List<Model.mdlPromo>();

            var mdlCompetitorActivityList = new List<Model.mdlCompetitorActivity>();

            var mdlCompetitor = new List<Model.mdlCompetitor>();

            var mdlPhotoTypeList = new List<Model.mdlPhotoType>();

            var mdlCompetitorProduct = new List<Model.mdlCompetitorProduct>();

            var mdlMenuMobileList = new List<Model.mdlMenuMobile>();

            var mdlPOSMProductList = new List<Model.mdlPOSM>();

            var mdlSisaStockType = new List<Model.mdlSisaStockType>();

            var mdlQuestion = new List<Model.mdlQuestion>();

            var mdlAnswer = new List<Model.mdlAnswer>();

            var mdlAnswerType = new List<Model.mdlAnswer_Type>();

            var mdlQuestionSet = new List<Model.mdlQuestion_Set>();

            var mdlQuestionCategory = new List<Model.mdlQuestion_Category>();

            var mdlCompetitorActivityType = new List<Model.mdlCompetitorActivityType>();

            var mdlDepositType = new List<Model.mdlDepositType>();
            //if (mdlCallPlan.Count > 0) -- fernandes

            //fernandes
            foreach(var cp in mdlCallPlan)
            {
                if(cp.Result == "FAILED" || mdlCallPlanDetail.Count == 0)
                {
                    mdlJson.Result = "Tidak ada call plan";
                }
                else if(cp.Result == "FINISH")
                {
                    mdlJson.Result = "Call plan sudah selesai";
                }
                else
                {
                mdlCustomerList = CustomerFacade.LoadCustomer(json);
                mdlCustomerTypeList = CustomerTypeFacade.LoadCustomerType();

                //mdlDeliveryOrderList = DeliveryOrderFacade.LoadDeliveryOrder(json);
                //mdlDeliveryOrderDetailList = DeliveryOrderFacade.LoadDeliveryOrderDetail(json, mdlDeliveryOrderList);

                //mdlUnvalidDOProductList = DeliveryOrderFacade.UnvalidDOProduct(mdlDeliveryOrderList);

                mdlProductList = ProductFacade.LoadProduct(json);

                //var mdlProductList = ProductFacade.LoadProduct();  //005 close code

                //mdlReturOrderList = ReturFacade.LoadReturOrder(json);
                //mdlReturOrderDetailList = ReturFacade.LoadReturOrderDetail(json);

                mdlCost = CostFacade.LoadCost();  //002

                mdlReasonList = ReasonFacade.LoadReason();
                //christopher
                mdlDailyMsgList = DailyMessageFacade.LoadDailyMessage2(json); //003
                //christopher
                mdlProductUOMList = ProductFacade.LoadProductUOM(mdlDeliveryOrderDetailList); //007

                //christopher
                mdlMobileConfigList = MobileConfigFacade.LoadMobileConfig(json);

                //christopher
                mdlWarehouseList = WarehouseFacade.LoadWarehouse(mdlCallPlan);

                mdlVisit = VisitFacade.LoadVisit(mdlCallPlan);
                mdlVisitDetailList = VisitFacade.LoadVisitDetail(mdlVisit);

                mdlDailyCostList = CostFacade.LoadDailyCost(mdlVisit);

                //wyeth

                mdlPromoList = PromoFacade.LoadPromo(json);

                //mdlCompetitorActivityList = CompetitorFacade.LoadCompetitorActivity(json);

                //mdlCompetitor = CompetitorFacade.LoadCompetitor(json);

                //mdlCompetitorProduct = CompetitorFacade.LoadCompetitorProduct(json, mdlCompetitor);

                mdlMenuMobileList = MenuFacade.LoadMenuMobile(json.Role);

                mdlPOSMProductList = POSMFacade.LoadPOSMProduct();

                mdlSisaStockType = SisaStockTypeFacade.LoadSisaStockType(json);

                //mdlCompetitorActivityType = CompetitorActivityTypeFacade.LoadCompetitorActivityType(json);

                mdlPhotoTypeList = PhotoTypeFacade.LoadPhotoType();

                mdlQuestion = QuestionFacade.LoadQuestion(json);

                //mdlAnswer = AnswerFacade.LoadAnswer();

                //mdlAnswerType = AnswerFacade.LoadAnswerType();

                //mdlQuestionSet = QuestionFacade.LoadQuestionSet();

                //mdlQuestionCategory = QuestionFacade.LoadQuestionCategory();

                mdlDepositType = DepositTypeFacade.LoadDepositType();

                mdlJson.Result = "Unduh berhasil";
                }
            }
            //006--

            //var mdlVisitList = VisitFacade.LoadVisit(json);
            //var mdlVisitDetailList = VisitFacade.LoadVisitDetail(json);


            mdlJson.CustomerList = mdlCustomerList;
            mdlJson.CustomerTypeList = mdlCustomerTypeList;

            mdlJson.DeliveryOrderList = mdlDeliveryOrderList;
            mdlJson.DeliveryOrderDetailList = mdlDeliveryOrderDetailList;

            mdlJson.ProductList = mdlProductList;
            //mdlJson.ProductList = mdlProductList; //05 Close Code

            mdlJson.ReturOrderList = mdlReturOrderList;
            mdlJson.ReturOrderDetailList = mdlReturOrderDetailList;

            mdlJson.CallPlanList = mdlCallPlan;
            mdlJson.CallPlanDetailList = mdlCallPlanDetail;

            mdlJson.CostList = mdlCost; //002

            mdlJson.DailyMessageList = mdlDailyMsgList; //003

            mdlJson.ReasonList = mdlReasonList;

            mdlJson.ProductUOMList = mdlProductUOMList; //007

            //christopher
            mdlJson.MobileConfigList = mdlMobileConfigList;

            mdlJson.WarehouseList = mdlWarehouseList;

            mdlJson.UnvalidDOProductList = mdlUnvalidDOProductList;

            mdlJson.Visit = mdlVisit;
            mdlJson.VisitDetailList = mdlVisitDetailList;

            mdlJson.DailyCostList = mdlDailyCostList;

            //wyeth

            mdlJson.PromoList = mdlPromoList;
            //mdlJson.CompetitorActivityList = mdlCompetitorActivityList;
            mdlJson.CompetitorList = mdlCompetitor;
            mdlJson.SisaStockTypeList = mdlSisaStockType;
            mdlJson.CompetitorActivityTypeList = mdlCompetitorActivityType;
            mdlJson.CompetitorProductList = mdlCompetitorProduct;
            mdlJson.MenuMobileList = mdlMenuMobileList;
            mdlJson.POSMProductList = mdlPOSMProductList;
            mdlJson.PhotoTypeList = mdlPhotoTypeList;
            mdlJson.QuestionList = mdlQuestion;
            mdlJson.AnswerList = mdlAnswer;
            mdlJson.AnswerTypeList = mdlAnswerType;
            mdlJson.QuestionSetList = mdlQuestionSet;
            mdlJson.QuestionCategoryList = mdlQuestionCategory;

            mdlJson.DepositTypeList = mdlDepositType;

            //mdlJson.VisitList = mdlVisitList;
            //mdlJson.VisitDetailList = mdlVisitDetailList;

            var mdlJsonListNew = new Model.mdlJsonList();
            mdlJsonList.Add(mdlJson);
            mdlJsonListNew.mdlJson = mdlJsonList;

            return mdlJsonListNew;
            //001
        }


        //christopher
        public static Model.mdlResult InsertAndroidKey(Model.mdlSaveAndroidKeyParam param)
        {
            var mdlResult = new Model.mdlResult();
            var checkEmployee = AndroidKeyFacade.CheckEmployee(param);
            if (checkEmployee.EmployeeID == null || checkEmployee.EmployeeID == "")
            {
                mdlResult.Result = Manager.AndroidKeyFacade.InsertAndroidKey(param);
            }
            else
            {
                mdlResult.Result = Manager.AndroidKeyFacade.UpdateAndroidKey(param);
            }
            return mdlResult;
        }

        public static Model.mdlResultSV SetUserConfig(Core.Model.mdlSetDeviceIDParam param)
        {
            var mdlResult = new Model.mdlResultSV();
            mdlResult.Title = "Set User Config";
            var mdlUserConfig = new Model.mdlSetDeviceID();

            mdlUserConfig = UserConfigFacade.SetUserConfig(param);
            if (mdlUserConfig.EmployeeID == null || mdlUserConfig.Uid == "")
            {
                UserConfigFacade.UploadUid(param);
                mdlUserConfig = UserConfigFacade.SetUserConfig(param);
            }

            if (mdlUserConfig.EmployeeID == null || mdlUserConfig.EmployeeID == "")
            {

                mdlUserConfig.EmployeeID = "";
                mdlUserConfig.BranchID = "";
                mdlUserConfig.BranchName = "";
                mdlUserConfig.VehicleNumber = "";
                mdlUserConfig.IpLocal = "";
                mdlUserConfig.PortLocal = "";
                mdlUserConfig.IpPublic = "";
                mdlUserConfig.PortPublic = "";
                mdlUserConfig.IpAlternative = "";
                mdlUserConfig.PortAlternative = "";
                mdlUserConfig.Password = "";
                mdlUserConfig.EmployeeName = "";
                mdlUserConfig.Version = "";

                mdlResult.StatusCode = "00";
                mdlResult.StatusMessage = "User Config Not Found";
                mdlResult.Value = mdlUserConfig;

                //string json = Services.RestPublisher.Serialize(mdlResult);
                //var result = Services.RestPublisher.Deserialize<Model.mdlResultSV>(json);
                //return result;
            }
            else
            {
                var mdlparamAndroidKey = new Model.mdlSaveAndroidKeyParam();
                mdlparamAndroidKey.AndroidKey = param.token;
                mdlparamAndroidKey.EmployeeID = mdlUserConfig.EmployeeID;
                mdlparamAndroidKey.BranchID = mdlUserConfig.BranchID;
                //var mobileConfig = MobileConfigFacade.GetMobileConfigbyID(mdlUserConfig.BranchID);

                var paramNew = new Model.mdlParam();
                paramNew.BranchID = mdlUserConfig.BranchID;
                paramNew.EmployeeID = mdlUserConfig.EmployeeID;

                var mobileConfig = MobileConfigFacade.LoadMobileConfig(paramNew);
                var mobileConfigVersion = mobileConfig.FirstOrDefault(fld => fld.ID.Equals("VERSION"));
                if (mobileConfigVersion != null)
                    mdlUserConfig.Version = mobileConfigVersion.Value;
                //FERNANDES-PasswordResetUpdate
                var mobileConfigPasswordReset = mobileConfig.FirstOrDefault(fld => fld.ID.Equals("PASSWORDRESET"));
                if (mobileConfigPasswordReset != null)
                    mdlUserConfig.PasswordReset = mobileConfigPasswordReset.Value;

                var employee = EmployeeFacade.GetEmployeeByID(mdlUserConfig.EmployeeID);
                if (employee != null)
                    mdlUserConfig.EmployeeName = employee.EmployeeName;

                string res = string.Empty;
                var checkEmployee = AndroidKeyFacade.CheckEmployee(mdlparamAndroidKey);
                if (checkEmployee.EmployeeID == null || checkEmployee.EmployeeID == "")
                {
                    res = Manager.AndroidKeyFacade.InsertAndroidKey(mdlparamAndroidKey);
                }
                else
                {
                    res = Manager.AndroidKeyFacade.UpdateAndroidKey(mdlparamAndroidKey);
                }

                mdlResult.StatusCode = "01";
                mdlResult.StatusMessage = "Set User Config Success";
                mdlResult.Value = mdlUserConfig;
                
            }

            string json = Services.RestPublisher.Serialize(mdlResult);
            var result = Services.RestPublisher.Deserialize<Model.mdlResultSV>(json);
            return mdlResult;

        }


        //christopher
        public static Model.mdlDailyMsgList GetDailyMessage(Model.mdlParam param)
        {

            var mdlDailyMsgList = new Model.mdlDailyMsgList();
            mdlDailyMsgList.DailyMessageList = DailyMessageFacade.LoadDailyMessage2(param);

            return mdlDailyMsgList;
        }

        public static Model.mdlCheckinCourierRadius CheckinCourierRadius(Core.Model.mdlCheckinCourierRadiusParam param)
        {
            return CheckinRadiusFacade.CheckinCourierRadius(param);
        }

        public static Model.mdlResult PushNotificationConfirmation(Core.Model.mdlPushNotificationConfirmationParam param)
        {
            return PushNotificationFacade.PushNotificationConfirmation(param);
        }


        public static Core.Model.mdlResultList UploadJson(Core.Model.mdlUploadJsonParam lParamlist)
        {
            return Core.Manager.UploadFacade.UploadJson(lParamlist);
        }

    }
}
