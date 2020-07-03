using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Data;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.CartServ
{
    public class CartService:ICartService
    {
        private readonly ApplicationDbContext _dataContext;

        public CartService(ApplicationDbContext dbContext)
        {
            _dataContext = dbContext;
        }

        public async Task<List<Cart>> GetCartAsync(string UserId)
        {
            return await _dataContext.Cart.Where(x => x.UserId == UserId).ToListAsync();
        }

        public async Task<Cart> GetCartByUserIdAsync(string UserId)
        {
            return await _dataContext.Cart.SingleOrDefaultAsync(x => x.UserId == UserId);
        }

        public async Task<int> CreateCartAsync(Cart Cart)
        {
            await _dataContext.Cart.AddAsync(Cart);
            var created = await _dataContext.SaveChangesAsync();
            return created;
        }

        public async Task<int> UpdateCartAsync(Cart Cart)
        {
            _dataContext.Cart.Update(Cart);
            var created = await _dataContext.SaveChangesAsync();
            return created;
        }

        public async Task<bool> DeleteCartAsync(Int64 CartId)
        {
            var Cart = await GetCartByIdAsync(CartId);

            if (Cart == null)
                return false;

            _dataContext.Cart.Remove(Cart);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<Cart> GetCartByIdAsync(long CartId)
        {
            return await _dataContext.Cart.SingleOrDefaultAsync(x => x.Id == CartId);
        }
    }
}
