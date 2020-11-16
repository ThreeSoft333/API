using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Services.EComm.UserNotifCountServ
{
    public interface IUserNotificationCountService
    {
        bool Create(string UserId);
        bool WhenAddNewNotification();
        bool Update(string userId);
        int getCountByUser(string userId);
    }
}
