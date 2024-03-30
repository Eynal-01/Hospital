using Hospital.Entities.Data;
using HospitalProject.Entities.DbEntities;

namespace Hospital.WebUI.Models
{
    public class ChatViewModel
    {
        public Chat? CurrentChat { get; set; }
        public Doctor? SenderDoctor { get; set; }
        public Admin? SenderAdmin { get; set; }
        public string? CurrentUserId { get; set; }
        public Admin? FriendAdmin { get; set; }
        public Doctor? FriendDoctor { get; set; }
        public List<NotificationViewModel>? Notifications { get; set; }
    }
}
