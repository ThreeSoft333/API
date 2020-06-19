using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Data;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.UserFavouritesServ
{
    public class UserFavService : IUserFavService
    {
        private readonly ApplicationDbContext _dataContext;

        public UserFavService(ApplicationDbContext dbContext)
        {
            _dataContext = dbContext;
        }
        public async Task<int> CreateUserFavAsync(UserFavourites userFavourites)
        {
            await _dataContext.UserFavourites.AddAsync(userFavourites);
            var created = await _dataContext.SaveChangesAsync();
            return created;
        }

        public async Task<bool> DeleteUserFavAsync(string UserId, Int64 ProductId)
        {
            var UserFav = await GetAsync(UserId, ProductId);

            if (UserFav == null)
                return false;

            _dataContext.UserFavourites.Remove(UserFav);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<UserFavourites> GetAsync(string UserId, Int64 ProductId)
        {
            return await _dataContext.UserFavourites.Where(u => u.UserId == UserId).SingleOrDefaultAsync(x => x.ProductId == ProductId);
        }
    }
}
