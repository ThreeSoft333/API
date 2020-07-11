using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1;
using ThreeSoftECommAPI.Contracts.V1.Requests.EComm.ProductAttr;
using ThreeSoftECommAPI.Contracts.V1.Responses.EComm;
using ThreeSoftECommAPI.Domain.EComm;
using ThreeSoftECommAPI.Services.EComm.ProductAttributeServ;

namespace ThreeSoftECommAPI.Controllers.V1
{
    public class ProductAttributeController : Controller
    {
        private readonly IProductAttributeService _productAttributeService;
        public ProductAttributeController(IProductAttributeService productAttributeService)
        {
            _productAttributeService = productAttributeService;
        }

        [HttpGet(ApiRoutes.Product_Attr.GetAll)]
        public async Task<IActionResult> GetAll([FromRoute] Int64 productId)
        {
            return Ok(await _productAttributeService.Get(productId));
        }

        [HttpPost(ApiRoutes.Product_Attr.Create)]
        public async Task<IActionResult> Create([FromBody] CreateProductAttributeRequest attributeRequest)
        {
            var Attr = new ProductAttributes
            {
                ArabicName = attributeRequest.ArabicName,
                EnglishName = attributeRequest.EnglishName,
                ProductId = attributeRequest.ProductId
            };

            var status = await _productAttributeService.Create(Attr);

            if (status == 1)
            {
                var response = new
                {
                    status = Ok().StatusCode,
                    message = "Success"
                };
                return Ok(response);
            }
            return NotFound(new ErrorResponse
            {
                message = "Not Found",
                status = NotFound().StatusCode
            });
        }

        [HttpPost(ApiRoutes.Product_Attr.Update)]
        public async Task<IActionResult> Update([FromRoute] Int32 id, [FromBody] UpdateProductAttributeRequest attributeRequest)
        {
            var Attr = new ProductAttributes
            {
                Id = id,
                ArabicName = attributeRequest.ArabicName,
                EnglishName = attributeRequest.EnglishName,
            };

            var status = await _productAttributeService.Update(Attr);

            if (status == 1)
                return Ok();
            return NotFound(new ErrorResponse
            {
                message = "Not Found",
                status = NotFound().StatusCode
            });
        }

        [HttpDelete(ApiRoutes.Product_Attr.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Int32 id)
        {
            var deleted = await _productAttributeService.Delete(id);

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
