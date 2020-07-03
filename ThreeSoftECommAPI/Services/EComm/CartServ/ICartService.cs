using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.CartServ
{
   public interface ICartService
    {
        Task<List<Cart>> GetCartAsync(string UserId);
        Task<Cart> GetCartByUserIdAsync(string UserId);
        Task<Cart> GetCartByIdAsync(Int64 CartId);
        Task<int> CreateCartAsync(Cart cart);
        Task<int> UpdateCartAsync(Cart cart);
        Task<bool> DeleteCartAsync(Int64 cartId);
    }
}
