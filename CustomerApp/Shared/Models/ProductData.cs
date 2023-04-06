using CommunityToolkit.Maui.Core.Extensions;
using Postgrest.Attributes;
using Postgrest.Models;
using System.Collections.ObjectModel;

namespace CustomerApp.Shared.Models;

[Table("product")]
public class ProductData : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("description")]
    public string Description { get; set; }
    [Column("special_price")]
    public decimal? SpecialPrice { get; set; }
    [Column("image_url")]
    public string? ImageUrl { get; set; }
    [Reference(typeof(BaseData))]
    public BaseData Base { get; set; }
    [Reference(typeof(AddOnData))]
    public ObservableCollection<AddOnData> AddOns { get; set; }
}

