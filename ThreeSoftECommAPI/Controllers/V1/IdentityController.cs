using Microsoft.AspNetCore.Mvc;
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
using ThreeSoftECommAPI.Services.Identity;

namespace ThreeSoftECommAPI.Controllers.V1
{
    public class IdentityController: Controller
    {
        private readonly IIdentityService _identityService;
        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
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

            var appUser = await _identityService.GetUserById(authResponse.UserId);
            string UserRole = "";
            switch (request.Role)
            {
                case 1:
                    await _identityService.AddUserRole(appUser, "Owner");
                    UserRole = "Owner";
                    break;
                case 2:
                    await _identityService.AddUserRole(appUser, "Admin");
                    UserRole = "Admin";
                    break;
                default:
                    await _identityService.AddUserRole(appUser, "Customer");
                    UserRole = "Customer";
                    break;
            }
           
            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                UserId = authResponse.UserId,
                UserName = authResponse.UserName,
                Email = authResponse.Email,
                Address = authResponse.Address,
                ImgUrl = authResponse.ImgUrl,
                ImgCoverUrl = authResponse.ImgCoverUrl,
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

        [HttpPost(ApiRoutes.Identity.Update)]
        public async Task<IActionResult> Update([FromBody] UserUpdateRequest request)
        {
            var User = await _identityService.GetUserById(request.Id);

            if(User != null)
            {
                User.Address = request.Address;
                User.FullName = request.FullName;
                User.Email = request.Email;
                User.DateOfBirth = request.DateOfBirth;
                User.CoverImageUrl = request.CoverImageUrl;
                User.ProfileImageUrl = request.ProfileImageUrl;

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
            string ApiPath = "http://husamalraie-001-site3.gtempurl.com/";
            var ProfileImage = Request.Form.Files["profile_image"];
            var CoverImage = Request.Form.Files["cover_image"];

            var folderName = Path.Combine("Resources/Images/UserImage/");
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
                ProfileImage_Path = ApiPath+ dbPath_ProfileImage,
                CoverImagePath = ApiPath+ dbPath_CoverImage
            });
        }
    }
}
