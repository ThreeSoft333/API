using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1.Responses.EComm;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.ProductReviewsServ
{
   public interface IProductReviewService
    {
        Task<int> CreateProductReviewAsync(ProductReviews productReviews);
        Task<List<ProductReviewResponse>> GetProductReviews(Int64 ProductId);
        Task<List<ProductReviews>> GetNewProductReviews(int Status);
        Task<ProductReviews> GetById(Int64 Id);
        Task<int> UpdateStatus(Int64 Id);
        Task<bool> Delete(Int64 Id);
    }
}
