namespace SodaShared.Models;

public class PurchaseItem
{
    public int Id { get; set; }
    public int PurchaseId { get; set; }
    public int ProductId { get; set; }
    public int BaseId { get; set; }
    public int SizeId { get; set; }
    public Purchase? Purchase { get; set; }
    public Product? Product { get; set; }
    public Base Base { get; set; }
    public Size Size { get; set; }
    public List<AddOn> AddOns { get; set; }
    public Decimal CalculatedPrice => (AddOns?.Sum(a => a.Price) ?? 0) + Base.Price + Size.Price;
    public string Name => Product?.Name ?? Base.Name;
}