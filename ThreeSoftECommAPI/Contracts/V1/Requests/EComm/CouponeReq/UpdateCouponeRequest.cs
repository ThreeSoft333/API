using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Requests.EComm.CouponeReq
{
    public class UpdateCouponeRequest
    {
        public string Code { get; set; }
        public Int32 Type { get; set; } // 1 - fixed 2 - percent
        public Int32 Status { get; set; } // 1 - Active 0 - Inactive
        public Int32 Quantity { get; set; }
        public decimal Amount { get; set; }
        public Int32 Percentage { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
    }
}
