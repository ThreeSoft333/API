using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Requests.EComm.NotificationReq
{
    public class NotificationRequest
    {
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
        public string BodyAr { get; set; }
        public string BodyEn { get; set; }
        public string ImageUrl { get; set; }
    }
}
