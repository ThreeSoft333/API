using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.UserFavouritesServ
{
   public interface IUserFavService
    {
        
        Task<int> CreateUserFavAsync(UserFavourites userFavourites);
        Task<bool> DeleteUserFavAsync(string UserId,Int64 ProductId);
        Task<UserFavourites> GetAsync(string UserId, Int64 ProductId);
    }
}
