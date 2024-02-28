using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Hospital.WebUI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SendSMSController : ControllerBase
    {
        private readonly string accountSid = "ACa25acf39d02f079fc43f5feab218351c";
        private readonly string authToken = "ea8d2ab1731a4d0a0ee4e87f54243a13";
        [HttpPost("SendText")]
        public async Task<IActionResult> SendText(string phoneNumber)
        {
            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "Hello, Your appointment completed successfully",
                from: new Twilio.Types.PhoneNumber("+18144984093"),
                to: new Twilio.Types.PhoneNumber("+994" + phoneNumber.ToString())
                );
            return StatusCode(200, new { message = message.Sid });
        }
    }
}