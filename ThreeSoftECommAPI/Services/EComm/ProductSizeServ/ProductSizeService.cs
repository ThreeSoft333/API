using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Data;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.ProductSizeServ
{
    public class ProductSizeService:IProductSizeService
    {
        private readonly ApplicationDbContext _dataContext;

        public ProductSizeService(ApplicationDbContext dbContext)
        {
            _dataContext = dbContext;
        }

        public async Task<List<ProductSize>> GetProductSizeAsync(Int64 ProductId, int status)
        {
            switch (status)
            {
                case 1:
                    return await _dataContext.ProductSizes.Where(p => p.ProductId == ProductId && p.Status == 1).ToListAsync();
                case 0:
                    return await _dataContext.ProductSizes.Where(p => p.ProductId == ProductId && p.Status == 0).ToListAsync();
                default:
                    return await _dataContext.ProductSizes.ToListAsync();
            }
        }


        public async Task<ProductSize> GetProductSizeByIdAsync(long Id)
        {
            return await _dataContext.ProductSizes.SingleOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<int> CreateProductSizeAsync(ProductSize productSizes)
        {
            var CheckExist = await _dataContext.ProductSizes.Where(p => p.ProductId == productSizes.ProductId)
                .SingleOrDefaultAsync(x => x.Size == productSizes.Size);

            if (CheckExist != null)
                return -1;

            await _dataContext.ProductSizes.AddAsync(productSizes);
            var created = await _dataContext.SaveChangesAsync();
            return created;
        }

        public async Task<int> UpdateProductSizeAsync(ProductSize productSizes)
        {
            var CheckExist = await _dataContext.ProductSizes.Where(p => p.ProductId == productSizes.ProductId && p.Id != productSizes.Id)
              .SingleOrDefaultAsync(x => x.Size == productSizes.Size);

            if (CheckExist != null)
                return -1;

            _dataContext.ProductSizes.Update(productSizes);
            var Updated = await _dataContext.SaveChangesAsync();
            return Updated;
        }

        public async Task<bool> DeleteProductSizeAsync(long Id)
        {
            var ProdColor = await GetProductSizeByIdAsync(Id);

            if (ProdColor == null)
                return false;

            _dataContext.ProductSizes.Remove(ProdColor);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}
