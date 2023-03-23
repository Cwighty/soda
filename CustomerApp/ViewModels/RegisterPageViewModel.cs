using CommunityToolkit.Mvvm.Input;
using Supabase;
using Supabase.Gotrue;
using Client = Supabase.Client;

namespace CustomerApp.ViewModels;

public partial class RegisterPageViewModel : BaseViewModel
{
    private readonly Client client;
    private readonly NavigationService navigationService;

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string password;

    [ObservableProperty]
    private string name;

    public RegisterPageViewModel(Client client, NavigationService navigationService)
    {
        this.client = client;
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
        var response = await client.Auth.SignUp(Email, Password);
        var model = new CustomerData
        {
            Id = response.User.Id,
            Name = Name,
            Email = Email
        };
        await client.From<CustomerData>().Insert(model);

        await navigationService.GoTo("../../");
        
    }
}
