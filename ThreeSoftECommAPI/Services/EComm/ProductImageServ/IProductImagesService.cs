using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.ProductImageServ
{
   public interface IProductImagesService
    {
        Task<List<ProductImage>> GetProductImageAsync(Int64 ProductId);
        Task<ProductImage> GetProductImageByIdAsync(Int64 Id);
        Task<int> CreateProductImageAsync(ProductImage productImage);
        Task<int> UpdateProductImageAsync(ProductImage productImage);
        Task<bool> DeleteProductImageAsync(Int64 Id);
    }
}
