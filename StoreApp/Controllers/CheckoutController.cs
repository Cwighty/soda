using Microsoft.AspNetCore.Mvc;
using SodaShared.Models;
using SodaShared.Services;
using Stripe;

namespace StoreApp.Controllers;

[Route("[controller]")]
[ApiController]
public class CheckoutController : Controller
{
    private readonly PurchaseRepository purchaseRepo;

    public CheckoutController(PurchaseRepository purchaseService)
    {
        this.purchaseRepo = purchaseService;
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

        var purchase = await CreateNewPurchaseAsync(purchaseRequest, totalPrice);

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
        
        var purchaseId = await purchaseRepo.PersistPurchase(newPurchase);
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
                var lookUpAddon = await purchaseRepo.GetAddon(addon.Id);
                totalPrice += lookUpAddon!.Price;
            }
            // Sum up bases
            var based = await purchaseRepo.GetBase(item.BaseId);
            totalPrice += based!.Price;
            // Sum up size options
            var size = await purchaseRepo.GetSize(item.SizeId);
            totalPrice += size?.Price ?? 0;
        }
        if (totalPrice == 0)
        {
            //Must pay at least 1 dollar for stripe to work
            return 1M;
        }
        return totalPrice;
    }

   
}