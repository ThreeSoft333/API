using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Data;
using ThreeSoftECommAPI.Domain.Identity;

namespace ThreeSoftECommAPI.Services.EComm.UserAddressesServ
{
    public class UserAddresseService:IUserAddressesService
    {
        private readonly ApplicationDbContext _dataContext;

        public UserAddresseService(ApplicationDbContext dbContext)
        {
            _dataContext = dbContext;
        }

        public async Task<List<UserAddresses>> GetUserAddressesAsync(string UserId)
        {
          return await _dataContext.UserAddresses.Where(x => x.UserId == UserId && x.status == 1).ToListAsync();
        }
      
        public async Task<UserAddresses> GetUserAddresseByIdAsync(int id)
        {
            return await _dataContext.UserAddresses.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> CreateUserAddresseAsync(UserAddresses userAddresse)
        {

            await _dataContext.UserAddresses.AddAsync(userAddresse);
            var created = await _dataContext.SaveChangesAsync();
            return created;
        }

        public async Task<bool> DeleteUserAddresseAsync(int id)
        {
            var add = await GetUserAddresseByIdAsync(id);

            if (add == null)
                return false;

            _dataContext.UserAddresses.Remove(add);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}
