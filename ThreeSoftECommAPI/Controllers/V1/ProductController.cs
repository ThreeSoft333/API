﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1;
using ThreeSoftECommAPI.Contracts.V1.Requests.EComm.ProductReq;
using ThreeSoftECommAPI.Contracts.V1.Responses.EComm;
using ThreeSoftECommAPI.Domain.EComm;
using ThreeSoftECommAPI.Services.EComm.ProductServ;

namespace ThreeSoftECommAPI.Controllers.V1
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductController:Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet(ApiRoutes.Product.GetAll)]
        public async Task<IActionResult> GetAll([FromRoute] Int64 SubCatgId, [FromQuery] int status)
        {
            return Ok(await _productService.GetProductsAsync(SubCatgId, status));
        }

        [HttpGet(ApiRoutes.Product.GetAllforApp)]
        public async Task<IActionResult> GetProductsBySubCategoryAsync([FromRoute] Int64 SubCatgId,[FromQuery] string UserId)
        {
            return Ok(await _productService.GetProductsBySubCategoryAsync(UserId, SubCatgId));
        }

        [HttpGet(ApiRoutes.Product.ProductsMostRecent)]
        public async Task<IActionResult> GetProductsMostRecentAsync([FromQuery] string UserId,[FromQuery] int count)
        {
            return Ok(await _productService.GetProductsMostRecentAsync(UserId,count));
        }

        [HttpGet(ApiRoutes.Product.ProductMostWanted)]
        public async Task<IActionResult> GetProductsMostWantedAsync([FromQuery] string UserId, [FromQuery] int count)
        {
            return Ok(await _productService.GetProductsMostWantedAsync(UserId,count));
        }

        [HttpGet(ApiRoutes.Product.GetProductTopRated)]
        public async Task<IActionResult> GetProductsTopRatedAsync([FromQuery] string UserId, [FromQuery] int count)
        {
            return Ok(await _productService.GetProductsTopRatedAsync(UserId,count));
        }


        [HttpGet(ApiRoutes.Product.GetUserFav)]
        public async Task<IActionResult> GetUserFav([FromRoute] string UserId)
        {
            return Ok(await _productService.GetProductsUserFavAsync(UserId));
        }

        [HttpGet(ApiRoutes.Product.ViewProduct)]
        public async Task<IActionResult> ViewProduct([FromRoute] Int64 productId)
        {
            var Product = await _productService.GetProductByIdAsync(productId);

            if (Product == null)
                return NotFound(new ErrorResponse
                {
                    message = "Not Found",
                    status = NotFound().StatusCode
                });

            return Ok(await _productService.ViewProductAsync(productId));
        }

        [HttpGet(ApiRoutes.Product.Get)]
        public async Task<IActionResult> Get([FromRoute] Int64 productId)
        {
            var Product = await _productService.GetProductByIdAsync(productId);

            if (Product == null)
                return NotFound(new ErrorResponse
                {
                    message = "Not Found",
                    status = NotFound().StatusCode
                });

            return Ok(Product);
        }

        [HttpPost(ApiRoutes.Product.Create)]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest productRequest)
        {
            var Product = new Product
            {
                SubCategoryId = productRequest.SubCategoryId,
                ArabicName = productRequest.ArabicName,
                EnglishName = productRequest.EnglishName,
                ArabicDescription = productRequest.ArabicDescription,
                EnglishDescription = productRequest.EnglishDescription,
                Price = productRequest.Price,
                SalePrice = productRequest.SalePrice,
                Quantity = productRequest.Quantity,
                Condition = productRequest.Condition,
                Material = productRequest.Material,
                status = productRequest.status,
                ImgUrl = productRequest.ImgUrl,
                colorId = productRequest.colorId,
                sizeId = productRequest.sizeId,
                CreatedAt = DateTime.Now
            };

            var status = await _productService.CreateProductAsync(Product);

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
                var response = new ProductResponse { Id = Product.Id };
                return Ok(response);
            }
            return NotFound(new ErrorResponse
            {
                message = "Not Found",
                status = NotFound().StatusCode
            });
        }

        [HttpPost(ApiRoutes.Product.Update)]
        public async Task<IActionResult> Update([FromRoute] Int64 productId, [FromBody] UpdateProductRequest productRequest)
        {
            var Product = new Product
            {
                Id = productId,
                SubCategoryId = productRequest.SubCategoryId,
                ArabicName = productRequest.ArabicName,
                EnglishName = productRequest.EnglishName,
                ArabicDescription = productRequest.ArabicDescription,
                EnglishDescription = productRequest.EnglishDescription,
                Price = productRequest.Price,
                SalePrice = productRequest.SalePrice,
                Quantity = productRequest.Quantity,
                Condition = productRequest.Condition,
                Material = productRequest.Material,
                status = productRequest.status,
                ImgUrl = productRequest.ImgUrl,
                UpdatedAt = DateTime.Now
            };

            var status = await _productService.UpdateProductAsync(Product);

            if (status == -1)
            {
                return Conflict(new ErrorResponse
                {
                    message = "Dublicate Entry",
                    status = Conflict().StatusCode

                });
            }

            if (status == 1)
                return Ok(Product);

            return NotFound(new ErrorResponse
            {
                message = "Not Found",
                status = NotFound().StatusCode
            });
        }

        [HttpDelete(ApiRoutes.Product.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Int64 productId)
        {
            var deleted = await _productService.DeleteProductAsync(productId);

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

        [HttpPost(ApiRoutes.Product.Upload), DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            var file = Request.Form.Files[0];
            var folderName = Path.Combine("Resources", "Images", "ProductImg");
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