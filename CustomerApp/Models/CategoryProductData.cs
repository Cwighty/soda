using Postgrest.Attributes;
using Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApp.Models;

[Table("category_product")]
public class CategoryProductData : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }
    [Column("category_id")]
    public int CategoryId { get; set; }
    [Column("product_id")]
    public int ProductId { get; set; }
    [Reference(typeof(CategoryData))]
    public CategoryData Category { get; set; }
    [Reference(typeof(ProductData))]
    public ProductData Product { get; set; }
}
