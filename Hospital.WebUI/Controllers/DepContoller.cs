using Hospital.Business.Abstract;
using Hospital.Entities.Data;
using HospitalProject.Entities.DbEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.WebUI.Controllers
{
	public class DepContoller : Controller
	{
		private UserManager<CustomIdentityUser> _userManager;
		private RoleManager<CustomIdentityRole> _roleManager;
		private IWebHostEnvironment _webHost;
		private readonly CustomIdentityDbContext _context;
		private readonly IPatientService _patientService;
		private readonly IDataService _dataService;

		public DepContoller(UserManager<CustomIdentityUser> userManager, RoleManager<CustomIdentityRole> roleManager, IWebHostEnvironment webHost, CustomIdentityDbContext context, IPatientService patientService, IDataService dataService)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_webHost = webHost;
			_context = context;
			_patientService = patientService;
			_dataService = dataService;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> GetAllDepartment()
		{
			var departments = await _context.Departments.ToListAsync();
			return Ok(departments);
		}
	}
}
