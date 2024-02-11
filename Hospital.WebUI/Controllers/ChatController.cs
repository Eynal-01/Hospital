using Hospital.Business.Abstract;
using Hospital.Business.Concrete;
using Hospital.Entities.Data;
using Hospital.WebUI.Helpers;
using Hospital.WebUI.Models;
using HospitalProject.Entities.DbEntities;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.WebUI.Controllers
{
    public class ChatController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly IChatService _chatService;

        private readonly IMessageService _messageService;

        private readonly INotificationService _notificationService;

        public ChatController(
            IUserService userService,
            IChatService chatService,
            IMessageService messageService,
            INotificationService notificationService)
        {
            _userService = userService;
            _chatService = chatService;
            _messageService = messageService;
            _notificationService = notificationService;
        }

        [HttpPost("AddMessage")]
        public async Task<ActionResult<Message>> AddMessage([FromBody] SendMessageViewModel model)
        {
            try
            {
                // For Current User
                var message = new Message()
                {
                    Id = Guid.NewGuid().ToString(),
                    Content = model.Message.Content,
                    ReceiverUserId = model.Message.ReceiverUserId,
                    SenderUserId = model.Message.SenderUserId,
                    ChatId = model.Message.ChatId,
                    SentDate = DateTime.Now
                };
                await _messageService.AddMessageAsync(message);

                message.ReceiverUser = await _userService.GetUserByIdAsync(model.Message.ReceiverUserId);

                message.SenderUser = await _userService.GetUserByIdAsync(model.Message.SenderUserId);

                message.Chat = await _chatService.GetChatByIdAsync(model.Message.ChatId);

                // For User To Send Message
                var otherUserChat = await _chatService.GetChatAsync(model.Message.ReceiverUserId, model.Message.SenderUserId);

                var message2 = new Message()
                {
                    Id = Guid.NewGuid().ToString(),

                    Content = model.Message.Content,

                    ReceiverUserId = model.Message.ReceiverUserId,

                    SenderUserId = model.Message.SenderUserId,

                    ChatId = otherUserChat.Id,

                    SentDate = DateTime.Now
                };

                await _messageService.AddMessageAsync(message2);

                var currentUser = await UserHelper.GetCurrentUserAsync(HttpContext);

                var messageNotificationVM = new MessageNotificationViewModel()
                {
                    Message = message,
                    Notification = null
                };

                if (!model.FirstMessageSent)


                {

                    var notification = new Notification()
                    {
                        Id = Guid.NewGuid().ToString(),

                        Date = DateTime.Now,

                        IsCheck = false,

                        SenderId = currentUser.Id,

                        Sender = currentUser,
                        ReceiverId = model.Message.ReceiverUserId,

                        Receiver = await _userService.GetUserByIdAsync(model.Message.ReceiverUserId),

                        //Message = NotificationType.GetSentYouMessageMessage(currentUser.UserName),
                    };

                    messageNotificationVM.Notification = notification;

                    await _notificationService.AddAsync(notification);
                }

                // Success
                return Ok(messageNotificationVM);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetChats")]
        public async Task<ActionResult<IEnumerable<CustomIdentityUser>>> GetChats(string userId)
        {
            try
            {
                var chats = await _chatService.GetAllUserChats(userId);

                var list = chats.ToList();

                var currentUser = await UserHelper.GetCurrentUserAsync(HttpContext);

                var usersTasks = list.Select(async c =>
                {
                    if (c.SenderUserId != currentUser.Id)
                    {
                        return await _userService.GetUserByIdAsync(c.SenderUserId);
                    }
                    else if (c.ReceiverUserId != currentUser.Id)
                    {
                        return await _userService.GetUserByIdAsync(c.ReceiverUserId);
                    }

                    return null; // Return null for cases when neither SenderUserId nor ReceiverUserId matches the currentUser's Id
                });

                var users = await Task.WhenAll(usersTasks);

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetLastMessage")]
        public async Task<ActionResult<string>> GetLastMessage(string userId)
        {
            try
            {
                var currentUser = await UserHelper.GetCurrentUserAsync(HttpContext);

                var chat = await _chatService.GetChatAsync(currentUser.Id, userId);

                var message = await _messageService.GetLastMessageOfChatAsync(chat);

                if (message == null)
                {
                    return Ok(String.Empty);
                }
                else
                {
                    return Ok(message.Content);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}