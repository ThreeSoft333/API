using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Data;
using ThreeSoftECommAPI.Domain.EComm;
using ThreeSoftECommAPI.Services.EComm.SubCategoryServ;

namespace ThreeSoftECommAPI.Services.EComm.SubSubCategoryServ
{
    public class SubCategoryService:ISubCategoryService
    {
        private readonly ApplicationDbContext _dataContext;

        public SubCategoryService(ApplicationDbContext dbContext)
        {
            _dataContext = dbContext;
        }

        public async Task<List<SubCategory>> GetSubCategoriesAsync(int CategoryId, int status)
        {
            switch (status)
            {
                case 1:
                    return await _dataContext.subCategory.Where(c => c.CategoryId == CategoryId).Where(x => x.Status == 1).ToListAsync();
                case 0:
                    return await _dataContext.subCategory.Where(c => c.CategoryId == CategoryId).Where(x => x.Status == 0).ToListAsync();
                default:
                    return await _dataContext.subCategory.Where(c => c.CategoryId == CategoryId).ToListAsync();
            }
        }

        public async Task<SubCategory> GetSubCategoryByIdAsync(int SubCatgId)
        {
            return await _dataContext.subCategory.SingleOrDefaultAsync(x => x.Id == SubCatgId);
        }

        public async Task<int> CreateSubCategoryAsync(SubCategory SubCategory)
        {
            var CheckArName = await _dataContext.subCategory.SingleOrDefaultAsync(x => x.ArabicName == SubCategory.ArabicName);
            var CheckEnName = await _dataContext.subCategory.SingleOrDefaultAsync(x => x.EnglishName == SubCategory.EnglishName);

            if (CheckArName != null || CheckEnName != null)
                return -1;

            await _dataContext.subCategory.AddAsync(SubCategory);
            var created = await _dataContext.SaveChangesAsync();
            return created;
        }

        public async Task<int> UpdateSubCategoryAsync(SubCategory SubCategory)
        {
            var CheckArName = await _dataContext.subCategory.Where(y => y.Id != SubCategory.Id).SingleOrDefaultAsync(x => x.ArabicName == SubCategory.ArabicName);
            var CheckEnName = await _dataContext.subCategory.Where(y => y.Id != SubCategory.Id).SingleOrDefaultAsync(x => x.EnglishName == SubCategory.EnglishName);

            if (CheckArName != null || CheckEnName != null)
                return -1;

            _dataContext.subCategory.Update(SubCategory);
            var Updated = await _dataContext.SaveChangesAsync();
            return Updated;
        }

        public async Task<bool> DeleteSubCategoryAsync(int SubCatgId)
        {
            var SubCatg = await GetSubCategoryByIdAsync(SubCatgId);

            if (SubCatg == null)
                return false;

            _dataContext.subCategory.Remove(SubCatg);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}
