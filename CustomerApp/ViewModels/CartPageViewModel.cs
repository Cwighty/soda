using CommunityToolkit.Mvvm.ComponentModel;

namespace CustomerApp.ViewModels
{
    public partial class CartPageViewModel : BaseViewModel
    {
        public override Task Initialize()
        {
            return Task.CompletedTask;
        }

        public override Task Stop()
        {
            return Task.CompletedTask;
        }
    }
}
