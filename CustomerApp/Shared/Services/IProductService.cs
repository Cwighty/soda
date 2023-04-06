namespace CustomerApp.Shared.Services;

public interface IProductService
{
    Task<List<AddOn>> GetAddOns();
    Task<List<BaseProduct>> GetBaseProducts();
    Task<List<Base>> GetBases();
    Task<List<BaseType>> GetBaseTypes();
    Task<List<Category>> GetCategorizedProducts();
    Task<List<Product>> GetProducts();
    Task<List<Product>> GetProductsByBase(int baseId);
}