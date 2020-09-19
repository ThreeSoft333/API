using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Requests.Identity
{
    public class UpdateFcmRegToken
    {
        public string UserId { get; set; }
        public string FcmRegToken { get; set; }
    }
}
