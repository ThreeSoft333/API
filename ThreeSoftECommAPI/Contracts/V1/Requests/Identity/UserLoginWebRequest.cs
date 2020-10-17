using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Requests.Identity
{
    public class UserLoginWebRequest
    {
        public string userName { get; set; }
        public string Password { get; set; }
    }
}
