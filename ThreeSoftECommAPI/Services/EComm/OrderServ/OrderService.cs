﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1.Responses.EComm;
using ThreeSoftECommAPI.Data;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.OrderServ
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _dataContext;
        public OrderService(ApplicationDbContext dbContext)
        {
            _dataContext = dbContext;
        }
        public async Task<int> CreateOrderAsync(Order order)
        {
            var checkBrevOrderStatus = await _dataContext.Orders
                .Where(x => x.UserId == order.UserId && x.Status != 4).FirstOrDefaultAsync();

            if (checkBrevOrderStatus == null)
            {
                await _dataContext.Orders.AddAsync(order);
                var created = await _dataContext.SaveChangesAsync();
                return created;
            }
            return -1;
        }

        public Task<bool> DeleteOrderAsync(long OrderId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MyOrderResponse>> GetMyOrdersAsync(string UserId)
        {

            var query = await (from o in _dataContext.Orders
                               where o.UserId == UserId


                               select new MyOrderResponse
                               {
                                   OrderId = o.Id,
                                   OrderStatus = o.Status,
                                   OrderDate = o.CreatedAt,
                                   ProductCount = _dataContext.OrderItems.Where(x => x.OrderId == o.Id).Count(),
                                   Total = _dataContext.Orders
                                   .Join(_dataContext.OrderItems,o => o.Id,oi => oi.OrderId ,(o,oi) => new {o,oi})
                                   .Join(_dataContext.product, ooi => ooi.oi.ProductId, p => p.Id, (ooi, p) => new { ooi, p })
                                   .Where(x => x.ooi.o.UserId == UserId).Select(x => x.p.SalePrice != 0 ? x.p.SalePrice: x.p.Price).Sum(),
                                   userAddresse = _dataContext.UserAddresses.SingleOrDefault(x => x.Id == o.UserAddressesId),
                                   orderItems = ((List<OrderItems>)(from i in _dataContext.OrderItems
                                                                    where i.OrderId == o.Id
                                                                    join prod in _dataContext.product on i.ProductId equals prod.Id
                                                                    join col in _dataContext.ProductColors on prod.colorId equals col.Id into prodcol
                                                                    from c in prodcol.DefaultIfEmpty()
                                                                    join size in _dataContext.ProductSizes on prod.sizeId equals size.Id into prodsize
                                                                    from s in prodsize.DefaultIfEmpty()

                                                                    select new OrderItems
                                                                    {
                                                                        Id = i.Id,
                                                                        Quantity = i.Quantity,
                                                                        product = new Product
                                                                        {
                                                                            Id = prod.Id,
                                                                            ArabicName = prod.ArabicName,
                                                                            EnglishName = prod.EnglishName,
                                                                            ArabicDescription = prod.ArabicDescription,
                                                                            EnglishDescription = prod.EnglishDescription,
                                                                            Condition = prod.Condition,
                                                                            Material = prod.Material,
                                                                            Price = prod.Price,
                                                                            ImgUrl = prod.ImgUrl,
                                                                            colors = c,
                                                                            size = s
                                                                        }
                                                                    })
                                                                         )


                               }).ToListAsync();

            return query;
        }

        public Task<List<Order>> GetOrdersAsync(string UserId)
        {
            throw new NotImplementedException();
        }

        public async Task<MyOrderResponse> GetOrderStatusAsync(string UserId)
        {
            var query = await (from p in _dataContext.Orders
                               where p.UserId == UserId
                               orderby p.Id, p.Id descending
                               select new MyOrderResponse

                               {
                                   OrderId = p.Id,
                                   OrderStatus = p.Status,
                                   OrderDate = p.CreatedAt,
                                   ProductCount = _dataContext.OrderItems.Where(x => x.OrderId == p.Id).Count(),
                                   Total = _dataContext.Orders
                                   .Join(_dataContext.OrderItems, o => o.Id, oi => oi.OrderId, (o, oi) => new { o, oi })
                                   .Join(_dataContext.product, ooi => ooi.oi.ProductId, p => p.Id, (ooi, p) => new { ooi, p })
                                   .Where(x => x.ooi.o.UserId == UserId).Select(x => x.p.SalePrice != 0 ? x.p.SalePrice : x.p.Price).Sum(),
                                   userAddresse = _dataContext.UserAddresses.SingleOrDefault(x => x.Id == p.UserAddressesId),
                                   orderItems = ((List<OrderItems>)(from i in _dataContext.OrderItems
                                                                    join prod in _dataContext.product on i.ProductId equals prod.Id
                                                                    where i.OrderId == p.Id

                                                                    select new OrderItems
                                                                    {
                                                                        Id = i.Id,
                                                                        Quantity = i.Quantity,
                                                                        product = prod
                                                                    })
                                                                         )


                               }).FirstOrDefaultAsync();

            return query;
        }
    }
}
