using Hospital.Entities.Data;
using Hospital.WebUI.Models;
using HospitalProject.Entities.DbEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.WebUI.Controllers
{
    [Authorize(Roles = "doctor")]
    public class DoctorController : Controller
    {
        private readonly UserManager<CustomIdentityUser> _userManager;
        public CustomIdentityDbContext _dbContext { get; set; }

        public DoctorController(UserManager<CustomIdentityUser> userManager, CustomIdentityDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> ShowAllAppointments()
        {
            List<Appointment> appointments =new List<Appointment>();
            var allAppointments = await _dbContext.Appointments.ToListAsync();
            for (int i = 0; i < allAppointments.Count(); i++)
            {
                if (allAppointments[i].DoctorId== CurrentUser().Id.ToString())
                {
                    appointments.Add(allAppointments[i]);   
                }
            }
            return Ok(appointments);
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Chat()
        {
            return View();
        }

        public IActionResult Error404()
        {
            return View();
        }

        public IActionResult BlogList()
        {
            return View();
        }

        public async Task<Doctor> CurrentUser()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var admin = await _dbContext.Doctors.FirstOrDefaultAsync(a => a.UserName == user.UserName && a.Email == user.Email);
            return admin;
        }

        public IActionResult Error500()
        {
            return View();
        }

        public IActionResult AllDepartments()
        {
            return View();
        }

        public IActionResult AllDoctors()
        {
            return View();
        }

        public IActionResult Appoinment()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult DoctorSchedule()
        {
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        public IActionResult Inbox()
        {
            return View();
        }

        public IActionResult Locked()
        {
            return View();
        }

        public IActionResult MailSingle()
        {
            return View();
        }

        public IActionResult MoreDepartments()
        {
            return View();
        }

        public IActionResult PatientInvoice()
        {
            return View();
        }

        public IActionResult PatientProfile()
        {
            return View();
        }

        public IActionResult Patients()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var roles = await _userManager.GetRolesAsync(user);
            var selected = roles.FirstOrDefault();
            return RedirectToAction("Login", "Authentication", new { selected });
        }
    }
}
