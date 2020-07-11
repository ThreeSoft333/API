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
        public Int64 Id { get; set; }
        public string RejectReason { get; set; }
        public Int32 Status { get; set; } // 1-pinding 2-accepted 3-rejected 4-delivering 5-delivered
        public Int32 DeliveryMethod { get; set; } // 1-pickup 2-delivery
        public string UserId { get; set; }
        public Int32? UserAddressesId { get; set; }
        public Int32? CouponId { get; set; }
        public Int32 PaymentMethod { get; set; } // 1-cash 2- visa
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }

        [ForeignKey(nameof(UserAddressesId))]
        public UserAddresses userAddresses { get; set; }

        [ForeignKey(nameof(CouponId))]
        public Coupon coupon { get; set; }
    }
}
