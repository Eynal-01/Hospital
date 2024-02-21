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
using Microsoft.AspNetCore.Components.Forms;
using Hospital.Entities.DbEntities;
using Hospital.Business.Abstract;

namespace Hospital.WebUI.Controllers
{
    [Authorize(Roles = "patient")]

    public class HomeController : Controller
    {
        private readonly UserManager<CustomIdentityUser> _userManager;
        public CustomIdentityDbContext _dbContext { get; set; }
        private readonly IDataService _dataService;

        public HomeController(CustomIdentityDbContext dbContext, UserManager<CustomIdentityUser> userManager, IDataService dataService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _dataService = dataService;
        }

        [HttpGet]
        public async Task<IActionResult> Appointment()
        {
            var doctors = await _dbContext.Doctors.ToListAsync();
            var departments = await _dbContext.Departments.ToListAsync();
            var availableDates = await _dbContext.AvailableDates.ToListAsync();
            var availableTimes = await _dbContext.AvailableTimes.ToListAsync();
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
            if (availableTimes != null)
            {
                viewModel.AvailableTimes = availableTimes;
            }
            if (availableDates != null)
            {
                viewModel.AvailableDates = availableDates;
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Appoinment(AppoinmentViewModel viewModel)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var patient = await _dbContext.Patients.FirstOrDefaultAsync(p => p.Email == user.Email && p.UserName == user.UserName);
            var appointments = await _dbContext.Appointments.ToListAsync();
            var receivedData = _dataService.RetrieveData();
            var department = await _dbContext.Departments.FirstOrDefaultAsync(d => d.Id == viewModel.DepartmentId);
            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(d => d.Id == viewModel.DoctorId);

            var appoinment = new Appointment
            {
                AppointmentDateId = viewModel.AvailableDateId,
                AppointmentTimeId = viewModel.AvailableTimeId,
                Age = patient.Age,
                DoctorId = doctor.Id,
                DepartmentId = department.Id,
                PatientId = patient.Id.ToString(),
                Message = viewModel.Message,
            };

            //viewModel.AvailableDates = receivedData;
            //for (int i = 0; i < appointments.Count; i++)
            //{
            //    if (appointments[i].AppointmentTimeId == appoinment.AppointmentTimeId && appointments[i].DoctorId == appoinment.DoctorId)
            //    {
            //        Console.Beep();
            //        break;
            //    }
            //    else
            //    {
            //        var doctor1 = _dbContext.Doctors.FirstOrDefault(d => d.Id == appoinment.DoctorId);
            //        await _dbContext.Appointments.AddAsync(appoinment);
            //        await _dbContext.SaveChangesAsync();
            //    }
            //}
            return RedirectToAction("Appoinment", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableDays()
        {
            var counter = 0;
            var admin = await _dbContext.Admins.FirstOrDefaultAsync(a => a.Id == a.Id);
            if (admin != null)
            {
                counter = admin.WorkDaysCount;
            }
            DateTime startDate = DateTime.Today;
            List<string> dateList = new List<string>();
            for (int i = 0; i < counter; i++)
            {
                DateTime currentDate = startDate.AddDays(i);
                var d=currentDate.ToShortDateString();
                dateList.Add(d);
            }
            return Ok(dateList);
        }

        public async Task<IActionResult> GetDoctors(int departmentId)
        {
            var doctors = _dbContext.Doctors.Where(d => d.DepartmentId == departmentId).ToListAsync();
            return Ok(doctors);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
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