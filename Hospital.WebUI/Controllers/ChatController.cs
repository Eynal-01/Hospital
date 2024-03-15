using Hospital.Entities.Data;
using Hospital.WebUI.Models;
using HospitalProject.Entities.DbEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.WebUI.Controllers
{
    public class ChatController : Controller
    {
        private readonly CustomIdentityDbContext _context;
        private IWebHostEnvironment _webHost;
        private readonly UserManager<CustomIdentityUser> _userManager;

        public ChatController(UserManager<CustomIdentityUser> userManager, CustomIdentityDbContext context, IWebHostEnvironment webHost)
        {
            _userManager = userManager;
            _context = context;
            _webHost = webHost;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetUserChatInUser()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var useDo = await _context.Doctors.FirstOrDefaultAsync(d => d.UserName == user.UserName && d.Email == user.Email);
            var useAd = await _context.Admins.FirstOrDefaultAsync(d => d.UserName == user.UserName && d.Email == user.Email);
            var userRole = await _userManager.GetRolesAsync(user);

            List<Doctor> doctors = new List<Doctor>();
            List<Admin> admins = new List<Admin>();

            var admins1 = new List<Admin>();

            if (useAd != null)
            {
                admins1 = await _context.Admins.Where(d => d.Id != useAd.Id).ToListAsync();
            }
            else
            {
                admins1 = await _context.Admins.ToListAsync();
            }
            admins.AddRange(admins1);

            var doctors1 = new List<Doctor>();
            if (useDo != null)
            {
                doctors1 = await _context.Doctors.Where(d => d.Id != useDo.Id).ToListAsync();
            }
            else
            {
                doctors1 = await _context.Doctors.ToListAsync();
            }
            doctors.AddRange(doctors1);

            return Ok(new { doctors = doctors, admins = admins });
        }

        public async Task<IActionResult> GetClickedUserMessagesDoctor(string doctorId)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var user = await _context.Doctors.FirstOrDefaultAsync(d => d.UserName == currentUser.UserName && d.Email == currentUser.Email);
            var chat = await _context.Chats.Include(nameof(Chat.ReceiverDoctor)).FirstOrDefaultAsync(c => c.SenderId == user.Id && c.ReceiverId == doctorId || c.ReceiverId == user.Id && c.SenderId == doctorId);
            var receiver = await _context.Admins.FirstOrDefaultAsync(u => u.Id == doctorId);

            //var messageNotification = await _context.FriendRequests.Where(f => f.ReceiverId == user.Id && f.SenderId == receiver.Id && f.Status == "Message").ToListAsync();

            //if (messageNotification != null)
            //{
            //    _dbContext.FriendRequests.RemoveRange(messageNotification);
            //    await _dbContext.SaveChangesAsync();
            //}

            if (chat == null)
            {
                try
                {
                    var chat1 = await _context.Chats.ToListAsync();
                    //var id = "0";
                    //if (chat1.Count() == 0)
                    //{
                    //    id = "1";
                    //}
                    //else
                    //{
                    //    id = chat1[chat1.Count() - 1].Id + 1;
                    //}

                    chat = new Chat
                    {
                        //Id = id,
                        Messages = new List<Message>(),
                        ReceiverId = doctorId,
                        SenderId = user.Id,
                    };

                    await _context.Chats.AddAsync(chat);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            if (chat.ReceiverId == user.Id)
            {
                chat.ReceiverDoctor = _context.Doctors.FirstOrDefault(u => u.Id == chat.SenderId);
            }

            List<Message> messages = new List<Message>();
            if (chat != null)
            {
                messages = await _context.Messages.Where(c => c.ChatId == chat.Id).OrderBy(d => d.WriteTime).ToListAsync();
            }

            chat.Messages = messages;


            var model = new ChatViewModel
            {
                CurrentChat = chat,
                SenderDoctor = user,
                CurrentUserId = user.Id,
            };

            return Ok(model);
        }

        public async Task<IActionResult> GetClickedUserMessages(string doctorId)
        {
            //var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == doctorId);

            ////var user = await _userManager.GetUserAsync(HttpContext.User);
            ////var friend = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            //var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            //var sender = await _context.Doctors.FirstOrDefaultAsync(d => d.UserName == currentUser.UserName && d.Email == currentUser.Email);

            //var chat = await _context.Chats.Include(nameof(Chat.ReceiverUser)).FirstOrDefaultAsync(c => c.SenderUserId == sender.Id && c.ReceiverUserId == doctorId || c.ReceiverUserId == sender.Id && c.SenderUserId == doctorId);


            //if (chat == null)
            //{
            //    chat = new Chat
            //    {
            //        Messages = new List<Message>(),
            //        ReceiverUserId = doctorId,
            //        SenderUserId = sender.Id,
            //    };

            //    await _context.Chats.AddAsync(chat);
            //    await _context.SaveChangesAsync();                
            //}

            //if (chat.ReceiverUserId == sender.Id)
            //{
            //    chat.ReceiverUser = _context.Users.FirstOrDefault(u => u.Id == chat.SenderUserId);
            //}

            //List<Message> messages = new List<Message>();
            //if (chat != null)
            //{
            //    messages = await _context.Messages.Where(c => c.ChatId == chat.Id).OrderBy(d => d.SentDate).ToListAsync();
            //}

            //chat.Messages = messages;

            //return Ok(new { CurrenUserId = sender.Id, ReceiverUserId = doctor.Id, ReceiverName = chat.ReceiverUser.UserName, SenderName = sender.UserName, Chat = chat, ReceiverImageUrl = doctor.Avatar, SenderImageUrl = sender.Avatar });









            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var user = await _context.Admins.FirstOrDefaultAsync(d => d.UserName == currentUser.UserName && d.Email == currentUser.Email);
            var chat = await _context.Chats.Include(nameof(Chat.ReceiverDoctor)).FirstOrDefaultAsync(c => c.SenderId == user.Id && c.ReceiverId == doctorId || c.ReceiverId == user.Id && c.SenderId == doctorId);
            var receiver = await _context.Doctors.FirstOrDefaultAsync(u => u.Id == doctorId);

            //var messageNotification = await _context.FriendRequests.Where(f => f.ReceiverId == user.Id && f.SenderId == receiver.Id && f.Status == "Message").ToListAsync();

            //if (messageNotification != null)
            //{
            //    _dbContext.FriendRequests.RemoveRange(messageNotification);
            //    await _dbContext.SaveChangesAsync();
            //}

            if (chat == null)
            {
                try
                {
                    var chat1 = await _context.Chats.ToListAsync();
                    //var id = "0";
                    //if (chat1.Count() == 0)
                    //{
                    //    id = "1";
                    //}
                    //else
                    //{
                    //    id = chat1[chat1.Count() - 1].Id + 1;
                    //}

                    chat = new Chat
                    {
                        //Id = id,
                        Messages = new List<Message>(),
                        ReceiverId = doctorId,
                        SenderId = user.Id,
                    };

                    await _context.Chats.AddAsync(chat);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            if (chat.ReceiverId == user.Id)
            {
                chat.ReceiverAdmin = _context.Admins.FirstOrDefault(u => u.Id == chat.SenderId);
            }

            List<Message> messages = new List<Message>();
            if (chat != null)
            {
                messages = await _context.Messages.Where(c => c.ChatId == chat.Id).OrderBy(d => d.WriteTime).ToListAsync();
            }

            chat.Messages = messages;


            var model = new ChatViewModel
            {
                CurrentChat = chat,
                SenderAdmin = user,
                CurrentUserId = user.Id,
            };

            return Ok(model);
        }

        [HttpPost(Name = "AddMessage")]
        public async Task<IActionResult> AddMessage(MessageViewModel model)
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                var role = await _userManager.GetRolesAsync(currentUser);
                dynamic user;
                if (role[0] == "admin")
                {
                     user = await _context.Admins.FirstOrDefaultAsync(d => d.UserName == currentUser.UserName && d.Email == currentUser.Email);
                }
                else
                {
                    user = await _context.Doctors.FirstOrDefaultAsync(d => d.UserName == currentUser.UserName && d.Email == currentUser.Email);
                }
                var chat = await _context.Chats.FirstOrDefaultAsync(c => c.SenderId == model.SenderId && c.ReceiverId == model.ReceiverId
           || c.SenderId == model.ReceiverId && c.ReceiverId == model.SenderId);
                if (chat != null)
                {
                    var message = new Message
                    {
                        ChatId = chat.Id,
                        Content = model.Message,
                        WriteTime = DateTime.Now,
                        DateTimeString = DateTime.Now.ToShortTimeString(),
                        HasSeen = false,
                        IsImage = false,
                        ReceiverId = model.ReceiverId,
                        SenderId = model.SenderId,
                    };

                    //FriendRequest request = null;
                    if (user.Id != model.ReceiverId)
                    {
                        message.ReceiverId = model.ReceiverId;
                        message.SenderId = user.Id;

                        //request = new FriendRequest
                        //{
                        //    Content = model.Message,
                        //    Status = "Message",
                        //    SenderId = user.Id,
                        //    Sender = user,
                        //    ReceiverId = model.ReceiverId,
                        //};
                    }
                    else
                    {
                        message.SenderId = user.Id;
                        message.ReceiverId = model.SenderId;
                        //request = new FriendRequest
                        //{
                        //    Content = model.Message,
                        //    Status = "Message",
                        //    SenderId = user.Id,
                        //    Sender = user,
                        //    ReceiverId = model.SenderId,
                        //    RequestTime = DateTime.Now.ToShortDateString() + "\t\t" + DateTime.Now.ToShortTimeString(),
                        //};
                    }

                    //await _dbContext.FriendRequests.AddAsync(request);
                    await _context.Messages.AddAsync(message);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
