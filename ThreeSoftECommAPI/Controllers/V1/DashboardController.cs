﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1;
using ThreeSoftECommAPI.Contracts.V1.Responses.Charts;
using ThreeSoftECommAPI.Services.EComm.AdvertisingServ;
using ThreeSoftECommAPI.Services.EComm.CartItemsServ;
using ThreeSoftECommAPI.Services.EComm.CategoryServ;
using ThreeSoftECommAPI.Services.EComm.OffersServ;
using ThreeSoftECommAPI.Services.EComm.OrderItemServ;
using ThreeSoftECommAPI.Services.EComm.OrderServ;
using ThreeSoftECommAPI.Services.EComm.ProductServ;
using ThreeSoftECommAPI.Services.EComm.UserNotifCountServ;

namespace ThreeSoftECommAPI.Controllers.V1
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DashboardController : Controller
    {
        private readonly ICategoryService _CategoryService;
        private readonly IAdvertisingService _AdvertisingService;
        private readonly IProductService _ProductService;
        private readonly IOffersService _OffersService;
        private readonly ICartItemService _cartItemService;
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;
        private readonly IUserNotificationCountService _userNotificationCountService;


        public DashboardController(ICategoryService CategoryService,
                                   IAdvertisingService advertisingService,
                                   IProductService productService,
                                   IOffersService offersService,
                                   ICartItemService cartItemService,
                                   IOrderService orderService,
                                   IOrderItemService orderItemService,
                                   IUserNotificationCountService userNotificationCountService)
        {
            _CategoryService = CategoryService;
            _AdvertisingService = advertisingService;
            _ProductService = productService;
            _OffersService = offersService;
            _cartItemService = cartItemService;
            _orderService = orderService;
            _orderItemService = orderItemService;
            _userNotificationCountService = userNotificationCountService;
        }

        [HttpGet(ApiRoutes.Dashboard.Get)]
        public async Task<IActionResult> GetAll([FromQuery] string UserId)
        {
            var advertize = await _AdvertisingService.GetAdvertisingAsync(1);
            var CatgTop = await _CategoryService.GetCategoriesTopAsync();
            var ProdMostRecent = await _ProductService.GetProductsMostRecentAsync(UserId, 8);
            var MostWanted = await _ProductService.GetProductsMostWantedAsync(UserId, 8);
            var TopRated = await _ProductService.GetProductsTopRatedAsync(UserId, 8);
            var Offers = await _OffersService.GetOffersTopAsync(UserId, 8);
            var CartItem = await _cartItemService.GetCartItemByUserIdAsync(UserId);
            var OrderStatus = await _orderService.GetLastOrderStatusNo(UserId);
            var userNotifCount = _userNotificationCountService.getCountByUser(UserId);

            int ItemCount = 0;
            for (int i = 0; i < CartItem.Count; i++)
            {
                ItemCount += CartItem[i].Quantity;
            }
            var obj = new
            {
                Advertize = advertize,
                Category_Tob = CatgTop,
                Product_Most_Recent = ProdMostRecent,
                Product_Most_Wanted = MostWanted,
                Product_TopRated = TopRated,
                Offers = Offers,
                Cart_Items_Count = ItemCount,
                Order_Status = OrderStatus,
                notification_count = userNotifCount
            };

            return Ok(obj);
        }

        [HttpGet(ApiRoutes.Dashboard.drillDownChart)]
        public async Task<IActionResult> getdrillDownChartData([FromQuery] string lang)
        {
            DrillDownChartCategory drillDownChartCategories = new DrillDownChartCategory();
            drillDownChartCategories.categoryPcntCharts = await _orderItemService.getCategoryPercent(lang);
            drillDownChartCategories.subCategoryPcntCharts = await _orderItemService.getSubCategoryPercent(lang);

            return Ok(drillDownChartCategories);
        }

    }
}
