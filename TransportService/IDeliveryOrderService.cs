using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;
using System.ServiceModel.Web;

namespace TransportService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDeliveryOrderService" in both code and config file together.
    [ServiceContract]
    public interface IDeliveryOrderService
    {
        //001 
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/getJson")]
        Stream GetJson(Core.Model.mdlParam json);
        //001

        //002
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/UpdateRetur")]
        Core.Model.mdlResultList UpdateRetur(List<Core.Model.mdlReturOrderParam> json);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/UpdateReturDetail")]
        Core.Model.mdlResultList UpdateReturDetail(List<Core.Model.mdlReturOrderDetailParam> json);
        //002

        //003
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/InsertCustImage")]
        Core.Model.mdlResultList InsertCustomerImage(List<Core.Model.mdlCustomerImageParam> json);
        //003

        //--004
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/UpdateDO")]
        Core.Model.mdlResultList UpdateDeliveryOrder(List<Core.Model.mdlDeliveryOrderParam> lDOParamlist);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/UpdateDODetail")]
        Core.Model.mdlResultList UpdateDeliveryOrderDetail(List<Core.Model.mdlDeliveryOrderDetailParam> lDODetailParamlist);
        //004--

        //--005
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/InsertVisit")]
        Core.Model.mdlResultList InsertVisit(List<Core.Model.mdlVisitParam> lVisitParamlist);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/InsertVisitDetail")]
        Core.Model.mdlResultList InsertVisitDetail(List<Core.Model.mdlVisitDetailParamNew> lVisitDetailParamlist); //005--

        //--006
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/InsertDailyCost")]
        Core.Model.mdlResultList InsertDailyCost(List<Core.Model.mdlDailyCostParam> lParamlist); //006--

        //--007,010
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/InsertTracking")]
        Core.Model.mdlResultList InsertTracking(Core.Model.mdlTrackingParam lParam); //007,010--

        //--008
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/InsertReturOrder")]
        Core.Model.mdlResultList InsertReturOrder(List<Core.Model.mdlReturOrderParam> lParamlist);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/InsertReturOrderDetail")]
        Core.Model.mdlResultList InsertReturOrderDetail(List<Core.Model.mdlReturOrderDetailParam> lParamlist);
  
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/insertandroidkey")]
        Core.Model.mdlResult InsertAndroidKey(Core.Model.mdlSaveAndroidKeyParam param);
        //010
        //christopher
        //011 
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/getdailymessage")]
        Core.Model.mdlDailyMsgList GetDailyMessage(Core.Model.mdlParam param);
        //011

        //christopher
        //011 
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/getmobileconfig")]
        List<Core.Model.mdlMobileConfig> GetMobileConfig(Core.Model.mdlParam param);
        //011


        //christopher
        //011 
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/setuserconfig")]
        Core.Model.mdlResultSV SetUserConfig(Core.Model.mdlSetDeviceIDParam param);
        //011


        ////christopher
        ////011 
        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/getuserconfig")]
        //List<Core.Model.mdlMobileConfig> GetUserConfig(Core.Model.mdlParam param);
        ////011

        //christopher
        //012 
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/pushnotificationconfirmation")]
        Core.Model.mdlResult PushNotificationConfirmation(Core.Model.mdlPushNotificationConfirmationParam param);
        //012


        //013 
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/checkincourierradius")]
        Core.Model.mdlCheckinCourierRadius CheckinCourierRadius(Core.Model.mdlCheckinCourierRadiusParam param);
        //013

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/uploadjson")]
        Core.Model.mdlResultList UploadJson(Core.Model.mdlUploadJsonParam lParamlist);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/UploadSqlLite")]
        Core.Model.mdlResult UploadSqLite(Stream data);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/checkversion")]
        Core.Model.mdlResult CheckVersion(Core.Model.mdlParam param);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/insertlogvisit")]
        Core.Model.mdlResultList InsertLogVisit(List<Core.Model.mdlLogVisitParam> param);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/checkconnection")]
        Core.Model.mdlResult CheckConnection();

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/insertcostvisit")]
        Core.Model.mdlResultList InsertCostVisit(List<Core.Model.mdlCostVisit> param);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/LoadVehicle")]
        Core.Model.mdlVehicleList LoadVehicleByBranch(Core.Model.mdlVehicleBranchParam param);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/UploadBBMRatio")]
        Core.Model.mdlResultList InsertBBMRatio(List<Core.Model.mdlBBMRatioParam> lParamlist);

        //wyeth

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/getlogin")]
        Stream GetLogin(Core.Model.mdlLoginParam param);


        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/InsertSisaStockVisit")]
        Core.Model.mdlResultList InsertSisaStockVisit(List<Core.Model.mdlSisaStockVisit> lSisaStockVisit);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/InsertCompetitorActivityVisit")]
        Core.Model.mdlResultList InsertCompetitorActivityVisit(List<Core.Model.mdlCompetitorActivityVisit> lCompetitorActivityVisit);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/InsertCompetitorActivityImage")]
        Core.Model.mdlResultList InsertCompetitorActivityImage(List<Core.Model.mdlCompetitorActivityImage> lCompetitorActivityImage);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/InsertPromoCompetitor")]
        Core.Model.mdlResultList InsertPromoCompetitor(List<Core.Model.mdlPromoCompetitor> lPromoCompetitor);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/InsertPromoVisit")]
        Core.Model.mdlResultList InsertPromoVisit(List<Core.Model.mdlPromoVisit> lPromoVisit);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/InsertPOSMVisit")]
        Core.Model.mdlResultList InsertPOSMVisit(List<Core.Model.mdlPOSMVisit> lPOSMVisit);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/InsertPOSMStock")]
        Core.Model.mdlResultList InsertPOSMStock(List<Core.Model.mdlPOSMStock> lPOSMStock);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/InsertCustomerImageType")]
        Core.Model.mdlResultList InsertCustomerImageType(List<Core.Model.mdlCustomerImageType> lCustomerImageType);

        [OperationContract]
        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/writeSurveyImage")]
        Core.Model.mdlResult WriteSurveyImage(Stream stream);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/InsertResultSurvey")]
        Core.Model.mdlResultList InsertResultSurvey(List<Core.Model.mdlResultSurvey> lResultSurvey);


        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/InsertResultSurveyDetail")]
        Core.Model.mdlResultList InsertResultSurveyDetail(List<Core.Model.mdlResultSurveyDetail> lResultSurveyDetail);


        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/GetDate")]
        string GetDate();

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/UploadDeposit")]
        Core.Model.mdlResultList UploadDeposit(List<Core.Model.mdlDepositParam> lResultDeposit);

        //------------------------------------------------- Closed Code ------------------------------------------------------//

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/UpdateCallPlan")]
        //Core.Model.mdlResultList UpdateCallPlan(List<Core.Model.mdlCallPlanParam> json);

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/UpdateCallPlanDetail")]
        //Core.Model.mdlResultList UpdateCallPlanDetail(List<Core.Model.mdlCallPlanDetailParam> json);

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/InsertCustomer")]
        //Core.Model.mdlResult InsertCustomer(Core.Model.mdlCustomerParam lParam);

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/UpdateCustomer")]
        //Core.Model.mdlResult UpdateCustomer(Core.Model.mdlCustomerParam lParam);

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/DeleteCustomer")]
        //Core.Model.mdlResult DeleteCustomer(Core.Model.mdlCustomerParam lParam);

        //CustomerType
        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/LoadCustomerType")]
        //List<Core.Model.mdlCustomerType> LoadCustomerType();

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/InsertCustomerType")]
        //Core.Model.mdlResult InsertCustomerType(Core.Model.mdlCustomerTypeParam lParam);

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/UpdateCustomerType")]
        //Core.Model.mdlResult UpdateCustomerType(Core.Model.mdlCustomerTypeParam lParam);

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/DeleteCustomerType")]
        //Core.Model.mdlResult DeleteCustomerType(Core.Model.mdlCustomerTypeParam lParam);

        //Branch
        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/LoadBranch")]
        //Core.Model.mdlBranchList LoadBranch();

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/InsertBranch")]
        //Core.Model.mdlResult InsertBranch(Core.Model.mdlBranchParam lParam);

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/UpdateBranch")]
        //Core.Model.mdlResult UpdateBranch(Core.Model.mdlBranchParam lParam);

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/DeleteBranch")]
        //Core.Model.mdlResult DeleteBranch(Core.Model.mdlBranchParam lParam);

        //Company
        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/LoadCompany")]
        //Core.Model.mdlCompanyList LoadCompany();

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/InsertCompany")]
        //Core.Model.mdlResult InsertCompany(Core.Model.mdlCompanyParam lParam);

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/UpdateCompany")]
        //Core.Model.mdlResult UpdateCompany(Core.Model.mdlCompanyParam lParam);

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/DeleteCompany")]
        //Core.Model.mdlResult DeleteCompany(Core.Model.mdlCompanyParam lParam);

        //Product
        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/LoadProduct")]
        //Core.Model.mdlProductList LoadProduct();

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/InsertProduct")]
        //Core.Model.mdlResult InsertProduct(Core.Model.mdlProductParam lParam);

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/UpdateProduct")]
        //Core.Model.mdlResult UpdateProduct(Core.Model.mdlProductParam lParam);

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/DeleteProduct")]
        //Core.Model.mdlResult DeleteProduct(Core.Model.mdlProductParam lParam);

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/InsertEmployee")]
        //Core.Model.mdlResult InsertEmployee(Core.Model.mdlEmployeeParam lParam);

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/UpdateEmployee")]
        //Core.Model.mdlResult UpdateEmployee(Core.Model.mdlEmployeeParam lParam);

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/DeleteEmployee")]
        //Core.Model.mdlResult DeleteEmployee(Core.Model.mdlEmployeeParam lParam);


        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/InsertEmployeeType")]
        //Core.Model.mdlResult InsertEmployeeType(Core.Model.mdlEmployeeTypeParam lParam);

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/UpdateEmployeeType")]
        //Core.Model.mdlResult UpdateEmployeeType(Core.Model.mdlEmployeeTypeParam lParam);

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/DeleteEmployeeType")]
        //Core.Model.mdlResult DeleteEmployeeType(Core.Model.mdlEmployeeTypeParam lParam);

        //--FERNANDES
        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/LoadVehicle")]
        //Core.Model.mdlVehicleList LoadVehicle(); //FERNANDES--

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/InsertVehicle")]
        //Core.Model.mdlResult InsertVehicle(Core.Model.mdlVehicleParam lParam);

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/UpdateVehicle")]
        //Core.Model.mdlResult UpdateVehicle(Core.Model.mdlVehicleParam lParam);

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/DeletelVehicle")]
        //Core.Model.mdlResult DeleteVehicle(Core.Model.mdlVehicleParam lParam);

        //--FERNANDES
        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/LoadVehicleType")]
        //Core.Model.mdlVehicleTypeList LoadVehicleType(); //FERNANDES--

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/InsertVehicleType")]
        //Core.Model.mdlResult InsertVehicleType(Core.Model.mdlVehicleTypeParam lParam);

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/UpdateVehicleType")]
        //Core.Model.mdlResult UpdateVehicleType(Core.Model.mdlVehicleTypeParam lParam);

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/DeleteVehicleType")]
        //Core.Model.mdlResult DeleteVehicleType(Core.Model.mdlVehicleTypeParam lParam);

        //nanda
        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/LoadEmployee")]
        //Core.Model.mdlEmployeeList LoadEmployee();

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/LoadEmployeeType")]
        //Core.Model.mdlEmployeeTypeList LoadEmployeeType();
        //nanda

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/getVisit")]
        //Core.Model.mdlVisitList GetVisit(Core.Model.mdlParam json);

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/loadVisit")]
        //Core.Model.mdlVisitList LoadVisit();

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/getCustomer")]
        //Core.Model.mdlCustomerList GetCustomer(Core.Model.mdlParam json);


        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/insertDrs")]
        //Core.Model.mdlJsonList GetJson(Core.Model.mdlParam json);
        //Core.Model.mdlResultList InsertConNote(List<Core.Model.mdlDrsMobile> json);

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/getConnote")]
        //Core.Model.mdlDe_mdeList GetConnote(Core.Model.mdlDrsMobile json);

        //[OperationContract]
        //[WebInvoke(Method = "GET",
        //    ResponseFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.Wrapped,
        //    UriTemplate = "getPO")]
        //List<Core.Model.PurchaseOrder> ListPO();

        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json,
        //    RequestFormat = WebMessageFormat.Json,
        //    UriTemplate = "/insertPO")]
        //List<Core.Model.mdlPO> InsertPO(Core.Model.mdlPO json);

        //PurchaseOrder


    }


}
