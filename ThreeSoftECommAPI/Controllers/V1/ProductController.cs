using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
                CreatedAt = productRequest.CreatedAt,
                CreatedBy = productRequest.CreatedBy,
                ImgUrl = productRequest.ImgUrl
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
                UpdatedAt = productRequest.UpdatedAt,
                UpdatedBy = productRequest.UpdatedBy,
                ImgUrl = productRequest.ImgUrl
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
    }
}
