using Hospital.Business.Abstract;
using Hospital.Business.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.WebUI.Controllers
{
    public class SendEmailController : Controller
    {
        private readonly IEmailSender _emailSender;
        public SendEmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmailText(string email)
            {
            
            var subject = "Hospital Project";
            var message = "Mail for course project";
            await _emailSender.SendEmailAsync(email, subject, message);
            return RedirectToAction("Index", "Home");
        }
    }
}
