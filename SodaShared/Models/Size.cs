using CommunityToolkit.Mvvm.ComponentModel;

namespace SodaShared.Models;

[ObservableObject]
public partial class Size
{
    public int Id { get; set; }

    public string Name { get; set; }
    public decimal Price { get; set; }

    public string? Img { get; set; }
    public string? Path => IsSelected ? SelectedPath : UnSelectedPath;

    public string? SelectedPath => $"white_{Img}";
    public string? UnSelectedPath => $"black_{Img}";

    [NotifyPropertyChangedFor(nameof(Path))]
    [ObservableProperty]
    private bool isSelected;
}

public static class SizeExtensions
{
    public static Size Clone(this Size size)
    {
        return new Size()
        {
            Id = size.Id,
            Name = size.Name,
            Price = size.Price,
            Img = size.Img,
            IsSelected = size.IsSelected
        };
    }
}
