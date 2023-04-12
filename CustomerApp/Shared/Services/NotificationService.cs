using Plugin.LocalNotification;
using Supabase;
using Supabase.Realtime;
using Supabase.Realtime.Socket;
using System.Net.Sockets;
using Client = Supabase.Client;

namespace CustomerApp.Shared.Services;

public class NotificationService
{
    private readonly Client client;

    private RealtimeChannel channel;

    public NotificationService(Client client)
    {
        this.client = client;
    }
    public void NotifyOrderComplete(SocketResponseEventArgs e)
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
        client.Realtime.Connect();
        channel = client.Realtime.Channel("realtime", "public", "purchase", "id", $"id=eq.{orderId}");
        channel.OnMessage += (sender, e) =>
        {
            NotifyOrderComplete(e);
        };

        await channel.Subscribe();
    }
}
