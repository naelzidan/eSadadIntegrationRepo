using Esadad.Infrastructure.DTOs;
using Esadad.Infrastructure.Enums;
using Esadad.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml;

namespace EsadadAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrepaidController : ControllerBase
    {
        // private readonly IBillPullService _billPullService;
        //IBillPullService billPullService,
        private readonly ICommonService _commonService;

        public PrepaidController( ICommonService commonService)
        {
           // _billPullService = billPullService;
            _commonService = commonService;
        }

        [HttpPost("PrepaidValidation")]
        public IActionResult PrepaidValidation([FromQuery(Name = "GUID")] Guid guid,
                                     [FromBody] XmlElement xmlElement,
                                     [FromQuery(Name = "username")] string? username = null,
                                     [FromQuery(Name = "password")] string? password = null)
        {


            // Log Request 
            string? billingNumber = xmlElement.SelectSingleNode("//BillingNo")?.InnerText;
            string? serviceType = xmlElement.SelectSingleNode("//ServiceType")?.InnerText;

            //Log to EsadadTransactionsLogs Table
            var tranLog = _commonService.InsertLog(TransactionTypeEnum.Request.ToString(), ApiTypeEnum.PrepaidValidationWithCategory.ToString(), guid.ToString(), xmlElement);

            //BillPullResponse billPullResponse = null;
            //if (!DigitalSignature.VerifySignature(xmlElement))
            //{

            //    billPullResponse = _billPullService.GetInvalidSignatureResponse(guid, billingNumber, serviceType);

            //    return Ok(billPullResponse);
            //}
            //else
            //{
            //    //Log Response
            //    billPullResponse = _billPullService.BillPull(guid, xmlElement);
            //    return Ok(billPullResponse);
            //}


            return Ok(new PrePaidResponse());

        }

    }
}
