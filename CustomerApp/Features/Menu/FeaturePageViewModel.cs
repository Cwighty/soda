using CommunityToolkit.Mvvm.Input;

namespace CustomerApp.Features.Menu;

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
        try
        {
            IsBusy = true;
            var categoryProducts = await productService.GetCategorizedProducts();
            CategorizedProducts = categoryProducts;
        }
        finally
        {
            IsBusy = false;
        }
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

    [RelayCommand]
    private async Task GoToCart()
    {
        await navigationService.GoTo("///CartPage");
    }


    [RelayCommand]
    private async Task GoToProfile()
    {
        await navigationService.GoTo("///ProfilePage");
    }
}
