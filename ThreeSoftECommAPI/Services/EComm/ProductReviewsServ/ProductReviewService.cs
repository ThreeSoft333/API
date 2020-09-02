using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1.Responses.EComm;
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

        public async Task<ProductReviews> GetById(Int64 Id)
        {
            return await _dataContext.ProductReviews.SingleOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<bool> Delete(long Id)
        {
            var ProdRev = await GetById(Id);

            if (ProdRev == null)
                return false;

            _dataContext.ProductReviews.Remove(ProdRev);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<List<ProductReviews>> GetNewProductReviews(int Status)
        {
            return await _dataContext.ProductReviews.Include(x => x.User)
               .Include(x => x.product).Where(x => x.Status == Status).ToListAsync();
        }

        public async Task<List<ProductReviewResponse>> GetProductReviews(long ProductId)
        {
            return await _dataContext.ProductReviews.Include(y => y.User)
                 .Where(x => x.ProductId == ProductId && x.Status == 1).Select(x => new ProductReviewResponse
                 {
                     ArabicDescreption = x.ArabicDescreption,
                     EnglishDescreption = x.EnglishDescreption,
                     Rate = x.Rate,
                     Status = x.Status,
                     CreatedAt = x.CreatedAt.ToString("dd/MM/yyyy hh:mm:tt"),
                     user = x.User
                 }).ToListAsync();
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
