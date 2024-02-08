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
            var admin = _context.Admins.FirstOrDefaultAsync(a => a.UserName == user.UserName && a.Email == user.Email).Result;

            if (admin != null)
            {
                var info = new AdminInfoViewModel
                {
                    Name = admin.UserName,
                    ImageUrl = admin.Avatar
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
