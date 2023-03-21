

namespace CustomerApp.Services;

public class NavigationService
{
    public async Task GoTo(string path)
    {
        await Shell.Current.GoToAsync(path);
    }
    public async Task GoTo(string path, IDictionary<string, object> parameters)
    {
        await Shell.Current.GoToAsync(path, parameters);
    }
    public async Task ClearStack()
    {
        await Shell.Current.Navigation.PopToRootAsync(false);
    }
}
