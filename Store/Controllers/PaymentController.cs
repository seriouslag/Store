using Store.Models.Dto;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Store.Controllers
{
    public class PaymentController : ApiController
    {
        private readonly StripeCustomerService _customers = new StripeCustomerService();
        private readonly StripeChargeService _charges = new StripeChargeService();

        public IHttpActionResult Checkout(ChargeDtos charge)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["PaymentSystem"].ToUpper() == "STRIPE")
            {
                return StripeCheckout(charge);
            } else
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        private IHttpActionResult StripeCheckout(ChargeDtos charge)
        {
            try
            {
                var customer = _customers.Create(new StripeCustomerCreateOptions
                {
                    Email = charge.Email,
                    SourceToken = charge.Token
                });

                var chargeOptions = new StripeChargeCreateOptions()
                {
                    Amount = charge.Amount,
                    Currency = "usd",
                    Description = "Charge for " + charge.Email ?? charge.Name,
                    CustomerId = customer.Id,
                    SourceTokenOrExistingSourceId = charge.Token // obtained with Stripe.js
                };

                var stripeCharge = _charges.Create(chargeOptions);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }

        }
    }
}
