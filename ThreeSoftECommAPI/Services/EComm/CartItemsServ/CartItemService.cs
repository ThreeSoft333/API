using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1.Responses.EComm;
using ThreeSoftECommAPI.Data;
using ThreeSoftECommAPI.Domain.EComm;
using ThreeSoftECommAPI.Services.EComm.CartItemsServ;

namespace ThreeSoftECommAPI.Services.EComm.CartItemItemsServ
{
    public class CartItemService: ICartItemService
    {
        private readonly ApplicationDbContext _dataContext;

        public CartItemService(ApplicationDbContext dbContext)
        {
            _dataContext = dbContext;
        }

        public async Task<List<CartItem>> GetCartItemAsync(long CartId)
        {
            return await _dataContext.cartItems.Include(x => x.product).Where(x => x.CartId == CartId).ToListAsync();
        }

        public async Task<List<CartItemResponse>> GetCartItemByUserIdAsync(string UserId)
        {

            var query = await _dataContext.product
                .Join(_dataContext.cartItems, p => p.Id, ci => ci.ProductId, (p, ci) => new { p, ci })
                .Join(_dataContext.Cart, cci => cci.ci.CartId, c => c.Id, (cci, c) => new { cci, c })
                .Join(_dataContext.ProductColors, ppc => ppc.cci.p.colorId, pc => pc.Id, (ppc, pc) => new { ppc, pc })
                .Join(_dataContext.ProductSizes, pps => pps.ppc.cci.p.sizeId, ps => ps.Id, (pps, ps) => new { pps, ps })
                .Where(x => x.pps.ppc.c.UserId == UserId)
                .Select(m => new CartItemResponse
                {
                    Id = m.pps.ppc.cci.ci.Id,
                    CartId = m.pps.ppc.cci.ci.CartId,
                    Quantity = m.pps.ppc.cci.ci.Quantity,
                    CreatedAt = m.pps.ppc.cci.ci.CreatedAt,
                    product = new ProductResponse
                    {
                        Id = m.pps.ppc.cci.p.Id,
                        ArabicName = m.pps.ppc.cci.p.ArabicName,
                        EnglishName = m.pps.ppc.cci.p.EnglishName,
                        ArabicDescription = m.pps.ppc.cci.p.ArabicDescription,
                        EnglishDescription = m.pps.ppc.cci.p.EnglishName,
                        ImgUrl = m.pps.ppc.cci.p.ImgUrl,
                        Price = m.pps.ppc.cci.p.Price,
                        SalePrice = m.pps.ppc.cci.p.SalePrice,
                        productColor = m.pps.pc,
                        productSize = m.ps,
                        productAttributes = (_dataContext.ProductAttributes
                             .Where(x => x.ProductId == m.pps.ppc.cci.p.Id).DefaultIfEmpty().ToList()),
                    }


                }).ToListAsync();
            


            return query;

           
        }

        public async Task<CartItem> GetCarItemtByIdAsync(Int64 CartItemId)
        {
            return await _dataContext.cartItems.SingleOrDefaultAsync(x => x.Id == CartItemId);
        }

        public async Task<int> CreateCartItemAsync(CartItem CartItem)
        {
            //var checkBrevOrderStatus = await _dataContext.Orders
            //   .Where(x => x.UserId == order.UserId && x.Status != 4).FirstOrDefaultAsync();

            await _dataContext.cartItems.AddAsync(CartItem);
            var created = await _dataContext.SaveChangesAsync();
            return created;
        }

        public async Task<int> UpdateCartItemAsync(CartItem cartItem)
        {
            try
            {
                var entry = await _dataContext.cartItems.FirstAsync(e => e.Id == cartItem.Id);
                 _dataContext.Entry(entry).CurrentValues.SetValues(cartItem);
                await _dataContext.SaveChangesAsync();
                return 1;
            }
            catch (Exception e)
            {
                // handle correct exception
                // log error
                return 0;
            }
        }

        //public async Task<int> UpdateCartItemAsync(Int64 cartItemId,Int32 quantity)
        //{
        //    var _cartItem = await GetCarItemtByIdAsync(cartItemId);

        //    if (_cartItem != null)
        //    {

        //        var Cartitem = new CartItem()
        //        {
        //            Id = _cartItem.Id,
        //            CartId = _cartItem.CartId,
        //            ProductId = _cartItem.ProductId,
        //            ProductColorId = _cartItem.ProductColorId,
        //            ProductSizeId = _cartItem.ProductSizeId,
        //            ProductAttrId = _cartItem.ProductAttrId,
        //            Quantity = quantity,
        //            UpdatedAt =DateTime.Now
        //        };

        //        _dataContext.cartItems.Update(Cartitem);

        //        var created = await _dataContext.SaveChangesAsync();
        //        return created;
        //    }

        //    //_dataContext.Entry(cartItem).CurrentValues.SetValues(cartItem);
        //    return 0;
        //}

        public async Task<bool> DeleteCartItemAsync(Int64 CartItemId)
        {
            var CartItem = await GetCarItemtByIdAsync(CartItemId);

            if (CartItem == null)
                return false;

            _dataContext.cartItems.Remove(CartItem);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> DeleteCartItemByUserAsync(string UserId)
        {
            var cart = _dataContext.Cart.SingleOrDefault(x=>x.UserId == UserId);
            var cartItem = _dataContext.cartItems.Where(x => x.CartId == cart.Id).ToList();

            if (cartItem == null)
                return false;

            _dataContext.cartItems.RemoveRange(cartItem);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }


        public async Task<CartItem> GetCarItemtByValueAsync(long CartId, long? ProductId)
        {
            return await _dataContext.cartItems.SingleOrDefaultAsync(x => x.CartId == CartId && x.ProductId == ProductId);
        }

      
    }
}
