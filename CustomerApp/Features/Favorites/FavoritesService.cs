using Supabase;

namespace CustomerApp.Features.Favorites;

public class FavoritesService
{
    private readonly Client client;
    private readonly IMapper mapper;
    private readonly UserService userService;

    public FavoritesService(Client client, IMapper mapper, UserService userService)
    {
        this.client = client;
        this.mapper = mapper;
        this.userService = userService;
    }
    
    public async Task<List<Product>> GetFavorites()
    {
        var res = await client.From<CustomerData>()
            .Get();
        return mapper.Map<List<Product>>(res.Models.FirstOrDefault()?.Favorites);
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
        var favorites = await GetFavorites();
        if (favorites.Find(f => f.Id == productId) != default(Product)){
            await RemoveFavoriteDrink(productId);
        }
        else
        {
            await FavoriteDrink(productId);
        }
    }
}
