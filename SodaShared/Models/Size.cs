using CommunityToolkit.Mvvm.ComponentModel;

namespace SodaShared.Models;

[ObservableObject]
public partial class Size
{
    public int Id { get; set; }

    public string Name { get; set; }
    public decimal Price { get; set; }
}

public static class SizeExtensions
{
    public static Size Clone(this Size size)
    {
        return new Size()
        {
            Id = size.Id,
            Name = size.Name,
            Price = size.Price
        };
    }
}
