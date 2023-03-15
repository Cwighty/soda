using Postgrest.Attributes;
using Postgrest.Models;

namespace CustomerApp.Models;

public class BaseTypeData : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
}
