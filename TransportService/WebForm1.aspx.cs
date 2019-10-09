/* documentation
 *001 Fernandes - 21 jul 2016
 *002 Nanda - 21 jul 2016
 *003 fernandes - 28 jul 2016
 *004 nanda - 01 agt 2016
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Core.Model;
using System.Net;
using System.IO;
using Core;
using System.Runtime.Serialization.Json;
using RESTFulWCFService;
using System.Text;
using System.Text.RegularExpressions;


namespace TransportService
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public string Gjson;
        protected void Page_Load(object sender, EventArgs e)
        {
            TimeSpan duration = DateTime.Now.Subtract(DateTime.Now.AddHours(-5));
            //var paramEmployee = new mdlEmployeeParam();
            //paramEmployee.EmployeeID = "004";
            //paramEmployee.EmployeeName = "NANDHA DS";
            //paramEmployee.EmployeeTypeID = "DRIVER";
            //paramEmployee.PlantID = "W00002";
            //paramEmployee.WarehouseID = "P00003";

            //var paramEmployeeType = new mdlEmployeeTypeParam();
            //paramEmployeeType.EmployeeTypeID = "Test";
            //paramEmployeeType.EmployeeTypeName = "Test";
            //paramEmployeeType.Description = "TEsttttttttttt2222";

            //var paramVehicle = new mdlVehicleParam();
            //paramVehicle.VehicleID = "B TEST MCM";
            //paramVehicle.VehicleName = "B TEST2 MCM";
            //paramVehicle.VehicleNumber = "B 1 MCM   ";
            //paramVehicle.STNKDate = "2015-12-13 00:00:00.000";
            //paramVehicle.YearManufacturing = "2015";
            //paramVehicle.KIRNumber = "11121113";
            //paramVehicle.EngineNumber = "123123213";
            //paramVehicle.VehicleTypeID = "11";
            //paramVehicle.SubVehicleTypeID = "0";
            //paramVehicle.PlantID = "P00001";
            //paramVehicle.CapacityVolume = "100";
            //paramVehicle.CapacityWeight = "100";
            //paramVehicle.Brand = "Honda";

            //var paramVehicleType = new mdlVehicleTypeParam();
            //paramVehicleType.VehicleTypeID = "13";
            //paramVehicleType.VehicleTypeName = "Lamborgini2";

            //var paramCustomer = new mdlCustomerParam();
            //paramCustomer.CustomerID = "001";
            ////paramCustomer.CustomerName = "nol nol satu";
            ////paramCustomer.CustomerAddress = "alamat 1";
            ////paramCustomer.CustomerTypeID = "WARUNG";
            ////paramCustomer.BranchID = "Cabang 1";
            ////paramCustomer.Longitude = "Longitude 1";
            ////paramCustomer.Latitude = "Latitude 1";
            ////paramCustomer.Phone = "Telpon 1";
            ////paramCustomer.PIC = "Si Satu";

            //var paramCustomerType = new mdlCustomerTypeParam();
            //paramCustomerType.CustomerTypeID = "SUPERMARKET";
            ////paramCustomerType.CustomerTypeName = "SM";
            ////paramCustomerType.Description = "Pelayanan Sendiri";

            ////#001 Nanda
            //var paramBranch = new mdlBranchParam();
            //paramBranch.BranchID = "ID004";
            //paramBranch.BranchDescription = "Jogja";
            //paramBranch.CompanyID = "C00002";
            ////

            //var paramCompany = new mdlCompanyParam();
            //paramCompany.CompanyID = "C00005";
            ////paramCompany.CompanyName = "Test";    

            //var paramProduct = new mdlProductParam();
            //paramProduct.ProductID = "P0003";
            ////paramProduct.ProductName = "Produk Tiga";
            ////paramProduct.ProductType = "Instan";
            ////paramProduct.ProductGroup = "Junk";

            //002 Nanda
             //json download json
            var paramJson = new mdlParam();
            paramJson.BranchID = "01A";
            paramJson.EmployeeID = "ABDUL M";

            //json retur order
            var paramjsonlist = new List<mdlReturOrderParam>();
            var paramjson = new mdlReturOrderParam();
            paramjson.ReturNumber = "R003";
            paramjson.CustomerID = "C0001";
            paramjson.EmployeeID = "EM0001";
            paramjson.VisitID = "V02";
            paramjson.BranchID = "ID01";
            paramjson.ReturDate = "2016-07-20 00:00:00.000";
            paramjson.ReturStatus = "RECEIVE";
            paramjson.Description = "Lorem Ipsum2";
            paramjson.Signature = "Lorem Ipsum2";
            paramjson.IsPrint = "1";
            paramjson.Remark = "Test2";
            paramjson.ReceivedDate = "2016-07-20 00:00:00.000";
            paramjson.IsNewRetur = "0";
            paramjsonlist.Add(paramjson);

            paramjson = new mdlReturOrderParam();
            paramjson.ReturNumber = "R002";
            paramjson.CustomerID = "C0002";
            paramjson.EmployeeID = "EM0001";
            paramjson.VisitID = "V02";
            paramjson.BranchID = "ID02";
            paramjson.IsPrint = "1";
            paramjson.ReturDate = "2016-07-20 00:00:00.000";
            paramjson.ReturStatus = "RECEIVE";
            paramjson.Description = "Lorem Ipsum2";
            paramjson.Signature = "Lorem Ipsum2";
            paramjson.Remark = "Test88";
            paramjson.ReceivedDate = "2016-07-20 00:00:00.000";
            paramjson.IsNewRetur = "0";
            paramjsonlist.Add(paramjson);

            // json retur order detail
            //var paramjsonlist = new List<mdlReturOrderDetailParam>();
            //var paramjson = new mdlReturOrderDetailParam();
            //paramjson.ReturNumber = "R100";
            //paramjson.Quantity = "10";
            //paramjson.QuantityReal = "10";
            //paramjson.ProductID = "P0001";
            //paramjson.UOM = "PACK";
            //paramjson.ArticleNumber = "AR0001";
            //paramjson.EmployeeID = "EM0001";
            //paramjsonlist.Add(paramjson);

            //paramjson = new mdlReturOrderDetailParam();
            //paramjson.ReturNumber = "R100";
            //paramjson.Quantity = "20";
            //paramjson.QuantityReal = "20";
            //paramjson.ProductID = "P0002";
            //paramjson.UOM = "PACK";
            //paramjson.ArticleNumber = "AR0002";
            //paramjson.EmployeeID = "EM0001";
            //paramjsonlist.Add(paramjson);

            // json image customer
            var paramimagelist = new List<mdlCustomerImageParam>();
            var paramimage = new mdlCustomerImageParam();
            paramimage.CustomerID = "MSID0013";
            paramimage.ImageBase64 = "/9j/4AAQSkZJRgABAQAAAQABAAD//gA7Q1JFQVRPUjogZ2QtanBlZyB2MS4wICh1c2luZyBJSkcgSlBFRyB2ODApLCBxdWFsaXR5ID0gOTAK/9sAQwADAgIDAgIDAwMDBAMDBAUIBQUEBAUKBwcGCAwKDAwLCgsLDQ4SEA0OEQ4LCxAWEBETFBUVFQwPFxgWFBgSFBUU/9sAQwEDBAQFBAUJBQUJFA0LDRQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQU/8AAEQgAJAAkAwEiAAIRAQMRAf/EAB8AAAEFAQEBAQEBAAAAAAAAAAABAgMEBQYHCAkKC//EALUQAAIBAwMCBAMFBQQEAAABfQECAwAEEQUSITFBBhNRYQcicRQygZGhCCNCscEVUtHwJDNicoIJChYXGBkaJSYnKCkqNDU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6g4SFhoeIiYqSk5SVlpeYmZqio6Slpqeoqaqys7S1tre4ubrCw8TFxsfIycrS09TV1tfY2drh4uPk5ebn6Onq8fLz9PX29/j5+v/EAB8BAAMBAQEBAQEBAQEAAAAAAAABAgMEBQYHCAkKC//EALURAAIBAgQEAwQHBQQEAAECdwABAgMRBAUhMQYSQVEHYXETIjKBCBRCkaGxwQkjM1LwFWJy0QoWJDThJfEXGBkaJicoKSo1Njc4OTpDREVGR0hJSlNUVVZXWFlaY2RlZmdoaWpzdHV2d3h5eoKDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uLj5OXm5+jp6vLz9PX29/j5+v/aAAwDAQACEQMRAD8A+B/BPhV/GPiCx02GQRC4cB5ipYRp1ZyB1wMnHfpX6FfAj4MfCrQtK061vtCsv7WvXBN/4iMNxlM4DCFxxu2yYC4+6DnqK+QPhpbDwB8VNZ0m3trbUXLm0s0vG2wsJBuUu3ZVGGJ9FPqK+kfDf7NugeIpb3WdS+Lfm6qMvczrErrE/UnLPkqM+v4187OqotM+poYXnT5j0z4l/s1fDvx5ouuPHodhY39uGaLVdGjWxEeGVVQxHaGLEkKWHJx7kfnz8R/hzffDXxReaPffvPLUTQXAXaLiFuUkA7ZHbsQR2r7z8LahqngmPU7LUPEuk+M9DutPuYLPWrbd+8faWWKQttYMDlgQSMrjdzXjf7V2hN4r+FnhLxcsHlT2kMlrOXk8ySeFypR9wA4WQuvIHLdODVxnzGdehyXtr/lY+TtB+Get+LbJr+yiiaHzCmZJlU5GOx+tFfZvgH9jn4oab4R00aN4gtra1nhS4aJZtm2R1BcEbGyQeM55x0ornli3d8rVvmdMcHHlXMnf1R5Z4e8PeEbz4+toOv6he/ZXs0Y6iZ2gmEm3Gc7iR0DDp6Yr6f8AhZonwQ0/Utahv9Ys72RLVoJbnXdRYNcBmXOPNkJwAcA8D0r8u4tVez1X7U8MsatN5nlux3eXkYGTz0719l/BvwpoGo+KIvFWj6Fq+tWVyEaze2MUgtjjDxlmZWQg5x6A8HtV1qPJbmehphKyqXjFan0X4U8LfByw07xBZRXl9dXafLFb31+/liE4eN0TO0ggKVYZB618+/tGeLHh+DMunSX/ANpuVaKREG0bIVlKrlQOC5cuTn+EcDNbH7Uel+GPhpFF4h1C7uX8ZXsZVbKXUWuX2/wA5PQevTrXh3xh14eGfg7pukahcSX3iDXYYL66n835I1ySsQXbg7RtGcnBTjqcmHg279CMZUS0W5654A/aZ+K2r+GbZtIhs5LOD9wJJXYFioHPHsRRXx74Z+KuseGtMFlaXssEQYttXkZwB/QUU5YOd3ypWMo46PKrt3PS/wBtLwzp3hX4qwW2mWyW0UlkZHCKF3v9pnG4gADOAPyryvwt8R/E3gmB00LWrvTElOXSCQhWPHOOmaKK9aMVKCTR5HM4yunY1/B+o3nxD+I2nS+I7y41eWWXLvcyFiwUEhfpx0rI+KXiO98Q+JriW7cEodqqgwoA46UUVUUlIipJuGr3OLDGiiitjguz/9k=";
            paramimage.ImageDate = "2016-09-16 14:00:00";
            paramimage.ImageID = "IM0001";
            paramimage.DocNumber = "test0001";
            paramimage.VisitID = "V01";
            paramimage.ImageType = "COST";
            paramimage.EmployeeID = "S101";
            paramimagelist.Add(paramimage);

            //paramimage = new mdlCustomerImageParam();
            //paramimage.CustomerID = "MSID0013";
            //paramimage.ImageBase64 = "TESTING2";
            //paramimage.ImageDate = "2016-09-16 14:02:00";
            //paramimage.ImageID = "IM0002";
            //paramimage.DocNumber = "test0002";
            //paramimage.VisitID = "V01";
            //paramimage.ImageType = "Cost";
            //paramimage.EmployeeID = "S101";
            //paramimagelist.Add(paramimage);

            //// json Call Plan
            //var paramjsonlist = new List<mdlCallPlanParam>();
            //var paramjson = new mdlCustomerImageParam();
            //paramjson.CustomerID = "C0001";
            //paramjson.ImageBase64 = "YJjho87gtguJYYGBJbbybbjbbybHBBXBSSSBXubsxsbxsbx";
            //paramjson.ImageDate = "2016-07-21";
            //paramjson.ImageID = "IM0001";
            //paramjson.ImageType = "DELIVERY";
            //paramjsonlist.Add(paramjson);

            //paramjson = new mdlCustomerImageParam();
            //paramjson.CustomerID = "C0002";
            //paramjson.ImageBase64 = "ho87gtguUJJyhbYBKHJ";
            //paramjson.ImageDate = "2016-07-21";
            //paramjson.ImageID = "IM0002";
            //paramjson.ImageType = "RETUR";
            //paramjsonlist.Add(paramjson);
            //002 Nanda

            //--001
            var paramDOlist = new List<mdlDeliveryOrderParam>();
            var paramDO = new mdlDeliveryOrderParam();
            //paramDO.DONumber = "DO006";
            //paramDO.CustomerID = "C0001";
            //paramDO.EmployeeID = "EM0001";
            //paramDO.BranchID = "ID001"; 
            //paramDO.DODate = "2016-07-25";
            //paramDO.DOStatus = "DEL 02";
            //paramDO.Description = "ORDER";
            //paramDO.Signature = "ABCDEF";
            //paramDO.IsPrint = "True";
            //paramDO.Remark = "test06";
            //paramDOlist.Add(paramDO);

            //paramDO = new mdlDeliveryOrderParam();
            paramDO.DONumber = "B1079978";
            paramDO.CallPlanID = "C1000001";
            paramDO.CustomerID = "DPZZ0008";
            paramDO.EmployeeID = "S101";
            paramDO.BranchID = "ID01";
            paramDO.VehicleID = "B 9586 BCM";
            paramDO.DODate = "2016-11-25 00:00:00";
 
            paramDO.DOStatus = "DONE";
            paramDO.Description = "ORDER";
            paramDO.Signature = "QAZXV";
            paramDO.IsPrint = "True";
            paramDO.VisitID = "C1000001";
            paramDO.Remark = "asdfas";
        
            paramDOlist.Add(paramDO);

            var paramDODetaillist = new List<mdlDeliveryOrderDetailParam>();
            var paramDODetail = new mdlDeliveryOrderDetailParam();
            paramDODetail.DONumber = "B1079978";
            paramDODetail.ProductID = "711880950120100";
            paramDODetail.UOM = "PC";
            paramDODetail.Quantity = "101";
            paramDODetail.QuantityReal = "10";
            paramDODetail.ProductGroup = "RF";
            paramDODetail.LotNumber = "";
            paramDODetail.EmployeeID = "";
            paramDODetail.ReasonID = "";
            paramDODetail.BoxID = "";
            paramDODetaillist.Add(paramDODetail);

            paramDODetail = new mdlDeliveryOrderDetailParam();
            paramDODetail.DONumber = "B1079978";
            paramDODetail.ProductID = "711800900000100";
            paramDODetail.UOM = "PC";
            paramDODetail.Quantity = "40";
            paramDODetail.QuantityReal = "31";
            paramDODetail.ProductGroup = "ER";
            paramDODetail.LotNumber = "";
            paramDODetail.EmployeeID = "";
            paramDODetail.ReasonID = "";
            paramDODetail.BoxID = "";
            paramDODetaillist.Add(paramDODetail);
            //001--

            //--003
            var paramCustomerImagelist = new List<mdlCustomerImageParam>();
            var paramCustomerImage = new mdlCustomerImageParam();
            paramCustomerImage.ImageID = "IM005";
            paramCustomerImage.ImageDate = "2016-07-27";
            paramCustomerImage.ImageBase64 = "TESTIM003";
            paramCustomerImage.ImageType = "testing";
            paramCustomerImage.CustomerID = "C0001";
            paramCustomerImage.EmployeeID = "EM0002";
            paramCustomerImage.WarehouseID = "WP2323";
            paramCustomerImage.DocNumber = "1";
            paramCustomerImage.VisitID = "V01";
            paramCustomerImagelist.Add(paramCustomerImage);

            //paramCustomerImage = new mdlCustomerImageParam();
            //paramCustomerImage.ImageID = "IM004";
            //paramCustomerImage.ImageDate = "2016-07-27";
            //paramCustomerImage.ImageBase64 = "TESTIM003";
            //paramCustomerImage.ImageType = "testinglagi";
            //paramCustomerImage.CustomerID = "C0002";
            //paramCustomerImage.EmployeeID = "EM0002";
            //paramCustomerImagelist.Add(paramCustomerImage);

            //Parameter for Visit
            var paramVisitlist = new List<mdlVisitParam>();
            var paramVisit = new mdlVisitParam();
            paramVisit.VisitID = "V007";
            paramVisit.BranchID = "ID00";
            paramVisit.EmployeeID = "EM0002";
            paramVisit.VehicleID = "B 117 VEN";
            paramVisit.VisitDate = "2016-08-15";
            paramVisit.isStart = true;
            paramVisit.isFinish = false;
            paramVisit.StartDate = "2016-08-15 15:00:00.00";
            paramVisit.EndDate = "2016-08-15 15:25:00.00";
            paramVisit.KMStart = "4321";
            paramVisit.KMFinish = "6543";
            paramVisitlist.Add(paramVisit);

           

            //Param for VisitDetail
            //var paramVisitDetaillist = new List<mdlVisitDetailParamNew>();
            //var paramVisitDetail = new mdlVisitDetailParamNew();
            //paramVisitDetail.VisitID = "V007";
            //paramVisitDetail.CustomerID = "C0001";
            //paramVisitDetail.WarehouseID = "wewewe";
            //paramVisitDetail.isStart = "false";
            //paramVisitDetail.isFinish = "true";
            //paramVisitDetail.StartDate = "2016-07-28 15:00:00.00";
            //paramVisitDetail.EndDate = "2016-07-28 15:25:00.00";
            //paramVisitDetail.Latitude = "latitude-1";
            //paramVisitDetail.Longitude = "longitude-1";
            //paramVisitDetail.ReasonID = "R003";
            //paramVisitDetail.ReasonDescription = "reason01";
            //paramVisitDetail.EmployeeID = "EM001";
            //paramVisitDetail.InRange = "false";
            //paramVisitDetail.isDeliver = 1;
            //paramVisitDetail.Distance = "5";
            //paramVisitDetaillist.Add(paramVisitDetail);

            //paramVisitDetail = new mdlVisitDetailParam();
            //paramVisitDetail.VisitID = "V006";
            //paramVisitDetail.CustomerID = "C0002";
            // paramVisitDetail.WarehouseID = "wewewe";
            //paramVisitDetail.isStart = false;
            //paramVisitDetail.isFinish = true;
            //paramVisitDetail.StartDate = "2016-07-28 15:00:00.00";
            //paramVisitDetail.EndDate = "2016-07-28 15:25:00.00";
            //paramVisitDetail.Latitude = "latitude-1";
            //paramVisitDetail.Longitude = "longitude-1";
            //paramVisitDetail.ReasonID = "R001";
            //paramVisitDetail.ReasonDescription = "reason01";
            //paramVisitDetail.EmployeeID = "EM001";
            //paramVisitDetail.isInRange = true;
            //paramVisitDetail.isDeliver = 1;
            //paramVisitDetail.Distance = "5";
            //paramVisitDetaillist.Add(paramVisitDetail);
            //003--

            //--004
            var paramDailyCostlist = new List<mdlDailyCostParam>();
            var paramDailyCost = new mdlDailyCostParam();
            paramDailyCost.DailyCostID = "PKC0003V0031";
            paramDailyCost.CostID = "PK";
            paramDailyCost.CostValue = "12000";
            paramDailyCost.CustomerID = "C0003";
            paramDailyCost.WarehouseID = "wkkd";
            paramDailyCost.Date = "2016-08-01 00:00:00.000";
            paramDailyCost.EmployeeID = "EM0002";
            paramDailyCost.VisitID = "V002";
            paramDailyCostlist.Add(paramDailyCost);

            //paramDailyCost = new mdlDailyCostParam();
            //paramDailyCost.DailyCostID = "PKC0003V004";
            //paramDailyCost.CostID = "PK";
            //paramDailyCost.CostValue = "30000";
            //paramDailyCost.CustomerID = "C0001";
            //paramDailyCost.Date = "2016-08-01 00:00:00.000";
            //paramDailyCost.EmployeeID = "EM0002";
            //paramDailyCost.VisitID = "V002";
            //paramDailyCostlist.Add(paramDailyCost);

            var paramtrackinglist = new List<mdlTrackingParam>();
            var paramtracking = new mdlTrackingParam();
            paramtracking.EmployeeID = "16AHP00601";
            paramtracking.Latitude = "-6.1769006";
            paramtracking.Longitude = "106.7918534";
            paramtracking.TrackingDate = "2016-08-19 13:13:00";
            paramtracking.TrackingID = "Tracking004";
            //paramtracking.TrackingTime = "18:54:00.0000000";
            paramtracking.VehicleID = "B 1 MCM";
            paramtracking.BranchID = "16A";
            paramtrackinglist.Add(paramtracking);

            //paramtracking = new mdlTrackingParam();
            //paramtracking.EmployeeID = "EM001";
            //paramtracking.Latitude = "-7.1769006";
            //paramtracking.Longitude = "107.7918534";
            //paramtracking.TrackingDate = "2016-08-19";
            //paramtracking.TrackingID = "Tracking004";
            ////paramtracking.TrackingTime = "19:54:00.0000000";
            //paramtracking.VehicleID = "B 1 MCM";
            //paramtracking.BranchID = "ID001";
            //paramtrackinglist.Add(paramtracking);
            //004--

            var paramUserConfig = new mdlSetDeviceIDParam();
            paramUserConfig.deviceID = "02:00:00:00:00:00";
            paramUserConfig.token = "23423412341";


            //var paramUpload = new mdlUploadJsonParam();
            //var paramUploadList = new mdlUploadJson();
            //paramUploadList.ListCost = paramDailyCostlist;
            //paramUploadList.ListCustomerImage = paramCustomerImagelist;
            //paramUploadList.ListDeliveryOrder = paramDOlist;
            //paramUploadList.ListDeliveryOrderDetail = paramDODetaillist;
            //paramUploadList.ListVisit = paramVisitlist;
            //paramUploadList.ListVisitDetail = paramVisitDetaillist;

            //paramUpload.UploadJson = paramUploadList;


            var paramLoadVehicle = new mdlVehicleBranchParam();
            paramLoadVehicle.BranchID = "01A";

            //string json = Core.Services.RestPublisher.Serialize(paramUserConfig);
            //string json = @"{""deviceID"":""30:CB:F8:5E:61:15"",""token"":""""}";
            //Gjson = json;
            //POST("http://192.168.1.180:8342/DeliveryOrderService.svc/getJson", json);

            //POST("http://localhost:15991/DeliveryOrderService.svc/getJson", json);

            //POST("http://localhost:15991/DeliveryOrderService.svc/setuserconfig", json);

            //POST("http://103.77.78.126:8001/DeliveryOrderService.svc/setuserconfig", json);

            //POST1("http://localhost:15991/DeliveryOrderService.svc/LoadEmployee");
            //POST1("http://localhost:15991/DeliveryOrderService.svc/LoadEmployeeType");

            //POST("http://localhost:15991/DeliveryOrderService.svc/InsertTracking", json);

            //POST("http://192.168.1.176:7890/DeliveryOrderService.svc/getvisit", json);

            //POST("http://localhost:15991/DeliveryOrderService.svc/InsertEmployee", json);
            //POST("http://localhost:15991/DeliveryOrderService.svc/UpdateEmployee", json);
            //POST("http://localhost:15991/DeliveryOrderService.svc/DeleteEmployee", json);

            //POST("http://localhost:15991/DeliveryOrderService.svc/InsertEmployeeType", json);
            //POST("http://localhost:15991/DeliveryOrderService.svc/UpdateEmployeeType", json);
            //POST("http://localhost:15991/DeliveryOrderService.svc/DeleteEmployeeType", json);

            //POST1("http://localhost:15991/DeliveryOrderService.svc/LoadVehicle");
            //POST("http://localhost:15991/DeliveryOrderService.svc/InsertVehicle", json);
            //POST("http://localhost:15991/DeliveryOrderService.svc/UpdateVehicle", json);
            //POST("http://localhost:15991/DeliveryOrderService.svc/DeletelVehicle", json);

            //POST1("http://localhost:15991/DeliveryOrderService.svc/LoadVehicleType");
            //POST("http://localhost:15991/DeliveryOrderService.svc/InsertVehicleType", json);
            //POST("http://localhost:15991/DeliveryOrderService.svc/UpdateVehicleType", json);
            //POST("http://localhost:15991/DeliveryOrderService.svc/DeleteVehicleType", json);


            //POST1("http://localhost:15991/DeliveryOrderService.svc/GetProduct");
            //POST1("http://192.168.1.180:8342/DeliveryOrderService.svc/GetProduct");


            //POST("http://localhost:15991/DeliveryOrderService.svc/InsertCompany", json);
            //POST("http://localhost:15991/DeliveryOrderService.svc/UpdateCompany", json);
            //POST("http://localhost:15991/DeliveryOrderService.svc/DeleteCompany", json);


            //POST("http://localhost:15991/DeliveryOrderService.svc/InsertCustomer", json);
            //POST("http://localhost:15991/DeliveryOrderService.svc/UpdateCustomer", json);
            //POST("http://localhost:15991/DeliveryOrderService.svc/DeleteCustomer", json);

            //POST("http://localhost:15991/DeliveryOrderService.svc/UpdateDO", json);
            //POST("http://localhost:15991/DeliveryOrderService.svc/UpdateDODetail", json);

            //POST("http://localhost:15991/DeliveryOrderService.svc/InsertVisit", json);
            //POST("http://192.168.1.180:8342/DeliveryOrderService.svc/InsertVisit", json);
            //POST("http://localhost:15991/DeliveryOrderService.svc/InsertVisitDetail", json);
            

            //POST("http://localhost:15991/DeliveryOrderService.svc/InsertPurchaseOrder", json);
            //POST1("http://localhost:15991/DeliveryOrderService.svc/getPO");
            //POST1("http://localhost:15991/DeliveryOrderService.svc/getPOdetail");

            //POST("http://192.168.1.195:7890/DeliveryOrderService.svc/getconnote", json);
            //POST("http://localhost:15991/DeliveryOrderService.svc/getconnote", json);

            //POST1("http://192.168.1.176:7890/DeliveryOrderService.svc/loadvisit");
            //POST("http://localhost:15991/DeliveryOrderService.svc/getvisit", json);

            //POST("http://localhost:15991/DeliveryOrderService.svc/getmobileconfig", json);
            //POST("http://192.168.1.180:8342/DeliveryOrderService.svc/getmobileconfig", json);

            //POST("http://localhost:15991/DeliveryOrderService.svc/InsertDailyCost", json);
            

            //POST("http://localhost:15991/DeliveryOrderService.svc/checkincourierradius", json);
            //POST("http://192.168.1.179:8342/DeliveryOrderService.svc/getdailymessage", json);


            //POST("http://localhost:15991/DeliveryOrderService.svc/uploadjson", json);
            //PlaceOrder(json);
        }

        private void PlaceOrder(string json)
        {
            string data = json;
            WebClient webClient = new WebClient();
            webClient.Headers["Content-type"] = "application/json";
            webClient.Encoding = Encoding.UTF8;
            webClient.UploadString("http://localhost:54164/TIKIService.svc/insertPO", "POST", data);


        }

        void POST(string url, string jsonContent)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(jsonContent);

            request.ContentLength = byteArray.Length;
            request.ContentType = @"application/json";

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
            long length = 0;
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    length = response.ContentLength;
                    Stream responseStream = response.GetResponseStream();
                    var streamReader = new StreamReader(responseStream);
                    Label1.Text = streamReader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                Label1.Text = ex.ToString();
            }
        }

        void POST1(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();



            request.ContentType = @"application/json";
            request.ContentLength = 0;

            long length = 0;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                length = response.ContentLength;
                Stream responseStream = response.GetResponseStream();
                var streamReader = new StreamReader(responseStream);
                Label1.Text = streamReader.ReadToEnd();
            }


            // Log exception and throw as for GET example above

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (txtJSON.Text == "")
            {
                POST1(txtLINK.Text);
            }
            else
            {
                POST(txtLINK.Text, txtJSON.Text);
            }
        }

        protected void btnJsoncodebehind_Click(object sender, EventArgs e)
        {
            txtJSON.Text = Gjson;
        }
    }
}