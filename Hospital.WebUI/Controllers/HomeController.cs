using Hospital.Entities.Data;
using Hospital.WebUI.Models;
using HospitalProject.Entities.DbEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly CustomIdentityDbContext _context;


        public HomeController(CustomIdentityDbContext dbContext, UserManager<CustomIdentityUser> userManager, IDataService dataService, CustomIdentityDbContext context)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _dataService = dataService;
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Appointment()
        {
            var departments = await _dbContext.Departments.ToListAsync();
            var viewModel = new AppoinmentViewModel
            {
                Departments = new List<Department>(),
            };
            if (departments != null)
            {
                viewModel.Departments = departments;
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Appointment(AppoinmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var patient = await _dbContext.Patients.FirstOrDefaultAsync(p => p.Email == user.Email && p.UserName == user.UserName);
                var appointments = await _dbContext.Appointments.ToListAsync();
                var receivedData = _dataService.RetrieveData();
                var department = await _dbContext.Departments.FirstOrDefaultAsync(d => d.Id == viewModel.DepartmentId);
                var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(d => d.Id == viewModel.DoctorId);
                var doctors = _dbContext.Doctors.ToList();

                var date1 = viewModel.AppointmentDate.ToString().Split('T')[0];
                var date2 = date1.Split(' ')[0];

                var appoinment = new Appointment
                {
                    Age = patient.Age,
                    DoctorId = doctor.Id,
                    DepartmentId = department.Id,
                    PatientId = patient.Id.ToString(),
                    Message = viewModel.Message,
                    AppointmentTime = viewModel.AppointmentTime,
                    AppointmentDate = DateTime.Parse(date2),
                };
                await _dbContext.Appointments.AddAsync(appoinment);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("SuccessPay", "Home");
            }
            return RedirectToAction("SuccessPay", "Home");
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
                    poo.Title = post[i].Title;
                    poo.ViewCount = post[i].ViewCount;
                    poo.Images = images;
                    posts.Add(poo);
                }
            }
            return Ok(new { posts = posts });
        }

        [HttpGet]
        public async Task<string> CheckInputs(string phoneNumber, string fullName, string date, string time, string message)
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
            if (phoneNumber != "0" && fullName != null && date == null && time != null && message != null)
            {
                v += "okay";
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


        private readonly TimeSpan morningStart = new TimeSpan(9, 0, 0); // 09:00
        private readonly TimeSpan morningEnd = new TimeSpan(13, 0, 0);  // 13:00
        private readonly TimeSpan afternoonStart = new TimeSpan(13, 0, 0);  // 13:00
        private readonly TimeSpan afternoonEnd = new TimeSpan(18, 0, 0);   // 18:00

        [HttpGet]
        public async Task<IActionResult> GetAvailableTimes(string doctorId, string appointmentDate)
        {
            var s = appointmentDate + " 00:00:00";
            var appointments = await _context.Appointments
                //.Where(a => a.DoctorId == doctorId && a.AppointmentDate.ToString() == s)
                .ToListAsync();

            var timeSlots = new List<string>();

            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == doctorId);
            if (doctor == null)
            {
                return NotFound("Doctor not found");
            }


            var timeAround = await _context.Schedules.FirstOrDefaultAsync(t => t.Id == doctor.ScheduleId);
            if (timeAround == null)
            {
                return NotFound("Schedule not found");
            }

            TimeSpan startTime;
            TimeSpan endTime;

            if (timeAround.WorkTime.Split('-')[0].Trim() == "09:00")
            {
                startTime = morningStart;
                endTime = morningEnd;
            }
            else
            {
                startTime = afternoonStart;
                endTime = afternoonEnd;
            }

            GenerateTimeSlotsForRange(startTime, endTime, ref timeSlots);

            foreach (var appointment in appointments)
            {
                var appointmentStartTime = TimeSpan.Parse(appointment.AppointmentTime.Split('-')[0].Trim());
                var appointmentEndTime = TimeSpan.Parse(appointment.AppointmentTime.Split('-')[1].Trim());

                var appointmentSlot = $"{appointmentStartTime:hh\\:mm} - {appointmentEndTime:hh\\:mm}";

                if (timeSlots.Contains(appointmentSlot) && appointment.AppointmentDate.ToString() == s)
                {
                    timeSlots.Remove(appointmentSlot);
                }
            }

            return Ok(timeSlots);
        }

        private void GenerateTimeSlotsForRange(TimeSpan startTime, TimeSpan endTime, ref List<string> timeSlots)
        {
            DateTime currentTime = DateTime.Today.Add(startTime);

            while (currentTime.TimeOfDay < endTime)
            {
                DateTime nextTime = currentTime.AddMinutes(30);
                var appTime = $"{currentTime.ToString("HH:mm")} - {nextTime.ToString("HH:mm")}";
                timeSlots.Add(appTime);

                currentTime = nextTime;
            }
        }


        //[HttpGet]
        //public async Task<IActionResult> GetAvailableTimes(string doctorId, string appointmentDate)
        //{
        //    var appointments = await _context.Appointments
        //        .Where(a => a.DoctorId == doctorId && a.AppointmentDate.Date == DateTime.Parse(appointmentDate).Date)
        //        .ToListAsync();

        //    var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == doctorId);
        //    if (doctor == null)
        //    {
        //        return NotFound("Doctor not found");
        //    }

        //    var timeAround = await _context.Schedules.FirstOrDefaultAsync(t => t.Id == doctor.ScheduleId);
        //    if (timeAround == null)
        //    {
        //        return NotFound("Schedule not found");
        //    }

        //    var timeSlots = GenerateTimeSlotsForRange(timeAround);

        //    // Retrieve all the dates for which there are appointments
        //    var appointmentDates = appointments.Select(a => a.AppointmentDate).Distinct();

        //    // Remove the entire date if all time slots are booked
        //    foreach (var appointmentDate in appointmentDates)
        //    {
        //        var bookedTimeSlots = appointments
        //            .Where(a => a.AppointmentDate.Date == appointmentDate)
        //            .Select(a => a.AppointmentTime)
        //            .ToList();

        //        if (bookedTimeSlots.Count == timeSlots.Count)
        //        {
        //            timeSlots.RemoveAll(slot => slot.Contains(appointmentDate.ToString("yyyy-MM-dd")));
        //        }
        //    }

        //    return Ok(timeSlots);
        //}

        //private List<string> GenerateTimeSlotsForRange(Schedule schedule)
        //{
        //    var timeSlots = new List<string>();
        //    TimeSpan startTime;
        //    TimeSpan endTime;

        //    if (schedule.WorkTime.Split('-')[0].Trim() == "09:00")
        //    {
        //        startTime = morningStart;
        //        endTime = morningEnd;
        //    }
        //    else
        //    {
        //        startTime = afternoonStart;
        //        endTime = afternoonEnd;
        //    }

        //    DateTime currentDate = DateTime.Today.Add(startTime);

        //    while (currentDate.TimeOfDay < endTime)
        //    {
        //        DateTime nextTime = currentDate.AddMinutes(30);
        //        var appTime = $"{currentDate.ToString("HH:mm")} - {nextTime.ToString("HH:mm")} {currentDate.Date:yyyy-MM-dd}";
        //        timeSlots.Add(appTime);
        //        currentDate = nextTime;
        //    }

        //    return timeSlots;
        //}



        //private void GenerateTimeSlotsForRange(TimeSpan startTime, TimeSpan endTime, ref List<string> timeSlots)
        //{
        //    DateTime currentTime = DateTime.Today.Add(startTime);

        //    while (currentTime.TimeOfDay < endTime)
        //    {
        //        DateTime nextTime = currentTime.AddMinutes(30);
        //        var appTime = $"{currentTime.ToString("HH:mm")} - {nextTime.ToString("HH:mm")}";
        //        timeSlots.Add(appTime);

        //        currentTime = nextTime;
        //    }

        //}

        //[HttpGet]
        //public async Task<IActionResult> GetAvailableTimes(string doctorId, string appointmentDate)
        //{
        //    var appointments = await _context.Appointments.ToListAsync();
        //    List<string> timeSlots = new List<string>();
        //    var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == doctorId);
        //    var timeAround = await _context.Schedules.FirstOrDefaultAsync(t => t.Id == doctor.ScheduleId);

        //    var morningSpan = new TimeSpan();

        //    var start = timeAround.WorkTime.Split('-')[0].Trim();
        //    var end = timeAround.WorkTime.Split('-')[1].Trim();
        //    if (start == "09:00")
        //    {
        //        GenerateTimeSlotsForRange(morningStart, morningEnd, ref timeSlots);
        //    }
        //    else
        //    {
        //        GenerateTimeSlotsForRange(afternoonStart, afternoonEnd, ref timeSlots);
        //    }

        //    for (int k = 0; k < appointments.Count(); k++)
        //    {
        //        if (appointments[k].AppointmentTime.Split('-')[0].Trim() == 
        //            && appointments[k].DoctorId == doctorId
        //            && appointments[k].AppointmentDate.ToString().Split(" ")[0] == appointmentDate)
        //        {
        //            timeSlots.Remove(appTime);
        //        }
        //    }
        //    return Ok(timeSlots);
        //}

        public async Task<IActionResult> GetDoctors(int departmentId)
        {
            var doctors = await _dbContext.Doctors.Where(d => d.DepartmentId == departmentId.ToString()).ToListAsync();
            return Ok(doctors);
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            return View();
        }
        public IActionResult BlogSindebar()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> BlogSingle(PostsShowViewModel post)
        {
            var user = await CurrentUser();

            var postT = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == post.PostId);
            post.Admin = await _dbContext.Admins.FirstOrDefaultAsync(a => a.Id == post.AdminId);
            post.Department = await _dbContext.Departments.FirstOrDefaultAsync(p => p.Id == post.DepartmentId);

            var postView = await _dbContext.PostViews.FirstOrDefaultAsync(f => f.PatientId == user.Id && f.PostId == postT.Id);

            if (postView == null)
            {
                postView = new PostView
                {
                    Post = postT,
                    PostId = postT.Id,
                    Patient = user,
                    PatientId = user.Id
                };

                postT.ViewCount += 1;
                post.ViewCount += 1;

                await _dbContext.PostViews.AddAsync(postView);

                _dbContext.Update(postT);
                await _dbContext.SaveChangesAsync();
            }

            return View(post);
        }
        public IActionResult Service()
        {
            return View();
        }

        public IActionResult SuccessPay()
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

        public async Task<IActionResult> DepartmentSingle(DepartmentSingleViewModel viewModel)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == viewModel.DepartmentId);

            ViewBag.Department = department;

            return View();
        }

        public IActionResult Doctor()
        {
            return View();
        }

        public IActionResult Appointments()
        {
            return View();
        }

        public async Task<IActionResult> DoctorSingle(DoctorProfileViewModel viewModel)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == viewModel.DepartmentId);
            viewModel.Department = department;
            ViewBag.Doctor = viewModel;
            return View();
        }

       
    }
}