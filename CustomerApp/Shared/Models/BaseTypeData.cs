using Postgrest.Attributes;
using Postgrest.Models;

namespace CustomerApp.Shared.Models;

[Table ("base_type")]
public class BaseTypeData : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Reference(typeof(SizeData))]
    public List<SizeData> Sizes { get; set; }
}

