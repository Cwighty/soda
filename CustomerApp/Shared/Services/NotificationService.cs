using Plugin.LocalNotification;
using Supabase;

namespace CustomerApp.Shared.Services;

public class NotificationService
{
    private readonly Client client;

    public NotificationService(Client client)
    {
        this.client = client;
    }
    public void NotifyOrderComplete()
    {
        var request = new NotificationRequest
        {
            NotificationId = 1,
            Title = "Order Complete",
            Description = "Your order has been completed",
            ReturningData = "Order Complete"
        };
        LocalNotificationCenter.Current.Show(request);
    }

    internal async Task SubscribeTo(int orderId)
    {
        var channel = client.Realtime.Channel("realtime", "public", "purchase", "id", $"id=eq.{orderId}");
        channel.OnMessage += (sender, e) =>
        {
            NotifyOrderComplete();
        };

        await channel.Subscribe();
    }
}
