using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.ProductSizeServ
{
   public interface IProductSizeService
    {
        Task<List<ProductSize>> GetProductSizeAsync(long productId, Int32 Status);
        Task<ProductSize> GetProductSizeByIdAsync(long id);
        Task<int> CreateProductSizeAsync(ProductSize productSize);
        Task<int> UpdateProductSizeAsync(ProductSize productSize);
        Task<bool> DeleteProductSizeAsync(long id);
    }
}
