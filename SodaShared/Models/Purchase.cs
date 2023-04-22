namespace SodaShared.Models;

public class Purchase
{
    public int Id { get; set; }
    public string? CustomerId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime? PickUpTime { get; set; }
    public decimal? SubTotal { get; set; }
    public decimal? TaxCollected { get; set; }
    public decimal? TotalPaid => SubTotal ?? 0 + TaxCollected ?? 0;
    public string Status { get; set; }
    public bool IsInProgress => Status == OrderStatus.IN_PROGRESS.ToFriendlyString();
    public Customer? Customer { get; set; }

    public List<PurchaseItem> PurchaseItems { get; set; }
}

public static class PurchaseExtensions
{
    public static Purchase Copy(this Purchase purchase)
    {
        return new Purchase
        {
            Id = purchase.Id,
            CustomerId = purchase.CustomerId,
            CreatedAt = purchase.CreatedAt,
            CompletedAt = purchase.CompletedAt,
            PickUpTime = purchase.PickUpTime,
            SubTotal = purchase.SubTotal,
            TaxCollected = purchase.TaxCollected,
            Status = purchase.Status,
            Customer = purchase.Customer,
            PurchaseItems = purchase.PurchaseItems
        };
    }
}