namespace CustomerApp.ViewModels;

[QueryProperty(nameof(Products), nameof(Products))]
public partial class ProductListPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private List<Product> products;
    public ProductListPageViewModel()
    {

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
