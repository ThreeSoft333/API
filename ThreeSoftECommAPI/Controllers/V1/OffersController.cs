using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1;
using ThreeSoftECommAPI.Contracts.V1.Requests.EComm.OfferReq;
using ThreeSoftECommAPI.Contracts.V1.Responses.EComm;
using ThreeSoftECommAPI.Domain.EComm;
using ThreeSoftECommAPI.Services.EComm.OffersServ;
using ThreeSoftECommAPI.Services.EComm.ProductServ;

namespace ThreeSoftECommAPI.Controllers.V1
{
    public class OffersController:Controller
    {
        private readonly IOffersService _OffersService;
        private readonly IProductService _productService;

        public OffersController(IOffersService offersService,IProductService productService)
        {
            _OffersService = offersService;
            _productService = productService;
        }

        [HttpGet(ApiRoutes.Offers.GetAll)]
        public async Task<IActionResult> GetAll([FromQuery] int status)
        {
            return Ok(await _OffersService.GetOffersAsync(status));
        }

        [HttpGet(ApiRoutes.Offers.GetByProduct)]
        public async Task<IActionResult> GetByProductIdAll([FromRoute] Int64 productId)
        {
            return Ok(await _OffersService.GetOffersByProductIdAsync(productId));
        }

        [HttpGet(ApiRoutes.Offers.GetForApp)]
        public async Task<IActionResult> GetAllForApp(string UserId)
        {
            return Ok(await _OffersService.GetOffersAllForAppAsync(UserId));
        }

        [HttpGet(ApiRoutes.Offers.Get)]
        public async Task<IActionResult> Get([FromRoute] Int64 offerId)
        {
            var Offer = await _OffersService.GetOffersByIdAsync(offerId);

            if (Offer == null)
                return NotFound(new ErrorResponse
                {
                    message = "Not Found",
                    status = NotFound().StatusCode
                });

            return Ok(Offer);
        }

        [HttpPost(ApiRoutes.Offers.Create)]
        public async Task<IActionResult> Create([FromBody] CreateOfferRequest offer)
        {
            var Offers = new Offers
            {
                ArabicDesc = offer.ArabicDesc,
                EnglishDesc = offer.EnglishDesc,
                offerPrice = offer.offerPrice,
                ImgUrl = offer.ImgUrl,
                ProductId = offer.ProductId,
                status = offer.status
            };

            var status = await _OffersService.CreateOffersAsync(Offers);

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
                await _productService.UpdateProductSalePriceAsync(offer.ProductId, offer.offerPrice);
                var response = new OfferResponse { Id = Offers.Id };
                return Ok(response);
            }
            return NotFound(new ErrorResponse
            {
                message = "Not Found",
                status = NotFound().StatusCode
            });
        }

        [HttpPost(ApiRoutes.Offers.Update)]
        public async Task<IActionResult> Update([FromRoute] Int64 offerId, [FromBody] UpdateOfferRequest OffersRequst)
        {
           
                var Offers = new Offers
                {
                    Id = offerId,
                    ArabicDesc = OffersRequst.ArabicDesc,
                    EnglishDesc = OffersRequst.EnglishDesc,
                    offerPrice = OffersRequst.offerPrice,
                    ImgUrl = OffersRequst.ImgUrl,
                    status = OffersRequst.status,
                    ProductId = OffersRequst.ProductId
                };

                var status = await _OffersService.UpdateOffersAsync(Offers);

            if (status == 1)
            {
                await _productService.UpdateProductSalePriceAsync(Offers.ProductId, Offers.offerPrice);
                return Ok(Offers);
            }
            
            return NotFound(new ErrorResponse
            {
                message = "Not Found",
                status = NotFound().StatusCode
            });
        }

        [HttpDelete(ApiRoutes.Offers.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Int64 offerId)
        {
            var deleted = await _OffersService.DeleteOffersAsync(offerId);

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

        [HttpPost(ApiRoutes.Offers.Upload), DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            var file = Request.Form.Files[0];
            var folderName = Path.Combine("Resources/Images/OffersImg/");
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
