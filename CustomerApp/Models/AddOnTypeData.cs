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
    public AddOnType()
    {

    }
    public int Id { get; set; }
    public string Name { get; set; }
}