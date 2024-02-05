using Hospital.Entities.Data;
using Hospital.WebUI.Models;
using HospitalProject.Entities.DbEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Hospital.WebUI.Controllers
{
    [Authorize(Roles = "patient")]

    public class HomeController : Controller
    {
        private readonly UserManager<CustomIdentityUser> _userManager;
        public CustomIdentityDbContext _dbContext { get; set; }

        public HomeController(CustomIdentityDbContext dbContext, UserManager<CustomIdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Appoinment()
        {
            var doctors = await _dbContext.Doctors.ToListAsync();
            var departments = await _dbContext.Departments.ToListAsync();
            var viewModel = new AppoinmentViewModel
            {
                Departments = new List<Department>(),
            };
            if (doctors != null)
            {
                viewModel.Doctors = doctors;
            }
            if (departments != null)
            {
                viewModel.Departments = departments;
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Appoinment(AppoinmentViewModel viewModel)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            //if (ModelState.IsValid)
            //{
            var patient = await _dbContext.Patients.FirstOrDefaultAsync(p => p.Email == user.Email && p.UserName == user.UserName);
            //var department = await _dbContext.Departments.FirstOrDefaultAsync(d => d.Id == viewModel.DepartmentId);
            //var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(d => d.Id == viewModel.DoctorId.ToString());
            //var appoinment = new Appointment
            //{
            //    AppointmentDate = viewModel.Date,
            //    AppointmentTime = viewModel.DateInTime,
            //    Age = patient.Age,
            //    DoctorId = 3,
            //    DepartmentId = 2,
            //    PatientId = "1"
            //};
            var appoinment = new Appointment
            {
                AppointmentDate = viewModel.Date,
                AppointmentTime = viewModel.DateInTime,
                Age = patient.Age,
                DoctorId = viewModel.DoctorId,
                DepartmentId = viewModel.DepartmentId,
                PatientId = patient.Id.ToString(),
            };
            await _dbContext.Appointments.AddAsync(appoinment);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Appoinment", "Home");
        }

        public IActionResult BlogSindebar()
        {
            return View();
        }

        public IActionResult BlogSingle()
        {
            return View();
        }

        public IActionResult Comfirmation()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Department()
        {
            return View();
        }

        public IActionResult DepartmentSingle()
        {
            return View();
        }

        public IActionResult Doctor()
        {
            return View();
        }

        public IActionResult DoctorSingle()
        {
            return View();
        }

        public IActionResult Service()
        {
            return View();
        }
    }
}
