using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Service.Services.BasketService.Dtos;
using Store.Service.Services.OrderService.Dtos;
using Store.Service.Services.PaymentService;
using Stripe;

namespace Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService paymentService;
        private readonly ILogger<PaymentController> logger;

        private const string endpointSecret = "whsec_89a1465f2b9968eed5e5b01e157b2bfd07ffe64b966d143acd78c88c1e94f56a";
        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            this.paymentService = paymentService;
            this.logger = logger;
        }

        [HttpPost("CreateOrUpdatePaymentIntentForExistingOrder/{basketId}")]

        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntentForExistingOrder(CustomerBasketDto input)
            => Ok(await paymentService.CreateOrUpdatePaymentIntentForExistingOrder(input));


        
        [HttpPost("CreateOrUpdatePaymentIntentForNewOrder/{basketId}")]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntentForNewOrder(string basketId)
           => Ok(await paymentService.CreateOrUpdatePaymentIntentForNewOrder(basketId));

        //[HttpPost("webhook")]

        //public class WebhookController : Controller
        //{

        //This is your Stripe CLI webhook secret for testing your endpoint locally.
        //const string endpointSecret = "whsec_89a1465f2b9968eed5e5b01e157b2bfd07ffe64b966d143acd78c88c1e94f56a";

        [HttpPost("webhook")]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], endpointSecret);

                PaymentIntent paymentIntent;
                OrderResultDto order;
                if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
                {
                    paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
                    logger.LogInformation("payment failed : ", paymentIntent.Id);
                    order = await paymentService.UpdateOrderPaymentFailed(paymentIntent.Id);
                    logger.LogInformation("order updayed to payment failed : ", order.Id);
                }
                else if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
                    logger.LogInformation("payment Succeeded : ", paymentIntent.Id);
                    order = await paymentService.UpdateOrderPaymentSucceeded(paymentIntent.Id );
                    logger.LogInformation("order updated to payment Succeeded : ", order.Id);
                }
                // ... handle other event types
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }
        //}
    }
}
