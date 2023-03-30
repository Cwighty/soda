using CommunityToolkit.Mvvm.Input;
using CustomerApp.Features.Login;

namespace CustomerApp.Features.Profile;

public partial class ProfilePageViewModel : BaseViewModel
{
    private readonly NavigationService navigationService;
    private readonly UserService userService;
    [ObservableProperty]
    private Customer customer;

    [ObservableProperty]
    private bool isLoggedIn;

    [ObservableProperty]
    private bool isNotLoggedIn;

    public ProfilePageViewModel(NavigationService navigationService, UserService userService)
    {
        this.navigationService = navigationService;
        this.userService = userService;
    }
    public async override Task Initialize()
    {
        if (userService.IsLoggedIn())
        {
            IsLoggedIn = true;
            IsNotLoggedIn = false;
            Customer = await userService.GetCustomer();
        }
        else
        {
            IsLoggedIn = false;
            IsNotLoggedIn = true;
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
}
