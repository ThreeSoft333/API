using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1.Responses;
using ThreeSoftECommAPI.Domain.EComm;
using ThreeSoftECommAPI.Helpers;

namespace ThreeSoftECommAPI.Services.EComm.SubCategoryServ
{
   public interface ISubCategoryService
    {
        Task<List<SubCategory>> GetSubCategoriesAsync(int CategoryId, int status);
        Task<List<SubCategory>> SearchSubCategoriesAsync(int CategoryId,string Name);
        PagedList<SubCategory> GetSubCategoriesAsync(int CategoryId, int status,Pagination pagination);
        Task<SubCategory> GetSubCategoryByIdAsync(Int32 subCategoryId);
        Task<int> CreateSubCategoryAsync(SubCategory subCategory);
        Task<int> UpdateSubCategoryAsync(SubCategory subCategory);
        Task<bool> DeleteSubCategoryAsync(Int32 subCategoryId);
    }
}
