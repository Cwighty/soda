using CommunityToolkit.Mvvm.Input;

namespace CustomerApp.Features.Login;

public partial class LoginPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private string email;
    [ObservableProperty]
    private string password;
    private readonly NavigationService navigationService;
    private readonly UserService userService;

    public LoginPageViewModel(NavigationService navigationService, UserService userService)
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
    private async Task Login()
    {
        await userService.Login(Email, Password);
        await navigationService.GoTo("///ProfilePage");
    }

    [RelayCommand]
    private async Task GoToRegister()
    {
        await navigationService.GoTo(nameof(RegisterPage));
    }
}
