using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Domain.Identity;

namespace ThreeSoftECommAPI.Domain.EComm
{
    public class UserFavourites
    {
        [Key]
        public Int64 Id { get; set; }
        public string UserId { get; set; }
        public Int64 ProductId  { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }




        [ForeignKey(nameof(UserId))]
        public AppUser appUser { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product product { get; set; }


    }
}
