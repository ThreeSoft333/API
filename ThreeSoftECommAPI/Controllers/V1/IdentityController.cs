using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1;
using ThreeSoftECommAPI.Contracts.V1.Requests.Identity;
using ThreeSoftECommAPI.Contracts.V1.Responses.Identity;
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

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                UserId = authResponse.UserId,
                UserName = authResponse.UserName,
                Email = authResponse.Email,
                Address = authResponse.Address,
                ImgUrl = authResponse.ImgUrl,
                ImgCoverUrl = authResponse.ImgCoverUrl
            });
        }

        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] UserRegistrationRequest request)
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

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                UserId = authResponse.UserId,
                UserName = authResponse.UserName,
                Email = authResponse.Email,
                Address = authResponse.Address,
                ImgUrl = authResponse.ImgUrl,
                ImgCoverUrl = authResponse.ImgCoverUrl
            });
        }
    }
}
