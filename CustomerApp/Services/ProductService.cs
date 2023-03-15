using Supabase;
namespace CustomerApp.Services;

public class ProductService
{
    private readonly Client client;

    public ProductService(Client client)
	{
        this.client = client;
    }

    public async Task<List<ProductData>> GetProducts()
    {
        var response = await client.From<ProductData>().Get();
        return response.Models;
    }

    public async Task<List<CategoryData>> GetCategorizedProducts()
    {
        var response = await client.From<CategoryData>().Get();
        return response.Models;
    }

    public async Task<List<BaseTypeData>> GetBaseTypes()
    {
        var response = await client.From<BaseTypeData>().Get();
        return response.Models;
    }
}
