using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.BrandServ
{
   public interface IBrandService
    {
        Task<List<Brand>> GetBrandsAsync();

        Task<Brand> GetBrandByIdAsync(Int32 brandId);

        Task<bool> CreateBrandAsync(Brand brand);
        Task<bool> UpdateBrandAsync(Brand brand);
        Task<bool> DeleteBrandAsync(Int32 brandId);
    }
}
