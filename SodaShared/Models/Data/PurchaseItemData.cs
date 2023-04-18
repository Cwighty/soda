using Postgrest.Attributes;
using Postgrest.Models;

namespace SodaShared.Models.Data;

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

   /* [Reference(typeof(PurchaseData), includeInQuery:false)]
    public PurchaseData? Purchase { get; set; }*/
    [Reference(typeof(ProductData), shouldFilterTopLevel:false)]
    public ProductData? Product { get; set; }
    [Reference(typeof(BaseData))]
    public BaseData Base { get; set; }
    [Reference(typeof(SizeData))]
    public SizeData Size { get; set; }

    [Reference(typeof(AddOnData), shouldFilterTopLevel:false)]
    public List<AddOnData> AddOns { get; set; }
}

