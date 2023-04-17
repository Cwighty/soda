using Postgrest.Attributes;
using Postgrest.Models;

namespace SodaShared.Models.Data;

[Table("base")]
public class BaseData : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("description")]
    public string Description { get; set; }
    [Column("price")]
    public decimal Price { get; set; }
    [Column("type_id")]
    public int BaseTypeId { get; set; }
    [Reference(typeof(BaseTypeData), shouldFilterTopLevel:false)]
    public BaseTypeData BaseType { get; set; }
}

