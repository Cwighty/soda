using Postgrest.Attributes;
using Postgrest.Models;

namespace SodaShared.Models.Data;

[Table("customer_favorite")]
public class CustomerFavoriteData : BaseModel
{
    [Column("customer_id")]
    public string CustomerId { get; set; }
    [Column("product_id")]
    public int ProductId { get; set; }
}
