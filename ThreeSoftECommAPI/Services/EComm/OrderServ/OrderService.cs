using Microsoft.EntityFrameworkCore;
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
            await _dataContext.Orders.AddAsync(order);
            var created = await _dataContext.SaveChangesAsync();
            return created;
        }

        public Task<bool> DeleteOrderAsync(long OrderId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MyOrderResponse>> GetMyOrdersAsync(string UserId)
        {
            var query = await (from o in _dataContext.Orders
                               where o.UserId == UserId && o.Status == 4


                               select new MyOrderResponse
                               {
                                   OrderId = o.Id,
                                   OrderStatus = o.Status,
                                   OrderDate = o.CreatedAt.ToString("dd/MM/yyyy hh:mm:ss"),
                                   CouponId = o.CouponId,
                                   ProductCount = _dataContext.OrderItems.Where(x => x.OrderId == o.Id).Count(),
                                   Total = _dataContext.OrderItems.Where(x => x.OrderId == o.Id)
                                   .Select(x => x.Total).Sum(),


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
                                                                            colors = c,
                                                                            size = s
                                                                        }
                                                                    })
                                                                         )


                               }).ToListAsync();

            return query;
        }

        public async Task<List<Order>> GetOrdersForAdmin(int status)
        {
            return await _dataContext.Orders.Include(x => x.userAddresses)
                .Include(x => x.User)
                .Where(x => x.Status == status).ToListAsync();
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
                                   OrderDate = p.CreatedAt.ToString("dd/MM/yyyy hh:mm:ss"), 
                                   CouponId = p.CouponId,
                                   ProductCount = _dataContext.OrderItems.Where(x => x.OrderId == p.Id).Count(),
                                   Total = _dataContext.OrderItems.Where(x => x.OrderId == p.Id)
                                   .Select(x => x.Total).Sum(),
                                   userAddresse = _dataContext.UserAddresses.SingleOrDefault(x => x.Id == p.UserAddressesId),
                                   orderItems = ((List<OrderItems>)(from i in _dataContext.OrderItems
                                                                    join prod in _dataContext.product on i.ProductId equals prod.Id
                                                                    where i.OrderId == p.Id

                                                                    select new OrderItems
                                                                    {
                                                                        Id = i.Id,
                                                                        Quantity = i.Quantity,
                                                                        product = prod,
                                                                        Price = i.Price,
                                                                        Total = i.Total
                                                                    })
                                                                         )


                               }).LastAsync();

            return query;
        }

        public async Task<bool> CheckPreviousOrder(string UserId)
        {
            var checkBrevOrderStatus = await _dataContext.Orders
               .Where(x => x.UserId == UserId && x.Status != 4).FirstOrDefaultAsync();

            if (checkBrevOrderStatus == null)
                return true;

            return false;

        }
    }
}
