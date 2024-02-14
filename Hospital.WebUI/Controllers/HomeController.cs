using Hospital.Entities.Data;
using Hospital.WebUI.Models;
using HospitalProject.Entities.DbEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System;
using System.Media;

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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Appoinment(AppoinmentViewModel viewModel)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var patient = await _dbContext.Patients.FirstOrDefaultAsync(p => p.Email == user.Email && p.UserName == user.UserName);
            var department = await _dbContext.Departments.FirstOrDefaultAsync(d => d.DepartmentName == viewModel.DepartmentName);
            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(d => d.FirstName + " " + d.LastName == viewModel.DoctorName);
            var appointments = await _dbContext.Appointments.ToListAsync();


            var appoinment = new Appointment
            {
                AppointmentDate = viewModel.Date,
                AppointmentTime = viewModel.AvailableTime,
                AvailableTimeId = viewModel.AvailableTimeId,
                Age = patient.Age,
                DoctorId = doctor.Id,
                DepartmentId = department.Id,
                PatientId = patient.Id.ToString(),
                Message = viewModel.Message,
            };
            for (int i = 0; i < appointments.Count; i++)
            {
                if (appointments[i].AvailableTimeId == appoinment.AvailableTimeId && appointments[i].DoctorId == appoinment.DoctorId)
                {
                    Console.Beep();
                    break;
                }
                else
                {
                    var doctor1 = _dbContext.Doctors.FirstOrDefault(d => d.Id == appoinment.DoctorId);
                    await _dbContext.Appointments.AddAsync(appoinment);
                    await _dbContext.SaveChangesAsync();
                }
            }
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
