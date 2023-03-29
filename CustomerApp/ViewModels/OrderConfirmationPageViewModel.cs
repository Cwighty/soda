using CommunityToolkit.Mvvm.Input;

namespace CustomerApp.ViewModels;

public partial class OrderConfirmationPageViewModel : BaseViewModel
{
    private readonly NavigationService navigationService;
    [ObservableProperty]
    private string cardNumber;
    
    [ObservableProperty]
    private string cardName;

    [ObservableProperty]
    private DateTime cardExpirationDate;

    [ObservableProperty]
    private decimal orderTotal;
    public OrderConfirmationPageViewModel(NavigationService navigationService)
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
    public async Task Confirm()
    {
        await navigationService.GoTo(nameof(OrderProcessedPage));
    }
}