using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Domain.Identity
{
    public class AppUser: IdentityUser
    {
        [Key]
        public override string Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string ProfileImageUrl { get; set; }
        public string CoverImageUrl { get; set; }
        public string DateOfBirth { get; set; }
        public string City { get; set; }
    }
}
