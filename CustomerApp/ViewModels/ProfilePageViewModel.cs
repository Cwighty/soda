namespace CustomerApp.ViewModels;

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
        if (!userService.IsLoggedIn())
        {
            await navigationService.GoTo(nameof(LoginPage));
        }
        else
        {
            Customer = await userService.GetCustomer();
        }

    }

    public override Task Stop()
    {
        return Task.CompletedTask;
    }
}
