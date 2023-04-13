using System.Collections.ObjectModel;

namespace SodaShared.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal? SpecialPrice { get; set; }
    public string? ImageUrl { get; set; }

    public Size Size { get; set; } = new Size() { Name = "Small" };
    public Base Base { get; set; } = new();
    public ObservableCollection<AddOn> AddOns { get; set; } = new();

}

public static class ProductExtensions
{
    public static Product Clone(this Product product)
    {
        return new Product()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            SpecialPrice = product.SpecialPrice,
            ImageUrl = product.ImageUrl,
            Size = product.Size,
            Base = product.Base,
            AddOns = new ObservableCollection<AddOn>(product.AddOns)
        };
    }
}

