using Hospital.Entities.Data;
using Hospital.WebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;

namespace Hospital.WebUI.ViewComponents
{
    public class AdminInfoViewComponent : ViewComponent
    {
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly CustomIdentityDbContext _context;
        //private IUs

        public AdminInfoViewComponent(UserManager<CustomIdentityUser> userManager, CustomIdentityDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public ViewViewComponentResult Invoke()
        {
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            var role = _userManager.GetRolesAsync(user).Result;
            dynamic userr;
            if (role[0] == "admin")
            {
                userr = _context.Admins.FirstOrDefaultAsync(a => a.UserName == user.UserName && a.Email == user.Email).Result;
            }
            else 
            {
                userr = _context.Doctors.FirstOrDefaultAsync(a => a.UserName == user.UserName && a.Email == user.Email).Result;
            }

            if (userr != null)
            {
                var info = new AdminInfoViewModel
                {
                    Name = userr.UserName,
                    ImageUrl = userr.Avatar,
                };
                return View(info);

            }
            else
            {
                return View();
            }
        }
    }
}
