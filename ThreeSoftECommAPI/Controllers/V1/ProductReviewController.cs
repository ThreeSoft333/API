using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1;
using ThreeSoftECommAPI.Contracts.V1.Requests.EComm.ProdReviewReq;
using ThreeSoftECommAPI.Contracts.V1.Responses.EComm;
using ThreeSoftECommAPI.Domain.EComm;
using ThreeSoftECommAPI.Services.EComm.ProductReviewsServ;

namespace ThreeSoftECommAPI.Controllers.V1
{
    public class ProductReviewController:Controller
    {
        private readonly IProductReviewService _productReviewService;

        public ProductReviewController(IProductReviewService productReviewService)
        {
            _productReviewService = productReviewService;
        }

        [HttpGet(ApiRoutes.Product_Review.GetAll)]
        public async Task<IActionResult> GetAll([FromRoute] Int64 productId)
        {
            return Ok(await _productReviewService.GetProductReviews(productId));
        }

        [HttpPost(ApiRoutes.Product_Review.Create)]
        public async Task<IActionResult> Create([FromBody] CreateProductReviewRequest productReviewRequest)
        {
            var ProdReview = new ProductReviews
            {
                ArabicDescreption = productReviewRequest.ArabicDescreption,
                EnglishDescreption = productReviewRequest.EnglishDescreption,
                ProductId = productReviewRequest.ProductId,
                UserId = productReviewRequest.UserId,
                CreatedAt = DateTime.Now
            };

            var status = await _productReviewService.CreateProductReviewAsync(ProdReview);

            if (status == 1)
            {
                var response = new ProductReviewResponse { Id = ProdReview.Id };
                return Ok(response);
            }
            return BadRequest(new ErrorResponse
            {
                message = "Internal Server Error",
                status = BadRequest().StatusCode
            });
        }
    }
}
