using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1.Responses.Charts;
using ThreeSoftECommAPI.Contracts.V1.Responses.EComm;
using ThreeSoftECommAPI.Contracts.V1.Responses.Reports;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.OrderServ
{
    public interface IOrderService
    {
        Task<int> CreateOrderAsync(Order order);
        Task<int> UpdateOrderAsync(long orderId, Int32 status,string RejectReason);
        Order GetOrderById(long orderId);
        Task<int> GetLastOrderStatusNo(string UserId);
        Task<List<Order>> GetOrdersAsync(string UserId);
        Task<List<MyOrderResponse>> GetMyOrdersAsync(string UserId);
        Task<MyOrderResponse> GetOrderStatusAsync(string UserId);
        Task<bool> DeleteOrderAsync(long OrderId);
        Task<List<Order>> GetOrdersForAdmin(int status);    
        Task<bool> CheckPreviousOrder(string UserId);
        Task<List<OrderStatusChartResponse>> OrderStatusChart();
        Task<List<OrderReportResp>> OrderReport(DateTime FromDate, DateTime ToDate);
        Task<OrderStatusCountResponse> OrderStatusCount();



    }
}
