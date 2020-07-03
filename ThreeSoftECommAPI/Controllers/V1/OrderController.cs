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
using ThreeSoftECommAPI.Services.EComm.OrderItemServ;
using ThreeSoftECommAPI.Services.EComm.OrderServ;

namespace ThreeSoftECommAPI.Controllers.V1
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;
        private readonly ICartService _cartService;
        private readonly ICartItemService _cartItemService;

        public OrderController(IOrderService orderService, IOrderItemService orderItemService, ICartService cartService, ICartItemService cartItemService)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
            _cartService = cartService;
            _cartItemService = cartItemService;
        }

        [HttpPost(ApiRoutes.OrderRoute.Create)]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest orderRequest)
        {
            var order = new Order
            {
                UserId = orderRequest.UserId,
                DeliveryMethod = orderRequest.DeliveryMethod,
                PaymentMethod = orderRequest.PaymentMethod,
                CouponId = orderRequest.CouponId,
                UserAddressesId = orderRequest.UserAddressesId,
                Status = 0,
                CreatedAt = DateTime.Now
            };

            var AddOrder = await _orderService.CreateOrderAsync(order);

            if(AddOrder == 1)
            {
                var cartItem = await _cartItemService.GetCartItemAsync(orderRequest.CartId);

                foreach(var item in cartItem)
                {
                    var orderItem = new OrderItems
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        CreatedAt = DateTime.Now
                    };

                    var addOrderItem = await _orderItemService.CreateOrderItemAsync(orderItem);

                    if(addOrderItem == 1)
                    {
                        await _cartItemService.DeleteCartItemAsync(item.Id);
                    }
                }

                var resp = new
                {
                    status = Ok().StatusCode,
                    message = "Successfully add order"
                };

                return Ok(resp);
            }
            else if (AddOrder == -1)
            {
                return Conflict(new
                {
                    status = Conflict().StatusCode,
                    message = "You have a previous incomplete order"
                });
            }
            return BadRequest(new
            {
                status = BadRequest().StatusCode,
                message = "internal server error"
            });
        }

        [HttpGet(ApiRoutes.OrderRoute.MyOrder)]
        public async Task<IActionResult> GetMyOrder(string userId)
        {
            return Ok(await _orderService.GetMyOrdersAsync(userId));
        }

        [HttpGet(ApiRoutes.OrderRoute.OrderStatus)]
        public async Task<IActionResult> GetOrderStatus(string userId)
        {
            return Ok(await _orderService.GetOrderStatusAsync(userId));
        }
    }
}
