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
        
        public CouponServices(ApplicationDbContext dbContext,ApplicationDbContext dbContext1)
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

        public  Coupon GetCouponByIdAsync(int CouponId)
        {
            try
            {
                return _dataContext.Coupons.SingleOrDefault(x=>x.Id==CouponId);
            }
            catch(Exception ex)
            {
                return null;
            }
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

        public bool DeleteCouponAsync(int CouponId)
        {
            var cop = GetCouponByIdAsync(CouponId);

            if (cop == null)
                return false;

            _dataContext.Coupons.Remove(cop);
            var deleted = _dataContext.SaveChanges();
            return deleted > 0;
        }

        public async Task<Coupon> GetCouponByNameAsync(string Name)
        {
            return await _dataContext.Coupons.Where(x => x.Code == Name.Trim() && x.Quantity > 0 && x.Status == 1).SingleOrDefaultAsync();
        }
        public bool PromoCodeQuantityMinusOne(int CouponId)
        {
            try
            {
                var coubon = GetCouponByIdAsync(CouponId);

                coubon.Quantity -= 1;

               var update = _dataContext.SaveChanges();
                return update > 0;
            }

            catch (Exception ex)
            {
                return false;
            }
        }

        public bool PromoCodeQuantityPlusOne(int CouponId)
        {
            try
            {
                var coubon = GetCouponByIdAsync(CouponId);

                coubon.Quantity += 1;

                var update = _dataContext.SaveChanges();
                return update > 0;
            }

            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
