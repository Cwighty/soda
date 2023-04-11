namespace CustomerApp.Shared.Services;

using Microsoft.Extensions.Configuration;
using System.Collections.ObjectModel;
using System.Net.Http.Json;
using System.Text.Json;
using Client = Supabase.Client;

public class PurchaseService
{
    private readonly Client client;
    private readonly IMapper mapper;
    private readonly UserService userService;
    private readonly HttpClient httpClient;
    private readonly IConfiguration config;

    public PurchaseService(
        Client client,
        IMapper mapper,
        UserService userService,
        HttpClient httpClient,
        IConfiguration config
        )
    {
        this.client = client;
        this.mapper = mapper;
        this.userService = userService;
        this.httpClient = httpClient;
        this.config = config;
    }
    public async Task<List<Purchase>> GetPurchaseHistory()
    {
        var response = await client.From<PurchaseData>().Get();
        return mapper.Map<List<Purchase>>(response.Models);
    }

    public async Task<CheckoutInitiationResponse> InitiateCheckout(List<PurchaseItem> cartItems, DateTime pickUpTime)
    {
        var storeAPI = config["StoreAPI"];
        var url = $"{storeAPI}checkout/items";
        var customer = await userService.GetCustomer();

        var purchase = new Purchase
        {
            Id = 0,
            CustomerId = customer?.Id,
            CreatedAt = DateTime.UtcNow,
            Status = "STARTED",
            PurchaseItems = cartItems,
            PickUpTime = pickUpTime
        };

        var res = await httpClient.PostAsJsonAsync(url, purchase);
        if (res.IsSuccessStatusCode)
        {
            var content = await res.Content.ReadAsStringAsync();
            var checkoutInitiationResponse = JsonSerializer.Deserialize<CheckoutInitiationResponse>(content);
            return checkoutInitiationResponse;
        }
        throw new Exception();
    }

    public async Task<int?> CheckoutOnline(List<PurchaseItem> cartItems, DateTime pickUpTime)
    {
        var initiation = await InitiateCheckout(cartItems.ToList(), pickUpTime);
        var storeAPI = config["StoreAPI"];
        var url = $"{storeAPI}confirm?intent={initiation.ClientSecret}&orderid={initiation.OrderNumber}";
        try
        {
            WebAuthenticatorResult authResult = await WebAuthenticator.Default.AuthenticateAsync(
                new Uri(url),
                new Uri("soda://success"));
            /*            authResult.Properties.TryGetValue("orderid", out var processOrderId);
                        return int.Parse(processOrderId);*/
            return initiation.OrderNumber;
        }
        catch
        {
            await CancelOrder(initiation.OrderNumber);
            return null;
        }
    }

    public async Task CancelOrder(int orderNumber)
    {
        var order = await client.From<PurchaseData>()
            .Where(p => p.Id == orderNumber)
            .Single();

        order.Status = "CANCELED";

        await order.Update<PurchaseData>();
    }

    public async Task<Purchase> GetPurchaseById(int orderId)
    {
        var purchase = await client.From<PurchaseData>()
            .Where(p => p.Id == orderId)
            .Single();
        return mapper.Map<Purchase>(purchase);
    }
}
