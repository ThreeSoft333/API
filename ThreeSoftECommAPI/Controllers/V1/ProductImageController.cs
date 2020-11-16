using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1;
using ThreeSoftECommAPI.Contracts.V1.Requests.EComm.ProductImgReq;
using ThreeSoftECommAPI.Contracts.V1.Responses.EComm;
using ThreeSoftECommAPI.Domain.EComm;
using ThreeSoftECommAPI.Services.EComm.ProductImageServ;

namespace ThreeSoftECommAPI.Controllers.V1
{
    public class ProductImageController:Controller
    {
        private readonly IProductImagesService _productImagesService;

        public ProductImageController(IProductImagesService ProductImageService)
        {
            _productImagesService = ProductImageService;
        }

        [HttpGet(ApiRoutes.Product_Image.GetAll)]
        public async Task<IActionResult> GetAll([FromRoute] Int64 ProductId)
        {
            return Ok(await _productImagesService.GetProductImageAsync(ProductId));
        }

        [HttpGet(ApiRoutes.Product_Image.Get)]
        public async Task<IActionResult> Get([FromRoute] Int64 id)
        {
            var ProdImg = await _productImagesService.GetProductImageByIdAsync(id);

            if (ProdImg == null)
                return NotFound(new ErrorResponse
                {
                    message = "Not Found",
                    status = NotFound().StatusCode
                });

            return Ok(ProdImg);
        }

        [HttpPost(ApiRoutes.Product_Image.Create)]
        public async Task<IActionResult> Create([FromBody] CreateProductImgRequest productImgRequest)
        {
            var ProductImage = new ProductImage
            {
                ProductId = productImgRequest.ProductId,
                ImgUrl = productImgRequest.ImgUrl,
                Ext = productImgRequest.Ext,
                CreatedAt = DateTime.Now,
            };

            var status = await _productImagesService.CreateProductImageAsync(ProductImage);

            if (status == 1)
            {
                var response = new ProductImgResponse { Id = ProductImage.Id };
                return Ok(response);
            }
            return NotFound(new ErrorResponse
            {
                message = "Not Found",
                status = NotFound().StatusCode
            });
        }

        [HttpPost(ApiRoutes.Product_Image.Update)]
        public async Task<IActionResult> Update([FromRoute] Int64 id, [FromBody] UpdateProductImgRequest productImageRequst)
        {
            var ProductImage = new ProductImage
            {
                Id = id,
                ProductId = productImageRequst.ProductId,
                ImgUrl = productImageRequst.ImgUrl,
                Ext = productImageRequst.Ext,
                UpdatedAt = productImageRequst.UpdatedAt
            };

            var status = await _productImagesService.UpdateProductImageAsync(ProductImage);

            if (status == 1)
                return Ok(ProductImage);
            return NotFound(new ErrorResponse
            {
                message = "Not Found",
                status = NotFound().StatusCode
            });
        }

        [HttpDelete(ApiRoutes.Product_Image.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Int64 id)
        {
            var deleted = await _productImagesService.DeleteProductImageAsync(id);

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

        [HttpDelete(ApiRoutes.Product_Image.DeleteByProduct)]
        public async Task<IActionResult> DeleteByProduct([FromRoute] Int64 productId)
        {
            var deleted = await _productImagesService.DeleteProductImageByProductIdAsync(productId);

            if (deleted)
            {
                deleteImagesFromFolder(productId);
                return Ok(new SuccessResponse
                {
                    message = "Successfully Deleted",
                    status = Ok().StatusCode
                });
            }
                
            return NotFound(new ErrorResponse
            {
                message = "Not Found",
                status = NotFound().StatusCode
            });
        }

        public async void deleteImagesFromFolder(Int64 ProductId)
        {
            var images = await _productImagesService.GetProductImageAsync(ProductId);

            for (int i = 0; i < images.Count; i++)
            {
                string Path = images[i].ImgUrl;
                FileInfo file = new FileInfo(Path);
                if (file.Exists)
                {
                    file.Delete();
                }
            }
        }
    }
}
