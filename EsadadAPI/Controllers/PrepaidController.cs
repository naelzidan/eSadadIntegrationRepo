using Esadad.Infrastructure.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml;

namespace EsadadAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrepaidController : ControllerBase
    {
        [HttpPost("BillPull")]
        public IActionResult PrepaidValidation([FromQuery(Name = "GUID")] Guid guid,
                                     [FromBody] XmlElement xmlElement,
                                     [FromQuery(Name = "username")] string? username = null,
                                     [FromQuery(Name = "password")] string? password = null)
        {
            return Ok(new PrePaidResponse());

        }

    }
}
