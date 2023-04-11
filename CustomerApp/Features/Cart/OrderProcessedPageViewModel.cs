namespace CustomerApp.Features.Cart;

[QueryProperty("OrderId", nameof(OrderId))]
public partial class OrderProcessedPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private int orderId;
    private readonly NavigationService navigationService;

    public OrderProcessedPageViewModel(NavigationService navigationService)
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
    public async Task GoToMenu()
    {
        await navigationService.GoTo("///MenuPage");
    }
}
