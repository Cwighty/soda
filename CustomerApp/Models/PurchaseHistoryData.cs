using Postgrest.Attributes;
using Postgrest.Models;

namespace CustomerApp.Models;

[Table("purchase_history")]
public class PurchaseHistoryData : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }
    [Column("purchase_id")]
    public int PurchaseId { get; set; }
    [Column("status")]
    public string Status { get; set; }
    [Column("completed_at")]
    public DateTime CompletedAt { get; set; }
    [Reference(typeof(PurchaseData))]
    public PurchaseData Purchase { get; set; }
}
