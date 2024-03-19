using Hospital.Entities.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Hospital.WebUI.Hubs
{
    public class UserHub : Hub
    {
        private readonly UserManager<CustomIdentityUser> _userManager;
        private IHttpContextAccessor _contextAccessor;
        private CustomIdentityDbContext _context;

        public UserHub(UserManager<CustomIdentityUser> userManager, IHttpContextAccessor contextAccessor, CustomIdentityDbContext context)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _context = context;
        }

        public override async Task OnConnectedAsync()
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
            var role = await _userManager.GetRolesAsync(user);
            //string ro = role[0];
            //await Clients.Others.SendAsync("Connect", ro);
            var d = role[0].Trim();
            await Clients.All.SendAsync("Connect", d);
        }

        public async Task AdminCall(string id)
        {
            await Clients.Users(new String[] { id }).SendAsync("AdminRefresh", id);
        }

        public async Task DoctorCall(string id)
        {
            await Clients.Users(new String[] { id }).SendAsync("DoctorPostShow", id);
        }
    }
}