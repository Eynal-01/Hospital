using Hospital.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.WebUI.Controllers
{
    public class ChatController : Controller
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Gets the chat service used by the controller.
        /// </summary>
        private readonly IChatService _chatService;

        /// <summary>
        /// Gets the message service used by the controller.
        /// </summary>
        private readonly IMessageService _messageService;

        /// <summary>
        /// Gets the notification service used by the controller.
        /// </summary>
        private readonly INotificationService _notificationService;





        public IActionResult Index()
        {
            return View();
        }
    }
}
