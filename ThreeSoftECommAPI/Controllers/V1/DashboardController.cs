using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1;
using ThreeSoftECommAPI.Services.EComm.AdvertisingServ;
using ThreeSoftECommAPI.Services.EComm.CartItemsServ;
using ThreeSoftECommAPI.Services.EComm.CategoryServ;
using ThreeSoftECommAPI.Services.EComm.OffersServ;
using ThreeSoftECommAPI.Services.EComm.ProductServ;

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

        public DashboardController(ICategoryService CategoryService,
                                   IAdvertisingService advertisingService,
                                   IProductService productService,
                                   IOffersService offersService,
                                   ICartItemService cartItemService)
        {
            _CategoryService = CategoryService;
            _AdvertisingService = advertisingService;
            _ProductService = productService;
            _OffersService = offersService;
            _cartItemService = cartItemService;
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
                Cart_Items_Count = ItemCount
            };

            return Ok(obj);
        }

        [HttpPost(ApiRoutes.Dashboard.Charge)]
        public IActionResult Charge(string StripeEmail)
        {
            var customers = new CustomerService();
            var Charges = new ChargeService();

          

            var options1 = new PaymentMethodCreateOptions
            {
                Type = "card",
                Card = new PaymentMethodCardCreateOptions
                {
                    Number = "4242424242424242",
                    ExpMonth = 3,
                    ExpYear = 2021,
                    Cvc = "314",
                },
            };

            var service1 = new PaymentMethodService();
            service1.Create(options1);

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = StripeEmail,
                Name = "husam",
                Phone = "00962788966075",
                PaymentMethod = options1.PaymentMethod,
                


            });

          

            var options = new PaymentIntentCreateOptions
            {
                Amount = 50000,
                Currency = "USD",
                Customer = customer.Id,
                PaymentMethodTypes = new List<string> {
                "card"
  },
            };

            var service = new PaymentIntentService();
            var intent = service.Create(options);




            //var Charge = Charges.Create(new ChargeCreateOptions
            //{
            //    Amount = 500,
            //    Description = "Test Payment",
            //    Currency = "USD",
            //    Customer = customer.Id,
            //    ReceiptEmail = StripeEmail,

            //});

            //if(Charge.Status == "succeeded")
            //{
            //    string BalanceTransactionId = Charge.BalanceTransactionId;
            //}

            return View();
        }
    }
}
