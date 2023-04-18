namespace CustomerApp.Features.Favorites;

public partial class FavoritesPageViewModel : BaseViewModel
{
    private readonly UserService userService;
    private readonly NavigationService navigationService;
    private readonly FavoritesService favoritesService;

    [ObservableProperty]
    private List<Product> favorites;
    
    public FavoritesPageViewModel(UserService userService, NavigationService navigationService, FavoritesService favoritesService) 
    {
        this.userService = userService;
        this.navigationService = navigationService;
        this.favoritesService = favoritesService;
    }

    public override async Task Initialize()
    {
        if (!userService.IsLoggedIn()) 
        {
            await navigationService.GoTo("///ProfilePage");
            return;
        }
        IsBusy = true;
        try
        {
            Favorites = await favoritesService.GetFavorites();
        }
        finally
        {
            IsBusy = false;
        }
    }

    public override Task Stop()
    {
        return Task.CompletedTask;
    }

    [RelayCommand]
    public async Task ToggleFavorite(Product product)
    {
        await favoritesService.ToggleFavorite(product.Id);
    }
}
