using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Domain.Identity;

namespace ThreeSoftECommAPI.Services.Identity
{
  public  interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string Email, string PhoneNumber, string password);
        Task<AuthenticationResult> LoginAsync(string Email, string PhoneNumber, string password);
    }
}
