using Supabase;

namespace CustomerApp.Features.Favorites;

public class FavoritesService
{
    private readonly Client client;
    private readonly IMapper mapper;
    private readonly UserService userService;
    private readonly NavigationService navigationService;

    public FavoritesService(Client client, IMapper mapper, UserService userService, NavigationService navigationService)
    {
        this.client = client;
        this.mapper = mapper;
        this.userService = userService;
        this.navigationService = navigationService;
    }
    
    public async Task<List<Product>> GetFavorites()
    {
        var res = await client.From<CustomerData>()
            .Get();
        return mapper.Map<List<Product>>(res.Models.FirstOrDefault()?.Favorites);
    }

    public async Task<bool> IsFavorite(int productId)
    {
        var favs = await GetFavorites();
        return favs.Select(p => p.Id == productId).Any();
    }

    private async Task FavoriteDrink(int productId)
    {
        var customer = await userService.GetCustomer();

        var fav = new CustomerFavoriteData
        {
            ProductId = productId,
            CustomerId = customer.Id,
        };

        await client.From<CustomerFavoriteData>().Insert(fav);

        
    }

    private async Task RemoveFavoriteDrink(int productId)
    {
        var customer = await userService.GetCustomer();

        await client.From<CustomerFavoriteData>()
            .Where(f => f.ProductId == productId)
            .Delete();
    }

    public async Task ToggleFavorite(int productId)
    {
        if (!userService.IsLoggedIn())
        {
            bool result = await AppShell.Current.DisplayAlert("Log In", "You must log in to add a favorite!", "Go To Login", "Cancel");
            if (result)
            {
                await navigationService.ClearStack();
                await navigationService.GoTo("///ProfilePage");
            }
            return;
        }
        var favorites = await GetFavorites();
        if (favorites.Find(f => f.Id == productId) != default(Product)){
            await RemoveFavoriteDrink(productId);
            await AppShell.Current.DisplayAlert("Removed!", "The drink was removed from your favorites!", "OK");
        }
        else
        {
            await FavoriteDrink(productId);
            await AppShell.Current.DisplayAlert("Added!", "The drink was added to your favorites!", "OK");
        }
    }
}
