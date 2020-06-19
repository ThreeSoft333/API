using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Responses.EComm
{
    public class ErrorResponse
    {
        public string message { get; set; }
        public int status { get; set; }
    }
}
