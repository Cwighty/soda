using Postgrest.Attributes;
using Postgrest.Models;

namespace CustomerApp.Shared.Models;

[Table("size")]
public class SizeData : BaseModel
{
    [Column("name")]
    public string Name { get; set; }
    [Column("price")]
    public Decimal Price { get; set; }
    [Column("img")]
    public string Img { get; set; }
}

