using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SodaShared.Models;
using SodaShared.Models.Data;
using StoreApp.Services;
using Stripe;
using Supabase;

namespace StoreApp.Controllers;

[Route("[controller]")]
[ApiController]
public class CheckoutController : Controller
{
    private readonly PurchaseService purchaseService;

    public CheckoutController(PurchaseService purchaseService)
    {
        this.purchaseService = purchaseService;
    }

    [HttpPost("items")]
    public async Task<ActionResult> CheckoutItemsAsync(Purchase purchaseRequest)
    {
        if (purchaseRequest == null)
        {
            return Json(new { error = "No purchase data" });
        }
        if (purchaseRequest.PurchaseItems.Count == 0)
        {
            return Json(new { error = "No items in cart" });
        }

        decimal totalPrice = await CalculatePriceBeforeTax(purchaseRequest);

        var purchase = CreateNewPurchaseAsync(purchaseRequest, totalPrice);

        PaymentIntent paymentIntent = CreatePaymentIntent(totalPrice);

        //return payment intent key and order id
        return Json(new { clientSecret = paymentIntent.ClientSecret, orderNumber = purchase.Id });
    }

    private async Task<Purchase> CreateNewPurchaseAsync(Purchase purchaseRequest, decimal totalPrice)
    {
        var newPurchase = purchaseRequest.Copy();
        newPurchase.CreatedAt = DateTime.UtcNow;
        newPurchase.SubTotal = totalPrice;
        newPurchase.TaxCollected = totalPrice * 0.07M;
        newPurchase.Status = "IN PROGRESS";
        
        var purchaseId = await purchaseService.PersistPurchase(newPurchase);
        newPurchase.Id = purchaseId;
        return newPurchase;
    }

    private static PaymentIntent CreatePaymentIntent(decimal totalPrice)
    {
        // Create a new stripe payment intent
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
    


    private async Task<decimal> CalculatePriceBeforeTax(Purchase purchase)
    {
        Decimal totalPrice = 0;
        foreach (var item in purchase.PurchaseItems)
        {
            // Sum up addons
            foreach (var addon in item.AddOns)
            {
                var lookUpAddon = await purchaseService.LookUpAddon(addon.Id);
                totalPrice += lookUpAddon!.Price;
            }
            // Sum up bases
            var based = await purchaseService.LookUpBase(item.BaseId);
            totalPrice += based!.Price;
            // Sum up size options
            var size = await purchaseService.LookUpSize(item.SizeId);
            totalPrice += size!.Price;
        }
        return totalPrice;
    }

   
}