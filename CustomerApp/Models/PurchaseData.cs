using Postgrest.Attributes;
using Postgrest.Models;

namespace CustomerApp.Models;

[Table("purchase")]
public class PurchaseData : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }
    [Column("customer_id")]
    public int CustomerId { get; set; }
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    [Column("status")]
    public string Status { get; set; }
    [Reference(typeof(CustomerData))]
    public CustomerData Customer { get; set; }
}