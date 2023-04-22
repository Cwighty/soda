namespace CustomerApp.Features.Profile;

public partial class OrderHistoryPageViewModel : BaseViewModel
{
    private readonly PurchaseRepository purchaseRepo;
    private readonly OrderService orderService;
    [ObservableProperty]
    private List<Purchase> purchases;

    public OrderHistoryPageViewModel(PurchaseRepository purchaseRepo, OrderService orderService)
    {
        this.purchaseRepo = purchaseRepo;
        this.orderService = orderService;
    }
    public async override Task Initialize()
    {
        var purchases = await purchaseRepo.GetAllPurchasesForUser();
        Purchases = purchases.OrderByDescending(p => p.CreatedAt).ToList();
    }

    public override Task Stop()
    {
        return Task.CompletedTask;
    }

    [RelayCommand]
    private async Task CancelOrder(Purchase purchase)
    {
        bool result = await Shell.Current.DisplayAlert("Cancel Order", "Are you sure?", "Yes", "No");
        if (result)
        {
            await orderService.CancelOrder(purchase.Id);
            await Initialize();
        }
    }
}
