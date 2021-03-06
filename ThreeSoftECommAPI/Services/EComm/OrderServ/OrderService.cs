﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1.Responses.Charts;
using ThreeSoftECommAPI.Contracts.V1.Responses.EComm;
using ThreeSoftECommAPI.Contracts.V1.Responses.Reports;
using ThreeSoftECommAPI.Data;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.OrderServ
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _dataContext;
        private readonly ApplicationDbContext _dataContext2;
        public OrderService(ApplicationDbContext dbContext, ApplicationDbContext dbContext2)
        {
            _dataContext = dbContext;
            _dataContext2 = dbContext2;
        }
        public async Task<int> CreateOrderAsync(Order order)
        {
            await _dataContext.Orders.AddAsync(order);
            var created = await _dataContext.SaveChangesAsync();
            return created;
        }

        public Task<bool> DeleteOrderAsync(long OrderId)
        {
            throw new NotImplementedException();
        }

        public decimal CalculateGrandTotal(Int64 OrderId)
        {
            var order = _dataContext2.Orders.SingleOrDefault(x => x.Id == OrderId);
            var coupon = _dataContext2.Coupons.SingleOrDefault(x => x.Id == order.CouponId);
            var orderItems = _dataContext2.OrderItems.Where(x => x.OrderId == order.Id).ToList();
            decimal grandTotal = 0;
            if (coupon.Type == 1)
            {
                grandTotal = orderItems.Select(x => x.Total).Sum() - coupon.Amount;
            }
            else
            {
                decimal percnDisc = Convert.ToDecimal(coupon.Percentage * 0.01);
                grandTotal = orderItems.Select(x => x.Total).Sum() * percnDisc;
            }

            return grandTotal;
        }
        public async Task<List<MyOrderResponse>> GetMyOrdersAsync(string UserId)
        {
            var orders = await _dataContext.Orders.Where(x => x.UserId == UserId && x.Status > 4).ToListAsync();

            List<MyOrderResponse> _lstOrderResp = new List<MyOrderResponse>();
            for (int j = 0; j < orders.Count(); j++)
            {
                var copoun = await _dataContext.Coupons.SingleOrDefaultAsync(x => x.Id == orders[j].CouponId);
                var orderItems = await _dataContext.OrderItems.Where(x => x.OrderId == orders[j].Id).ToListAsync();
                var userAddresse = _dataContext.UserAddresses.SingleOrDefault(x => x.Id == orders[j].UserAddressesId);
                var orderItemQuery = await (from i in _dataContext.OrderItems
                                            where i.OrderId == orders[j].Id
                                            join prod in _dataContext.product on i.ProductId equals prod.Id
                                            join col in _dataContext.ProductColors on prod.colorId equals col.Id into prodcol
                                            from c in prodcol.DefaultIfEmpty()
                                            join size in _dataContext.ProductSizes on prod.sizeId equals size.Id into prodsize
                                            from s in prodsize.DefaultIfEmpty()

                                            select new OrderItems
                                            {
                                                Id = i.Id,
                                                Quantity = i.Quantity,
                                                Price = i.Price,
                                                Total = i.Total,
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
                                                    productColor = c,
                                                    productSize = s
                                                }
                                            }).ToListAsync();

                decimal grandTotal = 0;
                decimal discount = 0;
                var Ordertotal = orderItems.Select(x => x.Total).Sum();
                if (copoun != null)
                {
                    if (copoun.Type == 1)
                    {
                        grandTotal = Ordertotal - copoun.Amount;
                    }
                    else
                    {
                        decimal percnDisc = Convert.ToDecimal(copoun.Percentage * 0.01);
                        discount = Ordertotal * percnDisc;
                        grandTotal = Ordertotal - discount;
                    }
                }
                else
                {
                    grandTotal = Ordertotal;
                }

                var myOrderResponse = new MyOrderResponse
                {
                    OrderId = orders[j].Id,
                    OrderStatus = orders[j].Status,
                    OrderDate = orders[j].CreatedAt.ToString("dd/MM/yyyy hh:mm:tt"),
                    CouponId = orders[j].CouponId,
                    ProductCount = orderItems.Count(),
                    Total = Ordertotal,
                    discount = discount,
                    GrandTotal = grandTotal,
                    userAddresse = userAddresse,
                    orderItems = (List<OrderItems>)orderItemQuery

                };

                _lstOrderResp.Add(myOrderResponse);
            }
            //var query = await (from o in _dataContext.Orders
            //                   where o.UserId == UserId && o.Status == 4
            //                   join cop in _dataContext.Coupons on o.CouponId equals cop.Id into ordCop
            //                   from orderCoupon in ordCop.DefaultIfEmpty()
            //                 



            //                   select new MyOrderResponse
            //                   {
            //                       OrderId = o.Id,
            //                       OrderStatus = o.Status,
            //                       OrderDate = o.CreatedAt.ToString("dd/MM/yyyy hh:mm:ss"),
            //                       CouponId = o.CouponId,
            //                       ProductCount = _dataContext.OrderItems.Where(x => x.OrderId == o.Id).Count(),
            //                       Total =





            //CalculateGrandTotal(Convert.ToInt64(o.Id)),

            //                       userAddresse = _dataContext.UserAddresses.SingleOrDefault(x => x.Id == o.UserAddressesId),
            //                       orderItems = ((List<OrderItems>)(from i in _dataContext.OrderItems
            //                                                        where i.OrderId == o.Id
            //                                                        join prod in _dataContext.product on i.ProductId equals prod.Id
            //                                                        join col in _dataContext.ProductColors on prod.colorId equals col.Id into prodcol
            //                                                        from c in prodcol.DefaultIfEmpty()
            //                                                        join size in _dataContext.ProductSizes on prod.sizeId equals size.Id into prodsize
            //                                                        from s in prodsize.DefaultIfEmpty()

            //                                                        select new OrderItems
            //                                                        {
            //                                                            Id = i.Id,
            //                                                            Quantity = i.Quantity,
            //                                                            Price = i.Price,
            //                                                            Total = i.Total,
            //                                                            product = new Product
            //                                                            {
            //                                                                Id = prod.Id,
            //                                                                ArabicName = prod.ArabicName,
            //                                                                EnglishName = prod.EnglishName,
            //                                                                ArabicDescription = prod.ArabicDescription,
            //                                                                EnglishDescription = prod.EnglishDescription,
            //                                                                Condition = prod.Condition,
            //                                                                Material = prod.Material,
            //                                                                Price = prod.Price,
            //                                                                ImgUrl = prod.ImgUrl,
            //                                                                colors = c,
            //                                                                size = s
            //                                                            }
            //                                                        })
            //                                                             )


            //                   }).ToListAsync();

            return _lstOrderResp;
        }

        public async Task<List<Order>> GetOrdersForAdmin(int status)
        {
            if (status == 4)
            {
                return await _dataContext.Orders
                    .Include(x => x.User)
                    .Include(x => x.userAddresses)
                    .Where(x => x.Status == 4 || x.Status == 6).ToListAsync();
            }
            else
            {
                return await _dataContext.Orders.Include(x => x.User).Include(x => x.userAddresses)
                    .Where(x => x.Status == status).ToListAsync();
            }
        }

        public Task<List<Order>> GetOrdersAsync(string UserId)
        {
            throw new NotImplementedException();
        }

        public async Task<MyOrderResponse> GetOrderStatusAsync(string UserId)
        {
            var order = await _dataContext.Orders.Where(x => x.UserId == UserId).OrderByDescending(x => x.Id).FirstOrDefaultAsync();

            var myOrderResponse = new MyOrderResponse();

            if (order != null)
            {
                var copoun = await _dataContext.Coupons.SingleOrDefaultAsync(x => x.Id == order.CouponId);
                var orderItems = await _dataContext.OrderItems.Where(x => x.OrderId == order.Id).ToListAsync();
                var userAddresse = _dataContext.UserAddresses.SingleOrDefault(x => x.Id == order.UserAddressesId);
                var orderItemQuery = await (from i in _dataContext.OrderItems
                                            where i.OrderId == order.Id
                                            join prod in _dataContext.product on i.ProductId equals prod.Id
                                            join col in _dataContext.ProductColors on prod.colorId equals col.Id into prodcol
                                            from c in prodcol.DefaultIfEmpty()
                                            join size in _dataContext.ProductSizes on prod.sizeId equals size.Id into prodsize
                                            from s in prodsize.DefaultIfEmpty()

                                            select new OrderItems
                                            {
                                                Id = i.Id,
                                                Quantity = i.Quantity,
                                                Price = i.Price,
                                                Total = i.Total,
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
                                                    productColor = c,
                                                    productSize = s
                                                }
                                            }).ToListAsync();

                decimal grandTotal = 0;
                decimal discount = 0;
                var Ordertotal = orderItems.Select(x => x.Total).Sum();
                if (copoun != null)
                {

                    if (copoun.Type == 1)
                    {
                        grandTotal = Ordertotal - copoun.Amount;
                    }
                    else
                    {
                        decimal percnDisc = Convert.ToDecimal(copoun.Percentage * 0.01);
                        discount = Ordertotal * percnDisc;
                        grandTotal = Ordertotal - discount;
                    }
                }
                else
                {
                    grandTotal = Ordertotal;
                }

                myOrderResponse = new MyOrderResponse
                {
                    OrderId = order.Id,
                    OrderStatus = order.Status,
                    OrderDate = order.CreatedAt.ToString("dd/MM/yyyy hh:mm:ss"),
                    CouponId = order.CouponId,
                    ProductCount = orderItems.Count(),
                    Total = Ordertotal,
                    discount = discount,
                    GrandTotal = grandTotal,
                    userAddresse = userAddresse,
                    orderItems = (List<OrderItems>)orderItemQuery
                };
            }


            //var query = await (from p in _dataContext.Orders
            //                   where p.UserId == UserId
            //                   orderby p.Id, p.Id descending
            //                   select new MyOrderResponse

            //                   {
            //                       OrderId = p.Id,
            //                       OrderStatus = p.Status,
            //                       OrderDate = p.CreatedAt.ToString("dd/MM/yyyy hh:mm:ss"), 
            //                       CouponId = p.CouponId,
            //                       ProductCount = _dataContext.OrderItems.Where(x => x.OrderId == p.Id).Count(),
            //                       Total = _dataContext.OrderItems.Where(x => x.OrderId == p.Id)
            //                       .Select(x => x.Total).Sum(),
            //                       userAddresse = _dataContext.UserAddresses.SingleOrDefault(x => x.Id == p.UserAddressesId),
            //                       orderItems = ((List<OrderItems>)(from i in _dataContext.OrderItems
            //                                                        join prod in _dataContext.product on i.ProductId equals prod.Id
            //                                                        where i.OrderId == p.Id

            //                                                        select new OrderItems
            //                                                        {
            //                                                            Id = i.Id,
            //                                                            Quantity = i.Quantity,
            //                                                            product = prod,
            //                                                            Price = i.Price,
            //                                                            Total = i.Total
            //                                                        })
            //                                                             )


            //                   }).LastAsync();

            return myOrderResponse;
        }

        public async Task<bool> CheckPreviousOrder(string UserId)
        {
            var checkBrevOrderStatus = await _dataContext.Orders
               .Where(x => x.UserId == UserId && x.Status <= 4).FirstOrDefaultAsync();

            if (checkBrevOrderStatus == null)
                return true;

            return false;

        }

        public async Task<int> UpdateOrderAsync(long orderId, Int32 status, string RejectReason)
        {
            var order = await _dataContext.Orders.SingleOrDefaultAsync(x => x.Id == orderId);

            order.Status = status;
            order.RejectReason = RejectReason;

            _dataContext.Orders.Update(order);
            var updated = await _dataContext.SaveChangesAsync();
            return updated;

        }

        //public async Task<List<OrderStatusChartResponse>> OrderStatusChart()
        //{
        //    //1 - Received 2 - In Progress Now 3 - Ready for Delivery 4 - The order was delivered 5 - Rejected

        //    var x = await (from order in _dataContext.Orders
        //                   group order by order.Status into g
        //                   select new OrderStatusChartResponse
        //                   {
        //                       label =  g.Key == 1 ? "Received" :
        //                                g.Key == 2 ? "In Progress Now" :
        //                                g.Key == 3 ? "Ready for Delivery" :
        //                                g.Key == 4 || g.Key == 6 ? "delivered" :
        //                                g.Key == 5 ? "Rejected" :
        //                                "",
        //                       value =  g.Count()
        //                   }).ToListAsync();

        //    return x;
        //}

        public async Task<List<OrderStatusChartResponse>> OrderStatusChart()
        {
            //1 - Received 2 - In Progress Now 3 - Ready for Delivery 4 - The order was delivered 5 - Rejected
            var ItemCount = await _dataContext.OrderItems.ToListAsync();
            var total = ItemCount.Sum(r => r.Id * r.Quantity);

            var x = await (from orderItem in _dataContext.OrderItems 
                           join product in _dataContext.product on orderItem.ProductId
                           equals product.Id
                           join subCategory in _dataContext.subCategory on product.SubCategoryId
                           equals subCategory.Id
                           join category in _dataContext.category on subCategory.CategoryId
                           equals category.Id
                           group category by category.ArabicName into g
                           select new OrderStatusChartResponse
                           {
                               label = g.Key.ToString(),
                               value = g.Count()
                           }).ToListAsync();

            return x;
        }

        public async Task<int> GetLastOrderStatusNo(string UserId)
        {
            var status = await _dataContext.Orders.Where(x => x.UserId == UserId)
                .OrderByDescending(x => x.Id).Select(x => x.Status).FirstOrDefaultAsync();

            return status;
        }

        public async Task<List<OrderReportResp>> OrderReport(DateTime FromDate, DateTime ToDate)
        {
            return await _dataContext.Orders
                .Include(x => x.User)
                .Include(x => x.userAddresses)
                .Include(x => x.OrderItems)
                .Where(x => x.CreatedAt >= FromDate && x.CreatedAt <= ToDate)
                .Select(x => new OrderReportResp
                {
                    orderId = x.Id,
                    customrName = x.User.FullName,
                    address = x.userAddresses.City,
                    date = x.CreatedAt.ToString("yyyy-MM-dd"),
                    paymentMethod = x.PaymentMethod == 1 ? "Cash" : "Credit",
                    amount = x.OrderItems.Select(x => x.Total).Sum()
                })
                .ToListAsync();
        }

        public async Task<OrderStatusCountResponse> OrderStatusCount()
        {
            var orders = await _dataContext.Orders.ToListAsync();
            //.Where(x => x.CreatedAt >= DateTime.Now.AddDays(-1))

            return new OrderStatusCountResponse
            {
                orders = orders.Count,
                Received = orders.Where(x => x.Status == 1).ToList().Count,
                inProgressNow = orders.Where(x => x.Status == 2).ToList().Count,
                readyForDelivary = orders.Where(x => x.Status == 3).ToList().Count,
                delivered = orders.Where(x => x.Status == 4 || x.Status == 6).ToList().Count,
                Rejected = orders.Where(x => x.Status == 5).ToList().Count,
            };
        }

        public Order GetOrderById(long orderId)
        {
            return _dataContext.Orders.SingleOrDefault(x => x.Id == orderId);
        }
    }
}
