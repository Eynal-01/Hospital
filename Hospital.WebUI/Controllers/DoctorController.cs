using Hospital.Entities.Data;
using Hospital.Entities.DbEntities;
using Hospital.WebUI.Abstract;
using Hospital.WebUI.Helpers;
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
        private IWebHostEnvironment _webHost;
        private readonly IMediaService _mediaService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="dbContext"></param>
        /// <param name="webHost"></param>
        /// <param name="mediaService"></param>

        public DoctorController(UserManager<CustomIdentityUser> userManager, CustomIdentityDbContext dbContext, IWebHostEnvironment webHost, IMediaService mediaService)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _webHost = webHost;
            _mediaService = mediaService;
        }

        [HttpGet]
        public async Task<IActionResult> ShowAllAppointments()
        {
            var doctor = await CurrentUser();
            List<Appointment> appointments = new List<Appointment>();
            var allAppointments = await _dbContext.Appointments.ToListAsync();
            for (int i = 0; i < allAppointments.Count(); i++)
            {
                if (allAppointments[i].DoctorId == doctor.Id.ToString())
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

        public async Task<IActionResult> Abouts()
        {

            var abouts = await _dbContext.Abouts.ToListAsync();
            var doctors = await _dbContext.Doctors.ToListAsync();
            var viewModel = new AllAboutsViewModel
            {
                Abouts = abouts,
                Doctors = doctors
            };
            ViewBag.ViewModel = viewModel;
            return View();
        }

        public async Task<IActionResult> BlogSingle(PostsShowViewModel post)
        {
            var user = await CurrentUser();

            var postT = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == post.PostId);
            post.Admin = await _dbContext.Admins.FirstOrDefaultAsync(a => a.Id == post.AdminId);
            post.Department = await _dbContext.Departments.FirstOrDefaultAsync(p => p.Id == post.DepartmentId);

            var postView = await _dbContext.PostViews.FirstOrDefaultAsync(f => f.DoctorId == user.Id && f.PostId == postT.Id);

            if (postView == null)
            {
                postView = new PostView
                {
                    Post = postT,
                    PostId = postT.Id,
                    Doctor = user,
                    DoctorId = user.Id
                };

                postT.ViewCount += 1;
                post.ViewCount += 1;

                await _dbContext.PostViews.AddAsync(postView);

                _dbContext.Update(postT);
                await _dbContext.SaveChangesAsync();
            }

            ViewBag.Post = post;

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

        public IActionResult AllDepartments(DepartmentsViewModel departments)
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

        public async Task<IActionResult> Profile(DoctorProfileViewModel doctor)
        {
            var department = await _dbContext.Departments.FirstOrDefaultAsync(d => d.Id == doctor.DepartmentId.ToString());
            doctor.Department = department;
            ViewBag.Doctor = doctor;
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var roles = await _userManager.GetRolesAsync(user);
            var selected = roles.FirstOrDefault();
            return RedirectToAction("Login", "Authentication", new { selected });
        }

        [HttpGet]
        public async Task<IActionResult> GetPatientsOfDoctor()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var appointments = _dbContext.Appointments.Where(a => a.DoctorId == user.Id.ToString()).ToList();

            IQueryable<Patient> patients = Enumerable.Empty<Patient>().AsQueryable(); 

            for (int i = 0; i < appointments.Count(); i++)
            {
                var appointment = appointments[i];
                patients = patients.Concat(_dbContext.Patients.Where(p => p.Id == appointment.PatientId));
            }

            return Ok(patients.ToList());
        }

    }
}
