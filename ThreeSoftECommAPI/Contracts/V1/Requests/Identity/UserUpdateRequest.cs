using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Requests.Identity
{
    public class UserUpdateRequest
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string ProfileImageUrl { get; set; }
        public string CoverImageUrl { get; set; }
        public string DateOfBirth { get; set; }
    }
}
