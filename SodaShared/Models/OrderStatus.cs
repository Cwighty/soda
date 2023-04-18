namespace SodaShared.Models;

public enum OrderStatus
{
    STARTED, IN_PROGRESS, COMPLETED, CANCELLED
}

public static class OrderStatusExtensions
{
    public static string ToFriendlyString(this OrderStatus status)
    {
        switch (status)
        {
            case OrderStatus.STARTED:
                return "STARTED";
            case OrderStatus.IN_PROGRESS:
                return "IN PROGRESS";
            case OrderStatus.COMPLETED:
                return "COMPLETED";
            case OrderStatus.CANCELLED:
                return "CANCELLED";
            default:
                throw new ArgumentOutOfRangeException(nameof(status), status, null);
        }
    }
}