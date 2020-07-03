using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Requests.EComm.CartItem
{
    public class CreateCartItemRequest
    {
        public string UserId { get; set; }
        public Int64 ProductId { get; set; }
        public Int32 Quantity { get; set; }
    }
}
