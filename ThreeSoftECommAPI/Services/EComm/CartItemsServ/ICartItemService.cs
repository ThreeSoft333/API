using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1.Responses.EComm;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.CartItemsServ
{
   public interface ICartItemService
    {
        Task<List<CartItem>> GetCartItemAsync(long CartId);
        Task<List<CartItemResponse>> GetCartItemByUserIdAsync(string UserId);
        Task<CartItem> GetCarItemtByIdAsync(long CartItemId);
        Task<CartItem> GetCarItemtByValueAsync(Int64 CartId, Int64? ProductId);
        Task<int> CreateCartItemAsync(CartItem cartItem);
        //Task<int> UpdateCartItemAsync(Int64 cartItemId, Int32 quantity);
        Task<int> UpdateCartItemAsync(CartItem cartItem);
        Task<bool> DeleteCartItemAsync(Int64 cartItemId);
        Task<bool> DeleteCartItemByUserAsync(string UserId);
    }
}
