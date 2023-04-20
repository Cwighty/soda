namespace CustomerApp.Features.Profile;

public partial class OrderHistoryPageViewModel : BaseViewModel
{
    private readonly PurchaseRepository purchaseRepo;

    [ObservableProperty]
    private List<Purchase> purchases;

    public OrderHistoryPageViewModel(PurchaseRepository purchaseRepo)
    {
        this.purchaseRepo = purchaseRepo;
    }
    public async override Task Initialize()
    {
        Purchases = await purchaseRepo.GetAllPurchasesForUser();
    }

    public override Task Stop()
    {
        return Task.CompletedTask;
    }
}
