using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Schema;
using ThreeSoftECommAPI.Contracts.V1;
using ThreeSoftECommAPI.Contracts.V1.Requests.EComm.CatgReq;
using ThreeSoftECommAPI.Contracts.V1.Responses;
using ThreeSoftECommAPI.Contracts.V1.Responses.EComm;
using ThreeSoftECommAPI.Domain.EComm;
using ThreeSoftECommAPI.Services.EComm.CategoryServ;

namespace ThreeSoftECommAPI.Controllers.V1
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoryController:Controller
    {
        private readonly ICategoryService _CategoryService;
        public CategoryController(ICategoryService CategoryService)
        {
            _CategoryService = CategoryService;
        }

        [HttpGet(ApiRoutes.Category.GetAll)]
        public async Task<IActionResult> GetAll([FromQuery] int status)
        {
            return Ok(await _CategoryService.GetCategoriesAsync(status));
        }

        [HttpGet(ApiRoutes.Category.GetCatgPag)]
        public IActionResult GetAll([FromQuery] int status, [FromQuery] Pagination pagination)
        {
            var category = _CategoryService.GetCategoriesAsync(status, pagination);
            return Ok(new
            {
                Category = category,
                PageSize = category.PageSize,
                TotalCount = category.TotalCount,
                TotalPage = category.TotalPage,
                HasNext = category.HasNext,
                HasPrevious = category.HasPrevious,
                CurrentPage = category.CurrentPage
            });
        }

        [HttpGet(ApiRoutes.Category.GetTob)]
        public async Task<IActionResult> GetTop()
        {
            

           

            return Ok(await _CategoryService.GetCategoriesTopAsync());
        }

        [HttpGet(ApiRoutes.Category.Get)]
        public async Task<IActionResult> Get([FromRoute] Int32 CatgId)
        {
            var Catg = await _CategoryService.GetCategoryByIdAsync(CatgId);

            if (Catg == null)
                return NotFound(new ErrorResponse
                {
                    message = "Not Found",
                    status = NotFound().StatusCode
                });

            return Ok(Catg);
        }

        [HttpPost(ApiRoutes.Category.Create)]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequest CategoryRequst)
        {
            var category = new Category
            {
                ArabicName = CategoryRequst.ArabicName,
                EnglishName = CategoryRequst.EnglishName,
                ImageUrl = CategoryRequst.ImageUrl,
                Status = CategoryRequst.Status
            };

          var status = await _CategoryService.CreateCategoryAsync(category);

            if(status == -1)
            {
                return Conflict(new ErrorResponse
                {
                    message = "Dublicate Entry",
                    status = Conflict().StatusCode
                });
            }

            if (status == 1)
            {
                var response = new CategoryResponse { Id = category.Id };
                return Ok(response);
            }
            return NotFound(new ErrorResponse
            {
                message = "Not Found",
                status = NotFound().StatusCode
            });
        }

        [HttpPost(ApiRoutes.Category.Update)]
        public async Task<IActionResult> Update([FromRoute] Int32 catgId, [FromBody] UpdateCategoryRequest CategoryRequst)
        {
            var category = new Category
            {
                Id = catgId,
                ArabicName = CategoryRequst.ArabicName,
                EnglishName = CategoryRequst.EnglishName,
                ImageUrl = CategoryRequst.ImageUrl,
                Status = CategoryRequst.Status
            };

            var status = await _CategoryService.UpdateCategoryAsync(category);

            if (status == -1)
            {
                return Conflict(new ErrorResponse
                {
                    message = "Dublicate Entry",
                    status = Conflict().StatusCode
                });
            }

            if (status == 1)
                return Ok(category);
            return NotFound(new ErrorResponse
            {
                message = "Not Found",
                status = NotFound().StatusCode
            });
        }

        [HttpDelete(ApiRoutes.Category.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Int32 CatgId)
        {
            var deleted = await _CategoryService.DeleteCategoryAsync(CatgId);

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

        [HttpPost(ApiRoutes.Category.Upload), DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            string folderPath = "wwwroot/Resources/Images/CategoryImg/";
            bool exists = Directory.Exists(folderPath);

            if (!exists)
                Directory.CreateDirectory(folderPath);

            var file = Request.Form.Files[0];
            var folderName = Path.Combine(folderPath );
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
