using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Requests.EComm.OrderReq
{
    public class UpdateOrderRequest
    {
        public long orderId { get; set; }
        public int status { get; set; }
        public string rejectReason { get; set; }
    }
}
