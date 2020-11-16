using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Domain.Identity;

namespace ThreeSoftECommAPI.Domain.EComm
{
    public class UserNotificationCount
    {
        [Key]
        public int id { get; set; }
        public string userId { get; set; }

        public int NotificationCount { get; set; }

        [ForeignKey(nameof(userId))]
        public AppUser User { get; set; }
    }
}
