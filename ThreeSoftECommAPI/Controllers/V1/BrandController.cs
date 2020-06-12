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
                Name = brandRequest.Name,
                ImgUrl = brandRequest.ImgUrl
            };

            await _brandService.CreateBrandAsync(brand);

            var response = new BrandResponse { Id = brand.Id };

            return Created("", response);
        }

        [HttpPost(ApiRoutes.Brands.Update)]
        public async Task<IActionResult> Update([FromRoute] Int32 brandId, [FromBody] CreateBrandRequest brandRequest)
        {
            var brand = new Brand
            {
                Id = brandId,
                Name = brandRequest.Name,
                ImgUrl = brandRequest.ImgUrl
            };

            var Updated = await _brandService.UpdateBrandAsync(brand);

            if (Updated)
                return Ok(brand);
            return NotFound();
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
