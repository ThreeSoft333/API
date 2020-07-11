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

        public async Task<List<ProductReviews>> GetNewProductReviews()
        {
            return await _dataContext.ProductReviews.Include(x => x.User)
               .Include(x => x.product).Where(x => x.Status == 0).ToListAsync();
        }

        public async Task<List<ProductReviews>> GetProductReviews(long ProductId)
        {
           return await _dataContext.ProductReviews.Include(y => y.User)
                .Where(x => x.ProductId == ProductId && x.Status == 1).ToListAsync();
        }

        public async Task<int> UpdateStatus(long Id)
        {
            var ProdReview = await _dataContext.ProductReviews.SingleOrDefaultAsync(x => x.Id == Id);

            if(ProdReview != null)
            {
                ProdReview.Status = 1;
                _dataContext.ProductReviews.Update(ProdReview);
                var updated = await _dataContext.SaveChangesAsync();
                return updated;
            }
            return 0;
        }
    }
}
