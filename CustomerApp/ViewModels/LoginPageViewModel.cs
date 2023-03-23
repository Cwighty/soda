using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApp.ViewModels
{
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
            await navigationService.GoTo("../");
        }

        [RelayCommand]
        private async Task GoToRegister()
        {
            await navigationService.GoTo(nameof(RegisterPage));
        }
    }
}
