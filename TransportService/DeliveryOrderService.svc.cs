using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Core.Manager;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceModel.Web;
using System.Device.Location;
using System.Globalization;
using System.Configuration;
using Core.Model;


namespace TransportService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DeliveryOrderService" in code, svc and config file together.
    public class DeliveryOrderService : IDeliveryOrderService
    {
        public Stream GetJson(Core.Model.mdlParam json)
        {
            String nanda = "nanda";
            if (nanda == "")
            { nanda = ""; }
            else
            {
                nanda = "22";
            }
            string chris = "chris2";
            var result = new Core.Model.mdlResultSvc();
            result.Title = "Get Json Download";
            var resultJson = new Core.Model.mdlJsonList();
            resultJson = JsonFacade.LoadJson(json);
            if (chris == "")
            {
                //do nothing
            }
            nanda = "22324324234";

            if (resultJson.mdlJson.FirstOrDefault().CallPlanList.Count > 0)
            {
                result.StatusCode = "01";
                result.StatusMessage = "Success";
            }
            else if (nanda == "")
            {
                //do nothing
            }
            else
            {
                result.StatusCode = "00";
                result.StatusMessage = "Failed";
            }
            result.Value = resultJson;
            var strJson = Core.Services.RestPublisher.Serialize(resultJson);
            var size = System.Text.ASCIIEncoding.Unicode.GetByteCount(strJson) / 1024;
            string sizeKB = size.ToString() + " KB";

            Core.Manager.LogFacade.InsertLog("DownloadJson", json.EmployeeID, result.StatusMessage, json.BranchID, json.DeviceID, "", sizeKB);

            string serializeJson = Core.Services.RestPublisher.Serialize(result);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(serializeJson));
            return ms;
        }

        public Core.Model.mdlResult CheckConnection()
        {
            var result = new Core.Model.mdlResult();
            result.Result = "1";

            return result;
        }

        public Core.Model.mdlResultList InsertReturOrder(List<Core.Model.mdlReturOrderParam> lParamlist)
        {
            string serlJson = JsonConvert.SerializeObject(lParamlist);
            string lEmployeeID = lParamlist.FirstOrDefault().EmployeeID;
            var resultInsertRetur = ReturFacade.InsertReturOrder(lParamlist);
            string JsonResultlist = JsonConvert.SerializeObject(resultInsertRetur);
            Core.Manager.LogFacade.InsertLog("DownloadJson", lEmployeeID, JsonResultlist, lParamlist.FirstOrDefault().BranchID, "", "", "");

            return resultInsertRetur;
        }

        public Core.Model.mdlResultList InsertReturOrderDetail(List<Core.Model.mdlReturOrderDetailParam> lParamlist)
        {
            string serlJson = JsonConvert.SerializeObject(lParamlist);
            string lEmployeeID = lParamlist.FirstOrDefault().EmployeeID;



            var resultInsertReturDetail = ReturFacade.InsertReturOrderDetail(lParamlist);

            string JsonResultlist = JsonConvert.SerializeObject(resultInsertReturDetail);

            Core.Manager.LogFacade.InsertLog("DownloadJson", lEmployeeID, JsonResultlist, "", "", "", "");

            return resultInsertReturDetail;
        }
        public Core.Model.mdlResultList UpdateRetur(List<Core.Model.mdlReturOrderParam> lParamlist)
        {
            string serlJson = JsonConvert.SerializeObject(lParamlist);
            string lEmployeeID = lParamlist.FirstOrDefault().EmployeeID;
            var resultRetur = ReturFacade.UpdateReturOrder(lParamlist);
            string JsonResultlist = JsonConvert.SerializeObject(resultRetur);

            Core.Manager.LogFacade.InsertLog("UpdateReturOrder", lEmployeeID, JsonResultlist, lParamlist.FirstOrDefault().BranchID, "", "", "");

            return resultRetur;
        }

        public Core.Model.mdlResultList UpdateReturDetail(List<Core.Model.mdlReturOrderDetailParam> lParamlist)
        {
            string serlJson = JsonConvert.SerializeObject(lParamlist);
            string lEmployeeID = lParamlist.FirstOrDefault().EmployeeID;
            var resultReturDetail = ReturFacade.UpdateReturOrderDetail(lParamlist);
            string JsonResultlist = JsonConvert.SerializeObject(resultReturDetail);

            Core.Manager.LogFacade.InsertLog("UpdateReturOrderDetail", lEmployeeID, JsonResultlist, "", "", "", "");

            return resultReturDetail;
        }

        public Core.Model.mdlResultList InsertCustomerImage(List<Core.Model.mdlCustomerImageParam> lParamlist)
        {
            string serlJson = JsonConvert.SerializeObject(lParamlist);
            string lEmployeeID = lParamlist.FirstOrDefault().EmployeeID;



            var resultCustomerImage = CustomerImageFacade.InsertCustomerImage(lParamlist);

            string JsonResultlist = JsonConvert.SerializeObject(resultCustomerImage);


            var strJson = Core.Services.RestPublisher.Serialize(lParamlist);
            var size = System.Text.ASCIIEncoding.Unicode.GetByteCount(strJson) / 1024;
            string sizeKB = size.ToString() + " KB";
            Core.Manager.LogFacade.InsertLog("InsertCustomerImage", lEmployeeID, JsonResultlist, lParamlist.FirstOrDefault().BranchID, lParamlist.FirstOrDefault().deviceID, "", sizeKB);

            return resultCustomerImage;
        }

        public Core.Model.mdlResult WriteSurveyImage(Stream stream)
        {
            string UploadImagePath = ConfigurationManager.AppSettings["imagePath"];
            var lResult = new mdlResult();

            //for multiple files
            try
            {
                MultipleMultipartParser parser = new MultipleMultipartParser(stream);

                DateTime dateTime = DateTime.UtcNow.Date;
                string day = dateTime.ToString("dd");
                string month = dateTime.ToString("MM");
                string year = dateTime.ToString("yyyy");
                string newUploadImagePath = UploadImagePath + year + "//" + month + "//" + day + "//";

                string path = UploadImagePath + year + "//" + month + "//" + day;
                DirectoryInfo dir = Directory.CreateDirectory(path);

                if (parser != null && parser.Success)
                {
                    foreach (var content in parser.StreamContents)
                    {
                        if (content.IsFile) // if file
                        {
                            File.WriteAllBytes(newUploadImagePath + content.FileName, content.Data);
                            continue;
                        }
                    }

                    lResult.Result = "SUCCESS";
                }
                else
                {
                    lResult.Result = "ERROR: Terjadi Kesalahan Convert File-File Image";
                }
            }
            catch (Exception ex)
            {
                String lEx = ex.ToString().Substring(0, 200);
                lResult.Result = "ERROR : " + lEx;
            }

            return lResult;
        }


        public Core.Model.mdlResultList UpdateDeliveryOrder(List<Core.Model.mdlDeliveryOrderParam> lDOParamlist)
        {
            string lEmployeeID = lDOParamlist.FirstOrDefault().EmployeeID;
            var resultlist = DeliveryOrderFacade.UpdateDeliveryOrder(lDOParamlist);
            string JsonResultlist = JsonConvert.SerializeObject(resultlist);
 

            var strJson = Core.Services.RestPublisher.Serialize(lDOParamlist);
            var size = System.Text.ASCIIEncoding.Unicode.GetByteCount(strJson) / 1024;
            string sizeKB = size.ToString() + " KB";
            Core.Manager.LogFacade.InsertLog("UpdateDeliveryOrder", lEmployeeID, JsonResultlist, lDOParamlist.FirstOrDefault().BranchID, lDOParamlist.FirstOrDefault().deviceID, "", sizeKB);

            return resultlist;
        }

        public Core.Model.mdlResultList UpdateDeliveryOrderDetail(List<Core.Model.mdlDeliveryOrderDetailParam> lDODetailParamlist) 
        {
            string serlJson = JsonConvert.SerializeObject(lDODetailParamlist);
            string lEmployeeID = lDODetailParamlist.FirstOrDefault().EmployeeID;



            var resultDO = DeliveryOrderFacade.UpdateDeliveryOrderDetail(lDODetailParamlist);

            string JsonResultlist = JsonConvert.SerializeObject(resultDO);
            var strJson = Core.Services.RestPublisher.Serialize(lDODetailParamlist);
            var size = System.Text.ASCIIEncoding.Unicode.GetByteCount(strJson) / 1024;
            string sizeKB = size.ToString() + " KB";
            Core.Manager.LogFacade.InsertLog("UpdateDeliveryOrderDetail", lEmployeeID, JsonResultlist, lDODetailParamlist.FirstOrDefault().BranchID, lDODetailParamlist.FirstOrDefault().deviceID, "", sizeKB);

            return resultDO;
        }


        public Core.Model.mdlResultList InsertVisit(List<Core.Model.mdlVisitParam> lVisitParamlist)
        {
            //string serlJson = JsonConvert.SerializeObject(lVisitParamlist);
            string lEmployeeID = lVisitParamlist.FirstOrDefault().EmployeeID;



            var resultVisit = VisitFacade.UploadVisit(lVisitParamlist);

            string JsonResultlist = JsonConvert.SerializeObject(resultVisit);
            //--009
            //JsonResultlist = JsonResultlist.Substring(0, 500);
            //009--

            var strJson = Core.Services.RestPublisher.Serialize(lVisitParamlist);
            var size = System.Text.ASCIIEncoding.Unicode.GetByteCount(strJson) / 1024;
            string sizeKB = size.ToString() + " KB";
            Core.Manager.LogFacade.InsertLog("InsertVisit", lEmployeeID, JsonResultlist, lVisitParamlist.FirstOrDefault().BranchID, lVisitParamlist.FirstOrDefault().deviceID, "", sizeKB);

            return resultVisit;
        }

        public Core.Model.mdlResultList InsertLogVisit(List<Core.Model.mdlLogVisitParam> lVisitParamlist)
        {
            //string serlJson = JsonConvert.SerializeObject(lVisitParamlist);
            string lEmployeeID = lVisitParamlist.FirstOrDefault().EmployeeID;



            var resultVisit = LogVisitFacade.InsertLogVisit(lVisitParamlist);

            string JsonResultlist = JsonConvert.SerializeObject(resultVisit);
            //--009
            //JsonResultlist = JsonResultlist.Substring(0, 500);
            //009--

            var strJson = Core.Services.RestPublisher.Serialize(lVisitParamlist);
            var size = System.Text.ASCIIEncoding.Unicode.GetByteCount(strJson) / 1024;
            string sizeKB = size.ToString() + " KB";
            Core.Manager.LogFacade.InsertLog("InsertLogVisit", lEmployeeID, JsonResultlist, lVisitParamlist.FirstOrDefault().BranchID, lVisitParamlist.FirstOrDefault().deviceID, "", sizeKB);

            return resultVisit;
        }

        public Core.Model.mdlResultList InsertVisitDetail(List<Core.Model.mdlVisitDetailParamNew> lVisitDetailParamlist)
        {
            //string serlJson = JsonConvert.SerializeObject(lVisitDetailParamlist);
            string lEmployeeID = lVisitDetailParamlist.FirstOrDefault().EmployeeID;



            var resultVisitDetail = VisitFacade.InsertVisitDetail(lVisitDetailParamlist);

            string JsonResultlist = JsonConvert.SerializeObject(resultVisitDetail);
            //--009
            //JsonResultlist = JsonResultlist.Substring(0, 500);
            //009--

            var strJson = Core.Services.RestPublisher.Serialize(lVisitDetailParamlist);
            var size = System.Text.ASCIIEncoding.Unicode.GetByteCount(strJson) / 1024;
            string sizeKB = size.ToString() + " KB";
            Core.Manager.LogFacade.InsertLog("InsertVisitDetail", lEmployeeID, JsonResultlist, lVisitDetailParamlist.FirstOrDefault().BranchID, lVisitDetailParamlist.FirstOrDefault().deviceID, "", sizeKB);

            return resultVisitDetail;
        }
        //006--

        public Core.Model.mdlResultList InsertDailyCost(List<Core.Model.mdlDailyCostParam> lParamlist)
        {
            string serlJson = JsonConvert.SerializeObject(lParamlist);
            string lEmployeeID = "";
            var resultDailyCost = new Core.Model.mdlResultList();

            if (lParamlist.Count > 0)
            {
                lEmployeeID = lParamlist.FirstOrDefault().EmployeeID;



                resultDailyCost = CostFacade.InsertDailyCost(lParamlist);

                string JsonResultlist = JsonConvert.SerializeObject(resultDailyCost);
                //--009
                //JsonResultlist = JsonResultlist.Substring(0, 500);
                //009--

                var strJson = Core.Services.RestPublisher.Serialize(lParamlist);
                var size = System.Text.ASCIIEncoding.Unicode.GetByteCount(strJson) / 1024;
                string sizeKB = size.ToString() + " KB";
                Core.Manager.LogFacade.InsertLog("InsertDailyCost", lEmployeeID, JsonResultlist, lParamlist.FirstOrDefault().BranchID, lParamlist.FirstOrDefault().deviceID, "", sizeKB);
            }
            else
            {
                var mdlResultList = new List<Core.Model.mdlResult>();

                var mdlResult = new Core.Model.mdlResult();

                mdlResult.Result = "0";
                mdlResultList.Add(mdlResult);
                resultDailyCost.ResultList = mdlResultList;
            }
            return resultDailyCost;
        }


        public Core.Model.mdlResultList InsertSisaStockVisit(List<Core.Model.mdlSisaStockVisit> lSisaStockVisit)
        {
            //string serlJson = JsonConvert.SerializeObject(lVisitParamlist);
            string lEmployeeID = lSisaStockVisit.FirstOrDefault().EmployeeID;

            
            SisaStockVisitFacade.DeleteSisaStockVisit(lSisaStockVisit.FirstOrDefault().VisitID);
            var resultSisaStockVisit = SisaStockVisitFacade.InsertSisaStockVisit(lSisaStockVisit);

            string JsonResultlist = JsonConvert.SerializeObject(resultSisaStockVisit);
            //--009
            //JsonResultlist = JsonResultlist.Substring(0, 500);
            //009--

            var strJson = Core.Services.RestPublisher.Serialize(lSisaStockVisit);
            var size = System.Text.ASCIIEncoding.Unicode.GetByteCount(strJson) / 1024;
            string sizeKB = size.ToString() + " KB";
            Core.Manager.LogFacade.InsertLog("InserSisaStocktVisit", lEmployeeID, JsonResultlist, lSisaStockVisit.FirstOrDefault().BranchID, lSisaStockVisit.FirstOrDefault().EmployeeID, "", sizeKB);

            return resultSisaStockVisit;
        }

        public Core.Model.mdlResultList InsertCompetitorActivityVisit(List<Core.Model.mdlCompetitorActivityVisit> lCompetitorActivityVisit)
        {
            //string serlJson = JsonConvert.SerializeObject(lVisitParamlist);
            string lEmployeeID = lCompetitorActivityVisit.FirstOrDefault().EmployeeID;


            CompetitorActivityVisitFacade.DeleteCompetitorActivityVisit(lCompetitorActivityVisit.FirstOrDefault().VisitID);
            var resultCompetitorActivityVisit = CompetitorActivityVisitFacade.InsertCompetitorActivityVisit(lCompetitorActivityVisit);

            string JsonResultlist = JsonConvert.SerializeObject(resultCompetitorActivityVisit);
            //--009
            //JsonResultlist = JsonResultlist.Substring(0, 500);
            //009--

            var strJson = Core.Services.RestPublisher.Serialize(lCompetitorActivityVisit);
            var size = System.Text.ASCIIEncoding.Unicode.GetByteCount(strJson) / 1024;
            string sizeKB = size.ToString() + " KB";
            Core.Manager.LogFacade.InsertLog("InsertCompetitorActivityVisit", lEmployeeID, JsonResultlist, lCompetitorActivityVisit.FirstOrDefault().BranchID, lCompetitorActivityVisit.FirstOrDefault().EmployeeID, "", sizeKB);

            return resultCompetitorActivityVisit;
        }

        public Core.Model.mdlResultList InsertCompetitorActivityImage(List<Core.Model.mdlCompetitorActivityImage> lCompetitorActivityImage)
        {
            //string serlJson = JsonConvert.SerializeObject(lVisitParamlist);
            string lEmployeeID = lCompetitorActivityImage.FirstOrDefault().EmployeeID;


            CompetitorActivityImageFacade.DeleteCompetitorActivityImage(lCompetitorActivityImage.FirstOrDefault().VisitID);
            var resultCompetitorActivityImage = CompetitorActivityImageFacade.InsertCompetitorActivityImage(lCompetitorActivityImage);

            string JsonResultlist = JsonConvert.SerializeObject(resultCompetitorActivityImage);
            //--009
            //JsonResultlist = JsonResultlist.Substring(0, 500);
            //009--

            var strJson = Core.Services.RestPublisher.Serialize(lCompetitorActivityImage);
            var size = System.Text.ASCIIEncoding.Unicode.GetByteCount(strJson) / 1024;
            string sizeKB = size.ToString() + " KB";
            Core.Manager.LogFacade.InsertLog("InsertCompetitorActivityImage", lEmployeeID, JsonResultlist, lCompetitorActivityImage.FirstOrDefault().BranchID, lCompetitorActivityImage.FirstOrDefault().EmployeeID, "", sizeKB);

            return resultCompetitorActivityImage;
        }

        public Core.Model.mdlResultList InsertPromoCompetitor(List<Core.Model.mdlPromoCompetitor> lPromoCompetitor)
        {
            //string serlJson = JsonConvert.SerializeObject(lVisitParamlist);
            string lEmployeeID = lPromoCompetitor.FirstOrDefault().EmployeeID;


            PromoCompetitorFacade.DeletePromoCompetitor(lPromoCompetitor.FirstOrDefault().VisitID);
            var resultPromoCompetitor = PromoCompetitorFacade.InsertPromoCompetitor(lPromoCompetitor);

            string JsonResultlist = JsonConvert.SerializeObject(resultPromoCompetitor);
            //--009
            //JsonResultlist = JsonResultlist.Substring(0, 500);
            //009--

            var strJson = Core.Services.RestPublisher.Serialize(lPromoCompetitor);
            var size = System.Text.ASCIIEncoding.Unicode.GetByteCount(strJson) / 1024;
            string sizeKB = size.ToString() + " KB";
            Core.Manager.LogFacade.InsertLog("InsertPromoCompetitor", lEmployeeID, JsonResultlist, lPromoCompetitor.FirstOrDefault().BranchID, lPromoCompetitor.FirstOrDefault().EmployeeID, "", sizeKB);

            return resultPromoCompetitor;
        }

        public Core.Model.mdlResultList InsertPromoVisit(List<Core.Model.mdlPromoVisit> lPromoVisit)
        {
            //string serlJson = JsonConvert.SerializeObject(lVisitParamlist);
            string lEmployeeID = lPromoVisit.FirstOrDefault().EmployeeID;



            var resultPromoVisit = PromoVisitFacade.InsertPromoVisit(lPromoVisit);

            string JsonResultlist = JsonConvert.SerializeObject(resultPromoVisit);
            //--009
            //JsonResultlist = JsonResultlist.Substring(0, 500);
            //009--

            var strJson = Core.Services.RestPublisher.Serialize(lPromoVisit);
            var size = System.Text.ASCIIEncoding.Unicode.GetByteCount(strJson) / 1024;
            string sizeKB = size.ToString() + " KB";
            Core.Manager.LogFacade.InsertLog("InserPromoVisit", lEmployeeID, JsonResultlist, lPromoVisit.FirstOrDefault().BranchID, lPromoVisit.FirstOrDefault().EmployeeID, "", sizeKB);

            return resultPromoVisit;
        }

        public Core.Model.mdlResultList InsertPOSMVisit(List<Core.Model.mdlPOSMVisit> lPOSMVisit)
        {
            //string serlJson = JsonConvert.SerializeObject(lVisitParamlist);
            string lEmployeeID = lPOSMVisit.FirstOrDefault().EmployeeID;


            POSMVisitFacade.DeletePOSMVisit(lPOSMVisit.FirstOrDefault().VisitID);
            var resultPOSMVisit = POSMVisitFacade.InsertPOSMVisit(lPOSMVisit);

            string JsonResultlist = JsonConvert.SerializeObject(resultPOSMVisit);
            //--009
            //JsonResultlist = JsonResultlist.Substring(0, 500);
            //009--

            var strJson = Core.Services.RestPublisher.Serialize(lPOSMVisit);
            var size = System.Text.ASCIIEncoding.Unicode.GetByteCount(strJson) / 1024;
            string sizeKB = size.ToString() + " KB";
            Core.Manager.LogFacade.InsertLog("InserPOSMVisit", lEmployeeID, JsonResultlist, lPOSMVisit.FirstOrDefault().BranchID, lPOSMVisit.FirstOrDefault().EmployeeID, "", sizeKB);

            return resultPOSMVisit;
        }

        public Core.Model.mdlResultList InsertPOSMStock(List<Core.Model.mdlPOSMStock> lPOSMStock)
        {
            //string serlJson = JsonConvert.SerializeObject(lVisitParamlist);
            string lEmployeeID = lPOSMStock.FirstOrDefault().EmployeeID;


            POSMStockFacade.DeletePOSMStock(lPOSMStock.FirstOrDefault().VisitID);
            var resultPOSMStock = POSMStockFacade.InsertPOSMStock(lPOSMStock);

            string JsonResultlist = JsonConvert.SerializeObject(resultPOSMStock);
            //--009
            //JsonResultlist = JsonResultlist.Substring(0, 500);
            //009--

            var strJson = Core.Services.RestPublisher.Serialize(lPOSMStock);
            var size = System.Text.ASCIIEncoding.Unicode.GetByteCount(strJson) / 1024;
            string sizeKB = size.ToString() + " KB";
            Core.Manager.LogFacade.InsertLog("InserPromoVisit", lEmployeeID, JsonResultlist, lPOSMStock.FirstOrDefault().BranchID, lPOSMStock.FirstOrDefault().EmployeeID, "", sizeKB);

            return resultPOSMStock;
        }

        public Core.Model.mdlResultList InsertResultSurvey(List<Core.Model.mdlResultSurvey> lResultSurvey)
        {
            //string serlJson = JsonConvert.SerializeObject(lVisitParamlist);
            string lEmployeeID = lResultSurvey.FirstOrDefault().EmployeeID;


            ResultSurveyFacade.DeleteResultSurvey(lResultSurvey.FirstOrDefault().SurveyID);
            var result = ResultSurveyFacade.InsertResultSurvey(lResultSurvey);

            string JsonResultlist = JsonConvert.SerializeObject(result);
            //--009
            //JsonResultlist = JsonResultlist.Substring(0, 500);
            //009--

            var strJson = Core.Services.RestPublisher.Serialize(lResultSurvey);
            var size = System.Text.ASCIIEncoding.Unicode.GetByteCount(strJson) / 1024;
            string sizeKB = size.ToString() + " KB";
            Core.Manager.LogFacade.InsertLog("InsertResultSurvey", lEmployeeID, JsonResultlist, lResultSurvey.FirstOrDefault().BranchID, lResultSurvey.FirstOrDefault().EmployeeID, "", sizeKB);

            return result;
        }

        public Core.Model.mdlResultList InsertResultSurveyDetail(List<Core.Model.mdlResultSurveyDetail> lResultSurveyDetail)
        {

            ResultSurveyFacade.DeleteResultSurveyDetail(lResultSurveyDetail.FirstOrDefault().SurveyID);
            var result = ResultSurveyFacade.InsertResultSurveyDetail(lResultSurveyDetail);

            string JsonResultlist = JsonConvert.SerializeObject(result);
            //--009
            //JsonResultlist = JsonResultlist.Substring(0, 500);
            //009--

            var strJson = Core.Services.RestPublisher.Serialize(lResultSurveyDetail);
            var size = System.Text.ASCIIEncoding.Unicode.GetByteCount(strJson) / 1024;
            string sizeKB = size.ToString() + " KB";
            Core.Manager.LogFacade.InsertLog("InsertResultSurveyDetail", "", JsonResultlist, "", "", "", sizeKB);

            return result;
        }

        public Core.Model.mdlResultList InsertCustomerImageType(List<Core.Model.mdlCustomerImageType> lCustomerImageType)
        {

            var result = CustomerImageTypeFacade.InsertCustomerImageType(lCustomerImageType);

           

            var strJson = Core.Services.RestPublisher.Serialize(result);
            var size = System.Text.ASCIIEncoding.Unicode.GetByteCount(strJson) / 1024;
            string sizeKB = size.ToString() + " KB";
            //Core.Manager.LogFacade.InsertLog("InserCustomerImageType", lEmployeeID, strJson, lPOSMStock.FirstOrDefault().BranchID, lPOSMStock.FirstOrDefault().EmployeeID, "", sizeKB);

            return result;
        }

        public Core.Model.mdlResultList InsertTracking(Core.Model.mdlTrackingParam lParam)
        {
            //string serlJson = JsonConvert.SerializeObject(lParam);
            //string lEmployeeID = lParamlist.FirstOrDefault().EmployeeID;

            //string result = Core.Manager.LogFacade.InsertLog("InsertTracking", serlJson, lParam.EmployeeID, "");
            var resultInsertTracking = TrackingFacade.InsertTracking(lParam);

            //update christopher
            var mdlIdleCounter = IdleCounterFacade.CheckIfIdleCounterExist(lParam.EmployeeID, lParam.BranchID);
            if (mdlIdleCounter.EmployeeID == "" || mdlIdleCounter.EmployeeID == null)
            {
                string resultInsertIdleCounter = IdleCounterFacade.InsertIdleCounter(lParam.EmployeeID, lParam.BranchID, lParam.Latitude, lParam.Longitude, Convert.ToDateTime(lParam.TrackingDate));
            }
            else
            {

                double idleRadius = 0;
                double idleTime = 0;

                List<Core.Model.mdlSettings> listSettings = GeneralSettingsFacade.GetCurrentSettings(lParam.BranchID);
                if (listSettings.Count > 0)
                {
                    foreach (var setting in listSettings)
                    {
                        if (setting.name == "IDLERADIUS")
                        {
                            idleRadius = Convert.ToDouble(setting.value);
                        }
                        else if (setting.name == "IDLETIME")
                        {
                            idleTime = Convert.ToDouble(setting.value);
                        }

                    }
                }

                double baseLatitude = double.Parse(mdlIdleCounter.Latitude, CultureInfo.InvariantCulture);
                double baseLongitude = double.Parse(mdlIdleCounter.Longitude, CultureInfo.InvariantCulture);

                double newLatitude = double.Parse(lParam.Latitude, CultureInfo.InvariantCulture);
                double newLongitude = double.Parse(lParam.Longitude, CultureInfo.InvariantCulture);
                var baseCoor = new GeoCoordinate(baseLatitude, baseLongitude);
                var newCoor = new GeoCoordinate(newLatitude, newLongitude);

                double distance = RadiusFacade.getDistance(baseCoor, newCoor);

                Core.Model.mdlLogIdleParam mdlLogIdle = new Core.Model.mdlLogIdleParam();
                mdlLogIdle.EmployeeID = lParam.EmployeeID;
                mdlLogIdle.BranchID = lParam.BranchID;
                mdlLogIdle.Longitude = lParam.Longitude;
                mdlLogIdle.Latitude = lParam.Latitude;
                mdlLogIdle.StartIdle = mdlIdleCounter.StartDate;
                
                mdlLogIdle.Now = lParam.TrackingDate; // fernandes

                TimeSpan duration = Convert.ToDateTime(mdlLogIdle.Now) - Convert.ToDateTime(mdlLogIdle.StartIdle); // fernandes
                //TimeSpan duration = DateTime.Now.Subtract(Convert.ToDateTime(mdlIdleCounter.StartDate));
                //mdlLogIdle.Duration = duration.ToString(@"hh\:mm\:ss");
                mdlLogIdle.Duration = duration.ToString();
                
                mdlLogIdle.Location = "";
                //mdlLogIdle.Location = ReverseGeocodingFacade.GetStreetName(lParam.Latitude, lParam.Longitude);

                string resultLogIdle = "";

                if (lParam.FlagCheckIn == "True")
                {
                    string resultUpdateBaseCounter = IdleCounterFacade.UpdateBaseIdleCounter(lParam.EmployeeID, lParam.BranchID, lParam.Latitude, lParam.Longitude, Convert.ToDateTime(lParam.TrackingDate));

                    mdlLogIdle.Status = "True";
                    resultLogIdle = LogIdleFacade.UpdateLogIdleClose(mdlLogIdle);
                }
                else
                {
                    if (distance > idleRadius)
                    {
                        //tidak idle

                        mdlLogIdle.Status = "True";
                        resultLogIdle = LogIdleFacade.UpdateLogIdleClose(mdlLogIdle);
                        string resultUpdateBaseCounter = IdleCounterFacade.UpdateBaseIdleCounter(lParam.EmployeeID, lParam.BranchID, lParam.Latitude, lParam.Longitude, Convert.ToDateTime(lParam.TrackingDate));
                    }
                    else
                    {
                        // idle
                        int newCounter = mdlIdleCounter.Counter + 1;
                        string resultUpdateCounter = IdleCounterFacade.UpdateIdleCounter(lParam.EmployeeID, lParam.BranchID, lParam.Latitude, lParam.Longitude, newCounter);

                        var mdlMobileConfig = IdleCounterFacade.GetMobileConfigIdleCounter(lParam.BranchID);
                        if (mdlMobileConfig.ID != null || mdlMobileConfig.ID != "")
                        {
                            double roundMaxCounter = Math.Ceiling(idleTime / Convert.ToDouble(mdlMobileConfig.Value));
                            if (newCounter >= Convert.ToInt32(roundMaxCounter))
                            {
                                string resultInsertIdleLog = IdleCounterFacade.InsertIdleLog(lParam.EmployeeID, lParam.BranchID, "", Convert.ToDateTime(lParam.TrackingDate));

                                mdlLogIdle.Status = "False";
                                var logIdle = LogIdleFacade.GetLogIdle(mdlLogIdle);
                                if (logIdle.BranchID != null)
                                {
                                    resultLogIdle = LogIdleFacade.UpdateLogIdle(mdlLogIdle);
                                }
                                else
                                {

                                    resultLogIdle = LogIdleFacade.InsertLogIdle(mdlLogIdle);
                                }
                            }
                        }

                    }
                }




            }


            return resultInsertTracking;
        }

        public Core.Model.mdlResult InsertAndroidKey(Core.Model.mdlSaveAndroidKeyParam param)
        {
            return JsonFacade.InsertAndroidKey(param);
        }

        public Core.Model.mdlResultSV SetUserConfig(Core.Model.mdlSetDeviceIDParam param)
        {
            return JsonFacade.SetUserConfig(param);
        }

        public Core.Model.mdlDailyMsgList GetDailyMessage(Core.Model.mdlParam param)
        {
            return JsonFacade.GetDailyMessage(param);
        }

        public List<Core.Model.mdlMobileConfig> GetMobileConfig(Core.Model.mdlParam param)
        {
            return MobileConfigFacade.LoadMobileConfig(param);
        }

        public Core.Model.mdlDailyMsgList GetUserConfig(Core.Model.mdlParam param)
        {
            return JsonFacade.GetDailyMessage(param);
        }

        public Core.Model.mdlCheckinCourierRadius CheckinCourierRadius(Core.Model.mdlCheckinCourierRadiusParam param)
        {
            return JsonFacade.CheckinCourierRadius(param);
        }

        public Core.Model.mdlResult PushNotificationConfirmation(Core.Model.mdlPushNotificationConfirmationParam param)
        {
            return JsonFacade.PushNotificationConfirmation(param);
        }

        public Core.Model.mdlResultList UploadJson(Core.Model.mdlUploadJsonParam lParamlist)
        {
            return Core.Manager.JsonFacade.UploadJson(lParamlist);
        }

        public Core.Model.mdlResult CheckVersion(Core.Model.mdlParam param)
        {
            return Core.Manager.MobileConfigFacade.CheckVersion(param);
        }

        public Core.Model.mdlVehicleList LoadVehicleByBranch(Core.Model.mdlVehicleBranchParam param)
        {
            return Core.Manager.VehicleFacade.LoadVehicleByBranch(param);
        }

        public Core.Model.mdlResultList InsertCostVisit(List<Core.Model.mdlCostVisit> lParamlist)
        {
            string serlJson = JsonConvert.SerializeObject(lParamlist);
            string lEmployeeID = "";
            var resultDailyCost = new Core.Model.mdlResultList();

            if (lParamlist.Count > 0)
            {
                lEmployeeID = lParamlist.FirstOrDefault().EmployeeID;



                resultDailyCost = CostFacade.InsertCostVisit(lParamlist);

                string JsonResultlist = JsonConvert.SerializeObject(resultDailyCost);
                //--009
                //JsonResultlist = JsonResultlist.Substring(0, 500);
                //009--
                var strJson = Core.Services.RestPublisher.Serialize(lParamlist);
                var size = System.Text.ASCIIEncoding.Unicode.GetByteCount(strJson) / 1024;
                string sizeKB = size.ToString() + " KB";
                Core.Manager.LogFacade.InsertLog("InsertCostVisit", lEmployeeID, JsonResultlist, lParamlist.FirstOrDefault().BranchID, lParamlist.FirstOrDefault().deviceID, "", sizeKB);
            }
            else
            {
                var mdlResultList = new List<Core.Model.mdlResult>();

                var mdlResult = new Core.Model.mdlResult();

                mdlResult.Result = "0";
                mdlResultList.Add(mdlResult);
                resultDailyCost.ResultList = mdlResultList;
            }
            return resultDailyCost;
        }

        //FERNANDES-RatioBBM Service (12 April 2017)
        public Core.Model.mdlResultList InsertBBMRatio(List<Core.Model.mdlBBMRatioParam> lBBMRatioParamlist)
        {
            string lEmployeeID = lBBMRatioParamlist.FirstOrDefault().EmployeeID;

            var result = BBMFacade.InsertBBMRatio(lBBMRatioParamlist);

            string JsonResultlist = JsonConvert.SerializeObject(result);

            var strJson = Core.Services.RestPublisher.Serialize(lBBMRatioParamlist);
            var size = System.Text.ASCIIEncoding.Unicode.GetByteCount(strJson) / 1024;
            string sizeKB = size.ToString() + " KB";
            Core.Manager.LogFacade.InsertLog("InsertRatioBBM", lEmployeeID, JsonResultlist, lBBMRatioParamlist.FirstOrDefault().BranchID, lBBMRatioParamlist.FirstOrDefault().DeviceID, "", sizeKB);

            return result;
        }

        //wyeth
        public Stream GetLogin(Core.Model.mdlLoginParam param)
        {
            var listMenu = Core.Manager.MenuFacade.LoadMenuMobile(param.Role);

            var result = new Core.Model.mdlLoginMenu();

            var mdlResult = new Core.Model.mdlResultSvc();
            mdlResult.Title = "Login";
            mdlResult.StatusCode = "01";
            mdlResult.StatusMessage = "";
            mdlResult.Value = listMenu;

            string json = Core.Services.RestPublisher.Serialize(mdlResult);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            return ms;



        }


        public Core.Model.mdlResult UploadSqLite(System.IO.Stream data)
        {
            return Core.Manager.SqlLiteFacade.UploadSqlLite(data);
        }

        public string GetDate()
        {
            DateTime dtDate = DateTime.UtcNow.Date;
            string sDate = dtDate.ToString("yyyy-MM-dd");
            return sDate;
        }

        public Core.Model.mdlResultList UploadDeposit(List<Core.Model.mdlDepositParam> lParamlist)
        {
            
            //string serlJson = JsonConvert.SerializeObject(lParamlist);
            var resultUploadDeposit = DepositFacade.UploadDeposit(lParamlist);
            //string JsonResultlist = JsonConvert.SerializeObject(resultUploadDeposit);
            //string lEmployeeID = lParamlist.FirstOrDefault().EmployeeID;
            // Core.Manager.LogFacade.InsertLog("DownloadJson", lEmployeeID, JsonResultlist, lParamlist.FirstOrDefault().BranchID, "", "", "");

            return resultUploadDeposit;
        }
    }
}
