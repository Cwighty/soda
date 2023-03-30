
using Postgrest;
using Supabase;
namespace CustomerApp.Shared.Services;

public class ProductService : IProductService
{
    private readonly Supabase.Client client;
    private readonly IMapper mapper;

    public ProductService(Supabase.Client client, IMapper mapper)
    {
        this.client = client;
        this.mapper = mapper;
    }

    public async Task<List<Product>> GetProducts()
    {
        var response = await client.From<ProductData>().Get();
        return mapper.Map<List<Product>>(response.Models);
    }

    public async Task<List<Category>> GetCategorizedProducts()
    {
        var response = await client.From<CategoryData>().Get();
        return mapper.Map<List<Category>>(response.Models);
    }

    public async Task<List<BaseType>> GetBaseTypes()
    {
        var response = await client.From<BaseTypeData>().Get();
        return mapper.Map<List<BaseType>>(response.Models);
    }

    public async Task<List<Base>> GetBases()
    {
        var response = await client.From<BaseData>().Get();
        return mapper.Map<List<Base>>(response.Models);
    }

    public async Task<List<Product>> GetProductsByBase(int baseId)
    {
        var response = await client.From<ProductData>()
            .Filter("base_id", Constants.Operator.Equals, baseId)
            .Get();
        return mapper.Map<List<Product>>(response.Models);
    }

    public async Task<List<BaseProduct>> GetBaseProducts()
    {
        var baseProducts = new List<BaseProduct>();
        foreach (var b in await GetBases())
        {
            baseProducts.Add(new(b.Name, await GetProductsByBase(b.Id)));
        }
        return baseProducts;
    }

    public async Task<List<AddOn>> GetAddOns()
    {
        var response = await client.From<AddOnData>().Get();
        return mapper.Map<List<AddOn>>(response.Models);
    }
}

public class CacheProductService
{
    private readonly IProductService productService;
    private readonly ICacheService cacheService;

    public CacheProductService(IProductService productService, ICacheService cacheService)
    {
        this.productService = productService;
        this.cacheService = cacheService;
    }

    public async Task<List<Product>> GetProducts()
    {
        var products = cacheService.Get<List<Product>>(nameof(GetProducts));
        if (products == null || products.Count == 0)
        {
            products = await productService.GetProducts();
            cacheService.Add(nameof(GetProducts), products);
        }
        return products;
    }
}
