using Postgrest.Attributes;
using Postgrest.Models;

namespace CustomerApp.Shared.Models;

[Table("addon")]
public class AddOnData : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("price")]
    public decimal Price { get; set; }
    [Reference(typeof(AddOnTypeData))]
    public AddOnTypeData AddOnType { get; set; }
}

