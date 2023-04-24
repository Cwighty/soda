namespace CustomerApp.Features.Menu;

public partial class FeaturePageViewModel : BaseViewModel
{
    [ObservableProperty]
    private List<Category> _categorizedProducts;

    private readonly IProductService productService;
    private readonly INavigationService navigationService;

    public FeaturePageViewModel(IProductService productService, INavigationService navigationService)
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
    public async Task NavigateToDetail(Product product)
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
    public async Task GoToFavorites()
    {
        await navigationService.GoTo(nameof(FavoritesPage));
    }


    [RelayCommand]
    public async Task GoToProfile()
    {
        await navigationService.GoTo("/LoginPage");
    }
}
