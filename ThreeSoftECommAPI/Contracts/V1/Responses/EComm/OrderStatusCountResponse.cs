using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Responses.EComm
{
    public class OrderStatusCountResponse
    {
        public int orders { get; set; }
        public int Received { get; set; }
        public int inProgressNow { get; set; }
        public int readyForDelivary { get; set; }
        public int delivered { get; set; }
        public int Rejected { get; set; }
    }
}
