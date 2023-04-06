using Postgrest.Attributes;
using Postgrest.Models;

namespace CustomerApp.Shared.Models;

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
    [Column("price_paid")]
    public decimal PricePaid { get; set; }
    [Column("status")]
    public string Status { get; set; }
    [Reference(typeof(CustomerData))]
    public CustomerData Customer { get; set; }
}

public class Purchase
{
    public int Id { get; set; }
    public string CustomerId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public decimal PricePaid { get; set; }
    public string Status { get; set; }
    public Customer Customer { get; set; }
}