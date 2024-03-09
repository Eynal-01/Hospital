using Hospital.Entities.Data;
using Hospital.Entities.DbEntities;
using Hospital.WebUI.Models;
using HospitalProject.Entities.DbEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Hospital.WebUI.Controllers
{
    public class AboutController : Controller
    {
        private readonly CustomIdentityDbContext _context;

        private readonly UserManager<CustomIdentityUser> _userManager;

        public AboutController(CustomIdentityDbContext context, UserManager<CustomIdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<List<About>> GetAllAbouts()
        {
            var abouts = await _context.Abouts.ToListAsync();

            return abouts;
        }

        public async Task<AllAboutsViewModel> GetAllAboutsUsers()
        {
            var abouts = await GetAllAbouts();
            var doctors = await _context.Doctors.Include(nameof(Doctor.Department)).ToListAsync();

            var viewModel = new AllAboutsViewModel
            {
                Doctors = doctors,
                Abouts = abouts
            };

            return viewModel;
        }

        //[HttpPost]
        //public async Task<IActionResult> AddAbouts(AddAboutViewModel viewModel)
        //{

        //}
    }
}
