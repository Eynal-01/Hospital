using Hospital.Entities.Data;
using HospitalProject.Entities.DbEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private UserManager<CustomIdentityUser> _userManager;
        private RoleManager<CustomIdentityRole> _roleManager;
        private IWebHostEnvironment _webHost;
        private readonly CustomIdentityDbContext _context;
        private readonly string accountSid = "AC3e0d0c3d03757e62aa7be691723ca5f7";
        private readonly string authToken = "ef5007e8c66d5a0ceafeb68f0f49f395";

        public SendSMSController(UserManager<CustomIdentityUser> userManager, RoleManager<CustomIdentityRole> roleManager, IWebHostEnvironment webHost, CustomIdentityDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _webHost = webHost;
            _context = context;
        }

        /// <summary>
        /// This method send message to patient when patient booking appointment.
        /// </summary>
        /// <returns></returns>
        [HttpPost("SendText")]
        public async Task<IActionResult> SendText()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var ids = user.Id;
            var email = user.Email;
            var appointments = await _context.Appointments.Where(a => a.PatientId == ids).OrderByDescending(a => a.Id).ToListAsync();
            var lastAppointment = new Appointment();

            for (int i = 0; i < 1; i++)
            {
                lastAppointment = appointments[i];
            }

            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == lastAppointment.DoctorId);
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == doctor.RoomId);
            var date = lastAppointment.AppointmentDate.ToString().Split(' ')[0];

            TwilioClient.Init(accountSid, authToken);
            var message = MessageResource.Create(
                body: $"Your appointment has been set successfully\nRoom No : {room.RoomNo}\nDoctor : {doctor.FirstName} {doctor.LastName}\nDate : {date}\nTime : {lastAppointment.AppointmentTime}",
                from: new Twilio.Types.PhoneNumber("+15135923952"),
                to: new Twilio.Types.PhoneNumber("+994" + "703088884"));
            return RedirectToAction("Index", "Home");
        }
    }
}