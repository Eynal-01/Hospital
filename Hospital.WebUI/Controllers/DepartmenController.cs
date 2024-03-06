using Hospital.Business.Abstract;
using Hospital.Entities.Data;
using Hospital.WebUI.Abstract;
using Hospital.WebUI.Models;
using HospitalProject.Entities.DbEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.WebUI.Controllers
{
    public class DepartmenController : Controller
    {
        private UserManager<CustomIdentityUser> _userManager;
        private RoleManager<CustomIdentityRole> _roleManager;
        private IWebHostEnvironment _webHost;
        private readonly CustomIdentityDbContext _context;


        private readonly IMediaService _mediaService;


        private readonly IPostService _postService;


        private readonly IAdminService _userService;

        public DepartmenController(UserManager<CustomIdentityUser> userManager, RoleManager<CustomIdentityRole> roleManager, IWebHostEnvironment webHost, CustomIdentityDbContext context, IMediaService mediaService, IPostService postService, IAdminService userService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _webHost = webHost;
            _context = context;
            _mediaService = mediaService;
            _postService = postService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<DepartmentsViewModel> GetAllDepartment()
        {
            var departments = await _context.Departments.ToListAsync();
            var departmentViewModel = new DepartmentsViewModel
            {
                Departments = departments
            };
            return departmentViewModel;
        }

        public async Task<IActionResult> DepartmentSinglePatient(string departmentId)
        {
            var viewModel = new DepartmentSingleViewModel
            {
                DepartmentId = departmentId,
            };

            return RedirectToAction("DepartmentSingle", "Home", viewModel);
        }

    }
}
