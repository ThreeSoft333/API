using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1;
using ThreeSoftECommAPI.Contracts.V1.Requests.EComm.CartItem;
using ThreeSoftECommAPI.Contracts.V1.Responses.EComm;
using ThreeSoftECommAPI.Domain.EComm;
using ThreeSoftECommAPI.Services.EComm.CartItemsServ;
using ThreeSoftECommAPI.Services.EComm.CartServ;

namespace ThreeSoftECommAPI.Controllers.V1
{
    public class CartController:Controller
    {
        private readonly ICartService _cartService;
        private readonly ICartItemService _cartItemService;
        public CartController(ICartService cartService, ICartItemService cartItemService)
        {
            _cartService = cartService;
            _cartItemService = cartItemService;
        }

        [HttpGet(ApiRoutes.CartRoute.GetCartItemByUser)]
        public async Task<IActionResult> GetCartItemByUser([FromRoute] string userId)
        {
            return Ok(await _cartItemService.GetCartItemByUserIdAsync(userId));
        }

        [HttpPost(ApiRoutes.CartRoute.Create)]
        public async Task<IActionResult> Create([FromBody] CreateCartItemRequest cartItemRequest)
        {
            var checkCartExist = await _cartService.GetCartByUserIdAsync(cartItemRequest.UserId);

            if (checkCartExist == null)
            {
                var CartId = await CreateCart(cartItemRequest.UserId);

                if (CartId > 0)
                {
                    var CartItemId = await CreateCartItem(CartId, cartItemRequest);

                    if (CartItemId > 0)
                    {
                        return Ok(new
                        {
                            cartId = CartId,
                            cartItemId = CartItemId,
                            status = Ok().StatusCode,
                            message = "Successfully Add"
                        });
                    }
                }
            }
            else
            {
                var checkCartItemExist = await _cartItemService.GetCarItemtByValueAsync(checkCartExist.Id,
                    cartItemRequest.ProductId);

                if (checkCartItemExist == null)
                {
                    var CartItemId = await CreateCartItem(checkCartExist.Id, cartItemRequest);

                    if (CartItemId > 0)
                    {
                        return Ok(new
                        {
                            cartId = checkCartExist.Id,
                            cartItemId = CartItemId,
                            status = Ok().StatusCode,
                            message = "Successfully Add"
                        });
                    }
                }
                else
                {
                    var _cartItem = new CartItem()
                    {
                        Id = checkCartItemExist.Id,
                        CartId = checkCartItemExist.CartId,
                        ProductId = checkCartItemExist.ProductId,
                        Quantity = checkCartItemExist.Quantity
                    };

                    var quantity = _cartItem.Quantity + cartItemRequest.Quantity;
                    
                    var updateCartItem = await UpdateCartItem(_cartItem.Id, quantity);

                    if (updateCartItem == 1)
                    {
                        return Ok(new
                        {
                            cartId = checkCartExist.Id,
                            cartItemId = checkCartItemExist.Id,
                            status = Ok().StatusCode,
                            message = "Successfully Add"
                        });
                    }
                }
            }
            return NotFound(new ErrorResponse
            {
                message = "Not Found",
                status = NotFound().StatusCode
            });
        }

        public async Task<Int64> CreateCart(string UserId)
        {
            var Cart = new Cart
            {
                UserId = UserId,
                CreatedAt = DateTime.Now
            };

            var status = await _cartService.CreateCartAsync(Cart);

            if(status == 1)
            {
                return Cart.Id;
            }

            return 0;

            
        }

        public async Task<Int64> CreateCartItem(Int64 CartId, CreateCartItemRequest cartItemRequest)
        {
            var CartItem = new CartItem
            {
                CartId = CartId,
                ProductId = cartItemRequest.ProductId,
                Quantity = cartItemRequest.Quantity,
                CreatedAt = DateTime.Now
            };

            var createItem = await _cartItemService.CreateCartItemAsync(CartItem);

            if (createItem > 0)
                return CartItem.Id;
            return 0;
        }

        public async Task<int> UpdateCartItem(Int64 CartItemId,Int32 quantity)
        {
            var item = await _cartItemService.GetCarItemtByIdAsync(CartItemId);

            if (item != null)
            {
                var CartItem = new CartItem
                {
                    Id = CartItemId,
                    CartId = item.CartId,
                    ProductId = item.ProductId,
                    Quantity = quantity,
                    UpdatedAt = DateTime.Now
                };

                var UpdateCartItem = await _cartItemService.UpdateCartItemAsync(CartItem);

                return UpdateCartItem;
            }
            return 0;
        }

        [HttpPost(ApiRoutes.CartRoute.Update)]
        public async Task<IActionResult> Update([FromRoute] Int64 cartItemId, [FromBody] UpdateCartItemRequest updateCartItem)
        {
            var item = await _cartItemService.GetCarItemtByIdAsync(cartItemId);

            if (item != null)
            {
                var CartItem = new CartItem
                {
                    Id = cartItemId,
                    CartId = item.CartId,
                    ProductId = item.ProductId,
                    Quantity = updateCartItem.Quantity,
                    UpdatedAt = DateTime.Now
                };

                var status = await _cartItemService.UpdateCartItemAsync(CartItem);


                if (status == 1)
                    return Ok(CartItem);
            }
            return NotFound(new ErrorResponse
            {
                message = "Not Found",
                status = NotFound().StatusCode
            });
        }

        [HttpDelete(ApiRoutes.CartRoute.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Int64 cartItemId)
        {
            var deleted = await _cartItemService.DeleteCartItemAsync(cartItemId);

            if (deleted)
                return Ok(new SuccessResponse
                {
                    message = "Successfully Deleted",
                    status = Ok().StatusCode
                });
            return NotFound(new ErrorResponse
            {
                message = "Not Found",
                status = NotFound().StatusCode
            });
        }
    }
}
