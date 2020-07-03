using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Domain.Identity;

namespace ThreeSoftECommAPI.Services.EComm.UserAddressesServ
{
   public interface IUserAddressesService
    {
        Task<List<UserAddresses>> GetUserAddressesAsync(string UserId);
        Task<UserAddresses> GetUserAddresseByIdAsync(Int32 id);
        Task<int> CreateUserAddresseAsync(UserAddresses userAddresses);
        Task<bool> DeleteUserAddresseAsync(Int32 id);
    }
}
