using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1;
using ThreeSoftECommAPI.Contracts.V1.Requests.EComm.ProductColorReq;
using ThreeSoftECommAPI.Contracts.V1.Responses.EComm;
using ThreeSoftECommAPI.Domain.EComm;
using ThreeSoftECommAPI.Services.EComm.ProductColorServ;

namespace ThreeSoftECommAPI.Controllers.V1
{
    public class ProductColorController:Controller
    {
        private readonly IProductColorService _productColorService;
        public ProductColorController(IProductColorService productColorService)
        {
            _productColorService = productColorService;
        }

        [HttpGet(ApiRoutes.Product_Colors.GetAll)]
        public async Task<IActionResult> GetAll([FromRoute] Int64 productId, [FromQuery] int status)
        {
            return Ok(await _productColorService.GetProductColorsAsync(productId,status));
        }

        [HttpGet(ApiRoutes.Product_Colors.Get)]
        public async Task<IActionResult> Get([FromRoute] Int32 id)
        {
            var ProdColors = await _productColorService.GetProductColorsByIdAsync(id);

            if (ProdColors == null)
                return NotFound(new ErrorResponse
                {
                    message = "Not Found",
                    status = NotFound().StatusCode
                });

            return Ok(ProdColors);
        }

        [HttpPost(ApiRoutes.Product_Colors.Create)]
        public async Task<IActionResult> Create([FromBody] CreateProductColorRequest productColorRequest)
        {
            var prodColor = new ProductColors
            {
                ProductId = productColorRequest.ProductId,
                ArabicName = productColorRequest.ArabicName,
                EnglishName = productColorRequest.EnglishName,
                Status = productColorRequest.Status,
                CreatedAt = DateTime.Now
            };

            var status = await _productColorService.CreateProductColorsAsync(prodColor);

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
                var response = new ProductColorResponse { Id = prodColor.Id };
                return Ok(response);
            }
            return NotFound(new ErrorResponse
            {
                message = "Not Found",
                status = NotFound().StatusCode
            });
        }

        [HttpPost(ApiRoutes.Product_Colors.Update)]
        public async Task<IActionResult> Update([FromRoute] Int64 id, [FromBody] UpdateProductColorRequest productColorRequest)
        {
            var prodColor = new ProductColors
            {
                Id = id,
                ProductId = productColorRequest.ProductId,
                ArabicName = productColorRequest.ArabicName,
                EnglishName = productColorRequest.EnglishName,
                Status = productColorRequest.Status,
                CreatedAt = DateTime.Now
            };

            var status = await _productColorService.UpdateProductColorsAsync(prodColor);

            if (status == -1)
            {
                return Conflict(new ErrorResponse
                {
                    message = "Dublicate Entry",
                    status = Conflict().StatusCode

                });
            }

            if (status == 1)
                return Ok(prodColor);
            return NotFound(new ErrorResponse
            {
                message = "Not Found",
                status = NotFound().StatusCode
            });
        }

        [HttpDelete(ApiRoutes.Product_Colors.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Int64 id)
        {
            var deleted = await _productColorService.DeleteProductColorsAsync(id);

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
