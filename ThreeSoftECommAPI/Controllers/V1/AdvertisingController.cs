using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1;
using ThreeSoftECommAPI.Contracts.V1.Requests.EComm.AdvertisingReq;
using ThreeSoftECommAPI.Contracts.V1.Responses.EComm;
using ThreeSoftECommAPI.Domain.EComm;
using ThreeSoftECommAPI.Services.EComm.AdvertisingServ;

namespace ThreeSoftECommAPI.Controllers.V1
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AdvertisingController:Controller
    {
        private readonly IAdvertisingService _advertisingService;

        public AdvertisingController(IAdvertisingService advertisingService)
        {
            _advertisingService = advertisingService;
        }

        [HttpGet(ApiRoutes.Advertise.GetAll)]
        public async Task<IActionResult> GetAll([FromQuery] int status)
        {
            return Ok(await _advertisingService.GetAdvertisingAsync(status));
        }

        [HttpGet(ApiRoutes.Advertise.Get)]
        public async Task<IActionResult> Get([FromRoute] Int32 AdvertizeId)
        {
            var advertize = await _advertisingService.GetAdvertisingByIdAsync(AdvertizeId);

            if (advertize == null)
                return NotFound(new ErrorResponse
                {
                    message = "Not Found",
                    status = NotFound().StatusCode
                });
                 
            return Ok(advertize);
        }

        [HttpPost(ApiRoutes.Advertise.Create)]
        public async Task<IActionResult> Create([FromBody] CreateAdvertisingRequst advertisingRequst)
        {
            var advertize = new Advertising
            {
                ArabicDescription = advertisingRequst.ArabicDescription,
                EnglishDescription = advertisingRequst.EnglishDescription,
                ImgUrl = advertisingRequst.ImgUrl,
                Status = advertisingRequst.Status
            };

           var status = await _advertisingService.CreateAdvertisingAsync(advertize);

            if (status == -1)
            {
                return Conflict(new ErrorResponse
                {
                    message = "Dublicate Entry",
                    status = Conflict().StatusCode
                });
            }

            if (status == 1)
            {
                var response = new BrandResponse { Id = advertize.Id };
                return Ok(response);
            }
            return NotFound(new ErrorResponse
            {
                message = "Not Found",
                status = NotFound().StatusCode
            });
        }

        [HttpPost(ApiRoutes.Advertise.Update)]
        public async Task<IActionResult> Update([FromRoute] Int32 AdvertiseId, [FromBody] UpdateAdvertisingRequest advertisingRequst)
        {
            var advertise = new Advertising
            {
                Id = AdvertiseId,
                ArabicDescription = advertisingRequst.ArabicDescription,
                EnglishDescription = advertisingRequst.EnglishDescription,
                ImgUrl = advertisingRequst.ImgUrl,
                Status = advertisingRequst.Status
            };

            var status = await _advertisingService.UpdateAdvertisingAsync(advertise);

            if (status == -1)
            {
                return Conflict(new ErrorResponse
                {
                    message = "Dublicate Entry",
                    status = Conflict().StatusCode
                });
            }

            if (status == 1)
                return Ok(advertise);

            return NotFound(new ErrorResponse
            {
                message = "Not Found",
                status = NotFound().StatusCode
            });
        }

        [HttpDelete(ApiRoutes.Advertise.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Int32 advertiseId)
        {
            var deleted = await _advertisingService.DeleteAdvertisingAsync(advertiseId);

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

        [HttpPost(ApiRoutes.Advertise.Upload), DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            string folderPath = "wwwroot/Resources/Images/Advertizing/";
            bool exists = Directory.Exists(folderPath);

            if (!exists)
                Directory.CreateDirectory(folderPath);

            var file = Request.Form.Files[0];
            var folderName = Path.Combine(folderPath);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (file.Length > 0)
            {
                var fileName = DateTime.Now.Ticks + "_" + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return Ok(new { dbPath });
            }
            return BadRequest();
        }
    }
}
