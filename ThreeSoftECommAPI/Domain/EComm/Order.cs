using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Domain.Identity;

namespace ThreeSoftECommAPI.Domain.EComm
{
    public class Order
    {
        [Key]
        public Int32 Id { get; set; }
        public double Total { get; set; }
        public double SubTotal { get; set; }
        public string RejectReason { get; set; }
        public Int32 Status { get; set; } // 1-pinding 2-accepted 3-rejected 4-delivering 5-delivered
        public Int32 DeliveryMethod { get; set; } // 1-pickup 2-delivery
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }

    }
}
