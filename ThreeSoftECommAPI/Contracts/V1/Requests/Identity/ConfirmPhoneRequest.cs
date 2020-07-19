using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Requests.Identity
{
    public class ConfirmPhoneRequest
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
