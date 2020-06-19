using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Data;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.ProductColorServ
{
    public class ProductColorService:IProductColorService
    {
        private readonly ApplicationDbContext _dataContext;

        public ProductColorService(ApplicationDbContext dbContext)
        {
            _dataContext = dbContext;
        }

        public async Task<List<ProductColors>> GetProductColorsAsync(Int64 ProductId, int status)
        {
            switch (status)
            {
                case 1:
                    return await _dataContext.ProductColors.Where(p => p.ProductId == ProductId && p.Status == 1).ToListAsync();
                case 0:
                    return await _dataContext.ProductColors.Where(p => p.ProductId == ProductId && p.Status == 0).ToListAsync();
                default:
                    return await _dataContext.ProductColors.ToListAsync();
            }
        }
      

        public async Task<ProductColors> GetProductColorsByIdAsync(long Id)
        {
            return await _dataContext.ProductColors.SingleOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<int> CreateProductColorsAsync(ProductColors productColor)
        {
            var CheckExist = await _dataContext.ProductColors.Where(p =>p.ProductId == productColor.ProductId)
                .SingleOrDefaultAsync(x => x.ArabicName == productColor.ArabicName || x.EnglishName == productColor.EnglishName);

            if (CheckExist != null)
                return -1;

            await _dataContext.ProductColors.AddAsync(productColor);
            var created = await _dataContext.SaveChangesAsync();
            return created;
        }

        public async Task<int> UpdateProductColorsAsync(ProductColors productColor)
        {
            var CheckExist = await _dataContext.ProductColors.Where(p => p.ProductId == productColor.ProductId && p.Id != productColor.Id)
              .SingleOrDefaultAsync(x => x.ArabicName == productColor.ArabicName || x.EnglishName == productColor.EnglishName);

            if (CheckExist != null)
                return -1;

            _dataContext.ProductColors.Update(productColor);
            var Updated = await _dataContext.SaveChangesAsync();
            return Updated;
        }

        public async Task<bool> DeleteProductColorsAsync(long Id)
        {
            var ProdColor = await GetProductColorsByIdAsync(Id);

            if (ProdColor == null)
                return false;

            _dataContext.ProductColors.Remove(ProdColor);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}
