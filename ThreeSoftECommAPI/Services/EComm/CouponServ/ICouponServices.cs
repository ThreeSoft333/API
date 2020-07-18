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
        Task<Coupon> GetCouponByIdAsync(Int32 CouponId);
        Task<Coupon> GetCouponByNameAsync(string Name);
        Task<int> CreateCouponAsync(Coupon Coupon);
        Task<int> UpdateCouponAsync(Coupon Coupon);
        Task<bool> DeleteCouponAsync(Int32 CouponId);
    }
}
