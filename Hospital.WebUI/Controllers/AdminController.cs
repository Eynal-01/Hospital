﻿using Hospital.Entities.Data;
using Hospital.WebUI.Helpers;
using Hospital.WebUI.Models;
using HospitalProject.Entities.DbEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private UserManager<CustomIdentityUser> _userManager;
        private IWebHostEnvironment _webHost;
        private readonly CustomIdentityDbContext _context;

        public AdminController(UserManager<CustomIdentityUser> userManager, CustomIdentityDbContext context, IWebHostEnvironment webHost)
        {
            _userManager = userManager;
            _context = context;
            _webHost = webHost;
        }

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

            var viewModel = new AddDoctorViewModel
            {
                ImageUrl = user.Avatar
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddDoctor(AddDoctorViewModel viewModel)
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

            var doctor = new Doctor
            {
                Address = viewModel.Address,
                BirthDate = viewModel.DateOfBirth,
                City = viewModel.City,
                Country = viewModel.Country,
                Email = viewModel.Email,
                FirstName = viewModel.FirstName,
                Gender = viewModel.Gender,
                LastName = viewModel.LastName,
                UserName = viewModel.Username,
                PhoneNumber = viewModel.MobileNumber.ToString(),
                Avatar = viewModel.ImageUrl
            };

            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
            return View();
        }

        public IActionResult AddBlog()
        {
            return View();
        }

        public IActionResult AddDepartment()
        {
            return View();
        }

        public IActionResult AddApointment()
        {
            return View();
        }

        public IActionResult AddPatient()
        {
            return View();
        }

        public IActionResult AddSalary()
        {
            return View();
        }

        public IActionResult AddSchedule()
        {
            return View();
        }

        public IActionResult Appointments()
        {
            return View();
        }

        public IActionResult Attendance()
        {
            return View();
        }

        public IActionResult Calendar()
        {
            return View();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        public IActionResult Chat()
        {
            return View();
        }

        public IActionResult Departments()
        {
            return View();
        }

        public IActionResult Doctors()
        {
            return View();
        }

        public IActionResult EditDepartment()
        {
            return View();
        }

        public IActionResult EditDoctor()
        {
            return View();
        }

        public IActionResult EditPatient()
        {
            return View();
        }

        public IActionResult AddProfile()
        {
            return View();
        }

        public IActionResult EditSchedule()
        {
            return View();
        }

        public IActionResult Error404()
        {
            return View();
        }

        public IActionResult Error500()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
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

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Schedule()
        {
            return View();
        }

        public IActionResult Settings()
        {
            return View();
        }
    }
}
