using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1;
using ThreeSoftECommAPI.Contracts.V1.Requests.EComm.OrderReq;
using ThreeSoftECommAPI.Domain.EComm;
using ThreeSoftECommAPI.Services.EComm.CartItemsServ;
using ThreeSoftECommAPI.Services.EComm.CartServ;
using ThreeSoftECommAPI.Services.EComm.CouponServ;
using ThreeSoftECommAPI.Services.EComm.OrderItemServ;
using ThreeSoftECommAPI.Services.EComm.OrderServ;
using ThreeSoftECommAPI.Services.EComm.ProductServ;

namespace ThreeSoftECommAPI.Controllers.V1
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;
        private readonly ICartService _cartService;
        private readonly ICartItemService _cartItemService;
        private readonly IProductService _productService;
        private readonly ICouponServices _couponServices;

        public OrderController(IOrderService orderService,
                               IOrderItemService orderItemService,
                               ICartService cartService,
                               ICartItemService cartItemService,
                               IProductService productService,
                               ICouponServices couponServices)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
            _cartService = cartService;
            _cartItemService = cartItemService;
            _productService = productService;
            _couponServices = couponServices;
        }

        [HttpPost(ApiRoutes.OrderRoute.Create)]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest orderRequest)
        {
            try
            {
                var order = new Order
                {
                    UserId = orderRequest.UserId,
                    DeliveryMethod = orderRequest.DeliveryMethod,
                    PaymentMethod = orderRequest.PaymentMethod,
                    CouponId = orderRequest.CouponId,
                    UserAddressesId = orderRequest.UserAddressesId,
                    Status = 1,
                    CreatedAt = DateTime.Now
                };

                var AddOrder = await _orderService.CreateOrderAsync(order);

                if (AddOrder == 1)
                {
                    var cartItem = await _cartItemService.GetCartItemAsync(orderRequest.CartId);

                    foreach (var item in cartItem)
                    {
                        var orderItem = new OrderItems
                        {
                            OrderId = order.Id,
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            Price = (item.product.SalePrice != 0 ? item.product.SalePrice : item.product.Price),
                            Total = (item.product.SalePrice != 0 ? item.product.SalePrice : item.product.Price) * item.Quantity,
                            CreatedAt = DateTime.Now
                        };

                        var addOrderItem = await _orderItemService.CreateOrderItemAsync(orderItem);

                        if (addOrderItem == 1)
                        {
                            await _cartItemService.DeleteCartItemAsync(item.Id);
                        }
                    }

                    if(orderRequest.CouponId != null)
                    {
                        if(orderRequest.CouponId != 0)
                        {
                            SubTrackNumberOfPromoCode(Convert.ToInt32(orderRequest.CouponId));
                        }
                    }

                    var resp = new
                    {
                        status = Ok().StatusCode,
                        message = "Successfully add order"
                    };

                    return Ok(resp);
                }

                return BadRequest(new
                {
                    status = BadRequest().StatusCode,
                    message = "internal server error"
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    status = BadRequest().StatusCode,
                    message = ex.Message
                });
            }
        }

        public void SubTrackNumberOfPromoCode(Int32 CouponId)
        {
            _couponServices.SubTrackNumberOfPromoCode(CouponId);
        }
        [HttpGet(ApiRoutes.OrderRoute.MyOrder)]
        public async Task<IActionResult> GetMyOrder(string userId)
        {
            return Ok(await _orderService.GetMyOrdersAsync(userId));
        }

        [HttpGet(ApiRoutes.OrderRoute.OrderStatus)]
        public async Task<IActionResult> GetOrderStatus(string userId)
        {
            var orderStatus = await _orderService.GetOrderStatusAsync(userId);
            if (orderStatus.OrderId != 0)
            {
                return Ok(orderStatus);
            }
            else
            {
                return NotFound(new
                {
                    status = NotFound().StatusCode,
                    message = "Not Found"
                });
            }
        }

        [HttpGet(ApiRoutes.OrderRoute.OrdersForAdmin)]
        public async Task<IActionResult> GetOrderForAdmin([FromRoute] Int32 status)
        {
            return Ok(await _orderService.GetOrdersForAdmin(status));
        }

        [HttpGet(ApiRoutes.OrderRoute.orderItems)]
        public async Task<IActionResult> GetOrderItemsForAdmin([FromRoute] Int64 orderId)
        {
            return Ok(await _orderItemService.GetOrderItemForAdmin(orderId));
        }

        [HttpGet(ApiRoutes.OrderRoute.CheckPreviousOrder)]
        public async Task<IActionResult> CheckPreviousOrder([FromRoute] string userId)
        {
            var Check = await _orderService.CheckPreviousOrder(userId);

             if (!Check)
            {
                return Conflict(new
                {
                    status = Conflict().StatusCode,
                    message = "You have a previous incomplete order"
                });
            }
            return Ok(new {

                status = Ok().StatusCode,
                message = ""
            });
        }

        [HttpPost(ApiRoutes.OrderRoute.ReOrder)]
        public async Task<IActionResult> ReOrder([FromBody] ReorderRequest reorderRequest)
        {
            try
            {
                var Cart = await _cartService.GetCartByUserIdAsync(reorderRequest.UserId);
                var orderItems = await _orderItemService.GetOrderItems(reorderRequest.OrderId);
                var ListUnActive = new List<Product>();
                if (orderItems != null)
                {
                    for (int i = 0; i < orderItems.Count; i++)
                    {
                        var CheckProductActive = await _productService.CheckProductsIfActive(orderItems[i].ProductId);

                        if (!CheckProductActive)
                        {
                            var product = await _productService.GetProductByIdAsync(orderItems[i].ProductId);
                            ListUnActive.Add(product);
                        }
                    }

                    if (ListUnActive.Count == 0)
                    {
                        await _cartItemService.DeleteCartItemByUserAsync(reorderRequest.UserId);
                        for (int i = 0; i < orderItems.Count; i++)
                        {

                            var CartItem = new CartItem
                            {
                                CartId = Cart.Id,
                                ProductId = orderItems[i].ProductId,
                                Quantity = orderItems[i].Quantity,
                                CreatedAt = DateTime.Now
                            };

                            var createItem = await _cartItemService.CreateCartItemAsync(CartItem);
                        }

                        return Ok(await _cartItemService.GetCartItemByUserIdAsync(reorderRequest.UserId));
                    }
                    else
                    {
                        return NotFound(new
                        {
                            status = NotFound().StatusCode,
                            message = "There are inactive products",
                            Inactive_Products = ListUnActive
                        });
                    }
                }
                return NotFound(new
                {
                    status = NotFound().StatusCode,
                    message = "Not Found"
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    status = BadRequest().StatusCode,
                    message = ex.Message
                });
            }
        }

        [HttpPost(ApiRoutes.OrderRoute.ReOrderActive)]
        public async Task<IActionResult> ReOrderActive([FromBody] ReorderRequest reorderRequest)
        {
            try
            {
                var Cart = await _cartService.GetCartByUserIdAsync(reorderRequest.UserId);
                var orderItems = await _orderItemService.GetOrderItems(reorderRequest.OrderId);

                if (orderItems != null)
                {
                    await _cartItemService.DeleteCartItemByUserAsync(reorderRequest.UserId);

                    for (int i = 0; i < orderItems.Count; i++)
                    {
                        var CheckProductActive = await _productService.CheckProductsIfActive(orderItems[i].ProductId);

                        if (CheckProductActive)
                        {
                            var CartItem = new CartItem
                            {
                                CartId = Cart.Id,
                                ProductId = orderItems[i].ProductId,
                                Quantity = orderItems[i].Quantity,
                                CreatedAt = DateTime.Now
                            };

                            var createItem = await _cartItemService.CreateCartItemAsync(CartItem);
                        }
                    }

                    return Ok(await _cartItemService.GetCartItemByUserIdAsync(reorderRequest.UserId));
                }
                return NotFound(new
                {
                    status = NotFound().StatusCode,
                    message = "Not Found"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = BadRequest().StatusCode,
                    message = ex.Message
                });
            }
        }

        [HttpPost(ApiRoutes.OrderRoute.Update)]
        public async Task<IActionResult> Update([FromBody] UpdateOrderRequest updateOrder)
        {
            try
            {
                var updated = await _orderService.UpdateOrderAsync(updateOrder.orderId, updateOrder.status, updateOrder.rejectReason);
                if (updated == 1)
                {
                    return Ok(new
                    {
                        status = Ok().StatusCode,
                        message = "order update Successfully"
                    });
                }

                return NotFound(new
                {
                    status = NotFound().StatusCode,
                    message = "Internal Server error"
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    status = BadRequest().StatusCode,
                    message = ex.Message
                });
            }
        }

        [HttpGet(ApiRoutes.OrderRoute.OrderStatusChart)]
        public async Task<IActionResult> OrderStatusChart()
        {
            return Ok(await _orderService.OrderStatusChart());
        }

        [HttpGet(ApiRoutes.OrderRoute.orderReport)]
        public async Task<IActionResult> OrderReport([FromQuery] DateTime fromdate,[FromQuery] DateTime todate)
        {
            return Ok(await _orderService.OrderReport(fromdate,todate));
        }

        [HttpGet(ApiRoutes.OrderRoute.orderStatusCount)]
        public async Task<IActionResult> OrderStatusCount()
        {
            return Ok(await _orderService.OrderStatusCount());
        }


    }
}
