using Plugin.LocalNotification;
using Supabase;
using Supabase.Realtime;
using Supabase.Realtime.PostgresChanges;
using Supabase.Realtime.Socket;
using System.Net.Sockets;
using static Supabase.Client;
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
        client.Realtime.Connect();
        await client.From<PurchaseData>().On(ChannelEventType.Update, (sender, args) =>
        {
            var data = args.Response.Payload.Data;
            if (data.Record.Status == "COMPLETED")
            {
                NotifyOrderComplete();
            }
        });
    }
}
