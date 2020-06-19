using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Data;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.CategoryServ
{
    public class CategoryService:ICategoryService
    {
        private readonly ApplicationDbContext _dataContext;

        public CategoryService(ApplicationDbContext dbContext)
        {
            _dataContext = dbContext;
        }

        public async Task<List<Category>> GetCategoriesAsync(int status)
        {
            switch (status)
            {
                case 1:
                    return await _dataContext.category.Where(x => x.Status == 1).ToListAsync();
                case 0:
                    return await _dataContext.category.Where(x => x.Status == 0).ToListAsync();
                default:
                    return await _dataContext.category.ToListAsync();
            }
        }

        public async Task<List<Category>> GetCategoriesTopAsync()
        {
            return await _dataContext.category.Take(8).ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int CatgId)
        {
            return await _dataContext.category.SingleOrDefaultAsync(x => x.Id == CatgId);
        }

        public async Task<int> CreateCategoryAsync(Category Category)
        {
            var CheckArName = await _dataContext.category.SingleOrDefaultAsync(x => x.ArabicName == Category.ArabicName);
            var CheckEnName = await _dataContext.category.SingleOrDefaultAsync(x => x.EnglishName == Category.EnglishName);

            if (CheckArName != null || CheckEnName!=null)
                return -1;

            await _dataContext.category.AddAsync(Category);
            var created = await _dataContext.SaveChangesAsync();
            return created;
        }

        public async Task<int> UpdateCategoryAsync(Category Category)
        {
            var CheckArName = await _dataContext.category.Where(y => y.Id != Category.Id).SingleOrDefaultAsync(x => x.ArabicName == Category.ArabicName);
            var CheckEnName = await _dataContext.category.Where(y => y.Id != Category.Id).SingleOrDefaultAsync(x => x.EnglishName == Category.EnglishName);

            if (CheckArName != null || CheckEnName != null)
                return -1;

            _dataContext.category.Update(Category);
            var created = await _dataContext.SaveChangesAsync();
            return created;
        }

        public async Task<bool> DeleteCategoryAsync(int CatgId)
        {
            var Catg = await GetCategoryByIdAsync(CatgId);

            if (Catg == null)
                return false;

            _dataContext.category.Remove(Catg);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

     
    }
}
