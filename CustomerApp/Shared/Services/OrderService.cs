namespace CustomerApp.Shared.Services;
using Client = Supabase.Client;

public class OrderService
{
    private readonly Client client;
    private readonly IMapper mapper;

    public OrderService(Client client,IMapper mapper)
    {
        this.client = client;
        this.mapper = mapper;
    }
    public async Task<List<Purchase>> GetPurchaseHistory()
    {
        var response = await client.From<PurchaseData>().Get();
        return mapper.Map<List<Purchase>>(response.Models);
    }
}
