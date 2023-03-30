using CommunityToolkit.Mvvm.Input;

namespace CustomerApp.Features.Login;

public partial class RegisterPageViewModel : BaseViewModel
{
    private readonly UserService userService;
    private readonly NavigationService navigationService;

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string password;

    [ObservableProperty]
    private string name;

    public RegisterPageViewModel(UserService userService, NavigationService navigationService)
    {
        this.userService = userService;
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
    private async Task CreateAccount()
    {
        await userService.Register(Email, Password, Name);
        await navigationService.GoTo("../../");
    }
}
