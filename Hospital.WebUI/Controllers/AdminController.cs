using Hospital.Business.Abstract;
using HospitalProject.Entities.DbEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.WebUI.Controllers
{
    //[Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        public IDoctorService _doctorService { get; set; }
        public IPatientService _patientService { get; set; }
        public IAdminService _adminService { get; set; }

        public AdminController(
            IDoctorService doctorService,
            IPatientService patientService, 
            IAdminService adminService)
        {
            _doctorService = doctorService;
            _patientService = patientService;
            _adminService = adminService;
        }

        public IActionResult Activities()
        {
            return View();
        }

        public IActionResult AddApointment()
        {
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

        public IActionResult AddDoctor()
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

        public async Task<IEnumerable<Doctor>> GetAllDoctors()
        {
            var doctors = await _doctorService.GetAllDoctors();       
            return doctors;
        }

    }
}
