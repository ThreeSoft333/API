using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1.Responses.Charts;
using ThreeSoftECommAPI.Contracts.V1.Responses.Reports;
using ThreeSoftECommAPI.Data;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.OrderItemServ
{
    public class OrderItemService : IOrderItemService
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
        public async Task<List<CategoryPcntChart>> getCategoryPercent(string lang)
        {
            var orderitemCount = await _dataContext.OrderItems.SumAsync(x => x.Quantity);

            var catgPcnt = await _dataContext.OrderItems
            .GroupBy(x => lang == "ar" ? x.product.subCategory.category.ArabicName : x.product.subCategory.category.EnglishName)
            .Select(x => new CategoryPcntChart
            {
                name = x.Key,
                y = Convert.ToDecimal(string.Format("{0:0.0}", (Convert.ToDecimal(x.Sum(i => i.Quantity)) / Convert.ToDecimal(orderitemCount)) * 100)),
                drilldown = x.Key
            })
            .ToListAsync();

            return catgPcnt;

        }
        public async Task<List<SubCategoryPcntChart>> getSubCategoryPercent(string lang)
        {
            var CategoriesId = await _dataContext.OrderItems
           .GroupBy(x => x.product.subCategory.category.Id)
           .Select(x => new
           {
               Id = x.Key,
           })
           .ToListAsync();


            List<SubCategoryPcntChart> subCategoryPcntCharts = new List<SubCategoryPcntChart>();

            for (int i = 0; i < CategoriesId.Count; i++)
            {
                var _category = await _dataContext.category
                    .SingleOrDefaultAsync(x => x.Id == Convert.ToInt32(CategoriesId[i].Id));

                var orderitemCount = await _dataContext.OrderItems
                    .Where(x => x.product.subCategory.CategoryId == Convert.ToInt32(CategoriesId[i].Id))
                    .SumAsync(x => x.Quantity);

                var SubCatgPcnt = await _dataContext.OrderItems
                    .Where(x => x.product.subCategory.CategoryId == Convert.ToInt32(CategoriesId[i].Id))
                    .GroupBy(x => x.product.subCategory.Id)
                    .Select(x => new
                    {
                        Id = x.Key,
                        value = Convert.ToDecimal(string.Format("{0:0.0}", (Convert.ToDecimal(x.Sum(i => i.Quantity)) / Convert.ToDecimal(orderitemCount)) * 100))
                    })
                     .ToListAsync();

                
                List<object> data = new List<object>();

                for (int j = 0; j < SubCatgPcnt.Count; j++)
                {
                    List<object> data1 = new List<object>();

                    var subCategory = await _dataContext.subCategory
                        .Include(x => x.category)
                        .SingleOrDefaultAsync(x => x.Id == SubCatgPcnt[j].Id);

                        data1.Add(lang == "ar" ? subCategory.ArabicName : subCategory.EnglishName);
                        data1.Add(SubCatgPcnt[j].value);

                        data.Add(data1);
                    
                }

                var obj = new SubCategoryPcntChart
                {
                    name = lang == "ar" ? _category.ArabicName : _category.EnglishName,
                    id = lang == "ar" ? _category.ArabicName : _category.EnglishName,
                    data = data
                };

                subCategoryPcntCharts.Add(obj);


            }
            return subCategoryPcntCharts;
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
        public async Task<List<OrderProductReportResp>> OrderProductReport(long productId, DateTime FromDate, DateTime ToDate)
        {
            return await _dataContext.OrderItems
                .Where(x => x.ProductId == productId && x.CreatedAt >= FromDate && x.CreatedAt <= ToDate)
                .Select(x => new OrderProductReportResp
                {
                    orderId = x.OrderId,
                    count = x.Quantity,
                    price = x.Price,
                    total = x.Total
                }).ToListAsync();
        }
    }
}
