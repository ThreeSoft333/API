using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Requests.Identity
{
    public class ResetPasswordRequest
    {
        public string PhoneNumber { get; set; }
    }
}
