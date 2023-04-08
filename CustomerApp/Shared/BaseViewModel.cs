using CommunityToolkit.Mvvm.ComponentModel;

namespace CustomerApp.Shared;
public abstract partial class BaseViewModel : ObservableObject, IViewModel
{
    public BaseViewModel() { }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy = false;
    
    public bool IsNotBusy => !IsBusy;

    public abstract Task Initialize();
    public abstract Task Stop();
}
public interface IViewModel
{
    Task Initialize();
    Task Stop();
}
