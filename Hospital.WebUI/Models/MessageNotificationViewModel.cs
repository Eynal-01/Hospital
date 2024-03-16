using HospitalProject.Entities.DbEntities;

namespace Hospital.WebUI.Models
{
    public class MessageNotificationViewModel
    {
        public Message? Message { get; set; }
        public Notification? Notification { get; set; }
    }
}
