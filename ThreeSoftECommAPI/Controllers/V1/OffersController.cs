using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1;
using ThreeSoftECommAPI.Contracts.V1.Requests.EComm.OfferReq;
using ThreeSoftECommAPI.Contracts.V1.Responses.EComm;
using ThreeSoftECommAPI.Domain.EComm;
using ThreeSoftECommAPI.Services.EComm.OffersServ;

namespace ThreeSoftECommAPI.Controllers.V1
{
    public class OffersController:Controller
    {
        private readonly IOffersService _OffersService;

        public OffersController(IOffersService offersService)
        {
            _OffersService = offersService;
        }

        [HttpGet(ApiRoutes.Offers.GetAll)]
        public async Task<IActionResult> GetAll([FromQuery] int status)
        {
            return Ok(await _OffersService.GetOffersAsync(status));
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
        public async Task<IActionResult> Create([FromBody] CreateOfferRequest OffersRequst)
        {
            var Offers = new Offers
            {
                ArabicDesc = OffersRequst.ArabicDesc,
                EnglishDesc = OffersRequst.EnglishDesc,
                offerPrice = OffersRequst.offerPrice,
                ImgUrl = OffersRequst.ImgUrl,
                ProductId = OffersRequst.ProductId
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
                status = OffersRequst.status
            };

            var status = await _OffersService.UpdateOffersAsync(Offers);

            if (status == 1)
                return Ok(Offers);
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
    }
}
