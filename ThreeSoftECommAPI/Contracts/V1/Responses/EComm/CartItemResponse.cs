using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Contracts.V1.Responses.EComm
{
    public class CartItemResponse
    {
        public Int64 Id { get; set; }
        public Int64 CartId { get; set; }
        public Int32 Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public ProductResponse product { get; set; }
    }
}
