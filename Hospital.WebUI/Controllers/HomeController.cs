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

        [HttpGet]
        public async Task<IActionResult> Appoinment()
        {
            var doctors = await _dbContext.Doctors.ToListAsync();
            var departments = await _dbContext.Departments.ToListAsync();
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
            DateTime today = DateTime.Today;
            var adminselect = 10;
            for (int i = 0; i < 10; i++)
            {
                today.AddDays(1);
            }
            //DateTime tomorrow = today.AddDays(1);
            //DateTime dayAfterTomorrow = today.AddDays(2);

            //List<AvailableDate> availableDates = new List<AvailableDate>()
            //{
            //    new AvailableDate { Date = today },
            //    new AvailableDate { Date = tomorrow },
            //    new AvailableDate { Date = dayAfterTomorrow }
            //};
            ////availableDates.Add(new AvailableDate { Date = today });
            ////availableDates.Add(new AvailableDate { Date = tomorrow });
            ////availableDates.Add(new AvailableDate { Date = dayAfterTomorrow });

            //viewModel.AvailableDates = availableDates;

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var patient = await _dbContext.Patients.FirstOrDefaultAsync(p => p.Email == user.Email && p.UserName == user.UserName);
            var department = await _dbContext.Departments.FirstOrDefaultAsync(d => d.Id.ToString() == viewModel.DepartmentId);
            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(d => d.Id == viewModel.DoctorId);
            var appointments = await _dbContext.Appointments.ToListAsync();

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
        public async Task<IActionResult> GetAvailableDays(string availableCount)
        {
            DateTime startDate = DateTime.Today;
            List<DateTime> dateList = new List<DateTime>();
            for (int i = 0; i < int.Parse(availableCount); i++)
            {
                DateTime currentDate = startDate.AddDays(i);
                dateList.Add(currentDate);
            }
            return Ok(dateList);
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
