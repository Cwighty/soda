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
        decimal totalPrice = await CalculatePriceBeforeTax(purchase);

        purchase.CreatedAt = DateTime.UtcNow;
        purchase.SubTotal = totalPrice;
        purchase.TaxCollected = totalPrice * 0.07M;
        purchase.Status = "IN PROGRESS";

        var purchaseId = await SavePurchase(purchase);

        await SavePurchaseItemsWithAddOns(purchaseId, purchase.PurchaseItems);

        PaymentIntent paymentIntent = CreatePaymentIntent(totalPrice);

        //return payment intent key and order id
        return Json(new { clientSecret = paymentIntent.ClientSecret, orderNumber = purchaseId });
    }

    private static PaymentIntent CreatePaymentIntent(decimal totalPrice)
    {
        // Create a new payment intent
        var paymentIntentService = new PaymentIntentService();
        var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
        {
            Amount = (long)totalPrice * 100,
            Currency = "usd",
            AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
            {
                Enabled = true,
            },
        });
        return paymentIntent;
    }

    private async Task SavePurchaseItemsWithAddOns(int purchaseId, List<PurchaseItem> purchaseItems)
    {
        var sizes = (await client.From<SizeData>().Get()).Models;
        foreach (var item in purchaseItems)
        {
            var newPurchaseItemData = mapper.Map<PurchaseItemData>(item);
            newPurchaseItemData.PurchaseId = purchaseId;
            newPurchaseItemData.SizeId = sizes.Where(s => s.Name == item.Size.Name).FirstOrDefault().Id;
            var newItem = await client.From<PurchaseItemData>()
                .Insert(newPurchaseItemData, new Postgrest.QueryOptions { Returning = Postgrest.QueryOptions.ReturnType.Representation });
            
            var itemId = newItem.Models.FirstOrDefault()!.Id;
            foreach (var addon in item.AddOns)
            {
                var newAddon = new PurchaseItemAddOnData
                {
                    PurchaseItemId = itemId,
                    AddOnId = addon.Id
                };
                await client.From<PurchaseItemAddOnData>()
                    .Insert(newAddon);
            }
        }
    }

    private async Task<int> SavePurchase(Purchase purchase)
    {
        var newPurchaseData = mapper.Map<PurchaseData>(purchase);
        var newPurchase = await client.From<PurchaseData>()
            .Insert(newPurchaseData, new Postgrest.QueryOptions { Returning = Postgrest.QueryOptions.ReturnType.Representation });

        var purchaseId = newPurchase.Models.FirstOrDefault()!.Id;
        return purchaseId;
    }

    private async Task<decimal> CalculatePriceBeforeTax(Purchase purchase)
    {
        Decimal totalPrice = 0;
        foreach (var item in purchase.PurchaseItems)
        {
            // Sum up addons
            foreach (var addon in item.AddOns)
            {
                var lookUpAddon = await client.From<AddOnData>().Where(a => a.Id == addon.Id).Single();
                totalPrice += lookUpAddon!.Price;
            }
            // Sum up bases
            var based = await client.From<BaseData>().Where(b => b.Id == item.BaseId).Single();
            totalPrice += based!.Price;
            // Sum up size options
            var size = await client.From<SizeData>().Where(s => s.Id  == item.SizeId).Single();
            totalPrice += size!.Price;
        }

        return totalPrice;
    }
}