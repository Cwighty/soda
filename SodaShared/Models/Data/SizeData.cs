using Postgrest.Attributes;
using Postgrest.Models;

namespace SodaShared.Models.Data;

[Table("size")]
public class SizeData : BaseModel
{
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("price")]
    public Decimal Price { get; set; }
    [Column("img")]
    public string Img { get; set; }
}

