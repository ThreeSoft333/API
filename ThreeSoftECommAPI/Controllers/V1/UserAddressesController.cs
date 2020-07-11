using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1;
using ThreeSoftECommAPI.Contracts.V1.Requests.EComm.UserAddressesReq;
using ThreeSoftECommAPI.Contracts.V1.Responses.EComm;
using ThreeSoftECommAPI.Domain.Identity;
using ThreeSoftECommAPI.Services.EComm.UserAddressesServ;

namespace ThreeSoftECommAPI.Controllers.V1
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserAddressesController:Controller
    {
        private readonly IUserAddressesService _addressesService;
        public UserAddressesController(IUserAddressesService addressesService)
        {
            _addressesService = addressesService;
        }

        [HttpGet(ApiRoutes.UserAddresse.GetAll)]
        public async Task<IActionResult> GetAll([FromRoute] string userId)
        {
            return Ok(await _addressesService.GetUserAddressesAsync(userId));
        }

        [HttpGet(ApiRoutes.UserAddresse.Get)]
        public async Task<IActionResult> Get([FromRoute] Int32 id)
        {
            var userAdd = await _addressesService.GetUserAddresseByIdAsync(id);

            if (userAdd == null)
                return NotFound(new ErrorResponse
                {
                    message = "Not Found",
                    status = NotFound().StatusCode
                });

            return Ok(userAdd);
        }

        [HttpPost(ApiRoutes.UserAddresse.Create)]
        public async Task<IActionResult> Create([FromBody] CreateUserAddresseRequest addresseRequest)
        {
            var Useraddresse = new UserAddresses
            {
                UserId = addresseRequest.UserId,
                Title = addresseRequest.Title,
                Country = addresseRequest.Country,
                City = addresseRequest.City,
                Province = addresseRequest.Province,
                Street = addresseRequest.Street,
                BuildingNo = addresseRequest.BuildingNo,
                PostCode = addresseRequest.PostCode,
                Lat = addresseRequest.Lat,
                Lon = addresseRequest.Lon,
                status = addresseRequest.status,
                CreateAt = DateTime.Now
            };

            var status = await _addressesService.CreateUserAddresseAsync(Useraddresse);

            if (status == 1)
            {
                var response = new
                {
                    Id = Useraddresse.Id,
                    message = "Successfully add",
                    status = Ok().StatusCode
                };
                return Ok(response);
            }
            return NotFound(new ErrorResponse
            {
                message = "Not Found",
                status = NotFound().StatusCode
            });
        }

        [HttpDelete(ApiRoutes.UserAddresse.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Int32 id)
        {
            var deleted = await _addressesService.DeleteUserAddresseAsync(id);

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
