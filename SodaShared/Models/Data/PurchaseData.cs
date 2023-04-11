using Postgrest.Attributes;
using Postgrest.Models;

namespace SodaShared.Models.Data;

[Table("purchase")]
public class PurchaseData : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }
    [Column("customer_id")]
    public string CustomerId { get; set; }
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    [Column("completed_at")]
    public DateTime? CompletedAt { get; set; }
    [Column("pick_up_time")]
    public DateTime? PickUpTime { get; set; }
    [Column("subtotal")]
    public decimal SubTotal { get; set; }
    [Column("tax_collected")]
    public decimal? TaxCollected { get; set; }
    [Column("status")]
    public string Status { get; set; }
    
}

public class PurchaseWithItemsData : PurchaseData
{
    [Reference(typeof(PurchaseItemData))]
    public List<PurchaseItemData>? PurchaseItems { get; set; }
}

public class PurchaseWithItemsAndAddOnsData : PurchaseData
{
    [Reference(typeof(PurchaseItemDataWithAddOns))]
    public List<PurchaseItemDataWithAddOns>? PurchaseItems { get; set; }
}

public class PurchaseWithCustomerData : PurchaseData
{
    [Reference(typeof(CustomerData))]
    public CustomerData Customer { get; set; }
}

