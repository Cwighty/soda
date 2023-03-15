using Postgrest.Attributes;
using Postgrest.Models;

namespace CustomerApp.Models;

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

    [Reference(typeof(ProductData))]
    public List<ProductData> Products { get; set; }
}
