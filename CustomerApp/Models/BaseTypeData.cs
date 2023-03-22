using Postgrest.Attributes;
using Postgrest.Models;

namespace CustomerApp.Models;

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

public class BaseType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Size> Sizes { get; set; }
}

public static class BaseTypeExtensions
{
    public static BaseType ToBaseType(this BaseTypeData type)
    {
        return new BaseType { Id = type.Id, Name = type.Name, Sizes = type.Sizes.ToSizes() };
    }
}

