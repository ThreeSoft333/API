using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Data;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.UserNotifCountServ
{
    public class UserNotificationCountService : IUserNotificationCountService
    {
        private readonly ApplicationDbContext _dataContext;

        public UserNotificationCountService(ApplicationDbContext dbContext)
        {
            _dataContext = dbContext;
        }

        public bool Create(string userId)
        {
            try
            {
                var user = new UserNotificationCount
                {
                    userId = userId,
                    NotificationCount = 0
                };

                _dataContext.Add(user);
                var created = _dataContext.SaveChanges();
                return created > 0;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public bool WhenAddNewNotification()
        {
            try
            {
                var userNotificationCounts = _dataContext.userNotificationCounts.ToList();
                userNotificationCounts.ForEach(a => a.NotificationCount += 1);
                var update = _dataContext.SaveChanges();
                return update > 0;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool Update(string userId)
        {
           var user =  _dataContext.userNotificationCounts.SingleOrDefault(x => x.userId == userId);
            user.NotificationCount = 0;
            var update = _dataContext.SaveChanges();
            return update > 0;
        }

        public int getCountByUser(string userId)
        {
            return _dataContext.userNotificationCounts.SingleOrDefault(x => x.userId == userId).NotificationCount;
        }
    }
}
