using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1;
using ThreeSoftECommAPI.Contracts.V1.Requests.EComm.ProductSizeReq;
using ThreeSoftECommAPI.Contracts.V1.Responses.EComm;
using ThreeSoftECommAPI.Domain.EComm;
using ThreeSoftECommAPI.Services.EComm.ProductSizeServ;

namespace ThreeSoftECommAPI.Controllers.V1
{
    public class ProductSizeController:Controller
    {
        private readonly IProductSizeService _productSizeService;
        public ProductSizeController(IProductSizeService productSizeService)
        {
            _productSizeService = productSizeService;
        }

        [HttpGet(ApiRoutes.Product_Sizes.GetAll)]
        public async Task<IActionResult> GetAll([FromRoute] Int64 productId, [FromQuery] int status)
        {
            return Ok(await _productSizeService.GetProductSizeAsync(productId, status));
        }

        [HttpGet(ApiRoutes.Product_Sizes.Get)]
        public async Task<IActionResult> Get([FromRoute] Int32 id)
        {
            var ProdSizes = await _productSizeService.GetProductSizeByIdAsync(id);

            if (ProdSizes == null)
                return NotFound(new ErrorResponse
                {
                    message = "Not Found",
                    status = NotFound().StatusCode
                });

            return Ok(ProdSizes);
        }

        [HttpPost(ApiRoutes.Product_Sizes.Create)]
        public async Task<IActionResult> Create([FromBody] CreateProductSizeRequest productSizeRequest)
        {
            var prodSize = new ProductSize
            {
                ProductId = productSizeRequest.ProductId,
                Size = productSizeRequest.Size,
                Unit = productSizeRequest.Unit,
                Status = productSizeRequest.Status
            };

            var status = await _productSizeService.CreateProductSizeAsync(prodSize);

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
                var response = new ProductSizeResponse { Id = prodSize.Id };
                return Ok(response);
            }
            return NotFound(new ErrorResponse
            {
                message = "Not Found",
                status = NotFound().StatusCode
            });
        }

        [HttpPost(ApiRoutes.Product_Sizes.Update)]
        public async Task<IActionResult> Update([FromRoute] Int64 id, [FromBody] UpdateProductSizeRequest productSizeRequest)
        {
            var prodSize = new ProductSize
            {
                Id = id,
                ProductId = productSizeRequest.ProductId,
                Size = productSizeRequest.Size,
                Unit = productSizeRequest.Unit,
                Status = productSizeRequest.Status
            };

            var status = await _productSizeService.UpdateProductSizeAsync(prodSize);

            if (status == -1)
            {
                return Conflict(new ErrorResponse
                {
                    message = "Dublicate Entry",
                    status = Conflict().StatusCode

                });
            }

            if (status == 1)
                return Ok(prodSize);
            return NotFound(new ErrorResponse
            {
                message = "Not Found",
                status = NotFound().StatusCode
            });
        }

        [HttpDelete(ApiRoutes.Product_Sizes.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Int64 id)
        {
            var deleted = await _productSizeService.DeleteProductSizeAsync(id);

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
