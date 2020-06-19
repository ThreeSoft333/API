using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Data;
using ThreeSoftECommAPI.Domain.EComm;
using ThreeSoftECommAPI.Services.EComm.ProductImageServ;

namespace ThreeSoftECommAPI.Services.ProductImageServ
{
    public class ProductImagesService:IProductImagesService
    {
        private readonly ApplicationDbContext _dataContext;

        public ProductImagesService(ApplicationDbContext dbContext)
        {
            _dataContext = dbContext;
        }

        public async Task<List<ProductImage>> GetProductImageAsync(Int64 ProductId)
        {
            return await _dataContext.ProductImages.Where(x => x.ProductId == ProductId).ToListAsync();
        }

        public async Task<ProductImage> GetProductImageByIdAsync(Int64 Id)
        {
            return await _dataContext.ProductImages.SingleOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<int> CreateProductImageAsync(ProductImage productImage)
        {
            await _dataContext.ProductImages.AddAsync(productImage);
            var created = await _dataContext.SaveChangesAsync();
            return created;
        }

        public async Task<int> UpdateProductImageAsync(ProductImage productImage)
        {
            _dataContext.ProductImages.Update(productImage);
            var Updated = await _dataContext.SaveChangesAsync();
            return Updated;
        }

        public async Task<bool> DeleteProductImageAsync(Int64 Id)
        {
            var ProdImg = await GetProductImageByIdAsync(Id);

            if (ProdImg == null)
                return false;

            _dataContext.ProductImages.Remove(ProdImg);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}
