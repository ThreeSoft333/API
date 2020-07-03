using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Data;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.ProductReviewsServ
{
    public class ProductReviewService : IProductReviewService
    {
        private readonly ApplicationDbContext _dataContext;

        public ProductReviewService(ApplicationDbContext dbContext)
        {
            _dataContext = dbContext;
        }
        public async Task<int> CreateProductReviewAsync(ProductReviews productReviews)
        {
            await _dataContext.ProductReviews.AddAsync(productReviews);
            var created = await _dataContext.SaveChangesAsync();
            return created;
        }

        public async Task<List<ProductReviews>> GetProductReviews(long ProductId)
        {
           return await _dataContext.ProductReviews.Include(y => y.User).Where(x => x.ProductId == ProductId).ToListAsync();
        }
    }
}
