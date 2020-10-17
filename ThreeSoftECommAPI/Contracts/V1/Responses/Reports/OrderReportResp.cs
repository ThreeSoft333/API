using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Responses.Reports
{
    public class OrderReportResp
    {
        public Int64 orderId { get; set; }
        public string customrName { get; set; }
        public string date { get; set; }
        public string address { get; set; }
        public string paymentMethod { get; set; }
        public decimal amount { get; set; }
    }
}
