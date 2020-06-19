using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Domain.Identity;

namespace ThreeSoftECommAPI.Domain.EComm
{
    public class Coupon
    {
        [Key]
        public Int32 Id { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public Int32 Type { get; set; } // 1 - fixed 2 - percent
        public Int32 Status { get; set; } // 1 - Active 0 - Inactive
        public Int32 Quantity { get; set; } 
        public decimal Amount { get; set;}
        public Int32 Percentage { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }

        [ForeignKey(nameof(CreateBy))]
        public AppUser User { get; set; }

        [ForeignKey(nameof(UpdateBy))]
        public AppUser User1 { get; set; }


    }
}
