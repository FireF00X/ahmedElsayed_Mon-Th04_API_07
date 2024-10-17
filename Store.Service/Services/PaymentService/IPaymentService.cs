using Store.Service.Services.BasketService.Dto;
using Store.Service.Services.OrderService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(CustomerBasketDto input);
        Task<OrderDetailsDto> UpdatePaymentOrderSucceed(string paymentIntentId);
        Task<OrderDetailsDto> UpdatePaymentOrderFailed(string paymentIntentId);

    }

}
