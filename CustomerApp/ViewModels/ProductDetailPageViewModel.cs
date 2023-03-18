namespace CustomerApp.ViewModels;

[INotifyPropertyChanged]
[QueryProperty(nameof(Product), nameof(Product))]
public partial class ProductDetailPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private ProductData product;

    [ObservableProperty]
    private List<ProductSize> productSizes;

    [ObservableProperty]
    private ProductSize selectedProductSize;

    public ProductDetailPageViewModel()
    {
        ProductSizes = new() { 
            new ProductSize("Small", "black_drink.png"),
            new ProductSize("Medium", "black_drink.png"),
            new ProductSize("Large", "black_drink.png")
        };
    }
    public override Task Initialize()
    {
        return Task.CompletedTask;
    }

    public override Task Stop()
    {
        return Task.CompletedTask;
    }
}
