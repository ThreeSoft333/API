using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1;
using ThreeSoftECommAPI.Contracts.V1.Requests.EComm.SubCatgReq;
using ThreeSoftECommAPI.Contracts.V1.Responses.EComm;
using ThreeSoftECommAPI.Domain.EComm;
using ThreeSoftECommAPI.Services.EComm.SubCategoryServ;

namespace ThreeSoftECommAPI.Controllers.V1
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SubCategoryController:Controller
    {
        private readonly ISubCategoryService _SubCategoryService;

        public SubCategoryController(ISubCategoryService SubCategoryService)
        {
            _SubCategoryService = SubCategoryService;
        }

        [HttpGet(ApiRoutes.SubCategory.GetAll)]
        public async Task<IActionResult> GetAll([FromRoute] Int32 catgId, [FromQuery] int status)
        {
            return Ok(await _SubCategoryService.GetSubCategoriesAsync(catgId,status));
        }

        [HttpGet(ApiRoutes.SubCategory.Get)]
        public async Task<IActionResult> Get([FromRoute] Int32 CatgId)
        {
            var SubCatg = await _SubCategoryService.GetSubCategoryByIdAsync(CatgId);

            if (SubCatg == null)
                return NotFound(new ErrorResponse
                {
                    message = "Not Found",
                    status = NotFound().StatusCode
                });

            return Ok(SubCatg);
        }

        [HttpPost(ApiRoutes.SubCategory.Create)]
        public async Task<IActionResult> Create([FromBody] CreateSubCategoryRequest SubCategoryRequst)
        {
            var SubCategory = new SubCategory
            {
                CategoryId = SubCategoryRequst.CategoryId,
                ArabicName = SubCategoryRequst.ArabicName,
                EnglishName = SubCategoryRequst.EnglishName,
                ImgUrl = SubCategoryRequst.ImgUrl,
                Status = SubCategoryRequst.Status
            };

          var status = await _SubCategoryService.CreateSubCategoryAsync(SubCategory);

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
                var response = new SubCategoryResponse { Id = SubCategory.Id };
                return Ok(response);
            }
            return NotFound(new ErrorResponse
            {
                message = "Not Found",
                status = NotFound().StatusCode
            });
        }

        [HttpPost(ApiRoutes.SubCategory.Update)]
        public async Task<IActionResult> Update([FromRoute] Int32 subCatgId, [FromBody] UpdateSubCategoryRequest SubCategoryRequst)
        {
            var SubCategory = new SubCategory
            {
                Id = subCatgId,
                ArabicName = SubCategoryRequst.ArabicName,
                EnglishName = SubCategoryRequst.EnglishName,
                ImgUrl = SubCategoryRequst.ImgUrl,
                Status = SubCategoryRequst.Status
            };

            var status = await _SubCategoryService.UpdateSubCategoryAsync(SubCategory);

            if (status == -1)
            {
                return Conflict(new ErrorResponse
                {
                    message = "Dublicate Entry",
                    status = Conflict().StatusCode

                });
            }

            if (status == 1)
                return Ok(SubCategory);

            return NotFound(new ErrorResponse
            {
                message = "Not Found",
                status = NotFound().StatusCode
            });
        }

        [HttpDelete(ApiRoutes.SubCategory.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Int32 SubCatgId)
        {
            var deleted = await _SubCategoryService.DeleteSubCategoryAsync(SubCatgId);

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
