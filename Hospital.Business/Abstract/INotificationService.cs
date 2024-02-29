using HospitalProject.Entities.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Abstract
{
    public interface INotificationService
    {
        Task AddAsync(Notification notification);
        Task<IEnumerable<Notification>> GetAllNotificationOfUSer(string userId);
        Task<Notification> GetNotificationByIdAsync(string notificationId);
        Task<int> GetUnreadNotificationCountAsync(string userId);
        Task UpdateNotificationIsReadAsync(string notificationId);
        Task DeleteNotificationsOfUserAsync(string userId);
    }
}
