using Microsoft.AspNetCore.Mvc;

namespace Hospital.WebUI.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Start()
        {
            return View();
        }

        public IActionResult Selected(string selected)
        {
            selected = selected.Trim();
            selected = selected.ToLower();

            return RedirectToAction("Login", "Authentication", new { selected });
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

        public IActionResult AddPayment()
        {
            return View();
        }

        public IActionResult AllDepartments()
        {
            return View();
        }

        public IActionResult Appointment()
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

        public IActionResult DoctorProfile()
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

        public IActionResult AddProfile()
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
