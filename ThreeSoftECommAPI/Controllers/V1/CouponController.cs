using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1;
using ThreeSoftECommAPI.Contracts.V1.Requests.EComm.CouponeReq;
using ThreeSoftECommAPI.Contracts.V1.Responses.EComm;
using ThreeSoftECommAPI.Domain.EComm;
using ThreeSoftECommAPI.Services.EComm.CouponServ;

namespace ThreeSoftECommAPI.Controllers.V1
{
    public class CouponController:Controller
    {
        private readonly ICouponServices _couponService;
        public CouponController(ICouponServices couponService)
        {
            _couponService = couponService;
        }

        [HttpGet(ApiRoutes.Coupon.GetAll)]
        public async Task<IActionResult> GetAll([FromQuery] int status)
        {
            return Ok(await _couponService.GetCouponesAsync(status));
        }

        [HttpGet(ApiRoutes.Coupon.Get)]
        public async Task<IActionResult> Get([FromRoute] Int32 couponId)
        {
            var Catg = await _couponService.GetCouponByIdAsync(couponId);

            if (Catg == null)
                return NotFound(new ErrorResponse
                {
                    message = "Not Found",
                    status = NotFound().StatusCode
                });

            return Ok(Catg);
        }

        [HttpGet(ApiRoutes.Coupon.GetByName)]
        public async Task<IActionResult> GetByName([FromRoute] string name)
        {
            var Copon = await _couponService.GetCouponByNameAsync(name);

            if (Copon == null)
                return NotFound(new ErrorResponse
                {
                    message = "Not Found",
                    status = NotFound().StatusCode
                });

            return Ok(Copon);
        }

        [HttpPost(ApiRoutes.Coupon.Create)]
        public async Task<IActionResult> Create([FromBody] CreateCouponRequest CouponRequst)
        {
            var coupon = new Coupon
            {
                Code = CouponRequst.Code,
                Status = CouponRequst.Status,
                Quantity = CouponRequst.Quantity,
                Type = CouponRequst.Type,
                Amount = CouponRequst.Amount,
                Percentage = CouponRequst.Percentage,
                CreateAt = DateTime.Now,
                CreateBy = CouponRequst.CreateBy
            };

            var status = await _couponService.CreateCouponAsync(coupon);

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
                var response = new  {
                status = Ok().StatusCode,
                message = "Successfully Add"
                };
                return Ok(response);
            }
            return NotFound(new ErrorResponse
            {
                message = "Not Found",
                status = NotFound().StatusCode
            });
        }

        [HttpPost(ApiRoutes.Coupon.Update)]
        public async Task<IActionResult> Update([FromRoute] Int32 couponId, [FromBody] UpdateCouponeRequest CouponRequst)
        {
            var coupon = new Coupon
            {
                Id = couponId,
                Code = CouponRequst.Code,
                Status = CouponRequst.Status,
                Quantity = CouponRequst.Quantity,
                Type = CouponRequst.Type,
                Amount = CouponRequst.Amount,
                Percentage = CouponRequst.Percentage,
                UpdateAt = DateTime.Now,
                UpdateBy = CouponRequst.UpdateBy
            };

            var status = await _couponService.UpdateCouponAsync(coupon);

            if (status == -1)
            {
                return Conflict(new ErrorResponse
                {
                    message = "Dublicate Entry",
                    status = Conflict().StatusCode

                });
            }

            if (status == 1)
                return Ok(coupon);
            return NotFound(new ErrorResponse
            {
                message = "Not Found",
                status = NotFound().StatusCode
            });
        }

        [HttpDelete(ApiRoutes.Coupon.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Int32 couponId)
        {
            var deleted = await _couponService.DeleteCouponAsync(couponId);

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
