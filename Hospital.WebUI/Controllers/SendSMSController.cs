using Microsoft.AspNetCore.Mvc;
using System.Globalization;
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
        /// <summary>
        /// This method send message to client when he/she books appointment.
        /// </summary>
        /// <returns></returns>
        [HttpPost("SendText")]
        public async Task<IActionResult> SendText()
        {
            List<string> numbers = new List<string> { "703088884", "704372282", "518763907" };
            TwilioClient.Init(accountSid, authToken);
            for (int i = 0; i < numbers.Count(); i++)
            {
                var message = MessageResource.Create(
                    body: "Hello, Your appointment completed successfully",
                    from: new Twilio.Types.PhoneNumber("+18144984093"),
                    to: new Twilio.Types.PhoneNumber("+994" + numbers[i]));
                return StatusCode(200, new { message = message.Sid });
            }
            return RedirectToAction("Index", "Home");

        }
    }
}
//public class SendSMSController : ControllerBase
//{
//    private readonly string accountSid = "ACa25acf39d02f079fc43f5feab218351c";
//    private readonly string authToken = "ea8d2ab1731a4d0a0ee4e87f54243a13";
//    /// <summary>
//    /// This method send message to client when he/she books appointment.
//    /// </summary>
//    /// <returns></returns>
//    [HttpPost("SendText")]
//    public async Task<IActionResult> SendText()
//    {
//        List<string> numbers = new List<string> { "703088884", "704372282", "518763907" };
//        TwilioClient.Init(accountSid, authToken);
//        for (int i = 0; i < numbers.Count(); i++)
//        {
//            var message = MessageResource.Create(
//                body: "Hello, Your appointment completed successfully",
//                from: new Twilio.Types.PhoneNumber("+18144984093"),
//                to: new Twilio.Types.PhoneNumber("+994" + numbers[i]));
//            return StatusCode(200, new { message = message.Sid });
//        }
//        return Ok();

//    }
//}
//}
