using Postgrest.Attributes;
using Postgrest.Models;

namespace SodaShared.Models.Data;

[Table("addon_type")]
public class AddOnTypeData : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
}
