using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SodaShared.Models;
using SodaShared.Models.Data;
using Stripe;
using Supabase;

namespace StoreApp.Controllers;

[Route("[controller]")]
[ApiController]
public class CheckoutController : Controller
{
    private readonly Client client;
    private readonly IMapper mapper;

    public CheckoutController(Client client, IMapper mapper)
    {
        this.client = client;
        this.mapper = mapper;
    }

    [HttpPost("items")]
    public async Task<ActionResult> CheckoutItemsAsync(List<PurchaseItem> purchaseItems)
    {
        // Calculate the price
        var totalPrice = 20;

        // Create a new order in database
        var purchase = new PurchaseData() {
            CreatedAt = DateTime.UtcNow,
            PricePaid = totalPrice,
            Status = "IN PROGRESS",
        };

        var newPurchase = await client.From<PurchaseData>().Insert(purchase, new Postgrest.QueryOptions { Returning = Postgrest.QueryOptions.ReturnType.Representation });
        var test = await client.From<PurchaseData>().Get();
        var test1 = test.Models.Last();


        var purchaseItemsDatas = mapper.Map<List<PurchaseItemData>>(purchaseItems);

        var orderId = newPurchase.Models.FirstOrDefault()?.Id;
        if (orderId == null)
        {
            return Json(new { error = "Error creating order" });
        }



        // Create a new payment intent
        var paymentIntentService = new PaymentIntentService();
        var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
        {
            Amount = totalPrice,
            Currency = "usd",
            AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
            {
                Enabled = true,
            },
        });

        //return payment intent key and order id
        return Json(new { clientSecret = paymentIntent.ClientSecret, orderNumber =  orderId});
    }
}
