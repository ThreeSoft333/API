using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1.Requests.Identity;
using ThreeSoftECommAPI.Contracts.V1.Responses.Identity;
using ThreeSoftECommAPI.Domain.Identity;
using ThreeSoftECommAPI.Options;

namespace ThreeSoftECommAPI.Services.Identity
{
    public class IdentityService :IIdentityService
    {
        private readonly UserManager<AppUser> _userManager;
        
        private readonly JwtSettings _jwtSettings;

        public IdentityService(UserManager<AppUser> userManager, JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
        }

        public async Task<AuthenticationResult> RegisterAsync(string email, string MobileNo, string password)
        {
            var User = await _userManager.FindByNameAsync(MobileNo);

            
            if (User != null)
            {
                return new AuthenticationResult
                {
                    Errors = "User with this email address already exists"
                };
            }

            var newUser = new AppUser
            {
                Email = email,
                UserName = MobileNo,
                PhoneNumber = MobileNo
            };

            var CreateUser = await _userManager.CreateAsync(newUser, password);

            if (!CreateUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    Success = false,
                    Errors = CreateUser.Errors.Select(x => x.Description).FirstOrDefault()
                };
            }
            
            return GenerateAuthenticationResultForUser(newUser);

        }
        
        public async Task<string> GeneratePhoneNumberConfirmedToken(AppUser appUser)
        {
            return await _userManager.GenerateChangePhoneNumberTokenAsync(appUser, appUser.PhoneNumber);
        }
        public async Task<AuthenticationResult> ConfirmPhone(string UserId,string Token)
        {
            var User = await _userManager.FindByIdAsync(UserId);

            var confirmToken = await _userManager.ChangePhoneNumberAsync(User, User.PhoneNumber, Token);

            if (!confirmToken.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = confirmToken.Errors.Select(x => x.Description).FirstOrDefault()
                };
            }

            return GenerateAuthenticationResultForUser(User);

        }
        public async Task<AuthenticationResult> LoginAsync(string Email, string PhoneNumber, string password)
        {
            var User = await _userManager.FindByNameAsync(PhoneNumber);
            
            if (User == null)
            {
                return new AuthenticationResult
                {
                    Errors = "User does not exists"
                };
            }

            var userHasValidPassword = await _userManager.CheckPasswordAsync(User, password);

            if (!userHasValidPassword)
            {
                return new AuthenticationResult
                {
                    Errors = "User/password combination is wrong"
                };
            }

            return GenerateAuthenticationResultForUser(User);
        }

        private AuthenticationResult GenerateAuthenticationResultForUser(AppUser User)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                 {
                    new Claim(JwtRegisteredClaimNames.Sub,User.PhoneNumber),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email,User.Email),
                    new Claim("id",User.Id)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
                IssuedAt = DateTime.UtcNow
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);


            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
                UserId = User.Id,
                UserName = User.UserName,
                FullName = User.FullName,
                DateOfBirth = User.DateOfBirth,
                Email = User.Email,
                ImgUrl = User.ProfileImageUrl,
                ImgCoverUrl = User.CoverImageUrl,
                Address = User.Address,
                City = User.City,
                PhoneNumberConfirmed = User.PhoneNumberConfirmed
            };
        }

        public async Task<AuthenticationResult> ChangePasswordAsync(string PhoneNumber, string Currentpassword, string NewPassword)
        {
            var User = await _userManager.FindByNameAsync(PhoneNumber);

            if (User == null)
            {
                return new AuthenticationResult
                {
                    Success = false,
                    Errors = "User does not exists"
                };
            }

            var changePass = await _userManager.ChangePasswordAsync(User, Currentpassword, NewPassword);

            if (!changePass.Succeeded)
            {
                return new AuthenticationResult
                {
                    Success = false,
                    Errors = changePass.Errors.Select(x => x.Description).FirstOrDefault()
                };
            }

            return new AuthenticationResult
            {
                Success = true,
            };
        }

        public async Task<AuthenticationResult> UpdateUserInfoAsync(AppUser appUser)
        {
            var UserUpdate = await _userManager.UpdateAsync(appUser);

            if (!UserUpdate.Succeeded)
            {
                return new AuthenticationResult
                {
                    Success = false,
                    Errors = UserUpdate.Errors.Select(x => x.Description).FirstOrDefault()
                };
            }

            return new AuthenticationResult
            {
                Success = true,
            };
        }

        public async Task<AppUser> GetUserById(string UserId)
        {
            return await _userManager.FindByIdAsync(UserId);
        }
        public async Task<bool> AddUserRole(AppUser appUser,string Role)
        {
            var UserUpdate = await _userManager.AddToRoleAsync(appUser, Role);

            if (!UserUpdate.Succeeded)
            {
                return true;
            }

            return false;
        }

        public async Task<string> GetUserRole(AppUser appUser)
        {
            var UserRole = await _userManager.GetRolesAsync(appUser);
            return UserRole[0];
        }
    }
}
