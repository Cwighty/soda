namespace CustomerApp.Features.Login;

public partial class LoginPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private string email;
    [ObservableProperty]
    private string password;
    private readonly INavigationService navigationService;
    private readonly IUserService userService;

    public LoginPageViewModel(INavigationService navigationService, IUserService userService)
    {
        this.navigationService = navigationService;
        this.userService = userService;
    }
    public override Task Initialize()
    {
        Email = null;
        Password = null;
        return Task.CompletedTask;
    }

    public override Task Stop()
    {
        return Task.CompletedTask;
    }

    [RelayCommand]
    public async Task Login()
    {
        await userService.Login(Email, Password);
        await navigationService.GoTo("..");
    }

    [RelayCommand]
    public async Task GoToRegister()
    {
        await navigationService.GoTo(nameof(RegisterPage));
    }
}
