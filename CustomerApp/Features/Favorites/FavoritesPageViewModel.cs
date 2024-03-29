﻿namespace CustomerApp.Features.Favorites;

public partial class FavoritesPageViewModel : BaseViewModel
{
    private readonly IUserService userService;
    private readonly INavigationService navigationService;
    private readonly IFavoritesService favoritesService;

    [ObservableProperty]
    private List<Product> favorites;
    
    public FavoritesPageViewModel(IUserService userService, INavigationService navigationService, IFavoritesService favoritesService) 
    {
        this.userService = userService;
        this.navigationService = navigationService;
        this.favoritesService = favoritesService;
    }

    public override async Task Initialize()
    {
        if (!userService.IsLoggedIn()) 
        {
            await navigationService.ClearStack();
            await navigationService.GoTo(nameof(LoginPage));
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
        await Initialize();
    }

    [RelayCommand]
    public async Task GoToDetails(Product product)
    {
        var route = $"///MenuPage/{nameof(ProductDetailPage)}";
        await navigationService.GoTo(route,
            new Dictionary<string, object>()
            {
                ["Product"] = product
            });
    }
}
