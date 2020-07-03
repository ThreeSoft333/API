using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.ProductColorServ
{
   public interface IProductColorService
    {
        Task<List<ProductColors>> GetProductColorsAsync();
        Task<ProductColors> GetProductColorsByIdAsync(long id);
        Task<int> CreateProductColorsAsync(ProductColors productColors);
        Task<int> UpdateProductColorsAsync(ProductColors productColors);
        Task<bool> DeleteProductColorsAsync(long id);
    }
}
