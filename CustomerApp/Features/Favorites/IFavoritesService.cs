namespace CustomerApp.Features.Favorites
{
    public interface IFavoritesService
    {
        Task<List<Product>> GetFavorites();
        Task<bool> IsFavorite(int productId);
        Task ToggleFavorite(int productId);
    }
}