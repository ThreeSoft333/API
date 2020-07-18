using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Domain.EComm;
using ThreeSoftECommAPI.Domain.Identity;

namespace ThreeSoftECommAPI.Contracts.V1.Responses.EComm
{
    public class MyOrderResponse
    {
        public Int64 OrderId { get; set; }
        public Int32 ProductCount { get; set; }
        public Int32 OrderStatus { get; set; }
        public Int32? CouponId { get; set; }
        public string OrderDate { get; set; }
        public decimal Total { get; set; }
        public UserAddresses userAddresse { get; set; }
        public List<OrderItems> orderItems { get; set; }
    }
}
