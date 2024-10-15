using Esadad.Core.Entities;
using Esadad.Infrastructure.DTOs;
using Esadad.Infrastructure.Helpers;
using Esadad.Infrastructure.Interfaces;
using Esadad.Infrastructure.MemCache;
using Esadad.Infrastructure.Persistence;
using System.Xml;

namespace Esadad.Infrastructure.Services
{
    public class CommonService(EsadadIntegrationDbContext context) : ICommonService
    {
        private readonly EsadadIntegrationDbContext _context = context;

        public EsadadTransactionLog InsertLog(string transactionType, string apiName, string guid, XmlElement xmlElement, Object responseObject=null)
        {
           
            //BillPullRequest billPullRequestObj = null;
            EsadadTransactionLog esadadTransactionLog = null;

           
            //PaymentNotificationRequestDto paymentNotificationRequestDtoObj = null;

            if (transactionType.ToLower() == "request")
            {
                if (apiName == "BillPull")
                {
                    var billPullRequestObj = XmlToObjectHelper.DeserializeXmlToObject(xmlElement, new BillPullRequest());
                    esadadTransactionLog = new EsadadTransactionLog
                    {
                        TransactionType = transactionType,
                        ApiName = apiName,
                        Guid = guid,
                        Timestamp = billPullRequestObj.MsgHeader.TmStp,
                        BillingNumber = billPullRequestObj.MsgBody.AcctInfo.BillingNo,
                        BillNumber = billPullRequestObj.MsgBody.AcctInfo.BillNo,
                        ServiceType = billPullRequestObj.MsgBody.ServiceType,
                        Currency = MemoryCache.Biller.Services.First(b => b.ServiceTypeCode == billPullRequestObj.MsgBody.ServiceType).Currency,
                        TranXmlElement = xmlElement.OuterXml
                    };

                }
                else if (transactionType.ToLower() == "ReceivePaymentNotification")
                {
                    //PaymentNotificationRequestDto paymentNotificationRequestDto = new PaymentNotificationRequestDto();
                    var paymentNotificationRequestDtoObj = XmlToObjectHelper.DeserializeXmlToObject(xmlElement, new PaymentNotificationRequestDto());
                    esadadTransactionLog = new EsadadTransactionLog
                    {
                        TransactionType = transactionType,
                        ApiName = apiName,
                        Guid = guid,
                        Timestamp = paymentNotificationRequestDtoObj.MsgHeader.TmStp,
                        BillingNumber = paymentNotificationRequestDtoObj.MsgBody.Transactions.TrxInf.AcctInfo.BillingNo,
                        BillNumber = paymentNotificationRequestDtoObj.MsgBody.Transactions.TrxInf.AcctInfo.BillNo,
                        ServiceType = paymentNotificationRequestDtoObj.MsgBody.Transactions.TrxInf.ServiceTypeDetails.ServiceType,
                        Currency = MemoryCache.Biller.Services.First(b => b.ServiceTypeCode == paymentNotificationRequestDtoObj.MsgBody.Transactions.TrxInf.ServiceTypeDetails.ServiceType).Currency,
                        ValidationCode = paymentNotificationRequestDtoObj.MsgBody.Transactions.TrxInf.AcctInfo.BillNo,
                        PrepaidCat= paymentNotificationRequestDtoObj.MsgBody.Transactions.TrxInf.ServiceTypeDetails.PrepaidCat,
                        TranXmlElement = xmlElement.OuterXml
                    };
                }

            }else if (transactionType.ToLower() == "response" && responseObject != null)
                {
                if (apiName == "BillPull")
                {
                    var billPullResponseObj = (BillPullResponse) responseObject;
                    esadadTransactionLog = new EsadadTransactionLog
                    {
                        TransactionType = transactionType,
                        ApiName = apiName,
                        Guid = guid,
                        Timestamp = billPullResponseObj.MsgHeader.TmStp,
                        BillingNumber = billPullResponseObj.MsgBody.BillsRec.BillRec.AcctInfo.BillingNo,
                        BillNumber = billPullResponseObj.MsgBody.BillsRec.BillRec.AcctInfo.BillNo,
                        ServiceType = billPullResponseObj.MsgBody.BillsRec.BillRec.ServiceType,
                        Currency = MemoryCache.Biller.Services.First(b => b.ServiceTypeCode == billPullResponseObj.MsgBody.BillsRec.BillRec.ServiceType).Currency,
                        TranXmlElement = xmlElement.OuterXml
                    };

                }
                else if (transactionType.ToLower() == "ReceivePaymentNotification")
                {
                    //PaymentNotificationRequestDto paymentNotificationRequestDto = new PaymentNotificationRequestDto();
                    var paymentNotificationResponseObj = (PaymentNotificationResponse)responseObject;
                    esadadTransactionLog = new EsadadTransactionLog
                    {
                        TransactionType = transactionType,
                        ApiName = apiName,
                        Guid = guid,
                        Timestamp = paymentNotificationResponseObj.MsgHeader.TmStp,
                        //BillingNumber = paymentNotificationResponseObj.MsgBody.Transactions.TrxInf.,
                        //BillNumber = paymentNotificationResponseObj.MsgBody.Transactions.TrxInf.AcctInfo.BillNo,
                        //ServiceType = paymentNotificationResponseObj.MsgBody.Transactions.TrxInf.ServiceTypeDetails.ServiceType,
                        //Currency = MemoryCache.Biller.Services.First(b => b.ServiceTypeCode == paymentNotificationResponseObj.MsgBody.Transactions.TrxInf.ServiceTypeDetails.ServiceType).Currency,
                        //ValidationCode = paymentNotificationResponseObj.MsgBody.Transactions.TrxInf.AcctInfo.BillNo,
                        //PrepaidCat = paymentNotificationResponseObj.MsgBody.Transactions.TrxInf.ServiceTypeDetails.PrepaidCat,
                       
                        TranXmlElement = xmlElement.OuterXml
                    };

                }


            }

            var query = _context.EsadadTransactionsLogs.Add(esadadTransactionLog).Entity;

            _context.SaveChanges();
            return query;
        }

        //EsadadTransactionLog ICommonService.InsertLog(string transactionType, string apiName, string guid, XmlElement requestElement, object responseObject)
        //{
        //    throw new NotImplementedException();
        //}

        EsadadPaymentLog ICommonService.InsertPaymentLog(string transactionType, string apiName, string guid, XmlElement requestElement)
        {
            throw new NotImplementedException();
        }
    }
}