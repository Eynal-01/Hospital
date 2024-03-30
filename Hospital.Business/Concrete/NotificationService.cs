using Hospital.DataAccess.Abstract;
using HospitalProject.Entities.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Concrete
{
    public class NotificationService
    {
        private readonly INotificationDal _notificationDal;

        public NotificationService(INotificationDal notificationDal)
        {
            _notificationDal = notificationDal;
        }

        public async Task AddAsync(Notification notification)
        {
            await _notificationDal.AddAsync(notification);
        }

        public async Task DeleteUserNotificationsAsync(string userId)
        {
            var notifications = await _notificationDal.GetListAsync(n => n.SenderId == userId || n.ReceiverId == userId);

            if (notifications != null)
            {
                foreach (var notification in notifications)
                {
                    await _notificationDal.DeleteAsync(notification);
                }
            }
        }

        public async Task<IEnumerable<Notification>> GetAllNotificationsOfUserAsync(string userId)
        {
            return await _notificationDal.GetListAsync(n => n.ReceiverId == userId);
        }

        public async Task<Notification?> GetNotificationByIdAsync(string notificationId)
        {
            var notification = new Notification();
            return notification;
            //return await _notificationDal.GetAsync(n => n.Id == notificationId);
        }

        public async Task<int> GetUnreadNotificationCountAsync(string userId)
        {
            var notifications = await GetAllNotificationsOfUserAsync(userId);

            return notifications.Count(n => !n.IsCheck);
        }

        public async Task UpdateNotificationIsReadAsync(string notificationid)
        {
            var notification = await GetNotificationByIdAsync(notificationid);

            if (notification != null)
            {
                notification.IsCheck = true;

                await _notificationDal.UpdateAsync(notification);
            }
        }
    }
}
