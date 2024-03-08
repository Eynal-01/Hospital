using Hospital.Entities.Data;
using Hospital.WebUI.Models;
using HospitalProject.Entities.DbEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.WebUI.Controllers
{
    public class DoctorsShowController : Controller
    {
        private readonly CustomIdentityDbContext _context;
        private readonly UserManager<CustomIdentityUser> _userManager;

        public DoctorsShowController(CustomIdentityDbContext context, UserManager<CustomIdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllDoctors()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var current = await _context.Doctors.FirstOrDefaultAsync(d => d.Email == currentUser.Email && d.UserName == currentUser.UserName);
            var doctors = new List<Doctor>();
            var departments = new List<Department>();
            var departmentsNew = new List<Department>();
            if (current != null)
            {
                doctors = await _context.Doctors.Where(d => d.Id != current.Id).Include(nameof(Doctor.Department)).ToListAsync();
            }
            else
            {
                doctors = await _context.Doctors.Include(nameof(Doctor.Department)).ToListAsync();
            }

            for (int i = 0; i < doctors.Count(); i++)
            {
                var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == doctors[i].DepartmentId.ToString());
                departments.Add(department);
            }

            //for (int i = 0; i < departments.Count(); i++)
            //{
            //    for (int k = 0; k < departments.Count(); k++)
            //    {
            //        if (departments[i].DepartmentName != departments[k].DepartmentName)
            //        {
            //            departmentsNew.Add(departments[i]);
            //        }
            //    }
            //}
            var viewModel = new DoctorViewModel { Doctors = doctors, Departments = departments };

            return Ok(viewModel);
        }

        public async Task<IActionResult> PatientDoctorFilterPatient(int departmentId)
        {
            var doctors = new List<Doctor>();
            if (departmentId == 1)
            {
                doctors = await _context.Doctors.Include(nameof(Doctor.Department)).ToListAsync();
            }
            else
            {
                doctors = await _context.Doctors.Where(d => d.DepartmentId == departmentId.ToString()).Include(nameof(Doctor.Department)).ToListAsync();
            }
            return Ok(doctors);
        }

        public async Task<IActionResult> DoctorInDoctorProfile(string doctorId)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == doctorId);

            var viewModel = new DoctorProfileViewModel
            {
                DoctorId = doctor.Id,
                Address = doctor.Address,
                City = doctor.City,
                Country = doctor.Country,
                DepartmentId = doctor.DepartmentId.ToString(),
                Education = doctor.Education,
                Gender = doctor.Gender,
                ImageUrl = doctor.Avatar,
                Info = doctor.Bio,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                UserName = doctor.UserName,
            };

            return RedirectToAction("Profile", "Doctor", viewModel);
        }

        public async Task<IActionResult> AdminInDoctorProfile(string doctorId)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == doctorId);

            var viewModel = new DoctorProfileViewModel
            {
                DoctorId = doctor.Id,
                Address = doctor.Address,
                City = doctor.City,
                Country = doctor.Country,
                DepartmentId = doctor.DepartmentId.ToString(),
                Education = doctor.Education,
                Gender = doctor.Gender,
                ImageUrl = doctor.Avatar,
                Info = doctor.Bio,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                UserName = doctor.UserName,
            };

            return RedirectToAction("DoctorProfile", "Admin", viewModel);
        }

        public async Task<IActionResult> PatientInDoctorProfile(string doctorId)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == doctorId);

            var viewModel = new DoctorProfileViewModel
            {
                DoctorId = doctor.Id,
                Address = doctor.Address,
                City = doctor.City,
                Country = doctor.Country,
                DepartmentId = doctor.DepartmentId.ToString(),
                Education = doctor.Education,
                Gender = doctor.Gender,
                ImageUrl = doctor.Avatar,
                Info = doctor.Bio,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                UserName = doctor.UserName,
            };

            return RedirectToAction("DoctorSingle", "Home", viewModel);
        }
    }
}
