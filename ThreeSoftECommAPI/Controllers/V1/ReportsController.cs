using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1;
using ThreeSoftECommAPI.Services.EComm.OrderItemServ;
using ThreeSoftECommAPI.Services.EComm.OrderServ;
using ThreeSoftECommAPI.Services.EComm.ProductServ;

namespace ThreeSoftECommAPI.Controllers.V1
{
    public class ReportsController:Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;
        private readonly IProductService _productService;

        public ReportsController(IOrderService orderService, 
                                 IOrderItemService orderItemService,
                                 IProductService productService)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
            _productService = productService;
        }

        [HttpGet(ApiRoutes.ReportsRout.orderReport)]
        public async Task<IActionResult> OrderReport([FromQuery] DateTime fromdate, [FromQuery] DateTime todate)
        {
            return Ok(await _orderService.OrderReport(fromdate, todate));
        }

        [HttpGet(ApiRoutes.ReportsRout.orderProductReport)]
        public async Task<IActionResult> orderProductReport([FromQuery] long prodId, [FromQuery] DateTime fromdate, [FromQuery] DateTime todate)
        {
            return Ok(await _orderItemService.OrderProductReport(prodId, fromdate, todate));
        }

        [HttpGet(ApiRoutes.ReportsRout.ProductBySubCategoryReport)]
        public async Task<IActionResult> ProductBySubCategoryReport([FromQuery] long subCatgId)
        {
            return Ok(await _productService.ProductsBySubCategoryReport(subCatgId));
        }
    }
}
