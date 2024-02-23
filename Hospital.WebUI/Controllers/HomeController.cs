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
            var department = await _dbContext.Departments.FirstOrDefaultAsync(d => d.Id == viewModel.DepartmentId);
            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(d => d.Id == viewModel.DoctorId);

            var appoinment = new Appointment
            {
                Age = patient.Age,
                DoctorId = doctor.Id,
                DepartmentId = department.Id,
                PatientId = patient.Id.ToString(),
                Message = viewModel.Message,
            };
            var doctor1 = _dbContext.Doctors.FirstOrDefault(d => d.Id == appoinment.DoctorId);
            await _dbContext.Appointments.AddAsync(appoinment);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Appoinment", "Home");
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
