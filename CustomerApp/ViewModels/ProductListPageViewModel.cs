using CommunityToolkit.Mvvm.Input;

namespace CustomerApp.ViewModels;

[QueryProperty(nameof(Products), nameof(Products))]
public partial class ProductListPageViewModel : BaseViewModel
{
    private readonly NavigationService navigationService;
    [ObservableProperty]
    private List<Product> products;
    public ProductListPageViewModel(NavigationService navigationService)
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
    private async Task Details(Product product)
    {
        await navigationService.GoTo(nameof(ProductDetailPage),
            new Dictionary<string, object>()
            {
                ["Product"] = product
            });
    }
}
