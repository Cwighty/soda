namespace CustomerApp.Features.Profile;

public partial class ProfilePageViewModel : BaseViewModel
{
    private readonly NavigationService navigationService;
    private readonly UserService userService;
    [ObservableProperty]
    private Customer customer;


    public ProfilePageViewModel(NavigationService navigationService, UserService userService)
    {
        this.navigationService = navigationService;
        this.userService = userService;
    }
    public async override Task Initialize()
    {
        if (userService.IsLoggedIn())
        {
            Customer = await userService.GetCustomer();
        }
        else
        {
            await Login();
        }

    }

    public override Task Stop()
    {
        return Task.CompletedTask;
    }

    [RelayCommand]
    private async Task Logout()
    {
        await userService.Logout();
        await navigationService.GoTo("///FeaturePage");
    }

    [RelayCommand]
    private async Task Login()
    {
        await navigationService.GoTo(nameof(LoginPage));
    }

    [RelayCommand]
    private async Task EditDetails()
    {
        await navigationService.GoTo(nameof(ProfileDetailsPage));
    }

    [RelayCommand]
    private async Task DeleteAccount()
    {
        var choice = await App.Current.MainPage.DisplayAlert("Delete Account", "Are you sure?", "Yes", "No");
        if (choice == true)
        {
            await userService.DeleteAccount();
            await navigationService.GoTo($"///{nameof(FeaturePage)}");
        }
    }

    [RelayCommand]
    private async Task OrderHistory()
    {
        await navigationService.GoTo(nameof(OrderHistoryPage));
    }
}
