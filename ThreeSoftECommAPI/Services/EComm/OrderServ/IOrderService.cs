using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.OrderServ
{
    public interface IOrderService
    {
        Task<int> CreateOrderAsync(Order order);
        Task<List<Order>> GetOrdersAsync(string UserId);
    }
}
