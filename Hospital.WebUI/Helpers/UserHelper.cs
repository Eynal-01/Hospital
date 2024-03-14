using Hospital.Entities.Data;
using Microsoft.AspNetCore.Identity;

namespace Hospital.WebUI.Helpers
{
    public class UserHelper
    {
        public static async Task<CustomIdentityUser?> GetCurrentUserAsync(HttpContext httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
                var userManager = httpContext.RequestServices.GetService<UserManager<CustomIdentityUser>>();

                return await userManager.GetUserAsync(httpContext.User);
            }

            return null!;
        }
    }
}
