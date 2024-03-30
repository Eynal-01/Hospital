using Hospital.Business.Abstract;
using Hospital.Entities.Data;
using HospitalProject.Entities.DbEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Twilio.AspNet.Core;

namespace Hospital.WebUI.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly IDataService _dataService;
        private readonly CustomIdentityDbContext _context;

        public AppointmentController(UserManager<CustomIdentityUser> userManager, IDataService dataService, CustomIdentityDbContext context)
        {
            _userManager = userManager;
            _dataService = dataService;
            _context = context;
        }

        public async Task<Doctor> CurrentUser()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var admin = await _context.Doctors.FirstOrDefaultAsync(a => a.UserName == user.UserName && a.Email == user.Email);
            return admin;
        }

        public async Task<IActionResult> ShowAllAppointmentsForDoctor()
        {
            var doctor = await CurrentUser();
            List<Appointment> appointments = new List<Appointment>();
            var allAppointments = await _context.Appointments.ToListAsync();
            appointments = await _context.Appointments.Where(a => a.DoctorId == doctor.Id)
                .Include(nameof(Appointment.Patient))
                .ToListAsync();
            return Ok(appointments);
        }
        public async Task<IActionResult> SearchDoctors(string word)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var current = await _context.Doctors.FirstOrDefaultAsync(d => d.UserName == user.UserName && d.Email == user.Email);
            if (word != null && word.Trim() != "")
            {
                List<Doctor> doctors = null;
                if (current != null)
                {
                    doctors = await _context.Doctors.Where(d => d.Id != current.Id && d.FirstName.Contains(word.Trim()) || d.LastName.Contains(word.Trim())).Include(nameof(Doctor.Department)).ToListAsync();
                }
                else
                {
                    doctors = await _context.Doctors.Where(d => d.FirstName.Contains(word.Trim()) || d.LastName.Contains(word.Trim())).Include(nameof(Doctor.Department)).ToListAsync();
                }
                return Ok(doctors);
            }

            List<Doctor> docss = null;
            if (current != null)
            {
                docss = await _context.Doctors.Include(nameof(Doctor.Department)).Where(d => d.Id != current.Id).ToListAsync();
            }
            else
            {
                docss = await _context.Doctors.Include(nameof(Doctor.Department)).ToListAsync();
            }

            return Ok(docss);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAppointmentsDescending()
        {
            var appos = _context.Appointments
                    .OrderByDescending(a => a.AppointmentDate)
                    .Include(a => a.Patient)
                    .Take(4)
                    .ToList();
            return Ok(appos);
        }

        public async Task<IActionResult> GetAllAppointmentsOfPatient()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var pa = await _context.Patients.FirstOrDefaultAsync(f => f.Email == user.Email && f.UserName == user.UserName);
            var appointments = await _context.Appointments.Where(a => a.PatientId == pa.Id).ToListAsync();

            for (int i = 0; i < appointments.Count(); i++)
            {
                var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == appointments[i].DoctorId);
                appointments[i].Doctor = doctor;

                var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == appointments[i].DepartmentId);
                appointments[i].Department = department;
            }
            return Ok(appointments);
        }

        public async Task<IActionResult> GetAllRecipesOfCurrent(string id)
        {
            var pa = await _context.Patients.FirstOrDefaultAsync(f => f.Id == id);
            var recipes = _context.Recipes.Where(r => r.PatientId == pa.Id).ToList();
            return Ok(recipes);
        }

        public async Task<IActionResult> GetByIdRecipe(string id)
        {
            var receip = await _context.Recipes.FirstOrDefaultAsync(d => d.Id.ToString() == id);

            return Ok(receip);
        }
    }
}