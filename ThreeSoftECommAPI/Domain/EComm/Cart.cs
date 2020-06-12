using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Domain.Identity;

namespace ThreeSoftECommAPI.Domain.EComm
{
    public class Cart
    {
        [Key]
        public Int64 Id { get; set; }
        public string UserId { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }

        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }

    }
}
