using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.SubCategoryServ
{
   public interface ISubCategoryService
    {
        Task<List<SubCategory>> GetSubCategoriesAsync(int CategoryId, int status);
        Task<SubCategory> GetSubCategoryByIdAsync(Int32 subCategoryId);
        Task<int> CreateSubCategoryAsync(SubCategory subCategory);
        Task<int> UpdateSubCategoryAsync(SubCategory subCategory);
        Task<bool> DeleteSubCategoryAsync(Int32 subCategoryId);
    }
}
