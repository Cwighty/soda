using Postgrest;
using Supabase;
namespace CustomerApp.Services;

public class ProductService
{
    private readonly Supabase.Client client;

    public ProductService(Supabase.Client client)
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

    public async Task<List<BaseData>> GetBases()
    {
        var response = await client.From<BaseData>().Get();
        return response.Models;
    }

    public async Task<List<ProductData>> GetProductsByBase(int baseId)
    {
        var response = await client.From<ProductData>()
            .Filter("base_id", Constants.Operator.Equals, baseId)
            .Get();
        return response.Models;
    }

    public async Task<List<BaseProduct>> GetBaseProducts()
    {
        var baseProducts = new List<BaseProduct>();
        foreach (var b in await GetBases()) { 
            baseProducts.Add(new(b.Name, await GetProductsByBase(b.Id)));
        }
        return baseProducts;
    }

    
}
