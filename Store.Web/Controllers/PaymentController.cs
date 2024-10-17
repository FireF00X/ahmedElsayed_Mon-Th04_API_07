using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Repository.Basket.Models;
using Store.Service.Services.BasketService.Dto;
using Store.Service.Services.PaymentService;
using Stripe;

namespace Store.Web.Controllers
{
    
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;
        const string endpointsecret = "whsec";
        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(CustomerBasketDto input)
        {
            return Ok(await _paymentService.CreateOrUpdatePaymentIntent(input));
        }

        [HttpPost]
        public async Task<IActionResult> Webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["strip-signature"], endpointsecret);
                PaymentIntent paymentIntent;
                if (stripEvent.Type == "payment_intent.succeeded")
                {
                    paymentIntent = stripEvent.Data.Object as PaymentIntent;
                    _logger.LogInformation("Payment succeeded", paymentIntent.Id);
                    await _paymentService.UpdatePaymentOrderSucceed(paymentIntent.Id);



                }
                else
                {
                    Console.WriteLine("Unhadeled Event {0}", stripEvent.Type);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
