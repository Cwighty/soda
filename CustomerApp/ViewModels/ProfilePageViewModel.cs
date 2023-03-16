namespace CustomerApp.ViewModels;

[INotifyPropertyChanged]
public partial class ProfilePageViewModel : BaseViewModel
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
