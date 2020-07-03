using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Domain.EComm
{
    public class CartItem
    {
        [Key]
        public Int64 Id { get; set; }
        public Int64 CartId { get; set; }
        public Int64 ProductId { get; set; }
        public Int32 Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [ForeignKey(nameof(CartId))]
        public Cart cart { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product product { get; set; }
    }
}
