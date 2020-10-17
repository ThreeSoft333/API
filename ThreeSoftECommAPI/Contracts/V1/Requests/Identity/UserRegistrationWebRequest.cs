using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Requests.Identity
{
    public class UserRegistrationWebRequest
    {
        public string fullName { get; set; }
        public string userName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Int32 Role { get; set; }
    }
}
