using Hospital.Business.Abstract;
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
using Org.BouncyCastle.Asn1.Mozilla;
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
        private readonly IDataService _dataService;
        private readonly IMediaService _mediaService;

        public AdminController(UserManager<CustomIdentityUser> userManager, RoleManager<CustomIdentityRole> roleManager, IWebHostEnvironment webHost, CustomIdentityDbContext context, IPatientService patientService, IDataService dataService, IMediaService mediaService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _webHost = webHost;
            _context = context;
            _patientService = patientService;
            _dataService = dataService;
            _mediaService = mediaService;
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
            var schedules = await _context.Schedules.ToListAsync();
            var doctors = await _context.Doctors.Include(d => d.Schedule).ToListAsync();
            var rooms = await _context.Rooms.ToListAsync();

            var viewModel = new AddDoctorViewModel
            {
                ImageUrl = user.Avatar,
                Departments = departments,
                Schedules = schedules,
                Rooms = rooms
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> NewPost()
        {
            var departments = await _context.Departments.ToListAsync();
            var viewModel = new NewPostViewModel
            {
                Departments = departments,
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Abouts()
        {
            var abouts = await _context.Abouts.ToListAsync();
            var doctors = await _context.Doctors.ToListAsync();
            var viewModel = new AllAboutsViewModel
            {
                Abouts = abouts,
                Doctors = doctors
            };
            ViewBag.ViewModel = viewModel;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddAbout()
        {
            var abouts = await _context.Abouts.ToListAsync();
            var viewModel = new AddAboutViewModel();
            if (abouts.Count() == 0)
            {
                viewModel = new AddAboutViewModel
                {
                    AboutsCount = abouts.Count(),
                };
            }
            else
            {
                viewModel = new AddAboutViewModel
                {
                    AboutsCount = abouts.Count(),
                    BigTitle = abouts[0].BigTitle,
                    //Content = abouts[0].Content,
                    FirstContent = abouts[0].FirstContent,
                    //Title = abouts[0].Title,
                    //ImageUrl = abouts[0].ImageUrl,
                    Id = abouts[0].Id
                };
            }
            var sibgleAbout = new AddAboutSingleViewModel
            {
                Content = "",
                Title = "",
                ImageUrl = ""
            };

            var allViewModel = new AllAboutViewModel
            {
                addAboutViewModel = viewModel,
                addSingleAboutViewModel = sibgleAbout
            };
            return View(allViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> AddAbout(AllAboutViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var abouts = await _context.Abouts.ToListAsync();
                var about = new About
                {
                    BigTitle = viewModel.addAboutViewModel.BigTitle,
                    //Content = viewModel.Content,
                    FirstContent = viewModel.addAboutViewModel.FirstContent,
                    //Title = viewModel.Title,
                };
                //if (viewModel.File != null)
                //{
                //    var helper = new ImageHelper(_webHost);

                //    var mediaUrl = await _mediaService.UploadMediaAsync(viewModel.File);

                //    if (mediaUrl != string.Empty)
                //    {
                //        var isVideoFile = _mediaService.IsVideoFile(viewModel.File);
                //        about.ImageUrl = mediaUrl;
                //    }
                //    else
                //    {
                //        return BadRequest("error");
                //    }
                //}
                if (abouts.Count() == 0)
                {
                    await _context.Abouts.AddAsync(about);
                }
                else
                {
                    var aboutOld = await _context.Abouts.FirstOrDefaultAsync(d => d.Id == viewModel.addAboutViewModel.Id);
                    aboutOld.FirstContent = viewModel.addAboutViewModel.FirstContent;
                    //aboutOld.Title = viewModel.Title;
                    //aboutOld.Content = viewModel.Content;
                    aboutOld.BigTitle = viewModel.addAboutViewModel.BigTitle;
                    _context.Abouts.Update(aboutOld);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("AddAbout", "Admin");
            }
            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> AddSingleAbout(AllAboutViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var abouts = await _context.Abouts.ToListAsync();
                var about = new About
                {
                    BigTitle = abouts[0].BigTitle,
                    FirstContent = abouts[0].FirstContent,
                    Content = viewModel.addSingleAboutViewModel.Content,
                    Title = viewModel.addSingleAboutViewModel.Title,
                };
                if (viewModel.addSingleAboutViewModel.File != null)
                {

                    var mediaUrl = await _mediaService.UploadMediaAsync(viewModel.addSingleAboutViewModel.File);

                    if (mediaUrl != string.Empty)
                    {
                        var isVideoFile = _mediaService.IsVideoFile(viewModel.addSingleAboutViewModel.File);
                        about.ImageUrl = mediaUrl;
                    }
                    else
                    {
                        return BadRequest("error");
                    }
                }

                await _context.Abouts.AddAsync(about);
                await _context.SaveChangesAsync();
                return RedirectToAction("AddAbout", "Admin");
            }
            return RedirectToAction("AddAbout", viewModel);
        }

        public async Task<IActionResult> GetAvailableDays(int availableCount)
        {
            var admins = await _context.Admins.ToListAsync();
            for (int i = 0; i < admins.Count(); i++)
            {
                admins[i].WorkDaysCount = availableCount;
                await _context.SaveChangesAsync();
            }
            return Ok(admins);
        }

        public async Task<IActionResult> DoctorShowPost()
        {
            var doctors = await _context.Doctors.ToListAsync();
            return Ok(doctors);
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

        [HttpPost]
        public async Task<IActionResult> AddDepartment(AddDepartmentViewModel viewModel)
        {
            if (ModelState.IsValid)
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
                        DepartmentName = viewModel.Name,
                        Content = viewModel.Content,
                    };
                }
                else
                {
                    department.Id = newDeparetmentId;
                    department.DepartmentName = viewModel.Name;
                    department.Content = viewModel.Content;
                }

                if (viewModel.File != null)
                {
                    var helper = new ImageHelper(_webHost);

                    var mediaUrl = await _mediaService.UploadMediaAsync(viewModel.File);

                    if (mediaUrl != string.Empty)
                    {
                        var isVideoFile = _mediaService.IsVideoFile(viewModel.File);
                        department.ImageUrl = mediaUrl;
                    }
                    else
                    {
                        return BadRequest("error");
                    }
                }

                await _context.Departments.AddAsync(department);
                await _context.SaveChangesAsync();
                return View();
            }
            return View(viewModel);
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

        public async Task<IActionResult> DoctorProfile(DoctorProfileViewModel doctor)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == doctor.DepartmentId.ToString());
            doctor.Department = department;
            ViewBag.Doctor = doctor;
            return View();
        }

        public async Task<IActionResult> PatientProfile(string id)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);
            var viewModel = new PatientProfileViewModel
            {
                //Address = patient.Address,
                Email = patient.Email,
                //Fullname = patient.FullName,
                Username = patient.UserName,
                PhoneNumber = patient.PhoneNumber,
                ImageUrl = patient.Avatar,
            };
            return RedirectToAction("PatientProfile1", "Admin", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddDoctor(AddDoctorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.ScheduleId != "")
                {
                    var con = viewModel.ScheduleId.Split("--");
                    var schedule = await _context.Schedules.FirstOrDefaultAsync(d => d.WorkTime == con[1].Trim());
                    var room = await _context.Rooms.FirstOrDefaultAsync(d => d.RoomNo == con[0].Trim());



                    if (viewModel.Password == viewModel.ConfirmPassword)
                    {
                        var newPassword = HashPassword(viewModel.Password);

                        if (viewModel.File != null)
                        {
                            var helper = new ImageHelper(_webHost);

                            var mediaUrl = await _mediaService.UploadMediaAsync(viewModel.File);

                            if (mediaUrl != string.Empty)
                            {
                                var isVideoFile = _mediaService.IsVideoFile(viewModel.File);
                                viewModel.ImageUrl = mediaUrl;
                            }
                            else
                            {
                                return BadRequest("error");
                            }
                        }

                        var doctor = new Doctor
                        {
                            Address = viewModel.Address,
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
                            Bio = viewModel.ShortBiography,
                            DepartmentId = viewModel.DepartmentId.ToString(),
                            Education = viewModel.Education,
                            ScheduleId = schedule.Id,
                            RoomId = room.Id,
                        };

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
                            }
                        }
                        await _userManager.AddToRoleAsync(customUser, "doctor");
                        await _context.Doctors.AddAsync(doctor);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            var departments = await _context.Departments.ToListAsync();
            var schedules = await _context.Schedules.ToListAsync();
            var rooms = await _context.Rooms.ToListAsync();
            viewModel.Departments = departments;
            viewModel.Schedules = schedules;
            viewModel.Rooms = rooms;
            return View(viewModel);
        }

        public async Task<IActionResult> Chat()
        {
            return View();
        }

        public async Task<IActionResult> GetAllDepartment()
        {
            var departments = await _context.Departments.ToListAsync();
            return Ok(departments);
        }

        public IActionResult AddBlog()
        {
            return View();
        }

        public IActionResult AddDepartment()
        {
            return View();
        }

        public IActionResult AllAppointments()
        {
            return View();
        }

        public IActionResult AddPatient()
        {
            return View();
        }

        public IActionResult Activities()
        {
            return View();
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


        public IActionResult PageOffline()
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

        public IActionResult Appointments()
        {
            return View();
        }

        public IActionResult PatientProfile1(PatientProfileViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            return View();
        }
        public async Task<IActionResult> BlogSingle(PostsShowViewModel post)
        {
            var user = await CurrentUser();

            var postT = await _context.Posts.FirstOrDefaultAsync(p => p.Id == post.PostId);
            post.Admin = await _context.Admins.FirstOrDefaultAsync(a => a.Id == post.AdminId);
            post.Department = await _context.Departments.FirstOrDefaultAsync(p => p.Id == post.DepartmentId);

            var postView = await _context.PostViews.FirstOrDefaultAsync(f => f.AdminId == user.Id && f.PostId == postT.Id);

            if (postView == null)
            {
                postView = new PostView
                {
                    Post = postT,
                    PostId = postT.Id,
                    Admin = user,
                    AdminId = user.Id
                };

                postT.ViewCount += 1;
                post.ViewCount += 1;

                await _context.PostViews.AddAsync(postView);

                _context.Update(postT);
                await _context.SaveChangesAsync();
            }

            ViewBag.Post = post;

            return View();
        }

        public async Task<IActionResult> FilterRooms(int time)
        {
            var timee = await _context.Schedules.FirstOrDefaultAsync(t => t.Id == time);
            var doctors = await _context.Doctors.Where(d => d.ScheduleId == timee.Id).ToListAsync();
            var rooms = new List<Room>();

            if (doctors.Count() > 0)
            {
                for (var i = 0; i < doctors.Count(); i++)
                {
                    rooms = await _context.Rooms.Where(r => r.Id != doctors[i].RoomId).ToListAsync();
                }
            }
            else
            {
                rooms = await _context.Rooms.ToListAsync();
            }
            return Ok(rooms);
        }
    }
}