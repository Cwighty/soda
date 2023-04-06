using Postgrest.Attributes;
using Postgrest.Models;

namespace CustomerApp.Shared.Models;

[Table("purchase_item")]
public class PurchaseItemData : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }
    [Column("purchase_id")]
    public int PurchaseId { get; set; }
    [Column("product_id")]
    public int ProductId { get; set; }
    [Column("base_id")]
    public int BaseId { get; set; }
    [Column("size_id")]
    public int SizeId { get; set; }
    [Reference(typeof(PurchaseData))]
    public PurchaseData Purchase { get; set; }
    [Reference(typeof(ProductData))]
    public ProductData? Product { get; set; }
    [Reference(typeof(BaseData))]
    public BaseData Base { get; set; }
    [Reference(typeof(SizeData))]
    public SizeData Size { get; set; }
    [Reference(typeof(AddOnData))]
    public List<AddOnData> AddOns { get; set; }

}

public class PurchaseItem
{
    public int Id { get; set; }
    public int PurchaseId { get; set; }
    public int ProductId { get; set; }
    public int BaseId { get; set; }
    public int SizeId { get; set; }
    public Purchase Purchase { get; set; }
    public Product? Product { get; set; }
    public Base Base { get; set; }
    public Size Size { get; set; }
    public List<AddOn> AddOns { get; set; }
    public Decimal CalculatedPrice => AddOns.Sum(a => a.Price) + Base.Price + Size.Price;
    public string Name => Product?.Name ?? Base.Name;
}