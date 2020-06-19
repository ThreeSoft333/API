using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.ProductReviewsServ
{
   public interface IProductReviewService
    {
        Task<int> CreateProductReviewAsync(ProductReviews productReviews);
        Task<List<ProductReviews>> GetProductReviews(Int64 ProductId);
    }
}
