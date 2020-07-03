﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1;
using ThreeSoftECommAPI.Services.EComm.AdvertisingServ;
using ThreeSoftECommAPI.Services.EComm.CategoryServ;
using ThreeSoftECommAPI.Services.EComm.OffersServ;
using ThreeSoftECommAPI.Services.EComm.ProductServ;

namespace ThreeSoftECommAPI.Controllers.V1
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DashboardController:Controller
    {
        private readonly ICategoryService _CategoryService;
        private readonly IAdvertisingService _AdvertisingService;
        private readonly IProductService _ProductService;
        private readonly IOffersService _OffersService;

        public DashboardController(ICategoryService CategoryService, IAdvertisingService advertisingService, 
            IProductService productService,IOffersService offersService)
        {
            _CategoryService = CategoryService;
            _AdvertisingService = advertisingService;
            _ProductService = productService;
            _OffersService = offersService;
        }

        [HttpGet(ApiRoutes.Dashboard.Get)]
        public async Task<IActionResult> GetAll([FromQuery] string UserId)
        {
            var advertize = await _AdvertisingService.GetAdvertisingAsync(1);
            var CatgTop = await _CategoryService.GetCategoriesTopAsync();
            var ProdMostRecent = await _ProductService.GetProductsMostRecentAsync(UserId,8);
            var MostWanted = await _ProductService.GetProductsMostWantedAsync(UserId,8);
            var TopRated = await _ProductService.GetProductsTopRatedAsync(UserId,8);
            var Offers = await _OffersService.GetOffersTopAsync(UserId,8);

            var obj = new
            {
                Advertize = advertize,
                Category_Tob = CatgTop,
                Product_Most_Recent = ProdMostRecent,
                Product_Most_Wanted = MostWanted,
                Product_TopRated = TopRated,
                Offers = Offers
            };

            return Ok(obj);
        }
    }
}