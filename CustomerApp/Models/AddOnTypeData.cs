using Postgrest.Attributes;
using Postgrest.Models;

namespace CustomerApp.Models;

[Table("addon_type")]
public class AddOnTypeData : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
}

public class AddOnType
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public static class AddOnTypeExtensions {
    public static AddOnType ToAddOn(this AddOnTypeData addOn)
    {
        return new AddOnType { Id = addOn.Id, Name = addOn.Name };
    }

}

