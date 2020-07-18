using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeSoftECommAPI.Contracts.V1;
using ThreeSoftECommAPI.Contracts.V1.Requests.EComm.Payment;
using ThreeSoftECommAPI.Contracts.V1.Responses.EComm;
using ThreeSoftECommAPI.Domain.EComm;
using ThreeSoftECommAPI.Services.EComm.PaymentServ;

namespace ThreeSoftECommAPI.Controllers.V1
{
    public class PaymentController:Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost(ApiRoutes.payment.Charge)]
        public async Task<IActionResult> Charge([FromBody] PaymentRequest paymentRequest)
        {
            try
            {
                var customers = new CustomerService();
                var Charges = new ChargeService();

                var Checkcustomer = await _paymentService.GetCustomerPaymentId(paymentRequest.UserId);
                var customer = new Customer();
                var CustomerId = "";

                if (Checkcustomer == null)
                {
                    customer = customers.Create(new CustomerCreateOptions
                    {
                        Email = paymentRequest.CustomerEmail,
                        Name = paymentRequest.CustomerName,
                        Phone = paymentRequest.CustomerPhone,
                    });

                    CustomerId = customer.Id;
                }
                else
                {
                    CustomerId = Checkcustomer.CustomerPaymentId;
                }

                var options1 = new PaymentMethodCreateOptions
                {
                    Type = "card",
                    Card = new PaymentMethodCardCreateOptions
                    {
                        Number = paymentRequest.CardNumber,
                        ExpMonth = paymentRequest.ExpMonth,
                        ExpYear = paymentRequest.ExpYear,
                        Cvc = paymentRequest.Cvc,
                    },
                };

                var service1 = new PaymentMethodService();
                service1.Create(options1);


                var options = new PaymentIntentCreateOptions
                {
                    Amount = paymentRequest.Amount,
                    Currency = "USD",
                    Customer = CustomerId,
                    PaymentMethodTypes = new List<string> {
                "card"
  },
                };

                var service = new PaymentIntentService();
                var intent = service.Create(options);

                if (intent.Status == "requires_payment_method")
                {
                    var PaymentTrans = new PaymentTransaction
                    {
                        UserId = paymentRequest.UserId,
                        CustomerPaymentId = CustomerId
                    };

                    await Create(PaymentTrans);

                    return Ok(new
                    {
                        status = Ok().StatusCode,
                        message = "Payment Successfully"
                    });
                }
                return BadRequest(new
                {
                    status = BadRequest().StatusCode,
                    message = "Internal server error"
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    status = BadRequest().StatusCode,
                    message = ex.Message
                });
            }
        }

        public async Task<PaymentTransaction> GetPaymentTrans(string UserId)
        {
            return await _paymentService.GetCustomerPaymentId(UserId);
        }

        public async Task<IActionResult> Create(PaymentTransaction paymentTransaction)
        {
            var paymentTrans = new PaymentTransaction
            {
                UserId = paymentTransaction.UserId,
                CustomerPaymentId = paymentTransaction.CustomerPaymentId
            };

            var status = await _paymentService.AddCustomer(paymentTrans);

            if (status == 1)
            {
               return Ok();
            }
            return NotFound(new ErrorResponse
            {
                message = "Not Found",
                status = NotFound().StatusCode
            });
        }

    }
}
