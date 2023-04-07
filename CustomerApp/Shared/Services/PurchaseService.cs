namespace CustomerApp.Shared.Services;

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

    public PurchaseService(Client client, IMapper mapper, UserService userService, HttpClient httpClient)
    {
        this.client = client;
        this.mapper = mapper;
        this.userService = userService;
        this.httpClient = httpClient;
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

    public async Task<string> Checkout(List<PurchaseItem> cartItems)
    {
        var url = "https://clj4547d-7140.usw2.devtunnels.ms/checkout/items";
        var res = await httpClient.PostAsJsonAsync(url, cartItems);
        var test = res.Content.ReadAsStringAsync().Result;
        return res.Content.ToString();
    }
}
