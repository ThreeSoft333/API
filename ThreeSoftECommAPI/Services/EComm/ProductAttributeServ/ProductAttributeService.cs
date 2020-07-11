using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Data;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.ProductAttributeServ
{
    public class ProductAttributeService : IProductAttributeService
    {
        private readonly ApplicationDbContext _dataContext;
        public ProductAttributeService(ApplicationDbContext dbContext)
        {
            _dataContext = dbContext;
        }
        public async Task<int> Create(ProductAttributes productAttributes)
        {
            await _dataContext.ProductAttributes.AddAsync(productAttributes);
            var Created = await _dataContext.SaveChangesAsync();
            return Created;
        }

        public async Task<bool> Delete(int Id)
        {
            var attr = await GetById(Id);

            if (attr == null)
                return false;

            _dataContext.ProductAttributes.Remove(attr);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<List<ProductAttributes>> Get(long ProductId)
        {
            return await _dataContext.ProductAttributes.Where(x => x.ProductId == ProductId).ToListAsync();
        }

        public async Task<ProductAttributes> GetById(int Id)
        {
            return await _dataContext.ProductAttributes.SingleOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<int> Update(ProductAttributes productAttributes)
        {
             _dataContext.ProductAttributes.Update(productAttributes);
            var Updated = await _dataContext.SaveChangesAsync();
            return Updated;
        }
    }
}
