using Postgrest.Attributes;
using Postgrest.Models;

namespace SodaShared.Models.Data;

[Table("base_type_size")]
public class BaseTypeSizeData : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }
    [Column("base_type_id")]
    public int BaseTypeId { get; set; }
    [Column("size_id")]
    public int SizeId { get; set; }
    [Reference(typeof(BaseTypeData))]
    public BaseTypeData BaseType { get; set; }
    [Reference(typeof(SizeData))]
    public SizeData Size { get; set; }
}
