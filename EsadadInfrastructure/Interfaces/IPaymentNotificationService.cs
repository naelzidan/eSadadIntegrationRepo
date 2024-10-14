using Esadad.Infrastructure.DTOs;
using System.Xml;

namespace Esadad.Infrastructure.Interfaces
{
    public interface IPaymentNotificationService
    {
        public PaymentNotificationResponse GetPaymentNotificationResponse(Guid guid, string billingNumber, string serviceType, PaymentNotificationResponseTrxInf paymentNotificationRequestTrxInf, XmlElement xmlElement);

        public PaymentNotificationResponse GetInvalidSignatureResponse(Guid guid, string billingNumber, string serviceType, XmlElement xmlElement);
    }
}