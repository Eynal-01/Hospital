using Hospital.Business.Abstract;
using Hospital.Entities.Data;
using Hospital.Entities.DbEntities;
using Hospital.WebUI.Helpers;
using Hospital.WebUI.Models;
using HospitalProject.Entities.DbEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Hospital.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private UserManager<CustomIdentityUser> _userManager;
        private RoleManager<CustomIdentityRole> _roleManager;
        private IWebHostEnvironment _webHost;
        private readonly CustomIdentityDbContext _context;
        private readonly IPatientService _patientService;

        public AdminController(UserManager<CustomIdentityUser> userManager, CustomIdentityDbContext context, IWebHostEnvironment webHost, IPatientService patientService, RoleManager<CustomIdentityRole> roleManager)
        {
            _userManager = userManager;
            _context = context;
            _webHost = webHost;
            _patientService = patientService;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Activities function for show activities. 
        /// </summary>
        /// <returns></returns>
        public IActionResult Activities()
        {
            return View();
        }

        public async Task<Admin> CurrentUser()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.UserName == user.UserName && a.Email == user.Email);
            return admin;
        }

        [HttpGet]
        public async Task<IActionResult> AddDoctor()
        {
            var user = await CurrentUser();
            var departments = await _context.Departments.ToListAsync();
            var viewModel = new AddDoctorViewModel
            {
                ImageUrl = user.Avatar,
                Departments = departments,
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddDoctor(AddDoctorViewModel viewModel)
        {
            var user = await CurrentUser();
            if (viewModel.Password == viewModel.ConfirmPassword)
            {
                if (viewModel != null)
                {
                    var helper = new ImageHelper(_webHost);
                    if (viewModel.File != null)
                    {
                        viewModel.ImageUrl = await helper.SaveFile(viewModel.File);
                        user.Avatar = viewModel.ImageUrl;
                    }
                }
                var newPassword = HashPassword(viewModel.Password);
                var doctor = new Doctor
                {
                    Address = viewModel.Address,
                    BirthDate = viewModel.DateOfBirth,
                    City = viewModel.City,
                    Country = viewModel.Country,
                    Email = viewModel.Email,
                    NormalizedEmail = viewModel.Email.ToUpper(),
                    FirstName = viewModel.FirstName,
                    Gender = viewModel.Gender,
                    LastName = viewModel.LastName,
                    UserName = viewModel.Username,
                    NormalizedUserName = viewModel.Username.ToUpper(),
                    PhoneNumber = viewModel.MobileNumber.ToString(),
                    Avatar = viewModel.ImageUrl,
                    PasswordHash = newPassword,
                    WorkStartTime = viewModel.WorkStartTime,
                    WorkEndTime = viewModel.WorkEndTime,
                    Bio = viewModel.ShortBiography,
                    DepartmentId = viewModel.DepartmentId,
                    Education = viewModel.Education,
                };

                //var customUser = new CustomIdentityUser()
                //{
                //    //Address = viewModel.Address,
                //    //BirthDate = viewModel.DateOfBirth,
                //    //City = viewModel.City,
                //    //Country = viewModel.Country,
                //    Email = viewModel.Email,
                //    NormalizedEmail = viewModel.Email.ToUpper(),
                //    //FirstName = viewModel.FirstName,
                //    //Gender = viewModel.Gender,
                //    //LastName = viewModel.LastName,
                //    UserName = viewModel.Username,
                //    NormalizedUserName = viewModel.Username.ToUpper(),
                //    PhoneNumber = viewModel.MobileNumber.ToString(),
                //    PasswordHash = newPassword,
                //    //Avatar = viewModel.ImageUrl
                //};


                var customUser = new CustomIdentityUser
                {
                    Email = viewModel.Email,
                    UserName = viewModel.Username,
                    PhoneNumber = viewModel.MobileNumber.ToString(),
                };

                var result = await _userManager.CreateAsync(customUser, viewModel.Password);

                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync("doctor"))
                    {
                        var role = new CustomIdentityRole
                        {
                            Name = "doctor"
                        };
                        var resul = await _roleManager.CreateAsync(role);
                        //if (!resul.Succeeded)
                        //{
                        //    ModelState.AddModelError("", "Error");
                        //    return View(registerViewModel);
                        //}
                    }
                }

                await _userManager.AddToRoleAsync(customUser, "doctor");

                //await _context.Users.AddAsync(customUser);
                await _context.Doctors.AddAsync(doctor);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("AddDoctor", "Admin");
        }

        public async Task<IActionResult> GetAllPost()
        {
            var user = await CurrentUser();

            var data = await _context.Admins.ToListAsync();
            var posts = new List<Post>();
            foreach (var item in data)
            {
                var post = await _context.Posts.Where(p => p.AdminId == item.Id).ToListAsync();
                for (int i = 0; i < post.Count(); i++)
                {
                    if (post[i].ImageUrl != null)
                    {
                        post[i].IsImage = true;
                    }
                    else
                    {
                        post[i].IsImage = false;
                    }
                }
                posts.AddRange(post);
            }
            return Ok(new { posts = posts });
        }

        public static string HashPassword(string password)
        {
            int saltSize = 16;
            int bytesRequired = 32;
            byte[] array = new byte[1 + saltSize + bytesRequired];
            int iterations = 1000; // 1000, afaik, which is the min recommended for Rfc2898DeriveBytes
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltSize, iterations))
            {
                byte[] salt = pbkdf2.Salt;
                Buffer.BlockCopy(salt, 0, array, 1, saltSize);
                byte[] bytes = pbkdf2.GetBytes(bytesRequired);
                Buffer.BlockCopy(bytes, 0, array, saltSize + 1, bytesRequired);
            }
            return Convert.ToBase64String(array);
        }

        public IActionResult AddBlog()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddDepartment()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment(AddDepartmentViewModel viewModel)
        {
            var departments = await _context.Departments.ToListAsync();
            var department = new Department();
            var newDeparetmentId = "1";
            if (departments.Count() > 0)
            {
                newDeparetmentId = (int.Parse(departments[departments.Count - 1].Id) + 1).ToString();
                department = new Department
                {
                    Id = newDeparetmentId,
                    DepartmentName = viewModel.Name
                };
            }
            else
            {
                department.Id = newDeparetmentId;
                department.DepartmentName = viewModel.Name;
            }
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return View();
        }

        public IActionResult Appointments()
        {
            return View();
        }

        public IActionResult AddPatient()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AllPatients()
        {
            var patients = await _context.Patients.ToListAsync();
            var appoinments = await _context.Appointments.ToListAsync();
            var appointmentPatients = patients.Where(p => p.Appointments != null).ToList();
            return Ok(appointmentPatients);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _context.Doctors.ToListAsync();
            return Ok(doctors);
        }

        [HttpGet]
        public async Task<IActionResult> ShowAllAppointments()
        {
            var allAppointments = await _context.Appointments
                .Include(nameof(Appointment.Doctor))
                .Include(nameof(Appointment.Patient))
                .Include(nameof(Appointment.Department))
                .ToListAsync();
            return Ok(allAppointments);
        }


        public async Task<IActionResult> GetDoctorIdDepartment(string doctorId)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == doctorId);

            var departmen = await _context.Departments.FirstOrDefaultAsync(d => d.Id == doctor.DepartmentId.ToString());

            var department = departmen.DepartmentName;
            return Ok(department);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAppointmentDoctor(string id)
        //{
        //    var doctor = await _context.Doctors.FirstOrDefaultAsync(i => i.Id == id);
        //    return Ok(doctor);
        //}


        public IActionResult AddPayment()
        {
            return View();
        }

        public IActionResult AllDepartments()
        {
            return View();
        }

        public IActionResult BlogList()
        {
            return View();
        }

        public IActionResult BlogSingle()
        {
            return View();
        }

        public async Task<IActionResult> DoctorProfile(string doctorId)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == doctorId);
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == doctor.DepartmentId.ToString());
            var viewModel = new DoctorProfileViewModel
            {
                Address = doctor.Address,
                City = doctor.City,
                Country = doctor.Country,
                Department = department.DepartmentName,
                Education = doctor.Education,
                Gender = doctor.Gender,
                ImageUrl = doctor.Avatar,
                Info = doctor.Bio,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                UserName = doctor.UserName,
            };
            return View(viewModel);
        }

        public IActionResult Doctors()
        {
            return View();
        }

        public IActionResult Events()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> NewPost()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewPost(NewPostViewModel viewModel)
        {
            var user = await CurrentUser();
            if (viewModel != null)
            {
                var helper = new ImageHelper(_webHost);
                if (viewModel.File != null)
                {
                    viewModel.ImageUrl = await helper.SaveFile(viewModel.File);
                    user.Avatar = viewModel.ImageUrl;
                }
            }

            var post = new Post
            {
                AdminId = user.Id,
                ImageUrl = viewModel.ImageUrl,
                Title = viewModel.BlogTitle,
                Content = viewModel.Content,
                PublishTime = DateTime.Now.ToShortDateString(),
            };

            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();

            return View();
        }

        public IActionResult PageOffline()
        {
            return View();
        }

        public IActionResult PatientInvoice()
        {
            return View();
        }

        public async Task<IActionResult> PatientProfile(string id)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);
            var viewModel = new PatientProfileViewModel
            {
                Address = patient.Address,
                Email = patient.Email,
                Fullname = patient.FullName,
                Username = patient.UserName,
                PhoneNumber = patient.PhoneNumber,
                ImageUrl = patient.Avatar,
            };
            return View(viewModel);
        }

        public async Task<IActionResult> GetAllDepartment()
        {
            var departments = await _context.Departments.ToListAsync();
            return Ok(departments);
        }

        public IActionResult Patients()
        {
            return View();
        }

        public IActionResult Payments()
        {
            return View();
        }

        public IActionResult EditSchedule()
        {
            return View();
        }

        public IActionResult Error400()
        {
            return View();
        }

        public IActionResult Error500()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}