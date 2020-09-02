using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Data;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.CouponServ
{
    public class CouponServices : ICouponServices
    {
        private readonly ApplicationDbContext _dataContext;
        public CouponServices(ApplicationDbContext dbContext)
        {
            _dataContext = dbContext;
        }
        public async Task<List<Coupon>> GetCouponesAsync(int status)
        {
            switch (status)
            {
                case 1:
                    return await _dataContext.Coupons.Where(x => x.Status == 1).ToListAsync();
                case 0:
                    return await _dataContext.Coupons.Where(x => x.Status == 0).ToListAsync();
                default:
                    return await _dataContext.Coupons.ToListAsync();
            }
        }

        public async Task<Coupon> GetCouponByIdAsync(int CouponId)
        {
            return await _dataContext.Coupons.SingleOrDefaultAsync(x => x.Id == CouponId);
        }

        public async Task<int> CreateCouponAsync(Coupon coupon)
        {
            var CheckCode = await _dataContext.Coupons.SingleOrDefaultAsync(x => x.Code == coupon.Code);

            if (CheckCode != null)
                return -1;

            await _dataContext.Coupons.AddAsync(coupon);
            var created = await _dataContext.SaveChangesAsync();
            return created;
        }

        public async Task<int> UpdateCouponAsync(Coupon coupon)
        {
            var CheckCode = await _dataContext.Coupons.Where(y => y.Id != coupon.Id).SingleOrDefaultAsync(x => x.Code == coupon.Code);

            if (CheckCode != null)
                return -1;

            _dataContext.Coupons.Update(coupon);
            var created = await _dataContext.SaveChangesAsync();
            return created;
        }

        public async Task<bool> DeleteCouponAsync(int CouponId)
        {
            var cop = await GetCouponByIdAsync(CouponId);

            if (cop == null)
                return false;

            _dataContext.Coupons.Remove(cop);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<Coupon> GetCouponByNameAsync(string Name)
        {
            return await _dataContext.Coupons.Where(x => x.Code == Name.Trim() && x.Quantity > 0 && x.Status == 1).SingleOrDefaultAsync();
        }
        public async Task<bool> SubTrackNumberOfPromoCode(int CouponId)
        {
            var coubon = await GetCouponByIdAsync(CouponId);

            var _coubon = new Coupon
            {
                Id = coubon.Id,
                Amount = coubon.Amount,
                Code = coubon.Code,
                Percentage = coubon.Percentage,
                Quantity = coubon.Quantity - 1,
                Status = coubon.Status,
                Type = coubon.Type,
                CreateAt = coubon.CreateAt,
                CreateBy = coubon.CreateBy,
            };

            _dataContext.Coupons.Update(_coubon);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }
    }
}
