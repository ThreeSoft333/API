using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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
                Email = User.Email,
                ImgUrl = User.ProfileImageUrl,
                ImgCoverUrl = User.CoverImageUrl,
                Address = User.Address,
            };
        }
    }
}
