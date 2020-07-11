using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.ProductAttributeServ
{
   public interface IProductAttributeService
    {
        Task<List<ProductAttributes>> Get(Int64 ProductId);
        Task<ProductAttributes> GetById(Int32 Id);
        Task<int> Create(ProductAttributes productAttributes);
        Task<int> Update(ProductAttributes productAttributes);
        Task<bool> Delete(Int32 Id);
    }
}
