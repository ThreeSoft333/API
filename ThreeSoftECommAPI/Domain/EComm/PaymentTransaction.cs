using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Domain.Identity;

namespace ThreeSoftECommAPI.Domain.EComm
{
    public class PaymentTransaction
    {
        [Key]
        public Int64 Id { get; set; }
        public string UserId { get; set; }
        public string CustomerPaymentId { get; set; }

        [ForeignKey(nameof(UserId))]
        public AppUser Users { get; set; }
    }
}
