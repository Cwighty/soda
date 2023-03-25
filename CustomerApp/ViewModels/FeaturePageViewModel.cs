using CommunityToolkit.Mvvm.Input;

namespace CustomerApp.ViewModels;

public partial class FeaturePageViewModel : BaseViewModel
{
    [ObservableProperty] 
    private List<Category> _categorizedProducts;

    private readonly IProductService productService;
    private readonly NavigationService navigationService;

    public FeaturePageViewModel(IProductService productService, NavigationService navigationService)
    {
        this.productService = productService;
        this.navigationService = navigationService;
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

    [RelayCommand]
    private async Task NavigateToDetail(Product product)
    {
        await navigationService
            .GoTo(
                nameof(ProductDetailPage),
                new Dictionary<string, object>()
                {
                    ["Product"] = product
                });
    }
}
