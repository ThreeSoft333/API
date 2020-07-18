using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Domain.EComm;

namespace ThreeSoftECommAPI.Services.EComm.PaymentServ
{
   public interface IPaymentService
    {
        Task<int> AddCustomer(PaymentTransaction payment);
        Task<PaymentTransaction> GetCustomerPaymentId(string UserId);
    }
}
