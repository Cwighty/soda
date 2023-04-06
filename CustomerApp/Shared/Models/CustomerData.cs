using Postgrest.Attributes;
using Postgrest.Models;

namespace CustomerApp.Shared.Models;

[Table("customer")]
public class CustomerData : BaseModel
{
    [PrimaryKey("id", true)]
    public string Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("email")]
    public string Email { get; set; }
    [Column("phone")]
    public string Phone { get; set; }
}

