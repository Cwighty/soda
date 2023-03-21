using CommunityToolkit.Maui.Core.Extensions;
using Postgrest.Attributes;
using Postgrest.Models;
using System.Collections.ObjectModel;

namespace CustomerApp.Models;

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

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal? SpecialPrice { get; set; }
    public string? ImageUrl { get; set; }
    public Base Base { get; set; }
    public ObservableCollection<AddOn> AddOns { get; set; }

    public string Size => AddOns.Where(a => a.AddOnType.Name == "Size").FirstOrDefault()?.Name ?? "Small";
    public Decimal CalculatedPrice => AddOns.Sum(a => a.Price) + Base.Price;
}

public static class ProductDataExtensions
{
    public static List<Product> ToProducts (this List<ProductData> datas)
    {
        return datas.Select(d => d.ToProduct()).ToList();
    }
    public static Product ToProduct(this ProductData data)
    {
        return new Product
        {
            Id = data.Id,
            Name = data.Name,
            Description = data.Description,
            SpecialPrice = data.SpecialPrice,
            ImageUrl = data.ImageUrl,
            Base = data.Base.ToBase(),
            AddOns = data.AddOns.ToList().ToAddOns().ToObservableCollection()
        };
    }
}

