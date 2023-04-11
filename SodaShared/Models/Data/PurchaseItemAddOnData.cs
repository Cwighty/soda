using Postgrest.Attributes;
using Postgrest.Models;

namespace SodaShared.Models.Data;

[Table("purchase_item_addon")]
public class PurchaseItemAddOnData : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }
    [Column("purchase_item_id")]
    public int PurchaseItemId { get; set; }
    [Column("addon_id")]
    public int AddOnId { get; set; }
}


