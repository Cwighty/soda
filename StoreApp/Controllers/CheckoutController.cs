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
    public async Task<ActionResult> CheckoutItemsAsync(Purchase purchase)
    {
        if (purchase == null)
        {
            return Json(new { error = "No purchase data" });
        }
        if (purchase.PurchaseItems.Count == 0)
        {
            return Json(new { error = "No items in cart" });
        }
        
        // Calculate the price
        var totalPrice = 20;

        purchase.CreatedAt = DateTime.UtcNow;
        purchase.PricePaid = totalPrice;
        purchase.Status = "IN PROGRESS";

        var newPurchaseData = mapper.Map<PurchaseData>(purchase);
        var newPurchase = await client.From<PurchaseData>()
            .Insert(newPurchaseData, new Postgrest.QueryOptions { Returning = Postgrest.QueryOptions.ReturnType.Representation });

        var purchaseId = newPurchase.Models.FirstOrDefault().Id;
        if (purchaseId == null)
        {
            return Json(new { error = "Error creating order" });
        }

        var sizes = (await client.From<SizeData>().Get()).Models;

        var purchaseDataItems = mapper.Map<List<PurchaseItemData>>(purchase.PurchaseItems);
        foreach (var item in purchaseDataItems)
        {
            item.PurchaseId = purchaseId;
            item.SizeId = sizes.Where(s => s.Name == item.Size.Name).FirstOrDefault().Id;
            await client.From<PurchaseItemData>()
                .Insert(item);
        }

        // Create a new payment intent
        var paymentIntentService = new PaymentIntentService();
        var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
        {
            Amount = totalPrice * 100,
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