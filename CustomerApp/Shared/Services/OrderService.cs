namespace CustomerApp.Shared.Services;
using Client = Supabase.Client;

public class OrderService
{
    private readonly Client client;
    private readonly IMapper mapper;
    private readonly UserService userService;

    public OrderService(Client client, IMapper mapper, UserService userService)
    {
        this.client = client;
        this.mapper = mapper;
        this.userService = userService;
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
        var purchase = await client.From<PurchaseData>().Insert(newPurchase);

        foreach (var item in products)
        {

        }


    }
}
