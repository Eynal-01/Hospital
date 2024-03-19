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

        public async Task<IActionResult> GetContactSerachUser(string userName)
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
                admins1 = await _context.Admins.Where(d => d.Id != useAd.Id && d.UserName.Contains(userName)).ToListAsync();
            }
            else
            {
                admins1 = await _context.Admins.Where(d => d.UserName.Contains(userName)).ToListAsync();
            }
            admins.AddRange(admins1);

            var doctors1 = new List<Doctor>();
            if (useDo != null)
            {
                doctors1 = await _context.Doctors.Where(d => d.Id != useDo.Id && d.FirstName.Contains(userName) || d.LastName.Contains(userName)).ToListAsync();
            }
            else
            {
                doctors1 = await _context.Doctors.Where(d => d.FirstName.Contains(userName) || d.LastName.Contains(userName)).ToListAsync();
            }
            doctors.AddRange(doctors1);

            return Ok(new { doctors = doctors, admins = admins });
        }

        public async Task<IActionResult> GetUserChatInUser()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var useDo = await _context.Doctors.FirstOrDefaultAsync(d => d.UserName == user.UserName && d.Email == user.Email);
            var useAd = await _context.Admins.FirstOrDefaultAsync(d => d.UserName == user.UserName && d.Email == user.Email);
            var userRole = await _userManager.GetRolesAsync(user);

            List<Doctor> doctors = new List<Doctor>();
            List<Admin> admins = new List<Admin>();

            List<Notification> myNotification;
            if (useAd != null)
            {
                myNotification = await _context.Notifications.Where(d => d.ReceiverId == useAd.Id).ToListAsync();
            }
            else
            {
                myNotification = await _context.Notifications.Where(d => d.ReceiverId == useDo.Id).ToListAsync();
            }

            var admins1 = new List<Admin>();

            if (useAd != null)
            {
                admins1 = await _context.Admins.Where(d => d.Id != useAd.Id).ToListAsync();
                //myNotification.Where(d => d.)
            }
            else
            {
                admins1 = await _context.Admins.ToListAsync();
            }

            for (int i = 0; i < admins1.Count(); i++)
            {
                var noti = myNotification.Where(d => d.SenderId == admins1[i].Id).ToList();
                admins1[i].MissedNotifCount = noti.Count();
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

            for (int i = 0; i < doctors1.Count(); i++)
            {
                var noti = myNotification.Where(d => d.SenderId == doctors1[i].Id).ToList();
                doctors1[i].MissedNotifCount = noti.Count();
            }

            doctors.AddRange(doctors1);

            return Ok(new { doctors = doctors, admins = admins });
        }

        public async Task<IActionResult> GetClickedUserMessagesDoctor(string doctorId)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var userDoctor = await _context.Doctors.FirstOrDefaultAsync(d => d.UserName == currentUser.UserName && d.Email == currentUser.Email);
            var userAdmin = await _context.Admins.FirstOrDefaultAsync(d => d.UserName == currentUser.UserName && d.Email == currentUser.Email);

            List<Notification> notifications;
            Chat chat = null;
            if (userAdmin != null)
            {
                notifications = await _context.Notifications.Where(d => d.SenderId == doctorId && d.ReceiverId == userAdmin.Id).ToListAsync();
                chat = await _context.Chats.Include(nameof(Chat.ReceiverDoctor)).FirstOrDefaultAsync(c => c.SenderId == userAdmin.Id && c.ReceiverId == doctorId || c.ReceiverId == userAdmin.Id && c.SenderId == doctorId);
            }
            else
            {
                notifications = await _context.Notifications.Where(d => d.SenderId == doctorId && d.ReceiverId == userDoctor.Id).ToListAsync();
                chat = await _context.Chats.Include(nameof(Chat.ReceiverDoctor)).FirstOrDefaultAsync(c => c.SenderId == userDoctor.Id && c.ReceiverId == doctorId || c.ReceiverId == userDoctor.Id && c.SenderId == doctorId);
            }

            _context.Notifications.RemoveRange(notifications);
            await _context.SaveChangesAsync();


            //var receiver = await _context.Admins.FirstOrDefaultAsync(u => u.Id == doctorId);

            if (chat == null)
            {
                try
                {
                    var chat1 = await _context.Chats.ToListAsync();

                    chat = new Chat
                    {
                        Messages = new List<Message>(),
                        ReceiverId = doctorId,
                    };

                    if (userAdmin != null)
                    {
                        chat.SenderId = userAdmin.Id;
                    }
                    else
                    {
                        chat.SenderId = userDoctor.Id;
                    }

                    await _context.Chats.AddAsync(chat);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            var receiverAdmin = await _context.Admins.FirstOrDefaultAsync(u => u.Id == doctorId);
            var receiverDoctor = await _context.Doctors.FirstOrDefaultAsync(u => u.Id == doctorId);

            if (receiverAdmin != null)
            {
                chat.ReceiverAdmin = receiverAdmin;
            }
            else
            {
                chat.ReceiverDoctor = receiverDoctor;
            }
            //if (userAdmin != null)
            //{
            //}
            //else
            //{
            //    chat.ReceiverDoctor = await _context.Doctors.FirstOrDefaultAsync(u => u.Id == doctorId);
            //}

            if (userAdmin != null)
            {
                if (chat.ReceiverId == userAdmin.Id)
                {
                    chat.ReceiverAdmin = _context.Admins.FirstOrDefault(u => u.Id == chat.SenderId);
                }
            }
            else
            {
                if (chat.ReceiverId == userDoctor.Id)
                {
                    chat.ReceiverDoctor = _context.Doctors.FirstOrDefault(u => u.Id == chat.SenderId);
                }
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
            };

            if (userAdmin != null)
            {
                model.SenderAdmin = userAdmin;
                model.SenderAdmin.MissedNotifCount = 0;
                model.CurrentUserId = userAdmin.Id;
            }
            else
            {
                model.SenderDoctor = userDoctor;
                model.CurrentUserId = userDoctor.Id;
            }

            return Ok(model);
        }

        public async Task<IActionResult> GetClickedUserMessages(string doctorId)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            //var user = await _context.Admins.FirstOrDefaultAsync(d => d.UserName == currentUser.UserName && d.Email == currentUser.Email);
            //var chat = await _context.Chats.Include(nameof(Chat.ReceiverDoctor)).Include(nameof(Chat.ReceiverAdmin)).FirstOrDefaultAsync(c => c.SenderId == user.Id && c.ReceiverId == doctorId || c.ReceiverId == user.Id && c.SenderId == doctorId);
            //var receiver = await _context.Doctors.FirstOrDefaultAsync(u => u.Id == doctorId);

            var userDoctor = await _context.Doctors.FirstOrDefaultAsync(d => d.UserName == currentUser.UserName && d.Email == currentUser.Email);
            var userAdmin = await _context.Admins.FirstOrDefaultAsync(d => d.UserName == currentUser.UserName && d.Email == currentUser.Email);


            //await _context.Notifications.Remove()

            //var senderDoctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == chat.);
            //var senderAdmin = await _context.Admins.FirstOrDefaultAsync(d => d.Id == doctorId);
            List<Notification> notifications;
            Chat chat = null;
            if (userAdmin != null)
            {
                notifications = await _context.Notifications.Where(d => d.SenderId == doctorId && d.ReceiverId == userAdmin.Id).ToListAsync();
                chat = await _context.Chats.Include(nameof(Chat.ReceiverDoctor)).FirstOrDefaultAsync(c => c.SenderId == userAdmin.Id && c.ReceiverId == doctorId || c.ReceiverId == userAdmin.Id && c.SenderId == doctorId);
            }
            else
            {
                notifications = await _context.Notifications.Where(d => d.SenderId == doctorId && d.ReceiverId == userDoctor.Id).ToListAsync();
                chat = await _context.Chats.Include(nameof(Chat.ReceiverDoctor)).FirstOrDefaultAsync(c => c.SenderId == userDoctor.Id && c.ReceiverId == doctorId || c.ReceiverId == userDoctor.Id && c.SenderId == doctorId);
            }

            //_context.Notifications.RemoveRange(notifications);
            //await _context.SaveChangesAsync();

            if (chat == null)
            {
                try
                {
                    var chat1 = await _context.Chats.ToListAsync();
                    chat = new Chat
                    {
                        Messages = new List<Message>(),
                        ReceiverId = doctorId,
                    };

                    if (userAdmin != null)
                    {
                        chat.SenderId = userAdmin.Id;
                    }
                    else
                    {
                        chat.SenderId = userDoctor.Id;
                    }

                    await _context.Chats.AddAsync(chat);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            var receiverAdmin = await _context.Admins.FirstOrDefaultAsync(u => u.Id == doctorId);
            var receiverDoctor = await _context.Doctors.FirstOrDefaultAsync(u => u.Id == doctorId);

            if (receiverAdmin != null)
            {
                chat.ReceiverAdmin = receiverAdmin;
            }
            else
            {
                chat.ReceiverDoctor = receiverDoctor;
            }


            if (userAdmin != null)
            {
                if (chat.ReceiverId == userAdmin.Id)
                {
                    chat.ReceiverAdmin = _context.Admins.FirstOrDefault(u => u.Id == chat.SenderId);
                }
            }
            else
            {
                if (chat.ReceiverId == userDoctor.Id)
                {
                    chat.ReceiverDoctor = _context.Doctors.FirstOrDefault(u => u.Id == chat.SenderId);
                }
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
                //SenderAdmin = user,
                //CurrentUserId = user.Id,
            };

            if (userAdmin != null)
            {
                model.SenderAdmin = userAdmin;
                model.CurrentUserId = userAdmin.Id;
            }
            else
            {
                model.SenderDoctor = userDoctor;
                model.CurrentUserId = userDoctor.Id;
            }

            return Ok(model);
        }

        public async Task<IActionResult> UserMessage(string id)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            //var user = await _context.Admins.FirstOrDefaultAsync(d => d.UserName == currentUser.UserName && d.Email == currentUser.Email);
            //var chat = await _context.Chats.Include(nameof(Chat.ReceiverDoctor)).Include(nameof(Chat.ReceiverAdmin)).FirstOrDefaultAsync(c => c.SenderId == user.Id && c.ReceiverId == doctorId || c.ReceiverId == user.Id && c.SenderId == doctorId);
            //var receiver = await _context.Doctors.FirstOrDefaultAsync(u => u.Id == doctorId);

            var userDoctor = await _context.Doctors.FirstOrDefaultAsync(d => d.UserName == currentUser.UserName && d.Email == currentUser.Email);
            var userAdmin = await _context.Admins.FirstOrDefaultAsync(d => d.UserName == currentUser.UserName && d.Email == currentUser.Email);

            //var senderDoctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == chat.);
            //var senderAdmin = await _context.Admins.FirstOrDefaultAsync(d => d.Id == doctorId);

            List<Notification> myNotification;
            if (userAdmin != null)
            {
                myNotification = await _context.Notifications.Where(d => d.ReceiverId == userAdmin.Id).ToListAsync();
            }
            else
            {
                myNotification = await _context.Notifications.Where(d => d.ReceiverId == userDoctor.Id).ToListAsync();
            }

            //_context.Notifications.RemoveRange(myNotification);
            //await _context.SaveChangesAsync();

            Chat chat = null;
            if (userAdmin != null)
            {
                chat = await _context.Chats.Include(nameof(Chat.ReceiverDoctor)).FirstOrDefaultAsync(c => c.SenderId == userAdmin.Id && c.ReceiverId == id || c.ReceiverId == userAdmin.Id && c.SenderId == id);
            }
            else
            {
                chat = await _context.Chats.Include(nameof(Chat.ReceiverDoctor)).FirstOrDefaultAsync(c => c.SenderId == userDoctor.Id && c.ReceiverId == id || c.ReceiverId == userDoctor.Id && c.SenderId == id);
            }

            if (chat == null)
            {
                try
                {
                    var chat1 = await _context.Chats.ToListAsync();
                    chat = new Chat
                    {
                        Messages = new List<Message>(),
                        ReceiverId = id,
                    };

                    if (userAdmin != null)
                    {
                        chat.SenderId = userAdmin.Id;
                    }
                    else
                    {
                        chat.SenderId = userDoctor.Id;
                    }

                    await _context.Chats.AddAsync(chat);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            var receiverAdmin = await _context.Admins.FirstOrDefaultAsync(u => u.Id == id);
            var receiverDoctor = await _context.Doctors.FirstOrDefaultAsync(u => u.Id == id);

            if (receiverAdmin != null)
            {
                chat.ReceiverAdmin = receiverAdmin;
            }
            else
            {
                chat.ReceiverDoctor = receiverDoctor;
            }


            if (userAdmin != null)
            {
                if (chat.ReceiverId == userAdmin.Id)
                {
                    chat.ReceiverAdmin = _context.Admins.FirstOrDefault(u => u.Id == chat.SenderId);
                }
            }
            else
            {
                if (chat.ReceiverId == userDoctor.Id)
                {
                    chat.ReceiverDoctor = _context.Doctors.FirstOrDefault(u => u.Id == chat.SenderId);
                }
            }

            List<Message> messages = new List<Message>();
            if (chat != null)
            {
                messages = await _context.Messages.Where(c => c.ChatId == chat.Id).OrderBy(d => d.WriteTime).ToListAsync();
            }

            chat.Messages = messages;
            List<NotificationViewModel> notificationList = new List<NotificationViewModel>();

            for (int i = 0; i < myNotification.Count(); i++)
            {
                var count = 0;
                for (int k = 0; k < myNotification.Count(); k++)
                {
                    if (myNotification[k].SenderId == myNotification[i].SenderId)
                    {
                        count += 1;
                    }
                }
                if (count > 0)
                {
                    var notification = new NotificationViewModel
                    {
                        NotificationCount = count,
                        SenderId = myNotification[i].SenderId,
                    };
                    notificationList.Add(notification);
                }
            }

            var model = new ChatViewModel
            {
                CurrentChat = chat,
                Notifications = notificationList,
                //SenderAdmin = user,
                //CurrentUserId = user.Id,
            };

            if (userAdmin != null)
            {
                model.SenderAdmin = userAdmin;
                model.CurrentUserId = userAdmin.Id;
            }
            else
            {
                //if (chat.Re)
                model.SenderDoctor = userDoctor;
                model.CurrentUserId = userDoctor.Id;
            }

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

                    Notification notification = null;
                    if (user.Id != model.ReceiverId)
                    {
                        message.ReceiverId = model.ReceiverId;
                        message.SenderId = user.Id;


                        notification = new Notification
                        {
                            ReceiverId = model.ReceiverId,
                            SenderId = user.Id,
                            //Sender = user,
                        };
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

                        notification = new Notification
                        {
                            SenderId = user.Id,
                            //Sender = user,
                            ReceiverId = model.SenderId,
                        };

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

                    await _context.Notifications.AddAsync(notification);
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
