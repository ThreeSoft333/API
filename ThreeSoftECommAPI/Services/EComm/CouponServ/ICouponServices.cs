using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.CouponServ
{
   public interface ICouponServices
    {
        Task<List<Coupon>> GetCouponesAsync(int status);
        Coupon GetCouponByIdAsync(Int32 CouponId);
        Task<Coupon> GetCouponByNameAsync(string Name);
        Task<int> CreateCouponAsync(Coupon Coupon);
        Task<int> UpdateCouponAsync(Coupon Coupon);
        bool DeleteCouponAsync(Int32 CouponId);
        bool PromoCodeQuantityMinusOne(Int32 CouponId);
        bool PromoCodeQuantityPlusOne(Int32 CouponId);
    }
}
