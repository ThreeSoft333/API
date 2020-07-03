using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.OrderItemServ
{
   public interface IOrderItemService
    {
        Task<int> CreateOrderItemAsync(OrderItems orderItems);
    }
}
