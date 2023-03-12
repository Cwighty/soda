using Postgrest.Attributes;
using Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApp.Models;

[Table("product_addon")]
public class ProductAddOnData : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }
    [Column("product_id")]
    public int ProductId { get; set; }
    [Column("addon_id")]
    public int AddOnId { get; set; }
    [Column("quantity")]
    public int Quantity { get; set; }
    [Reference(typeof(ProductData))]
    public ProductData Product { get; set; }
    [Reference(typeof(AddOnData))]
    public AddOnData AddOn { get; set; }
}
