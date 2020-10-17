using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Responses.Reports
{
    public class OrderProductReportResp
    {
        public Int64 orderId { get; set; }
        public Int32 count { get; set; }
        public decimal price { get; set; }
        public decimal total { get; set; }
    }
}
