using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1.Responses;
using ThreeSoftECommAPI.Domain.EComm;
using ThreeSoftECommAPI.Helpers;

namespace ThreeSoftECommAPI.Services.EComm.CategoryServ
{
   public interface ICategoryService
    {
        Task<List<Category>> GetCategoriesAsync(int status);
        Task<List<Category>> SearchCategoriesAsync(string Name);
        PagedList<Category> GetCategoriesAsync(int status,Pagination pagination);
        Task<List<Category>> GetCategoriesTopAsync();
        Task<Category> GetCategoryByIdAsync(Int32 categoryId);
        Task<int> CreateCategoryAsync(Category category);
        Task<int> UpdateCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(Int32 categoryId);
    }
}
