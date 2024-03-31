using Hospital.Entities.Data;
using HospitalProject.Entities.DbEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly string accountSid = "ACa25acf39d02f079fc43f5feab218351c";
        private readonly string authToken = "f92103789cc9722b7ff926953f807d25";

        public SendSMSController(UserManager<CustomIdentityUser> userManager, RoleManager<CustomIdentityRole> roleManager, IWebHostEnvironment webHost, CustomIdentityDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _webHost = webHost;
            _context = context;
        }

        [HttpPost("SendText")]
        public async Task<IActionResult> SendText()
        {
            var current = await _userManager.GetUserAsync(HttpContext.User);
            var user = await _context.Patients.FirstOrDefaultAsync(p => p.Email == current.Email && p.UserName == current.UserName);
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
                from: new Twilio.Types.PhoneNumber("+18144984093"),
                to: new Twilio.Types.PhoneNumber("+994" + "703088884"));
            return RedirectToAction("Index", "Home");
        }
    }
}