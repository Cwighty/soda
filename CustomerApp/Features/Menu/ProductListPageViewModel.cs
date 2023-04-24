namespace CustomerApp.Features.Menu;

[QueryProperty(nameof(Products), nameof(Products))]
public partial class ProductListPageViewModel : BaseViewModel
{
    private readonly INavigationService navigationService;
    [ObservableProperty]
    private List<Product> products;
    public ProductListPageViewModel(INavigationService navigationService)
    {
        this.navigationService = navigationService;
    }
    public override Task Initialize()
    {
        return Task.CompletedTask;
    }

    public override Task Stop()
    {
        return Task.CompletedTask;
    }

    [RelayCommand]
    public async Task Details(Product product)
    {
        await navigationService.GoTo(nameof(ProductDetailPage),
            new Dictionary<string, object>()
            {
                ["Product"] = product
            });
    }
}
