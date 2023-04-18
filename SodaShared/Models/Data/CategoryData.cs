using Postgrest.Attributes;
using Postgrest.Models;

namespace SodaShared.Models.Data;

[Table("category")]
public class CategoryData : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("description")]
    public string Description { get; set; }
    [Column("image_url")]
    public string ImageUrl { get; set; }

    [Reference(typeof(ProductData), shouldFilterTopLevel:false)]
    public List<ProductData> Products { get; set; }
}

