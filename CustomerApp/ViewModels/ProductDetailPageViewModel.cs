namespace CustomerApp.ViewModels;

[INotifyPropertyChanged]
[QueryProperty(nameof(Product), nameof(Product))]
public partial class ProductDetailPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private ProductData product;
    public override Task Initialize()
    {
        return Task.CompletedTask;
    }

    public override Task Stop()
    {
        return Task.CompletedTask;
    }
}
