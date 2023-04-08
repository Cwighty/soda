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

    public async Task InitializePurchase(List<Product> products)
    {
        var customer = await userService.GetCustomer();

        var newPurchase = new PurchaseData
        {
            CustomerId = customer?.Id,
            CreatedAt = DateTime.UtcNow,
            Status = "STARTED"
        };

        foreach (var item in products)
        {

        }


    }

    public async Task<CheckoutInitiationResponse> Checkout(List<PurchaseItem> cartItems)
    {
        var storeAPI = config["StoreAPI"];
        var url = $"{storeAPI}checkout/items";
        var res = await httpClient.PostAsJsonAsync(url, cartItems);
        var content = await res.Content.ReadAsStringAsync();
        var checkoutInitiationResponse = JsonSerializer.Deserialize<CheckoutInitiationResponse>(content);
        return checkoutInitiationResponse;
    }
}
