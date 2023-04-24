namespace CustomerApp.Shared.Services
{
    public interface INavigationService
    {
        Task ClearStack();
        Task GoTo(string path);
        Task GoTo(string path, IDictionary<string, object> parameters);
    }
}