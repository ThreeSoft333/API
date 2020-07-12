using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Requests.EComm.OrderReq
{
    public class ReorderRequest
    {
        public string UserId { get; set; }
        public Int64 OrderId { get; set; }
    }
}
