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
using Twilio.Rest.Trunking.V1;

namespace Hospital.WebUI.Controllers
{
    [Authorize(Roles = "patient")]

    public class HomeController : Controller
    {
        private readonly UserManager<CustomIdentityUser> _userManager;
        public CustomIdentityDbContext _dbContext { get; set; }
        private readonly IDataService _dataService;

        //public DoctorController _doctorController { get; set; }                                     

        public HomeController(CustomIdentityDbContext dbContext, UserManager<CustomIdentityUser> userManager, IDataService dataService/*, DoctorController doctorController*/)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _dataService = dataService;
            //_doctorController = doctorController;
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
            var doctors = _dbContext.Doctors.ToList();

            var appoinment = new Appointment
            {
                Age = patient.Age,
                DoctorId = doctor.Id,
                DepartmentId = department.Id,
                PatientId = patient.Id.ToString(),
                Message = viewModel.Message,
                AppointmentTime = viewModel.AppointmentTime,
                AppointmentDate = viewModel.AppointmentDate
            };
            for (int i = 0; i < doctors.Count(); i++)
            {
                if (doctors[i].Id == doctor.Id)
                {
                }
            }
            await _dbContext.Appointments.AddAsync(appoinment);
            await _dbContext.SaveChangesAsync();
            //await _doctorController.ShowAllAppointments();
            return RedirectToAction("SuccessPay","Home");
        }

        public async Task<Patient> CurrentUser()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var admin = await _dbContext.Patients.FirstOrDefaultAsync(a => a.UserName == user.UserName && a.Email == user.Email);
            return admin;
        }

        public async Task<IActionResult> GetAllPost()
        {
            var user = await CurrentUser();

            var data = await _dbContext.Admins.ToListAsync();
            var posts = new List<PostsShowViewModel>();
            foreach (var item in data)
            {
                var post = await _dbContext.Posts.Where(p => p.AdminId == item.Id).ToListAsync();
                for (int i = 0; i < post.Count(); i++)
                {
                    var images = post[i].ImageUrl.Split(':').ToList();
                    if (post[i].ImageUrl != null)
                    {
                        post[i].IsImage = true;
                    }
                    else
                    {
                        post[i].IsImage = false;
                    }

                    for (int k = 0; k < images.Count(); k++)
                    {
                        images[k] = images[k].TrimStart();
                    }

                    var poo = new PostsShowViewModel();
                    poo.PostId = post[i].Id;
                    poo.Admin = item;
                    poo.Content = post[i].Content;
                    //poo.PublishTime = posts[i].PublishTime;
                    poo.Title = post[i].Title;
                    poo.ViewCount = post[i].ViewCount;
                    poo.Images = images;
                    posts.Add(poo);
                }
            }
            return Ok(new { posts = posts });
        }

        [HttpGet]
        public async Task<string> CheckInputs(string phoneNumber, string fullName)
        {
            var v = "";
            if (phoneNumber == "0")
            {
                v += "phone is null";
            }
            if (fullName == null)
            {
                v += " fullname is null";
            }
            return v;
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableDays(string doctorId)
        {
            var noWorkingTimes = await _dbContext.NoWorkingTimes.ToListAsync();
            var counter = 0;
            var admins = await _dbContext.Admins.ToListAsync();
            for (int i = 0; i < admins.Count(); i++)
            {
                if (admins[i].WorkDaysCount > 0)
                {
                    counter = admins[i].WorkDaysCount;
                }
            }

            DateTime startDate = DateTime.Today;
            var appointments = await _dbContext.Appointments.ToListAsync();
            List<string> dateList = new List<string>();

            for (int i = 0; i < counter; i++)
            {
                DateTime currentDate = startDate.AddDays(i);
                var d = currentDate.ToShortDateString();
                var dt = DateTime.Parse(d);
                dateList.Add(d);
                for (int k = 0; k < noWorkingTimes.Count(); k++)
                {
                    if (noWorkingTimes[k].Day == dt && noWorkingTimes[k].DoctorId == doctorId)
                    {
                        dateList.Remove(d);
                    }
                }
            }
            return Ok(dateList);
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableTimes(string doctorId, string appointmentDate)
        {
            var appointments = await _dbContext.Appointments.ToListAsync();
            List<string> timeList = new List<string>();
            var times = await _dbContext.AvailableTimes.ToListAsync();
            for (int i = 0; i < times.Count(); i++)
            {
                var s = times[i].StartTime.ToShortTimeString();
                var e = times[i].EndTime.ToShortTimeString();
                var time = $"{s} - {e}";
                timeList.Add(time);

                for (int k = 0; k < appointments.Count(); k++)
                {
                    if (appointments[k].AppointmentTime == time
                        && appointments[k].DoctorId == doctorId
                        && appointments[k].AppointmentDate.ToString().Split(" ")[0] == appointmentDate)
                    {
                        timeList.Remove(time);
                    }
                }
            }
            return Ok(timeList);
        }

        public async Task<IActionResult> GetDoctors(int departmentId)
        {
            var doctors = await _dbContext.Doctors.Where(d => d.DepartmentId == departmentId).ToListAsync();
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

        public IActionResult SuccessPay()
        {
            return View();
        }
    }
}