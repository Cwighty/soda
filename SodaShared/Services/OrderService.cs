using Supabase;
namespace SodaShared.Services;

public class OrderService
{
    private readonly Client client;
    private readonly IMapper mapper;

    public OrderService(Client client, IMapper mapper)
    {
        this.client = client;
        this.mapper = mapper;
    }
    
    public async Task<List<Purchase>> GetOrders()
    {
        var res = await client.From<PurchaseData>().Get();
        var orders = res.Models;
        return mapper.Map<List<Purchase>>(orders);
    }
    
    public async Task MarkOrderComplete(int orderId)
    {
        
        var order = await client.From<PurchaseData>().Where(p => p.Id == orderId).Single();
        order!.Status = "COMPLETED";
        order!.CompletedAt = DateTime.Now;
        await client.From<PurchaseData>().Update(order);

        //send notifications
    }
}
