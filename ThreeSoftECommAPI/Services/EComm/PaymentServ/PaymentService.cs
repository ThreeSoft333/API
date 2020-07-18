using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Data;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.PaymentServ
{
    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _dataContext;
        public PaymentService(ApplicationDbContext dbContext)
        {
            _dataContext = dbContext;
        }
        public async Task<int> AddCustomer(PaymentTransaction payment)
        {
            await _dataContext.PaymentTransactions.AddAsync(payment);
            var created = await _dataContext.SaveChangesAsync();
            return created;
        }

        public async Task<PaymentTransaction> GetCustomerPaymentId(string UserId)
        {
            return await _dataContext.PaymentTransactions.SingleOrDefaultAsync(x => x.UserId == UserId);
        }
    }
}
