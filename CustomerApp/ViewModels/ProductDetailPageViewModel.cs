namespace CustomerApp.ViewModels;

[QueryProperty(nameof(Product), nameof(Product))]
public partial class ProductDetailPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private ProductData product;

    [ObservableProperty]
    private List<ProductSize> productSizes;


    private ProductSize selectedProductSize;
    public ProductSize SelectedProductSize
    {
        get { return selectedProductSize; }
        set
        {
            SetProperty(ref selectedProductSize, value);
            foreach (var product in ProductSizes)
            {
                product.IsSelected = product == value;
            }
        }
    }


    public ProductDetailPageViewModel()
    {
        ProductSizes = new() {
            new ProductSize("Small", "drink.png"),
            new ProductSize("Medium", "drink.png"),
            new ProductSize("Large", "drink.png")
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
