using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Data;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.OrderItemServ
{
    public class OrderItemService:IOrderItemService
    {
        private readonly ApplicationDbContext _dataContext;

        public OrderItemService(ApplicationDbContext dbContext)
        {
            _dataContext = dbContext;
        }
        public async Task<int> CreateOrderItemAsync(OrderItems orderItems)
        {
            await _dataContext.OrderItems.AddRangeAsync(orderItems);
            var created = await _dataContext.SaveChangesAsync();
            return created;
        }

        public async Task<List<OrderItems>> GetOrderItemForAdmin(long orderId)
        {
            return await _dataContext.OrderItems.Include(x => x.product)
                   .Where(x => x.OrderId == orderId).ToListAsync();
        }

        public async Task<List<OrderItems>> GetOrderItems(long orderId)
        {
            return await _dataContext.OrderItems.Include(x => x.product)
                   .Where(x => x.OrderId == orderId).ToListAsync();
        }
    }
}
