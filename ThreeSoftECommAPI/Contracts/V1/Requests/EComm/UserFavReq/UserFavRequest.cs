using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Requests.EComm.UserFavReq
{
    public class UserFavRequest
    {
        public string UserId { get; set; }
        public Int64 ProductId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
