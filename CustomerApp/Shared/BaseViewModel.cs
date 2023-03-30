namespace CustomerApp.Shared;
public abstract class BaseViewModel : ObservableObject, IViewModel
{
    public BaseViewModel() { }
    public abstract Task Initialize();
    public abstract Task Stop();
}
public interface IViewModel
{
    Task Initialize();
    Task Stop();
}
