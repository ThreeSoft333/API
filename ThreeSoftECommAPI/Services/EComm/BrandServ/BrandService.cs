using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Data;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.BrandServ
{
    public class BrandService : IBrandService
    {
        private readonly ApplicationDbContext _dataContext;

        public BrandService(ApplicationDbContext dbContext)
        {
            _dataContext = dbContext;
        }

        public async Task<List<Brand>> GetBrandsAsync()
        {
            return await _dataContext.Brand.ToListAsync();
        }

        public async Task<Brand> GetBrandByIdAsync(int brandId)
        {
            return await _dataContext.Brand.SingleOrDefaultAsync(x => x.Id == brandId);
        }

        public async Task<int> CreateBrandAsync(Brand brand)
        {
            var CheckArName = await _dataContext.Brand.SingleOrDefaultAsync(x => x.ArabicName == brand.ArabicName);
            var CheckEnName = await _dataContext.Brand.SingleOrDefaultAsync(x => x.EnglishName == brand.EnglishName);

            if (CheckArName != null || CheckEnName != null)
                return -1;

            await _dataContext.Brand.AddAsync(brand);
            var created = await _dataContext.SaveChangesAsync();
            return created;
        }

        public async Task<int> UpdateBrandAsync(Brand brand)
        {
            var CheckArName = await _dataContext.category.Where(y => y.Id != brand.Id).SingleOrDefaultAsync(x => x.ArabicName == brand.ArabicName);
            var CheckEnName = await _dataContext.category.Where(y => y.Id != brand.Id).SingleOrDefaultAsync(x => x.EnglishName == brand.EnglishName);

            if (CheckArName != null || CheckEnName != null)
                return -1;

            _dataContext.Brand.Update(brand);
            var created = await _dataContext.SaveChangesAsync();
            return created ;
        }
            
        public async Task<bool> DeleteBrandAsync(int brandId)
        {
            var brand = await GetBrandByIdAsync(brandId);

            if (brand == null)
                return false;

            _dataContext.Brand.Remove(brand);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

    

     

     
    }
}
