using API.BLL.Entities;
using API.BLL.Entities.Order;
using API.BLL.Interfaces___Copy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace API.Controllers
{
    
    public class PaymentController : BaseAPIController
    {
        private readonly IPaymentService paymentService;
        private readonly ILogger<PaymentController> logger;

        public PaymentController(IPaymentService paymentService,ILogger<PaymentController> logger)
        {
            this.paymentService = paymentService;
            this.logger = logger;
        }

        [HttpPost("basketid")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketid)
        {
            var basket=await paymentService.CrateOrUpdatePaymentIntent(basketid);
            if (basket is null)
                return BadRequest(new ApiResponse(400,"Problem With Your Basket"));
            return Ok(basket);
        }

        [HttpPost("WebHock")]
        public async Task<ActionResult> StripeWebHock()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], endpointSecret);
            PaymentIntent intent;
            Order order;
            try
            {
                switch (stripeEvent.Type)
                {
                    case "Payment intent.payment intent failed":
                        intent = (PaymentIntent)stripeEvent.Data.Object;
                        logger.LogInformation("payment failed:", intent.Id);
                        order = await paymentService.UpdateOrderPaymentFailed(paymentintendid);
                        logger.LogInformation("payment failed:", order.Id);

                    case "Payment intent.payment intent Succeded":
                        intent = (PaymentIntent)stripeEvent.Data.Object;
                        logger.LogInformation("payment Succeded:", intent.Id);
                        order = await paymentService.UpdateOrderPaymentSucceeded(paymentintendid);
                        logger.LogInformation("Order Updated payment Recived:", order.Id);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }
            return Ok();
        }
    }
}
