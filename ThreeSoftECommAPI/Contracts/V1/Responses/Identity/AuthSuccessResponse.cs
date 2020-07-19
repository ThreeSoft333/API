using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Responses.Identity
{
    public class AuthSuccessResponse
    {
        public string Token { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string DateOfBirth { get; set; }
        public string ImgUrl { get; set; }
        public string ImgCoverUrl { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public string City { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
    }
}
