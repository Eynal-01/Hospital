using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Hospital.WebUI.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]
    public class SendSMSController : ControllerBase
    {
        string accountSid = "ACa25acf39d02f079fc43f5feab218351c";
        string authToken = "9189f6339c990014712522cea219fa51";
        [HttpPost("SendText")]
        public ActionResult SendText()
        {
            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "Hello, Your appointment completed successfully",
                from: new Twilio.Types.PhoneNumber("+18144984093"),
                to: new Twilio.Types.PhoneNumber("+994" + "703088884")
                );

            return StatusCode(200, new { message = message.Sid });
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
