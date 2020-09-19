using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.NotificationServ
{
   public interface INotificationService
    {
        Task<List<Notification>> GetNotificationAsync();
        Task<Notification> GetNotificationByIdAsync(Int32 id);
        Task<int> CreateNotificationAsync(Notification notification);
        Task<int> UpdateNotificationAsync(Notification notification);
        Task<bool> DeleteNotificationAsync(Int32 id);
    }
}
