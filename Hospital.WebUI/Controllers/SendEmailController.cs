﻿using Hospital.Business.Abstract;
using Hospital.Business.Concrete;
using Hospital.Entities.Data;
using HospitalProject.Entities.DbEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Twilio.AspNet.Core;

namespace Hospital.WebUI.Controllers
{
    public class SendEmailController : Controller
    {
        private UserManager<CustomIdentityUser> _userManager;
        private RoleManager<CustomIdentityRole> _roleManager;
        private readonly CustomIdentityDbContext _context;
        private IWebHostEnvironment _webHost;
        private readonly IEmailSender _emailSender;

        public SendEmailController(UserManager<CustomIdentityUser> userManager,
            RoleManager<CustomIdentityRole> roleManager,
            IWebHostEnvironment webHost,
            IEmailSender emailSender,
            CustomIdentityDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _webHost = webHost;
            _emailSender = emailSender;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmailText()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var email = user.Email;
            var appointments = await _context.Appointments.Where(a => a.PatientId == "d0ffd81d-5ac0-4291-b44d-4381c4fe92b9").OrderByDescending(a => a.Id).ToListAsync();
            var lastAppointment = new Appointment();
            for (int i = 0; i < 1; i++)
            {
                lastAppointment = appointments[i];
            }
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == lastAppointment.DoctorId);
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == doctor.RoomId);
            var date = lastAppointment.AppointmentDate.ToString().Split(' ')[0];

            var subject = "Appointment information";
            var message = $"Your appointment has been set successfully\nRoom No : {room.RoomNo}\nDoctor : {doctor.FirstName} {doctor.LastName}\nDate : {date}\nTime : {lastAppointment.AppointmentTime}";
            await _emailSender.SendEmailAsync(email, subject, message);
            return RedirectToAction("Index", "Home");
        }
    }
}
