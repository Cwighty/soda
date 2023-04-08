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
        if (purchaseItems.Count == 0)
        {
            return Json(new { error = "No items in cart" });
        }
        // Calculate the price
        var totalPrice = 2000;

        // Create a new order in database
        var purchaseItemsDatas = mapper.Map<List<PurchaseItemData>>(purchaseItems);
        var purchase = new PurchaseData() {
            CreatedAt = DateTime.UtcNow,
            PricePaid = totalPrice,
            Status = "IN PROGRESS"
        };

        var newPurchase = await client.From<PurchaseData>()
            .Insert(purchase, new Postgrest.QueryOptions { Returning = Postgrest.QueryOptions.ReturnType.Representation });

        var purchaseId = newPurchase.Models.FirstOrDefault().Id;
        if (purchaseId == null)
        {
            return Json(new { error = "Error creating order" });
        }

        var sizes = (await client.From<SizeData>().Get()).Models;

        foreach (var item in purchaseItemsDatas)
        {
            item.PurchaseId = purchaseId;
            item.SizeId = sizes.Where(s => s.Name == item.Size.Name).FirstOrDefault().Id;
            await client.From<PurchaseItemData>()
                .Insert(item);
        }

        var test = await client.From<PurchaseWithItemsData>().Get();




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
        return Json(new { clientSecret = paymentIntent.ClientSecret, orderNumber =  purchaseId});
    }
}
