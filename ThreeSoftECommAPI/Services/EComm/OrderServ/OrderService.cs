using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public Task<List<Order>> GetOrdersAsync(string UserId)
        {
            throw new NotImplementedException();
        }

        //public async Task<List<Order>> GetOrdersAsync(string UserId)
        //{
        //    return await _dataContext.Orders.Include(p => p..Product).Where(x => x..status == 1).ToListAsync();
        //}
    }
}
