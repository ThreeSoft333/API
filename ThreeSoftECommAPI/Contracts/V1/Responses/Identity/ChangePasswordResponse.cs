using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Responses.Identity
{
    public class ChangePasswordResponse
    {
        public string message { get; set; }
        public int status { get; set; }
    }
}
