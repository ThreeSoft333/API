using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Domain.Identity
{
    public class UserAddresses
    {
        [Key]
        public Int32 Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }

        public string Title { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Street { get; set; }
        public string BuildingNo { get; set; }
        public string PostCode { get; set; }
        public string Lon { get; set; }
        public string Lat { get; set; }

    }
}
