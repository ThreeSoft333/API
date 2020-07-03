﻿using System;
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
        Task<AuthenticationResult> UpdateUserInfoAsync(AppUser appUser);
        Task<AppUser> GetUserById(string UserId);
    }
}
