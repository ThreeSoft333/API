using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1.Responses.Charts;
using ThreeSoftECommAPI.Contracts.V1.Responses.Reports;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.OrderItemServ
{
   public interface IOrderItemService
    {
        Task<int> CreateOrderItemAsync(OrderItems orderItems);
        Task<List<OrderItems>> GetOrderItemForAdmin(Int64 orderId);
        Task<List<OrderItems>> GetOrderItems(Int64 orderId);
        Task<List<CategoryPcntChart>> getCategoryPercent(string lang);
        Task<List<SubCategoryPcntChart>> getSubCategoryPercent(string lang);
        Task<List<OrderProductReportResp>> OrderProductReport(Int64 productId, DateTime FromDate, DateTime ToDate);
    }
}
