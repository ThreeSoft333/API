using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1.Requests.Identity;
using ThreeSoftECommAPI.Domain.Identity;

namespace ThreeSoftECommAPI.Services.Identity
{
  public  interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string Email, string PhoneNumber, string password);
        Task<AuthenticationResult> LoginAsync(string Email, string PhoneNumber, string password);
        Task<AuthenticationResult> ChangePasswordAsync(string PhoneNumber, string Currentpassword,string NewPassword);
        Task<string> GeneratePasswordResetToken(string PhoneNumber);
        Task<Int32> ResetPasswordAsync(string PhoneNumber, string Token, string NewPassword);
        Task<string> GeneratePhoneNumberConfirmedToken(AppUser appUser);
        Task<AuthenticationResult> ConfirmPhone(string UserId, string Token);
        Task<AuthenticationResult> UpdateUserInfoAsync(AppUser appUser);
        Task<AppUser> GetUserById(string UserId);
        Task<bool> AddUserRole(AppUser appUser, string Role);
        Task<string> GetUserRole(AppUser appUser);

    }
}
