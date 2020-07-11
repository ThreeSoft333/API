using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Requests.EComm.OrderReq
{
    public class CreateOrderRequest
    {
        public string UserId { get; set; }
        public Int32? UserAddressesId { get; set; }
        public Int32 DeliveryMethod { get; set; } // 1-pickup 2-delivery
        public Int32 PaymentMethod { get; set; }
        public Int32? CouponId { get; set; }
        public Int64 CartId { get; set; }

    }
}
