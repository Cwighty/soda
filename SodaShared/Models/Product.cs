using System.Collections.ObjectModel;

namespace SodaShared.Models;

public class Product
{
    public Product()
    {

    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal? SpecialPrice { get; set; }
    public string? ImageUrl { get; set; }

    public Size Size { get; set; } = new Size() { Name = "Small" };
    public Base Base { get; set; }
    public ObservableCollection<AddOn> AddOns { get; set; }

}