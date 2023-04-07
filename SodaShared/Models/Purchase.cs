namespace SodaShared.Models;

public class Purchase
{
    public int Id { get; set; }
    public string CustomerId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public decimal PricePaid { get; set; }
    public string Status { get; set; }
    public Customer Customer { get; set; }

    public List<PurchaseItem> PurchaseItems { get; set; }
}