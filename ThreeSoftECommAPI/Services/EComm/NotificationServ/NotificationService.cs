using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Data;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.NotificationServ
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _dataContext;

        public NotificationService(ApplicationDbContext dbContext)
        {
            _dataContext = dbContext;
        }
        public async Task<int> CreateNotificationAsync(Notification notification)
        {
            await _dataContext.Notifications.AddAsync(notification);
            var created = await _dataContext.SaveChangesAsync();
            return created;
        }

        public async Task<bool> DeleteNotificationAsync(int id)
        {
            var notf = await GetNotificationByIdAsync(id);

            if (notf == null)
                return false;

            _dataContext.Notifications.Remove(notf);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<List<Notification>> GetNotificationAsync()
        {
            return await _dataContext.Notifications.Select(
                x => new Notification
                {
                    TitleAr = x.TitleAr,
                    TitleEn = x.TitleEn,
                    BodyAr = x.BodyAr,
                    BodyEn = x.BodyEn,
                    ImageUrl = x.ImageUrl,
                    CreateDate = x.CreateDate.Substring(x.CreateDate.IndexOf(" ",1),9)
                }).ToListAsync();
        }

        public async Task<Notification> GetNotificationByIdAsync(int id)
        {
            return await _dataContext.Notifications.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> UpdateNotificationAsync(Notification notification)
        {
            _dataContext.Notifications.Update(notification);
            var created = await _dataContext.SaveChangesAsync();
            return created;
        }
    }
}
