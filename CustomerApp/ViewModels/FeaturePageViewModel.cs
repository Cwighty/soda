namespace CustomerApp.ViewModels;

[INotifyPropertyChanged]
public partial class FeaturePageViewModel : BaseViewModel
{
    [ObservableProperty] 
    private List<CategoryData> _categorizedProducts;

    private readonly ProductService productService;

    public FeaturePageViewModel(ProductService productService)
    {
        this.productService = productService;
    }
    public override async Task Initialize()
    {
        var categoryProducts = await productService.GetCategorizedProducts();
        CategorizedProducts = categoryProducts;
    }

    public override Task Stop()
    {
        return Task.CompletedTask;
    }
}
