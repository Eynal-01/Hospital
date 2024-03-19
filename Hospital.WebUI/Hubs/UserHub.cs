using Hospital.Entities.Data;
using HospitalProject.Entities.DbEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

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

        //public async Task LiveChatCall(string id, string id2)
        //{
        //    await Clients.Users(new String[] { id }).SendAsync("LiveChat", id2);
        //}

        public async Task LiveChatCall(string id1, string id2)
        {
            //await Clients.Client("389205e4-9899-4fc0-8fb0-e4e96a0bfa28").SendAsync("ReceiveMessage", id2);
            //await Clients.Client("3f5b0b6b-bbd7-4647-ae82-e4d1a06d3fff").SendAsync("ReceiveMessage", id1);

            var user1 = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id1);
            var user2 = await _context.Admins.FirstOrDefaultAsync(d => d.Id == id1);

            ////var user3 = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id1);
            ////var user3 = await _context.Admins.FirstOrDefaultAsync(d => d.Id == id1);

            if (user1 != null)
            {
                var cu = await _context.Users.FirstOrDefaultAsync(d => d.Email == user1.Email && d.UserName == user1.UserName);
                await Clients.Users(new String[] { cu.Id }).SendAsync("ReceiveMessage", id2);
            }
            else
            {
                var cu = await _context.Users.FirstOrDefaultAsync(d => d.Email == user2.Email && d.UserName == user2.UserName);
                await Clients.Users(new String[] { cu.Id }).SendAsync("ReceiveMessage", id2);
            }

            //await Clients.Users(new String[] { "389205e4-9899-4fc0-8fb0-e4e96a0bfa28" }).SendAsync("ReceiveMessage", id2);
            //await Clients.Users(new String[] { "3f5b0b6b-bbd7-4647-ae82-e4d1a06d3fff" }).SendAsync("ReceiveMessage", id2);
            //await Clients.All.SendAsync("ReceiveMessage", id1);
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