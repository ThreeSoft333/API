using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Domain.Identity
{
    public class AuthenticationResultWeb
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public string Errors { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        
    }
}
