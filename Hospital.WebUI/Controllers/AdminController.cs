﻿using Hospital.Business.Abstract;
using Hospital.Entities.Data;
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
        private readonly IDataService _dataService;

        public AdminController(UserManager<CustomIdentityUser> userManager, CustomIdentityDbContext context, IWebHostEnvironment webHost, IPatientService patientService, RoleManager<CustomIdentityRole> roleManager, IDataService dataService)
        {
            _userManager = userManager;
            _context = context;
            _webHost = webHost;
            _patientService = patientService;
            _roleManager = roleManager;
            _dataService = dataService;
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

        public async Task<IActionResult> GetAvailableDays(int availableCount)
        {
            var dataToSend = availableCount;
            _dataService.SaveData(dataToSend);
            return Ok(dataToSend);
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

            var departmen = await _context.Departments.FirstOrDefaultAsync(d => d.Id == doctor.DepartmentId);

            var department = departmen.DepartmentName;
            return Ok(department);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAppointmentDoctor(string id)
        //{
        //    var doctor = await _context.Doctors.FirstOrDefaultAsync(i => i.Id == id);
        //    return Ok(doctor);
        //}

        public async Task<IActionResult> DoctorProfile(string doctorId)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == doctorId);
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == doctor.DepartmentId);
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

        public IActionResult AddBlog()
        {
            return View();
        }

        public IActionResult AddDepartment()
        {
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

        public IActionResult NewPost()
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

        public IActionResult PatientProfile()
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

        public IActionResult BlogSingle()
        {
            return View();
        }
    }
}