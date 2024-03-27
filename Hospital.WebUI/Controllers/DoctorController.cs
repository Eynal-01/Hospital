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
using System.Runtime.InteropServices;
using Twilio.AspNet.Core;

namespace Hospital.WebUI.Controllers
{
    [Authorize(Roles = "doctor")]
    public class DoctorController : Controller
    {
        private readonly UserManager<CustomIdentityUser> _userManager;
        public CustomIdentityDbContext _dbContext { get; set; }
        private IWebHostEnvironment _webHost;
        private readonly IMediaService _mediaService;

        public DoctorController(UserManager<CustomIdentityUser> userManager, CustomIdentityDbContext dbContext, IWebHostEnvironment webHost, IMediaService mediaService)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _webHost = webHost;
            _mediaService = mediaService;
        }

        public async Task<IActionResult> PatientProfile(string id)
        {
            var patient = await _dbContext.Patients.FirstOrDefaultAsync(p => p.Id == id);
            var viewModel = new PatientProfileViewModel
            {
                //Address = patient.Address,
                Email = patient.Email,
                //Fullname = patient.FullName,
                Username = patient.UserName,
                PhoneNumber = patient.PhoneNumber,
                ImageUrl = patient.Avatar,
                Recipes = patient.Recipes,
                PatientId = id
            };
            return RedirectToAction("PatientProfile11", "Doctor", viewModel);
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

        public async Task<IActionResult> ShowAllDoctorPatient()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(d => d.UserName == user.UserName && d.Email == user.Email);
            var doctorAppointments = _dbContext.Appointments.Where(a => a.DoctorId == doctor.Id).ToList();
            var commonPats = _dbContext.Patients.ToList();
            var doctorPats = new List<Patient>();
            for (int i = 0; i < commonPats.Count(); i++)
            {
                for (int k = 0; k < doctorAppointments.Count(); k++)
                {
                    if (commonPats[i].Id == doctorAppointments[k].PatientId)
                    {
                        doctorPats.Add(commonPats[i]);
                    }
                }
            }
            return Ok(doctorPats);
        }

        public async Task<IActionResult> AddRecipeToPatient(string id, string content, string header)
        {
            var patient = await _dbContext.Patients.FirstOrDefaultAsync(p => p.Id == id);
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(d => d.Email == user.Email && d.UserName == user.UserName);
            var allRecipesCount = _dbContext.Recipes.Count();
            var departmentOfDoctor = await _dbContext.Departments.FirstOrDefaultAsync(d => d.Id == doctor.DepartmentId);
            Recipe newRecipe = new Recipe()
            {
                Content = content,
                RecipeHeader = header,
                PatientId = patient.Id,
                DoctorId = doctor.Id,
                WriteTime = DateTime.Now.ToShortDateString(),
                DoctorName = $"{doctor.FirstName} {doctor.LastName}",
                DepartmentName = departmentOfDoctor.DepartmentName
            };
            await _dbContext.Recipes.AddAsync(newRecipe);
            await _dbContext.SaveChangesAsync();

            //var recipesOfPatient = await _dbContext.Recipes.Where(r=>r.PatientId==patient.Id).ToListAsync();
            var pat = await _dbContext.Patients.FirstOrDefaultAsync(d => d.Id == id);
            PatientProfileViewModel viewModel = new PatientProfileViewModel
            {
                Email = pat.Email,
                ImageUrl = pat.Avatar,
                PatientId = pat.Id,
                PhoneNumber = pat.PhoneNumber,
                Username = user.UserName,
            };

            return RedirectToAction("PatientProfile11", viewModel);
        }

        public async Task<IActionResult> PatientProfile11(PatientProfileViewModel viewModel)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var cu = await _dbContext.Doctors.FirstOrDefaultAsync(x => x.UserName == user.UserName && x.Email == user.Email);
            var receips = await _dbContext.Recipes.Where(d => d.PatientId == viewModel.PatientId && d.DoctorId == cu.Id).ToListAsync();
            viewModel.Recipes = receips;
            ViewBag.ViewModel = viewModel;
            return View();
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

        public IActionResult Patients()
        {
            return View();
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
    }
}