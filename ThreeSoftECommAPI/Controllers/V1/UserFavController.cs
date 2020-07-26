using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1;
using ThreeSoftECommAPI.Contracts.V1.Requests.EComm.UserFavReq;
using ThreeSoftECommAPI.Contracts.V1.Responses.EComm;
using ThreeSoftECommAPI.Domain.EComm;
using ThreeSoftECommAPI.Services.EComm.UserFavouritesServ;

namespace ThreeSoftECommAPI.Controllers.V1
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserFavController:Controller
    {
        private readonly IUserFavService _userFavService;

        public UserFavController(IUserFavService userFavService)
        {
            _userFavService = userFavService;
        }

        [HttpPost(ApiRoutes.UserFav.Create)]
        public async Task<IActionResult> Create([FromBody] UserFavRequest userFavRequest)
        {
            var checkFav = await _userFavService.GetAsync(userFavRequest.UserId, userFavRequest.ProductId);
            if (checkFav == null)
            {
                var UserFav = new UserFavourites
                {
                    UserId = userFavRequest.UserId,
                    ProductId = userFavRequest.ProductId
                };

                var status = await _userFavService.CreateUserFavAsync(UserFav);

                if (status == 1)
                {
                    var response = new UserFavResponse { Id = UserFav.Id };
                    return Ok(response);
                }
                return NotFound(new ErrorResponse
                {
                    message = "Not Found",
                    status = NotFound().StatusCode
                });
            }
            return Conflict(new ErrorResponse
            {
                message = "Dublicate Entry",
                status = Conflict().StatusCode
            });
        }

        [HttpDelete(ApiRoutes.UserFav.Delete)]
        public async Task<IActionResult> Delete([FromRoute] string userId, [FromRoute] Int64 productId)
        {
            var deleted = await _userFavService.DeleteUserFavAsync(userId, productId);

            if (deleted)
                return Ok(new SuccessResponse
                {
                    message = "Successfully Deleted",
                    status = Ok().StatusCode
                });
            return NotFound(new ErrorResponse
            {
                message = "Not Found",
                status = NotFound().StatusCode
            });
        }
    }
}
