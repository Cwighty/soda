using Postgrest.Attributes;
using Postgrest.Models;

namespace CustomerApp.Models;

[Table("purchase_item")]
public class PurchaseItemData : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }
    [Column("purchase_id")]
    public int PurchaseId { get; set; }
    [Column("product_id")]
    public int ProductId { get; set; }
    [Column("quantity")]
    public int Quantity { get; set; }
    [Reference(typeof(PurchaseData))]
    public PurchaseData Purchase { get; set; }
    [Reference(typeof(ProductData))]
    public ProductData Product { get; set; }
}