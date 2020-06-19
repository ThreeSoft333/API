using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1;
using ThreeSoftECommAPI.Contracts.V1.Requests.EComm.BrandReq;
using ThreeSoftECommAPI.Contracts.V1.Responses.EComm;
using ThreeSoftECommAPI.Domain.EComm;
using ThreeSoftECommAPI.Services.EComm.BrandServ;

namespace ThreeSoftECommAPI.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet(ApiRoutes.Brands.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _brandService.GetBrandsAsync());
        }

        [HttpGet(ApiRoutes.Brands.Get)]
        public async Task<IActionResult> Get([FromRoute] Int32 brandId)
        {
            var brand = await _brandService.GetBrandByIdAsync(brandId);

            if (brand == null)
                return NotFound();
            return Ok(brand);
        }

        [HttpPost(ApiRoutes.Brands.Create)]
        public async Task<IActionResult> Create([FromBody] CreateBrandRequest brandRequest)
        {
            var brand = new Brand
            {
                ArabicName = brandRequest.ArabicName,
                EnglishName = brandRequest.EnglishName,
                ImgUrl = brandRequest.ImgUrl
            };

           var status= await _brandService.CreateBrandAsync(brand);

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
                var response = new BrandResponse { Id = brand.Id };
                return Ok(response);
            }
            return NotFound(new ErrorResponse
            {
                message = "Not Found",
                status = NotFound().StatusCode
            });
        }

        [HttpPost(ApiRoutes.Brands.Update)]
        public async Task<IActionResult> Update([FromRoute] Int32 brandId, [FromBody] CreateBrandRequest brandRequest)
        {
            var brand = new Brand
            {
                Id = brandId,
                ArabicName = brandRequest.ArabicName,
                EnglishName = brandRequest.EnglishName,
                ImgUrl = brandRequest.ImgUrl
            };

            var status = await _brandService.UpdateBrandAsync(brand);

            if (status == -1)
            {
                return Conflict(new ErrorResponse
                {
                    message = "Dublicate Entry",
                    status = Conflict().StatusCode
                });
            }

            if (status == 1)
                return Ok(brand);

            return NotFound(new ErrorResponse
            {
                message = "Not Found",
                status = NotFound().StatusCode
            });
        }

        [HttpDelete(ApiRoutes.Brands.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Int32 brandId)
        {
            var deleted = await _brandService.DeleteBrandAsync(brandId);

            if (deleted)
                return NoContent();
            return NotFound();
        }
    }
}
