using Postgrest.Attributes;
using Postgrest.Models;

namespace SodaShared.Models.Data;

[Table("addon")]
public class AddOnData : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("price")]
    public decimal Price { get; set; }
    [Column("addon_type_id")]
    public int AddOnTypeId { get; set; }
    [Reference(typeof(AddOnTypeData))]
    public AddOnTypeData AddOnType { get; set; }
}