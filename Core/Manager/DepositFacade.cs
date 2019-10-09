using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Newtonsoft.Json;
using System.Globalization;

namespace Core.Manager {
    
    public class DepositFacade : Base.Manager 
    {

        public static Model.mdlResultList UploadDeposit(List<Model.mdlDepositParam> lParamlist) 
        {

            var mdlResultList = new List<Model.mdlResult>();
            foreach (var lParam in lParamlist) {
                var mdlResult = new Model.mdlResult();

                List<SqlParameter> sp = new List<SqlParameter>() {
                    //header
                    new SqlParameter() {ParameterName = "@DepositID", SqlDbType = SqlDbType.NVarChar, Value = lParam.DepositID},
                    new SqlParameter() {ParameterName = "@VisitID", SqlDbType = SqlDbType.NVarChar, Value = lParam.VisitID},
                    new SqlParameter() {ParameterName = "@EmployeeID", SqlDbType = SqlDbType.NVarChar, Value = lParam.EmployeeID},
                    new SqlParameter() {ParameterName = "@Status", SqlDbType = SqlDbType.NVarChar, Value = lParam.Status},
                    new SqlParameter() {ParameterName = "@ReceivedDate", SqlDbType = SqlDbType.NVarChar, Value = lParam.ReceivedDate},
                    new SqlParameter() {ParameterName = "@Description", SqlDbType = SqlDbType.NVarChar, Value = lParam.Description},
                    new SqlParameter() {ParameterName = "@CustomerID", SqlDbType = SqlDbType.NVarChar, Value = lParam.CustomerID},
                    new SqlParameter() {ParameterName = "@CreatedBy", SqlDbType = SqlDbType.NVarChar, Value = lParam.CreatedBy},
                    //new SqlParameter() {ParameterName = "@CreatedDate", SqlDbType = SqlDbType.NVarChar, Value = lParam.CreatedDate},
                    new SqlParameter() {ParameterName = "@LastUpdateBy", SqlDbType = SqlDbType.NVarChar, Value = lParam.LastUpdateBy},
                    //new SqlParameter() {ParameterName = "@LastDate", SqlDbType = SqlDbType.NVarChar, Value = lParam.LastDate}
                    //detail
                    new SqlParameter() {ParameterName = "@Note", SqlDbType = SqlDbType.NVarChar, Value = lParam.Note},
                    new SqlParameter() {ParameterName = "@Amount", SqlDbType = SqlDbType.Decimal, Value = lParam.Amount},
                    new SqlParameter() {ParameterName = "@Seq", SqlDbType = SqlDbType.NVarChar, Value = lParam.Seq},
                    new SqlParameter() {ParameterName = "@DepositTypeID", SqlDbType = SqlDbType.NVarChar, Value = lParam.DepositTypeID}
                };

                String query = @"BEGIN TRAN
                                    DELETE FROM [DepositDetail]
                                    WHERE DepositID = @DepositID

                                    INSERT INTO[DepositDetail] (
                                        DepositID
                                        ,Note
                                        ,Amount
                                        ,Seq
                                        ,DepositTypeID
                                    )
                                   VALUES(
                                       @DepositID
                                       ,@Note
                                       ,@Amount
                                       ,@Seq
                                       ,@DepositTypeID
                                   )

                                    UPDATE [Deposit] SET
                                        VisitID = @VisitID
                                        ,EmployeeID = @EmployeeID
                                        ,Status = @Status
                                        ,ReceivedDate = @ReceivedDate
                                        ,Description = @Description
                                        ,CustomerID = @CustomerID
                                        ,LastUpdateBy = @LastUpdateBy
                                        ,LastDate = GETDATE()
                                    WHERE DepositID = @DepositID

                                    IF @@rowcount = 0
                                    BEGIN
                                        INSERT INTO [Deposit] (
                                            DepositID
                                            ,VisitID
                                            ,EmployeeID
                                            ,Status
                                            ,ReceivedDate
                                            ,Description
                                            ,CustomerID
                                            ,CreatedBy
                                            ,CreatedDate
                                            ,IsSMS
                                        ) 
                                        VALUES (
                                            @DepositID
                                            ,@VisitID
                                            ,@EmployeeID
                                            ,@Status
                                            ,@ReceivedDate
                                            ,@Description
                                            ,@CustomerID
                                            ,@CreatedBy
                                            ,GETDATE()
                                            ,0
                                        )
                                    END
                                COMMIT TRAN";

                mdlResult.Result = Manager.DataFacade.DTSQLVoidCommand(query, sp);

                if (mdlResult.Result == "1")
                {

                    // setelah upload berhasil akan memanggil API sms gateway
                    // sebelumnya cek dahulu apakah status smsnya berhasil atau tidak
                    List<SqlParameter> sp_checkstatus = new List<SqlParameter>() {
                        new SqlParameter() {ParameterName = "@DepositID", SqlDbType = SqlDbType.NVarChar, Value = lParam.DepositID}
                    };

                    string query_checkstatus = "SELECT IsSMS FROM DEPOSIT WHERE DepositID = @DepositID ";
                    DataTable dtCheck = DataFacade.DTSQLCommand(query_checkstatus, sp_checkstatus);
                    int statusSMS = 0;
                    foreach (DataRow rowCheck in dtCheck.Rows)
                    {
                        statusSMS = Convert.ToInt32(rowCheck["IsSMS"].ToString());
                    }

                    if (statusSMS == 1)
                    {
                        // do nothing
                        mdlResult.ResultValue = "UPLOAD SUCCESS";
                    }
                    else
                    {
                        // ambil company name
                        string CompanyName = "";
                        List<SqlParameter> spCompany = new List<SqlParameter>()
                        {

                        };

                        string qryCompany = "SELECT * FROM Company WHERE CompanyID = 'BPR'";
                        DataTable dtCompany = DataFacade.DTSQLCommand(qryCompany, spCompany);
                        foreach (DataRow rowCompany in dtCompany.Rows)
                        {
                            CompanyName = rowCompany["CompanyName"].ToString();
                        }

                        //default testing
                        string MsisdnSmsGateway = "82298332125";

                        //string MsisdnSmsGateway = lParam.CustomerMobilephone1;
                        string rekeningNo = lParam.BankAccountNumber;
                        string lTime = DateTime.Now.ToString("yyyyMMddhhmmss");
                        double value = lParam.Amount;
                        string valueDeli = String.Format(CultureInfo.InvariantCulture, "{0:0,0}", value);
                        string urlAPI = ConfigurationManager.AppSettings["UrlAPISmsGateway"];
                        string UserSmsGateway = ConfigurationManager.AppSettings["UserSmsGateway"];
                        string PasswordSmsGateway = ConfigurationManager.AppSettings["PasswordSmsGateway"];
                        string SenderIDSmsGateway = ConfigurationManager.AppSettings["SenderIDSmsGateway"];
                        string MessageSmsGateway = CompanyName + "\n" +
                            "SUKSES No=" + lParam.DepositID + "\n" +
                            "Setoran" + "\n" +
                            "Rek=" + rekeningNo + "\n" +
                            "an." + lParam.CustomerName + "\n" +
                            "Tgl=" + lTime + "\n" +
                            "Nom=Rp." + valueDeli;

                        var dict = new Dictionary<string, string>();
                        dict.Add("user", UserSmsGateway);
                        dict.Add("password", PasswordSmsGateway);
                        dict.Add("senderid", SenderIDSmsGateway);
                        dict.Add("message", MessageSmsGateway);
                        dict.Add("msisdn", MsisdnSmsGateway);
                        string paramSMS = StringFacade.urlencodeString(dict);

                        string resultSMS = StringFacade.PostAPISMSGateway(urlAPI, paramSMS);
                        var DesResult = JsonConvert.DeserializeObject<Model.mdlResultSmsGateway>(resultSMS);

                        if (DesResult.status == "SUCCESS")
                        {
                            mdlResult.ResultValue = "UPLOAD SUCCESS";

                            //ketika sms berhasil, update status isSMS
                            List<SqlParameter> sp_sms = new List<SqlParameter>() {
                                new SqlParameter() {ParameterName = "@DepositID", SqlDbType = SqlDbType.NVarChar, Value = lParam.DepositID}
                            };

                            string query_sms = "UPDATE DEPOSIT SET IsSMS = 1 WHERE DepositID =  @DepositID";
                            var mdlResultSms = new Model.mdlResult();
                            Manager.DataFacade.DTSQLVoidCommand(query_sms, sp_sms);

                            //simpan juga log smsnya
                            LogFacade.InsertLogSMS_Service(urlAPI, paramSMS, resultSMS);
                        }
                        else
                        {
                            mdlResult.ResultValue = "UPLOAD SUCCESS but SMS FAILED";
                        }
                    }
                    
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

            }

            var mdlResultListnew = new Model.mdlResultList();
            mdlResultListnew.ResultList = mdlResultList;
            return mdlResultListnew;
        }

    }

}