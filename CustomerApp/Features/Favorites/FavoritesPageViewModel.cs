namespace CustomerApp.Features.Favorites;

public class FavoritesPageViewModel : BaseViewModel
{
    private readonly UserService userService;
    private readonly NavigationService navigationService;

    public FavoritesPageViewModel(UserService userService, NavigationService navigationService) 
    {
        this.userService = userService;
        this.navigationService = navigationService;
    }

    public override async Task Initialize()
    {
        if (!userService.IsLoggedIn()) 
        {
            await navigationService.GoTo("///ProfilePage");
        }
    }

    public override Task Stop()
    {
        return Task.CompletedTask;
    }
}
