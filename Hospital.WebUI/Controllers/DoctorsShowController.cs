using Hospital.Entities.Data;
using Hospital.WebUI.Models;
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

            var viewModel=new DoctorViewModel { Doctors = doctors };

            return Ok(viewModel);
        }

        public async Task<IActionResult> DoctorInDoctorProfile(string doctor)
        {
            return RedirectToAction("Doctor", "DoctorProfile");
        }    
        
        public async Task<IActionResult> AdminInDoctorProfile(string doctor)
        {
            return RedirectToAction("Admin", "DoctorProfile");
        }
    }
}
