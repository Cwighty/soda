namespace CustomerApp.Features.Cart;

[QueryProperty("OrderId", nameof(OrderId))]
public partial class OrderProcessedPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private int orderId;
    [ObservableProperty]
    private Purchase purchase;

    [ObservableProperty]
    private string pickUpTimeRange;

    private readonly NavigationService navigationService;
    private readonly PurchaseService purchaseService;

    public OrderProcessedPageViewModel(NavigationService navigationService, PurchaseService purchaseService)
    {
        this.navigationService = navigationService;
        this.purchaseService = purchaseService;
    }
    public override async Task Initialize()
    {
        if(OrderId != 0)
        {
            Purchase = await purchaseService.GetPurchaseById(OrderId);
            PickUpTimeRange = $"{Purchase.PickUpTime?.ToString("h:mm tt")} - {(Purchase.PickUpTime + TimeSpan.FromMinutes(15))?.ToString("h:mm tt")}";
        }
    }

    public override Task Stop()
    {
        return Task.CompletedTask;
    }

    [RelayCommand]
    public async Task GoToMenu()
    {
        await navigationService.ClearStack();
        await navigationService.GoTo("///MenuPage");
    }
}
