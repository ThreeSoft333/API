using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1.Responses;
using ThreeSoftECommAPI.Contracts.V1.Responses.EComm;
using ThreeSoftECommAPI.Domain.EComm;
using ThreeSoftECommAPI.Helpers;

namespace ThreeSoftECommAPI.Services.EComm.ProductServ
{
   public interface IProductService
    {
        Task<List<Product>> GetProductsAsync(Int64 SubCatgId, int status);
        ProductRespPagination GetProductsBySubCategoryAsync(string UserId,Int64 SubCatgId,
            Pagination pagination);
        Task<List<ProductResponse>> GetProductsMostRecentAsync(string UserId, int count);
        Task<List<ProductResponse>> GetProductsMostWantedAsync(string UserId, int count);
        Task<List<ProductResponse>> GetProductsUserFavAsync(string UserId);
        Task<List<ProductResponse>> GetProductsTopRatedAsync(string UserId, int count);
        Task<List<ProductResponse>> SearchProductsAsync(string UserId,string SearchText);
        Task<Product> GetProductByIdAsync(Int64 ProductId);
        Task<ViewProductResponse> ViewProductAsync(Int64 ProductId);
        Task<int> CreateProductAsync(Product  product);
        Task<int> UpdateProductAsync(Product product);
        Task<int> UpdateProductSalePriceAsync(Int64 ProductId,decimal salePrice);
        Task<bool> DeleteProductAsync(Int64 ProductId);
        Task<bool> CheckProductsIfActive(long ProductId);










    }
}
