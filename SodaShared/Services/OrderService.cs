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
        order!.Status = OrderStatus.COMPLETED.ToFriendlyString();
        order!.CompletedAt = DateTime.Now;
        await client.From<PurchaseData>().Update(order);
    }
    public async Task CancelOrder(int orderId)
    {
        var order = await client.From<PurchaseData>().Where(p => p.Id == orderId).Single();
        order!.Status = OrderStatus.CANCELLED.ToFriendlyString();
        await client.From<PurchaseData>().Update(order);
    }

    public async Task ReopenOrder(int orderId)
    {
        var order = await client.From<PurchaseData>().Where(p => p.Id == orderId).Single();
        order!.Status = OrderStatus.IN_PROGRESS.ToFriendlyString();
        order!.CompletedAt = null;
        await client.From<PurchaseData>().Update(order);
    }

    public async Task<int> GetOrderCount()
    {
        try
        {
            var orders = await GetOrders();
            return orders.Where(o => o.Status == OrderStatus.IN_PROGRESS.ToFriendlyString()).Count();
        }
        catch
        {
            return 0;
        }
    }
}
