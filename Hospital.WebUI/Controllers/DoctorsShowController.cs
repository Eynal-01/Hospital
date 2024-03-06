using Hospital.Entities.Data;
using Hospital.WebUI.Models;
using HospitalProject.Entities.DbEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.WebUI.Controllers
{
    public class DoctorsShowController : Controller
    {
        private readonly CustomIdentityDbContext _context;

        public DoctorsShowController(CustomIdentityDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _context.Doctors.ToListAsync();

            var viewModel = new DoctorViewModel { Doctors = doctors };

            return Ok(viewModel);
        }

        public async Task<IActionResult> DoctorInDoctorProfile(string doctor)
        {
            var d = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == doctor);

            return RedirectToAction("Doctor", "DoctorProfile", d);
        }

        public async Task<IActionResult> AdminInDoctorProfile(string doctorId)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == doctorId);

            var viewModel = new DoctorProfileViewModel
            {
                Address = doctor.Address,
                City = doctor.City,
                Country = doctor.Country,
                DepartmentId = doctor.DepartmentId.ToString(),
                Education = doctor.Education,
                Gender = doctor.Gender/.
                ImageUrl = doctor.Avatar,
                Info = doctor.Bio,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                UserName = doctor.UserName,
            };

            return RedirectToAction("DoctorProfile", "Admin", viewModel);
        }

        public async Task<IActionResult> PatientInDoctorProfile(string doctor)
        {
            return RedirectToAction("Patient", "DoctorProfile");
        }
    }
}
