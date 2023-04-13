using Supabase;

namespace SodaShared.Services;

public class ProductCRUDService
{
    private readonly Client client;
    private readonly IMapper mapper;

    public ProductCRUDService(Client client, IMapper mapper)
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

    public async Task<List<BaseType>> GetBaseTypes() {
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
            .Where(b => b.Id == baseId)
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

    public async Task<List<Size>> GetSizes()
    {
        var response = await client.From<SizeData>().Get();
        return mapper.Map<List<Size>>(response.Models);
    }
    
    public async Task<AddOn> CreateAddOn(AddOn addOn)
    {
        var options = new Postgrest.QueryOptions
        {
            Returning = Postgrest.QueryOptions.ReturnType.Representation
        };
        var addOnData = mapper.Map<AddOnData>(addOn);
        var response = await client.From<AddOnData>().Insert(addOnData, options);
        return mapper.Map<AddOn>(response.Models.FirstOrDefault());
    }

    public async Task<Product> CreateProduct(Product product)
    {   
        var options = new Postgrest.QueryOptions
        {
            Returning = Postgrest.QueryOptions.ReturnType.Representation
        };
        var productData = mapper.Map<ProductData>(product);
        var response = await client.From<ProductData>().Insert(productData, options);

        var addons = product.AddOns;
        foreach (var a in addons)
        {
            var addOnProduct = new ProductAddOnData
            {
                AddOnId = a.Id,
                ProductId = response.Models.FirstOrDefault().Id
            };
            await client.From<ProductAddOnData>().Insert(addOnProduct);
        }

        return mapper.Map<Product>(response.Models.FirstOrDefault());
    }

    public async Task UpdateProduct(Product product)
    {
        var productData = mapper.Map<ProductData>(product);
        var response = await client.From<ProductData>()
            .Update(productData);
    }

    public async Task DeleteProduct(Product product)
    {
        var data = mapper.Map<ProductData>(product);
        await client.From<ProductData>().Delete(data);
    }

}
