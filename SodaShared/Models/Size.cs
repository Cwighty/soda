using CommunityToolkit.Mvvm.ComponentModel;

namespace SodaShared.Models;

[ObservableObject]
public partial class Size
{
    public Size()
    {

    }
    public string Name { get; set; }
    public Decimal Price { get; set; }

    public string? Img { get; set; }
    public string? Path => IsSelected ? SelectedPath : UnSelectedPath;

    public string? SelectedPath => $"white_{Img}";
    public string? UnSelectedPath => $"black_{Img}";

    [NotifyPropertyChangedFor(nameof(Path))]
    [ObservableProperty]
    private bool isSelected;
}
