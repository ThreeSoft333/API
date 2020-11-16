using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestClient.Net;
using RestClient.Net.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1;
using ThreeSoftECommAPI.Contracts.V1.Requests.Identity;
using ThreeSoftECommAPI.Contracts.V1.Responses.Identity;
using ThreeSoftECommAPI.Domain.Identity;
using ThreeSoftECommAPI.Services.EComm.UserNotifCountServ;
using ThreeSoftECommAPI.Services.Identity;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace ThreeSoftECommAPI.Controllers.V1
{
    public class IdentityController : Controller
    {
        private readonly IIdentityService _identityService;
        private readonly IConfiguration _configuration;
        private readonly IUserNotificationCountService _userNotificationCountService;
        public IdentityController(IIdentityService identityService, IConfiguration configuration,
            IUserNotificationCountService userNotificationCountService)
        {
            _identityService = identityService;
            _configuration = configuration;
            _userNotificationCountService = userNotificationCountService;
        }

        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new AuthFailedResponse
                    {
                        message = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage)).FirstOrDefault(),
                        status = BadRequest().StatusCode
                    });
                }

                var authResponse = await _identityService.RegisterAsync(request.Email, request.MobileNo, request.Password);

                if (!authResponse.Success)
                {
                    return BadRequest(new AuthFailedResponse
                    {
                        message = authResponse.Errors,
                        status = BadRequest().StatusCode
                    });
                }

                _userNotificationCountService.Create(authResponse.UserId);

                var appUser = await _identityService.GetUserById(authResponse.UserId);

                await _identityService.AddUserRole(appUser, "Customer");

                var PhoneNumberConfirmed = _identityService.GeneratePhoneNumberConfirmedToken(appUser);
                SendMessage(PhoneNumberConfirmed.Result);

                return Ok(new AuthSuccessResponse
                {
                    Token = authResponse.Token,
                    UserId = authResponse.UserId,
                    UserName = authResponse.UserName,
                    Email = authResponse.Email,
                    Address = authResponse.Address,
                    ImgUrl = authResponse.ImgUrl,
                    ImgCoverUrl = authResponse.ImgCoverUrl,
                    Role = "Customer"
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new AuthFailedResponse
                {
                    message = ex.Message,
                    status = BadRequest().StatusCode
                });
            }
        }

        [HttpPost(ApiRoutes.Identity.RegisterWeb)]
        public async Task<IActionResult> RegisterWeb([FromBody] UserRegistrationWebRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    message = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage)).FirstOrDefault(),
                    status = BadRequest().StatusCode
                });
            }

            var authResponse = await _identityService.RegisterWebAsync(request.fullName, request.userName, request.Email,request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    message = authResponse.Errors,
                    status = BadRequest().StatusCode
                });
            }

            var appUser = await _identityService.GetUserById(authResponse.UserId);

            string UserRole = "";
            switch (request.Role)
            {
                case 1:
                    await _identityService.AddUserRole(appUser, "Owner");
                    UserRole = "Owner";
                    break;
                default:
                    await _identityService.AddUserRole(appUser, "Admin");
                    UserRole = "Admin";
                    break;
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                UserId = authResponse.UserId,
                UserName = authResponse.UserName,
                FullName = authResponse.FullName,
                Email = authResponse.Email,
                Role = UserRole
            });
        }

        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var authResponse = await _identityService.LoginAsync(request.Email, request.MobileNo, request.Password);
            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    message = authResponse.Errors,
                    status = BadRequest().StatusCode
                });
            }

            var appUser = await _identityService.GetUserById(authResponse.UserId);

            if (!appUser.PhoneNumberConfirmed)
            {
                var Token = _identityService.GeneratePhoneNumberConfirmedToken(appUser);
                SendMessage(Token.Result);
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                UserId = authResponse.UserId,
                UserName = authResponse.UserName,
                FullName = authResponse.FullName,
                DateOfBirth = authResponse.DateOfBirth,
                Email = authResponse.Email,
                Address = authResponse.Address,
                ImgUrl = authResponse.ImgUrl,
                ImgCoverUrl = authResponse.ImgCoverUrl,
                Role = await _identityService.GetUserRole(appUser),
                City = authResponse.City,
                PhoneNumberConfirmed = authResponse.PhoneNumberConfirmed
            });
        }

        [HttpPost(ApiRoutes.Identity.LoginWeb)]
        public async Task<IActionResult> LoginWeb([FromBody] UserLoginWebRequest request)
        {
            var authResponse = await _identityService.LoginWebAsync(request.userName, request.Password);
            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    message = authResponse.Errors,
                    status = BadRequest().StatusCode
                });
            }

            var appUser = await _identityService.GetUserById(authResponse.UserId);

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                UserId = authResponse.UserId,
                UserName = authResponse.UserName,
                FullName = authResponse.FullName,
                Email = authResponse.Email,
                Role = await _identityService.GetUserRole(appUser)
            });
        }

        [HttpPost(ApiRoutes.Identity.ChangePassword)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var ChangePassRes = await _identityService.ChangePasswordAsync(request.PhoneNumber, request.CurrentPassword, request.NewPassword);

            if (!ChangePassRes.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    message = ChangePassRes.Errors,
                    status = BadRequest().StatusCode
                });
            }

            return Ok(new ChangePasswordResponse
            {
                status = Ok().StatusCode,
                message = "Password Changed Successfully"
            });
        }

        [HttpPost(ApiRoutes.Identity.ResetPassword)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var token = await _identityService.GeneratePasswordResetToken(request.PhoneNumber);

            if (token != null)
            {
                string NewPassword = GeneratePassword();

                var ResetPass = await _identityService.ResetPasswordAsync(request.PhoneNumber, token, NewPassword);

                if (ResetPass == 1)
                {
                    SendMessage(NewPassword);
                    return Ok(new
                    {
                        message = "Password Changed Successfully",
                        status = Ok().StatusCode
                    });
                }
            }
            return BadRequest(new AuthFailedResponse
            {
                message = "Invalid User",
                status = BadRequest().StatusCode
            });




        }

        [HttpPost(ApiRoutes.Identity.ConfirmPhone)]
        public async Task<IActionResult> ConfirmPhone([FromBody] ConfirmPhoneRequest request)
        {
            var ConfPhone = await _identityService.ConfirmPhone(request.UserId, request.Token);

            if (!ConfPhone.Success)
            {

                return BadRequest(new AuthFailedResponse
                {
                    message = ConfPhone.Errors,
                    status = BadRequest().StatusCode
                });
            }

            return Ok(new ChangePasswordResponse
            {
                status = Ok().StatusCode,
                message = "Phone Confirmed"
            });
        }

        [HttpPost(ApiRoutes.Identity.Update)]
        public async Task<IActionResult> Update([FromBody] UserUpdateRequest request)
        {
            var User = await _identityService.GetUserById(request.Id);

            if (User != null)
            {
                User.Address = request.Address;
                User.FullName = request.FullName;
                User.Email = request.Email;
                User.DateOfBirth = request.DateOfBirth;
                User.CoverImageUrl = request.CoverImageUrl;
                User.ProfileImageUrl = request.ProfileImageUrl;
                User.City = request.City;

                var UpUser = await _identityService.UpdateUserInfoAsync(User);

                if (!UpUser.Success)
                {
                    return BadRequest(new AuthFailedResponse
                    {
                        message = UpUser.Errors,
                        status = BadRequest().StatusCode
                    });
                }
            }
            else
            {
                return NotFound(new ChangePasswordResponse
                {
                    status = NotFound().StatusCode,
                    message = "User does not exist"
                });
            }

            return Ok(new ChangePasswordResponse
            {
                status = Ok().StatusCode,
                message = "User Info Updated Successfully"
            });
        }

        [HttpPost(ApiRoutes.Identity.UpdateFcmRegToken)]
        public async Task<IActionResult> UpdateToken([FromBody] UpdateFcmRegToken request)
        {
            var User = await _identityService.GetUserById(request.UserId);

            if (User != null)
            {
                User.FcmRegToken = request.FcmRegToken;

                var UpUser = await _identityService.UpdateUserInfoAsync(User);

                if (!UpUser.Success)
                {
                    return BadRequest(new AuthFailedResponse
                    {
                        message = UpUser.Errors,
                        status = BadRequest().StatusCode
                    });
                }
            }
            else
            {
                return NotFound(new ChangePasswordResponse
                {
                    status = NotFound().StatusCode,
                    message = "User does not exist"
                });
            }

            return Ok(new ChangePasswordResponse
            {
                status = Ok().StatusCode,
                message = "User Info Updated Successfully"
            });
        }

        [HttpPost(ApiRoutes.Identity.Upload), DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            string folderPath = "wwwroot/Resources/Images/UserImage/";
            bool exists = Directory.Exists(folderPath);

            if (!exists)
                Directory.CreateDirectory(folderPath);

            string ApiPath = "http://husamalraie-001-site3.gtempurl.com/";
            var ProfileImage = Request.Form.Files["profile_image"];
            var CoverImage = Request.Form.Files["cover_image"];

            var folderName = Path.Combine("wwwroot/Resources/Images/UserImage/");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            var dbPath_ProfileImage = "";
            var dbPath_CoverImage = "";


            if (ProfileImage.Length > 0)
            {
                var fileName = DateTime.Now.Ticks + "_" + ContentDispositionHeaderValue.Parse(ProfileImage.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                dbPath_ProfileImage = Path.Combine(folderName, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await ProfileImage.CopyToAsync(stream);
                }
            }

            if (CoverImage.Length > 0)
            {
                var fileName = DateTime.Now.Ticks + "_" + ContentDispositionHeaderValue.Parse(CoverImage.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                dbPath_CoverImage = Path.Combine(folderName, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await CoverImage.CopyToAsync(stream);
                }
            }

            return Ok(new
            {
                ProfileImage_Path = ApiPath + dbPath_ProfileImage,
                CoverImagePath = ApiPath + dbPath_CoverImage
            });
        }
        public IActionResult SendMessage(string Token)
        {
            string AccountId = _configuration.GetSection("accountId").Value;
            string AuthToken = _configuration.GetValue<string>("authToken");

            TwilioClient.Init(AccountId, AuthToken);

            var message = MessageResource.Create(
                body: Token,
                from: new Twilio.Types.PhoneNumber("+13344876852"),
                to: new Twilio.Types.PhoneNumber("+962788966075")
                );

            return Ok(new
            {
                sid = message.Sid,
                status = message.Status,
                message = message.ErrorMessage,
                price = message.Price,
                priceUnit = message.PriceUnit
            });
        }

        [HttpPost(ApiRoutes.Identity.ResendPhoneToken)]
        public async Task<IActionResult> ResendPhoneToken([FromRoute] string userId)
        {
            try
            {
                var appUser = await _identityService.GetUserById(userId);

                if (appUser != null)
                {
                    var Token = _identityService.GeneratePhoneNumberConfirmedToken(appUser);
                    SendMessage(Token.Result);

                    return Ok(new
                    {
                        status = Ok().StatusCode,
                        message = "Token Resend successfully",
                    });
                }

                return NotFound(new
                {
                    status = NotFound().StatusCode,
                    message = "Not Found User",
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = BadRequest().StatusCode,
                    message = ex.Message,
                });
            }
        }

        public string GeneratePassword()
        {
            int r, k;
            int passwordLength = 8;
            string password = "";
            char[] upperCase = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            char[] lowerCase = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            int[] numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            char[] spichal = { '@', '!', '£', '$', '%', '&' };
            Random rRandom = new Random();



            //for (int i = 0; i < passwordLength; i++)
            //{
            // r = rRandom.Next(4);

            //if (r == 0)
            //{
            k = rRandom.Next(0, 25);
            password += upperCase[k];
            // }

            //else if (r == 1)
            //{
            k = rRandom.Next(0, 25);
            password += lowerCase[k];
            // }

            //else if (r == 2)
            //{
            k = rRandom.Next(0, 9);
            password += numbers[k];

            k = rRandom.Next(0, 25);
            password += upperCase[k];
            //}
            //else if (r == 3)
            //{
            k = rRandom.Next(0, 5);
            password += spichal[k];
            //}
            k = rRandom.Next(0, 25);
            password += lowerCase[k];

            k = rRandom.Next(0, 9);
            password += numbers[k];

            k = rRandom.Next(0, 25);
            password += upperCase[k];

            //}

            return password;
        }

        [HttpGet(ApiRoutes.Identity.CustomerList)]
        public async Task<IActionResult> CustomerList()
        {
            var CustomerList = await _identityService.customerList();
            return Ok(CustomerList);
        }
    }
}
