using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Domain.EComm
{
    public class Payment
    {
        [Key]
        public Int32 Id { get; set; }
        public decimal Amount { get; set; }
        public Int64 OrderId { get; set; }
        public  Int32 Payment_Method { get; set; } // 1- cash 2- credit
        public Int32 Status { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }

    }
}
